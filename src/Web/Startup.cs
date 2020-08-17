using Ardalis.ListStartupServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Web.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EgitimAPI;
using Microsoft.EgitimAPI.ApplicationCore.Entities.EmailSettings;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services;
using Microsoft.EgitimAPI.ApplicationCore.Services.BlobService;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category;
using Microsoft.EgitimAPI.ApplicationCore.Services.CategoryService;
using Microsoft.EgitimAPI.ApplicationCore.Services.CommentService;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService;
using Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService;
using Microsoft.EgitimAPI.ApplicationCore.Services.EnrollmentService;
using Microsoft.EgitimAPI.ApplicationCore.Services.EventService;
using Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService;
using Microsoft.EgitimAPI.ApplicationCore.Services.LocationService;
using Microsoft.EgitimAPI.ApplicationCore.Services.NotificationService;
using Microsoft.EgitimAPI.ApplicationCore.Services.PostService;
using Microsoft.EgitimAPI.ApplicationCore.Services.QuestionService;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService;
using Microsoft.EgitimAPI.Infrastructure.Data;
using Microsoft.EgitimAPI.Infrastructure.Logging;
using Microsoft.EgitimAPI.Infrastructure.Services;
using Microsoft.EgitimAPI.Lib;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.eShopWeb.Web
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
         
            ConfigureProductionServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // use real database
            // Requires LocalDB which can be installed with SQL Server Express 2016
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284
            services.AddDbContext<EgitimContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("EgitimBackend")),ServiceLifetime.Transient);

//            // Add Identity DbContext
//            services.AddDbContext<AppIdentityDbContext>(options =>
//                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            ConfigureServices(services);
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCookieSettings(services);

            IdentityModelEventSource.ShowPII = true;

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowCredentials();
            }));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITenantAppService, TenantAppService>();
            services.AddScoped<ICourseAppService, CourseAppService>();
            services.AddScoped<IGivenCourseAppService, GivenCourseAppService>();
            services.AddScoped<IEducatorAppService, EducatorAppService>();
            services.AddScoped<IEnrollmentAppService, EnrollmentAppService>();
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<IEventAppService, EventAppService>();
            services.AddScoped<ICommentAppService, CommentAppService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IQuestionAppService, QuestionAppService>();
            services.AddScoped<INotificationAppService, NotificationAppService>();
            services.AddScoped<ICheckEdition, CheckEdition>();
            services.AddScoped<IPostAppService, PostAppService>();
            services.AddScoped<IBlobService, BlobService>();
            services.Configure<CatalogSettings>(Configuration);
            services.AddSingleton<IUriComposer>(new UriComposer(Configuration.Get<CatalogSettings>()));

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IEmailSender, EmailSender>();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            
            services.AddAutoMapper(typeof(Startup));
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Issuer"],
                        ValidAudience = Configuration["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"])),
                    };
                });
            
            // Add memory cache services
            services.AddMemoryCache();

            services.AddRouting(options =>
            {
                // Replace the type and the name used to refer to it with your own
                // IOutboundParameterTransformer implementation
                options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });
            
            services.Configure<MyConfig>(Configuration.GetSection("MyConfig"));
            services.AddMvc(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(
                         new SlugifyParameterTransformer()));
                
            }
            )
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizePage("/Basket/Checkout");
                    options.AllowAreas = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "EgitimAPI", Version = "v1" });
            });

            services.AddHealthChecks()
                .AddCheck<HomePageHealthCheck>("home_page_health_check")
                .AddCheck<ApiHealthCheck>("api_health_check");

            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(services);

                config.Path = "/allservices";
            });

            _services = services; // used to debug registered services
        }

        private static void ConfigureCookieSettings(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.Cookie = new CookieBuilder
                {
                    IsEssential = true // required for auth to work without explicit user consent; adjust to suit your privacy policy
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

            //app.UseDeveloperExceptionPage();
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(
                            new
                            {
                                status = report.Status.ToString(),
                                errors = report.Entries.Select(e => new
                                {
                                    key = e.Key,
                                    value = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                                })
                            });
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseShowAllServicesMiddleware();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Egitim API V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller:slugify=Home}/{action:slugify=Index}/{id?}");
            });
        }
    }
}
