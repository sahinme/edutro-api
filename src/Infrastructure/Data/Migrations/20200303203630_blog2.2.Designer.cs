﻿// <auto-generated />
using System;
using Microsoft.EgitimAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    [DbContext(typeof(EgitimContext))]
    [Migration("20200303203630_blog2.2")]
    partial class blog22
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Answers.Answer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<long>("EntityId");

                    b.Property<int>("EntityType");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("QuestionId");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Categories.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long?>("ParentCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Comments.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<long>("EntityId");

                    b.Property<string>("EntityName");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.AdvertisingCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<long?>("EducatorId");

                    b.Property<DateTime>("EndDateTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsEnded");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<float>("Price");

                    b.Property<DateTime>("StartDateTime");

                    b.Property<long?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("EducatorId");

                    b.HasIndex("TenantId");

                    b.ToTable("AdvertisingCourses");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int>("AdvertisingState");

                    b.Property<long>("CategoryId");

                    b.Property<bool>("Certificate");

                    b.Property<bool>("CertificateOfParticipation");

                    b.Property<long?>("CourseContentId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<decimal>("DiscountPrice");

                    b.Property<int>("DurationCount");

                    b.Property<string>("DurationType");

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("LocationId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<bool>("OnlineVideo");

                    b.Property<long>("OwnerId");

                    b.Property<int>("OwnerType");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quota");

                    b.Property<string>("Requirements");

                    b.Property<float>("Score");

                    b.Property<string>("ShortDescription");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Teachings");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CourseContentId");

                    b.HasIndex("LocationId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.CourseContent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentPath");

                    b.Property<int>("ContentType");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("CourseContents");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.FavoriteCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteCourses");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.GivenCourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CourseId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<long?>("EducatorId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("EducatorId");

                    b.HasIndex("TenantId");

                    b.ToTable("GivenCourses");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Editions.Edition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseCount");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<int>("EventCount");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LiveSupport");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<float>("Price");

                    b.HasKey("Id");

                    b.ToTable("Editions");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Educators.Educator", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPremium");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Profession");

                    b.Property<string>("ProfileImagePath");

                    b.Property<string>("Resume");

                    b.Property<float>("Score");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Educators");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Events.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<long>("CategoryId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("EventType");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("LocationId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("OwnerId");

                    b.Property<string>("OwnerType");

                    b.Property<double>("Price");

                    b.Property<int>("Quota");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Events.GivenEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<long?>("EducatorId");

                    b.Property<long>("EventId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("EducatorId");

                    b.HasIndex("EventId");

                    b.HasIndex("TenantId");

                    b.ToTable("GivenEvents");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Locations.Location", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications.Notification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<long>("ContentId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsRead");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("NotifyContentType");

                    b.Property<long>("OwnerId");

                    b.Property<string>("OwnerType");

                    b.Property<long>("SenderId");

                    b.Property<string>("SenderType");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.PostComments.PostComment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("PostId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("PostComments");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Posts.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CategoryId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<long?>("EducatorId");

                    b.Property<long>("EntityId");

                    b.Property<int>("EntityType");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("PostState");

                    b.Property<string>("ShortDescription");

                    b.Property<long?>("TenantId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("EducatorId");

                    b.HasIndex("TenantId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Questions.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<long>("EntityId");

                    b.Property<int>("EntityType");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Title");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.TenantEducator.TenantEducator", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<long>("EducatorId");

                    b.Property<bool>("IsAccepted");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<long>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("EducatorId");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantEducator");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants.Tenant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AboutUs");

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPremium");

                    b.Property<long>("LocationId");

                    b.Property<string>("LogoPath");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("PhoneNumber2");

                    b.Property<float>("Score");

                    b.Property<string>("TenantName");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Users.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Profession");

                    b.Property<string>("Surname");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Answers.Answer", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Educators.Educator", "Educator")
                        .WithMany("Answers")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants.Tenant", "Tenant")
                        .WithMany("Answers")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Users.User", "User")
                        .WithMany("Answers")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Questions.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Categories.Category", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Categories.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Comments.Comment", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.Course", "Course")
                        .WithMany("Comments")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Educators.Educator", "Educator")
                        .WithMany("Comments")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants.Tenant", "Tenant")
                        .WithMany("Comments")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.AdvertisingCourse", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Educators.Educator", "Educator")
                        .WithMany("AdvertisingCourses")
                        .HasForeignKey("EducatorId");

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants.Tenant", "Tenant")
                        .WithMany("AdvertisingCourses")
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.Course", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.CourseContent", "CourseContent")
                        .WithMany()
                        .HasForeignKey("CourseContentId");

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Locations.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.FavoriteCourse", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.GivenCourse", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Courses.Course", "Course")
                        .WithMany("Owners")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Educators.Educator", "Educator")
                        .WithMany("GivenCourses")
                        .HasForeignKey("EducatorId");

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants.Tenant", "Tenant")
                        .WithMany("GivenCourses")
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Events.Event", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Locations.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Events.GivenEvent", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Educators.Educator", "Educator")
                        .WithMany("GivenEvents")
                        .HasForeignKey("EducatorId");

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Events.Event", "Event")
                        .WithMany("Owners")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants.Tenant", "Tenant")
                        .WithMany("GivenEvents")
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.PostComments.PostComment", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Posts.Post", "Post")
                        .WithMany("PostComments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Posts.Post", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Educators.Educator", "Educator")
                        .WithMany()
                        .HasForeignKey("EducatorId");

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Questions.Question", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.TenantEducator.TenantEducator", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Educators.Educator", "Educator")
                        .WithMany("EducatorTenants")
                        .HasForeignKey("EducatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants.Tenant", "Tenant")
                        .WithMany("TenantEducators")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants.Tenant", b =>
                {
                    b.HasOne("Microsoft.EgitimAPI.ApplicationCore.Entities.Locations.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
