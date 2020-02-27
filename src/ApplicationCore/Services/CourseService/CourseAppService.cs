using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.BlobService;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.CourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService;
using Microsoft.EgitimAPI.ApplicationCore.Services.GivenCourseService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService.Dto;
using Microsoft.EgitimAPI.Lib;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CourseService
{
    public class CourseAppService:ICourseAppService
    {
        private readonly IAsyncRepository<Course> _courseRepository;
        private readonly IAsyncRepository<GivenCourse> _givenCourseRepository;
        private readonly IAsyncRepository<AdvertisingCourse> _advertisingCourseRepository;
        private readonly IAsyncRepository<FavoriteCourse> _favoriteCourseRepository;
        private readonly ICheckEdition _checkEdition;
        private readonly IBlobService _blobService;

        public CourseAppService(IAsyncRepository<Course> courseRepository,
            IAsyncRepository<GivenCourse> givenCourseRepository,
            IAsyncRepository<AdvertisingCourse> advertisingCourseRepository,
            IAsyncRepository<FavoriteCourse> favoriteCourseRepository,
                ICheckEdition checkEdition,
            IBlobService blobService
            )
        {
            _courseRepository = courseRepository;
            _givenCourseRepository =givenCourseRepository;
            _advertisingCourseRepository = advertisingCourseRepository;
            _favoriteCourseRepository = favoriteCourseRepository;
            _checkEdition = checkEdition;
            _blobService = blobService;
        }
        public async Task<Course> CreateCourse(CreateCourseDto input)
        {
            if (input.EducatorId == null)
            {
                long[] a = new long[0];
                input.EducatorId = a;
            }
            if (input.TenantId == null)
            {
                long[] b = new long[0];
                input.TenantId = b;
            }
            if (input.EducatorId.Length>0)
            {
                var isHaveRightEducator = await _checkEdition.HaveCreateCourseRight<Educator>(input.EducatorId);
                if (!isHaveRightEducator)
                {
                    throw new Exception("No right to create course for educator !");
                }
            }

            if ( input.TenantId.Length>0)
            {
                var isHaveRight = await _checkEdition.HaveCreateCourseRight<Tenant>(input.TenantId);
                if (!isHaveRight)
                {
                    throw new Exception("No right to create course tenant !");
                }
            }

            var imagePath = await _blobService.InsertFile(input.File);
            var course = new Course
            {
                Title = input.Title,
                Description = input.Description,
                Quota = input.Quota,
                Address = input.Address,
                OnlineVideo = input.OnlineVideo,
                Certificate = input.Certificate,
                CertificateOfParticipation = input.CertificateOfParticipation,
                Duration = input.Duration,
                Requirements = input.Requirements,
                Teachings = input.Teachings,
                Price = input.Price,
                DiscountPrice = input.DiscountPrice,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                CategoryId = input.CategoryId,
                LocationId = input.LocationId,
                OwnerType = input.OwnerType,
                OwnerId = input.OwnerId,
                ImagePath = imagePath
            };
            
            await _courseRepository.AddAsync(course);

            var count = (input.TenantId.Length > input.EducatorId.Length)
                ? input.TenantId.Length
                : input.EducatorId.Length;

                for (var i = 0; i <count; i++)
                {
                    var givenCourse = new GivenCourse
                    {
                        CourseId = course.Id,
                    };
                    if (input.TenantId.Length > i)
                    {
                        givenCourse.TenantId = input.TenantId[i];
                    }

                    if (input.EducatorId.Length > i)
                    {
                        givenCourse.EducatorId = input.EducatorId[i];
                    }
                    await _givenCourseRepository.AddAsync(givenCourse);
                }
            return course;
        }

        public async Task<CourseDto> GetCourseById(long id)
        {
            var isExsist = await _courseRepository.GetByIdAsync(id);
            if(isExsist==null) throw new Exception("There is no course with this id");
            
            var course = await _courseRepository.GetAll().Where(x=>x.Id==id).Include(x=>x.Category)
                .Include(x=>x.Location)
                .Include(x => x.Owners).ThenInclude(x => x.Tenant)
                .Include(x=>x.Owners).ThenInclude(x=>x.Educator)
                .Select(x => new CourseDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Quota = x.Quota,
                Price = x.Price,
                StartDate = x.StartDate,
                Address = x.Address,
                OnlineVideo = x.OnlineVideo,
                Certificate = x.Certificate,
                CertificateOfParticipation = x.CertificateOfParticipation,
                Duration = x.Duration,
                ImagePath = BlobService.BlobService.GetImageUrl(x.ImagePath),
                Requirements = x.Requirements,
                Teachings = x.Teachings,
                DiscountPrice = x.DiscountPrice,
                EndDate = x.EndDate,
                Score = x.Score,
                LocationName = x.Location.Name,
                Category = new CategoryDto
                {
                    Id = x.Category.Id,
                    Description = x.Category.Description,
                    DisplayName = x.Category.DisplayName,
                    ParentCategory = new ParentCategoryDto()
                },
                Tenants = x.Owners.Where(t=>t.TenantId!=null).Select(t => new CourseTenantDto
                {
                    TenantId = t.Tenant.Id,
                    TenantName = t.Tenant.TenantName,
                    LogoPath = t.Tenant.LogoPath
                }).ToList(),
                Educators = x.Owners.Where(e=>e.EducatorId!=null).Select(e=>new CourseEducatorDto
                {
                    EducatorId = e.Educator.Id,
                    EducatorName=e.Educator.Name,
                    Profession = e.Educator.Profession,
                    ProfileImgPath = e.Educator.ProfileImagePath
                }).ToList(),
                CourseOwnerInfo = x.OwnerType=="Tenant" ? x.Owners.Where(p=>p.Tenant.Id==x.OwnerId).Select(p=>new CourseOwnerInfo
                    {
                        EntityType = "Tenant",
                        Id = p.Tenant.Id,
                        Name = p.Tenant.TenantName,
                        Profession = p.Tenant.Title,
                        LogoPath = p.Tenant.LogoPath
                    }).ToList() : 
                    x.Owners.Where(a=>a.Educator.Id==x.OwnerId).Select(a=>new CourseOwnerInfo
                    {
                        EntityType = "Educator",
                        Id = a.Educator.Id,
                        Name = a.Educator.Name + " "+ a.Educator.Surname,
                        Profession = a.Educator.Profession,
                        LogoPath = a.Educator.ProfileImagePath
                    }).ToList()
            }).FirstOrDefaultAsync();
            return course;
        }

        public async Task<PagedResultDto<AdvertisingCourseDto>> GetAllAdvertisingCourses()
        {
            var courses = await _advertisingCourseRepository.GetAll().Include(x => x.Course)
                .ThenInclude(x => x.Owners).ThenInclude(x => x.Tenant)
                .Include(x => x.Course)
                .ThenInclude(x => x.Owners).ThenInclude(x => x.Educator)
                .Include(x => x.Course).ThenInclude(x => x.Category)
                .Include(x=>x.Course).ThenInclude(x=>x.LocationId)
                .Include(x => x.Tenant)
                .Include(x => x.Educator)
                .Where(x=>x.IsEnded==false && x.Course.AdvertisingState!=AdvertisingState.Stopped)
                .Select(x => new AdvertisingCourseDto
                {
                    CourseInfo = new CourseDto
                    {
                        Id = x.Course.Id,
                        Title = x.Course.Title,
                        Description = x.Course.Description,
                        Quota = x.Course.Quota,
                        Price = x.Course.Price,
                        StartDate = x.Course.StartDate,
                        EndDate = x.Course.EndDate,
                        CourseOwnerInfo = x.Course.OwnerType=="Tenant" ? x.Course.Owners.Where(p=>p.Tenant.Id==x.Course.OwnerId).Select(p=>new CourseOwnerInfo
                            {
                                Id = p.Tenant.Id,
                                Name = p.Tenant.TenantName,
                                Profession = null,
                                LogoPath = p.Tenant.LogoPath
                            }).ToList() : 
                            x.Course.Owners.Where(a=>a.Educator.Id==x.Course.OwnerId).Select(a=>new CourseOwnerInfo
                            {
                                Id = a.Educator.Id,
                                Name = a.Educator.Name + " "+ a.Educator.Surname,
                                Profession = a.Educator.Profession,
                                LogoPath = a.Educator.ProfileImagePath
                            }).ToList(),
                        Score = x.Course.Score,
                        LocationName = x.Course.Location.Name,
                        Category = new CategoryDto
                        {
                            Id = x.Course.Category.Id,
                            Description = x.Course.Category.Description,
                            DisplayName = x.Course.Category.DisplayName,
                            ParentCategory = new ParentCategoryDto()
                        },
                        Tenants = x.Course.Owners.Select(t => new CourseTenantDto
                        {
                            TenantId = t.Tenant.Id,
                            TenantName = t.Tenant.TenantName,
                            LogoPath = t.Tenant.LogoPath
                        }).ToList(),
                        Educators = x.Course.Owners.Select(e => new CourseEducatorDto
                        {
                            EducatorId = e.Educator.Id,
                            EducatorName = e.Educator.Name,
                            Profession = e.Educator.Profession,
                            ProfileImgPath = e.Educator.ProfileImagePath
                        }).ToList(),
                    },
                    
                }).ToListAsync();
            var result = new PagedResultDto<AdvertisingCourseDto>
            {
                Results = courses,
                Count = courses.Count
            };
            return result;
        }

        public async Task<Course> CreateAdvertisingCourse(CreateAdvertisingCourseDto input)
        {
            var course = new Course
            {
                Title = input.Title,
                Description = input.Description,
                Quota = input.Quota,
                Price = input.Price,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                CategoryId = input.CategoryId,
                LocationId = input.LocationId,
                OwnerType = input.OwnerType,
                OwnerId = input.OwnerId,
                AdvertisingState = AdvertisingState.Continues
            };
            await _courseRepository.AddAsync(course);

            var count = (input.TenantId.Length > input.EducatorId.Length)
                ? input.TenantId.Length
                : input.EducatorId.Length;
            
            for (var i = 0; i < count  ; i++)
            {
                var givenCourse = new GivenCourse
                {
                    CourseId = course.Id,
                    EducatorId = input.EducatorId[i],
                    TenantId = input.TenantId[i]
                };
                await _givenCourseRepository.AddAsync(givenCourse);
            }

            var advertisingModel = new AdvertisingCourse
            {
                CourseId = course.Id,
                TenantId = input.AdvertisingInfo.OwnerTenantId,
                EducatorId = input.AdvertisingInfo.OwnerEducatorId,
                Price = input.AdvertisingInfo.Price,
                StartDateTime = input.AdvertisingInfo.StartDateTime,
                EndDateTime = input.AdvertisingInfo.EndDateTime
            };
            await _advertisingCourseRepository.AddAsync(advertisingModel);

            return course;
        }

        public async Task AddFavoriteCourse(CreateFavoriteCourseDto input)
        {
            var model = new FavoriteCourse
            {
                CourseId = input.CourseId,
                UserId = input.UserId
            };
            await _favoriteCourseRepository.AddAsync(model);
        }

        public async Task<PagedResultDto<FavoriteCourseDto>> GetFavoriteCourses(long userId)
        {
            var courses = await _favoriteCourseRepository.GetAll().Where(x => x.UserId == userId)
                .Include(x => x.Course)
                .ThenInclude(x => x.Owners).ThenInclude(x => x.Educator)
                .Include(x=>x.Course).ThenInclude(x=>x.Location)
                .Include(x => x.User)
                .Select(x => new FavoriteCourseDto
                {
                    CourseId = x.Course.Id,
                    CourseName = x.Course.Title,
                    LocationName = x.Course.Location.Name,
                    Tenants = x.Course.Owners.Select(t=> new CourseTenantDto
                    {
                        TenantId = t.Tenant.Id,
                        LogoPath = t.Tenant.LogoPath,
                        TenantName = t.Tenant.TenantName
                    }).ToList(),
                    Educators = x.Course.Owners.Select(e=>new CourseEducatorDto
                    {
                        EducatorId = e.Educator.Id,
                        EducatorName=e.Educator.Name,
                        Profession = e.Educator.Profession,
                        ProfileImgPath = e.Educator.ProfileImagePath
                    }).ToList(),
                }).ToListAsync();
            return new PagedResultDto<FavoriteCourseDto>
            {
                Results = courses,
                Count = courses.Count
            };
        }
        
        public async Task<PagedResultDto<CourseDto>> GetCoursesByName(string courseName)
        {
            var courses = await _courseRepository.GetAll().Include(x=>x.Category)
                .Include(x=>x.Location)
                .Where(x => x.Title.Contains(courseName) || x.Description.Contains(courseName))
                .Select(x => new CourseDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Quota = x.Quota,
                Price = x.Price,
                ImagePath = BlobService.BlobService.GetImageUrl(x.ImagePath),
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                DiscountPrice = x.DiscountPrice,
                Score = x.Score,
                LocationName = x.Location.Name,
            }).ToListAsync();
            var result = new PagedResultDto<CourseDto>
            {
                Results = courses,
                Count = courses.Count
            };
            return result;
        }
        
        public async Task<PagedResultDto<GetCourseDto>> GetEntityCourses(string entityType,long id)
        {
            if (entityType == "Tenant")
            {
                var courses = await _courseRepository.GetAll().Include(x=>x.Category)
                    .Include(x=>x.Location)
                    .Include(x => x.Owners).ThenInclude(x => x.Tenant)
                    .Where(x => x.OwnerType == "Tenant" && x.OwnerId==id)
                    .Select(x => new GetCourseDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Price = x.Price,
                        DiscountPrice = x.DiscountPrice,
                        ShortDescription = x.ShortDescription
                    }).ToListAsync();
            
                var result = new PagedResultDto<GetCourseDto>
                {
                    Results = courses,
                    Count = courses.Count
                };
                return result;
            }

            var courses2 = await _courseRepository.GetAll().Include(x=>x.Category)
                .Include(x=>x.Location)
                .Include(x => x.Owners).ThenInclude(x => x.Educator)
                .Where(x => x.OwnerType == "Educator" && x.OwnerId==id)
                .Select(x => new GetCourseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    DiscountPrice = x.DiscountPrice,
                    ShortDescription = x.ShortDescription
                }).ToListAsync();
            
            var result2 = new PagedResultDto<GetCourseDto>
            {
                Results = courses2,
                Count = courses2.Count
            };
            return result2;
        }
        
        public async Task<PagedResultDto<CourseDto>> SearchCourses(string query,long locationId)
        {
            var courses = await _courseRepository.GetAll().Include(x=>x.Category)
                .Include(x=>x.Location)
                .Where(x => x.Title.Contains(query) || x.Description.Contains(query) && x.LocationId == locationId && x.IsDeleted==false )
                .Select(x => new CourseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Quota = x.Quota,
                    Price = x.Price,
                    ImagePath = BlobService.BlobService.GetImageUrl(x.ImagePath),
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    DiscountPrice = x.DiscountPrice,
                    Score = x.Score,
                    LocationName = x.Location.Name,
                }).ToListAsync();
            var result = new PagedResultDto<CourseDto>
            {
                Results = courses,
                Count = courses.Count
            };
            return result;
        }

        public async Task<PagedResultDto<CourseDto>> GetCoursesByCategory(long categoryId)
        {
            var courses = await _courseRepository.GetAll().Include(x => x.Category)
                .Include(x=>x.Location)
                .Where(x => x.Category.Id == categoryId).Select(x => new CourseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Quota = x.Quota,
                    Price = x.Price,
                    StartDate = x.StartDate,
                    ImagePath= BlobService.BlobService.GetImageUrl(x.ImagePath),
                    DiscountPrice = x.DiscountPrice,
                    EndDate = x.EndDate,
                    Score = x.Score,
                    LocationName = x.Location.Name,
                }).ToListAsync();
            var result = new PagedResultDto<CourseDto>
            {
                Results = courses,
                Count = courses.Count
            };
            return result;
        }

        public async Task DeleteCourse(long id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            course.IsDeleted = true;
            await _courseRepository.UpdateAsync(course);
        }

        public async Task UpdateCourse(UpdateCourseDto input)
        {
            var isExistCourse = await _courseRepository.GetAll().AnyAsync(x => x.Id == input.Id);
                if (!isExistCourse)
                    throw new Exception("Bu kurs mevcut degil!");

            var course = await _courseRepository.GetAll().FirstAsync(x => x.Id == input.Id);

            course.Id = input.Id;    
            course.Description = input.Description;
            course.Title = input.Title;
            course.Price = input.Price;
            course.Quota = input.Quota;
            course.Address = input.Address;
            course.OnlineVideo = input.OnlineVideo;
            course.Certificate = input.Certificate;
            course.CertificateOfParticipation = input.CertificateOfParticipation;
            course.Duration = input.Duration;
            course.Requirements = input.Requirements;
            course.Teachings = input.Teachings;
            course.DiscountPrice = input.DiscountPrice;
            course.StartDate = input.StartDate;
            course.EndDate = input.EndDate;
            course.CategoryId = input.CategoryId;

            await _courseRepository.UpdateAsync(course);

            var givenCourses = await _givenCourseRepository.GetAll().Where(x => x.CourseId == input.Id).ToListAsync();
            var count = (input.TenantId.Length > input.EducatorId.Length)
                ? input.TenantId.Length
                : input.EducatorId.Length;

            var loopCount = givenCourses.Count > count ? givenCourses.Count : count;  
            
            for (var i = 0; i <= loopCount; i++)
            { 
                // db deki her column tenantId educatorId degerini sirayla gunceller.Inputta karsiligi yoksa null degeri atar.
                // en son ikiside null ise egitmen ve kurum sayisi azalmistir. Columnu siler.  
                
                if (givenCourses.Count > i)
                {
                    if (input.TenantId.Length > i)
                    {
                        givenCourses[i].TenantId = input.TenantId[i];
                    }
                    else
                    {
                        if (givenCourses[i].TenantId.HasValue) givenCourses[i].TenantId = null;
                    }

                    if (input.EducatorId.Length > i)
                    {
                        givenCourses[i].EducatorId = input.EducatorId[i];
                    }
                    else
                    {
                        if (givenCourses[i].EducatorId.HasValue) givenCourses[i].EducatorId = null;
                    }
                    await _givenCourseRepository.UpdateAsync(givenCourses[i]);

                    if (givenCourses[i].TenantId != null || givenCourses[i].EducatorId != null) continue;
                    await _givenCourseRepository.DeleteAsync(givenCourses[i]);
                }
                // tablodaki data input dan az ise geri kalanlari db ye ekler. Fazladan egitmen yada kurum eklenmistir.
                
                else if(input.TenantId.Length > i || input.EducatorId.Length > i)
                {
                    var givenCourse = new GivenCourse
                    {
                        CourseId = course.Id,
                    };
                    if (input.TenantId.Length > i)
                    {
                        givenCourse.TenantId = input.TenantId[i];
                    }

                    if (input.EducatorId.Length > i)
                    {
                        givenCourse.EducatorId = input.EducatorId[i];
                    }
                    await _givenCourseRepository.AddAsync(givenCourse);
                }
            }
        }

        public async Task<PagedResultDto<CourseDto>> GetAllCourses()
        {
            var courses = await _courseRepository.GetAll().Where(x => x.IsDeleted == false && x.AdvertisingState != AdvertisingState.Stopped)
                .Include(x=>x.Location)
                .Select(x => new CourseDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Quota = x.Quota,
                    Price = x.Price,
                    StartDate = x.StartDate,
                    Address = x.Address,
                    OnlineVideo = x.OnlineVideo,
                    ImagePath = BlobService.BlobService.GetImageUrl(x.ImagePath),
                    Certificate = x.Certificate,
                    CertificateOfParticipation = x.CertificateOfParticipation,
                    Duration = x.Duration,
                    Requirements = x.Requirements,
                    Teachings = x.Teachings,
                    DiscountPrice = x.DiscountPrice,
                    EndDate = x.EndDate,
                    Score = x.Score,
                    LocationName = x.Location.Name,
                }).ToListAsync();
            var result = new PagedResultDto<CourseDto>
            {
                Results = courses,
                Count = courses.Count
            };
            return result;
        }
        
    }
}