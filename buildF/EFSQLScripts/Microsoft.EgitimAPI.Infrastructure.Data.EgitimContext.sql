IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE TABLE [Categories] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [DisplayName] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ParentCategoryId] bigint NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Categories_Categories_ParentCategoryId] FOREIGN KEY ([ParentCategoryId]) REFERENCES [Categories] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE TABLE [CourseContents] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ContentPath] nvarchar(max) NULL,
        [ContentType] int NOT NULL,
        CONSTRAINT [PK_CourseContents] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE TABLE [Educators] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [Name] nvarchar(max) NULL,
        [Surname] nvarchar(max) NULL,
        [Profession] nvarchar(max) NULL,
        [Resume] nvarchar(max) NULL,
        [ProfileImagePath] nvarchar(max) NULL,
        [IsPremium] bit NOT NULL,
        CONSTRAINT [PK_Educators] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE TABLE [Tenants] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [IsPremium] bit NOT NULL,
        [Password] nvarchar(max) NULL,
        [TenantName] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumber2] nvarchar(max) NULL,
        [LogoPath] nvarchar(max) NULL,
        CONSTRAINT [PK_Tenants] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE TABLE [Users] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [Name] nvarchar(max) NULL,
        [Surname] nvarchar(max) NULL,
        [Gender] nvarchar(1) NOT NULL,
        [Age] int NOT NULL,
        [Profession] nvarchar(max) NULL,
        [Username] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [EmailAddress] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE TABLE [Courses] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [Title] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Quota] int NOT NULL,
        [Price] float NOT NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        [CategoryId] bigint NOT NULL,
        [CourseContentId] bigint NULL,
        CONSTRAINT [PK_Courses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Courses_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Courses_CourseContents_CourseContentId] FOREIGN KEY ([CourseContentId]) REFERENCES [CourseContents] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE TABLE [TenantEducator] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [TenantId] bigint NOT NULL,
        [EducatorId] bigint NOT NULL,
        CONSTRAINT [PK_TenantEducator] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TenantEducator_Educators_EducatorId] FOREIGN KEY ([EducatorId]) REFERENCES [Educators] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TenantEducator_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Tenants] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE TABLE [FavoriteCourses] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UserId] bigint NOT NULL,
        [CourseId] bigint NOT NULL,
        CONSTRAINT [PK_FavoriteCourses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FavoriteCourses_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_FavoriteCourses_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE TABLE [GivenCourses] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CourseId] bigint NOT NULL,
        [TenantId] bigint NULL,
        [EducatorId] bigint NULL,
        CONSTRAINT [PK_GivenCourses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GivenCourses_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GivenCourses_Educators_EducatorId] FOREIGN KEY ([EducatorId]) REFERENCES [Educators] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_GivenCourses_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Tenants] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE INDEX [IX_Categories_ParentCategoryId] ON [Categories] ([ParentCategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE INDEX [IX_Courses_CategoryId] ON [Courses] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE INDEX [IX_Courses_CourseContentId] ON [Courses] ([CourseContentId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE INDEX [IX_FavoriteCourses_CourseId] ON [FavoriteCourses] ([CourseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE INDEX [IX_FavoriteCourses_UserId] ON [FavoriteCourses] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE INDEX [IX_GivenCourses_CourseId] ON [GivenCourses] ([CourseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE INDEX [IX_GivenCourses_EducatorId] ON [GivenCourses] ([EducatorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE INDEX [IX_GivenCourses_TenantId] ON [GivenCourses] ([TenantId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE INDEX [IX_TenantEducator_EducatorId] ON [TenantEducator] ([EducatorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    CREATE INDEX [IX_TenantEducator_TenantId] ON [TenantEducator] ([TenantId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014185922_ahahah')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191014185922_ahahah', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014223920_EventModel')
BEGIN
    CREATE TABLE [Events] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [Title] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Location] nvarchar(max) NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        [Quota] int NOT NULL,
        [Price] float NOT NULL,
        [EventType] int NOT NULL,
        CONSTRAINT [PK_Events] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014223920_EventModel')
BEGIN
    CREATE TABLE [GivenEvents] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [EventId] bigint NOT NULL,
        [TenantId] bigint NULL,
        [EducatorId] bigint NULL,
        CONSTRAINT [PK_GivenEvents] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GivenEvents_Educators_EducatorId] FOREIGN KEY ([EducatorId]) REFERENCES [Educators] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_GivenEvents_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GivenEvents_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Tenants] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014223920_EventModel')
BEGIN
    CREATE INDEX [IX_GivenEvents_EducatorId] ON [GivenEvents] ([EducatorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014223920_EventModel')
BEGIN
    CREATE INDEX [IX_GivenEvents_EventId] ON [GivenEvents] ([EventId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014223920_EventModel')
BEGIN
    CREATE INDEX [IX_GivenEvents_TenantId] ON [GivenEvents] ([TenantId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014223920_EventModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191014223920_EventModel', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014225930_EventModel_type')
BEGIN
    ALTER TABLE [Events] ADD [CategoryId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014225930_EventModel_type')
BEGIN
    CREATE INDEX [IX_Events_CategoryId] ON [Events] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014225930_EventModel_type')
BEGIN
    ALTER TABLE [Events] ADD CONSTRAINT [FK_Events_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191014225930_EventModel_type')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191014225930_EventModel_type', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015195620_AdvertisingCourses')
BEGIN
    CREATE TABLE [AdvertisingCourses] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CourseId] bigint NOT NULL,
        [TenantId] bigint NULL,
        [EducatorId] bigint NULL,
        CONSTRAINT [PK_AdvertisingCourses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AdvertisingCourses_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AdvertisingCourses_Educators_EducatorId] FOREIGN KEY ([EducatorId]) REFERENCES [Educators] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AdvertisingCourses_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Tenants] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015195620_AdvertisingCourses')
BEGIN
    CREATE INDEX [IX_AdvertisingCourses_CourseId] ON [AdvertisingCourses] ([CourseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015195620_AdvertisingCourses')
BEGIN
    CREATE INDEX [IX_AdvertisingCourses_EducatorId] ON [AdvertisingCourses] ([EducatorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015195620_AdvertisingCourses')
BEGIN
    CREATE INDEX [IX_AdvertisingCourses_TenantId] ON [AdvertisingCourses] ([TenantId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015195620_AdvertisingCourses')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191015195620_AdvertisingCourses', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015201339_AdvertisingCourses_2')
BEGIN
    ALTER TABLE [AdvertisingCourses] ADD [EndDateTime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015201339_AdvertisingCourses_2')
BEGIN
    ALTER TABLE [AdvertisingCourses] ADD [Price] real NOT NULL DEFAULT CAST(0 AS real);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015201339_AdvertisingCourses_2')
BEGIN
    ALTER TABLE [AdvertisingCourses] ADD [StartDateTime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015201339_AdvertisingCourses_2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191015201339_AdvertisingCourses_2', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015212242_AdvertisingCourses_3')
BEGIN
    DROP INDEX [IX_AdvertisingCourses_CourseId] ON [AdvertisingCourses];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015212242_AdvertisingCourses_3')
BEGIN
    ALTER TABLE [Courses] ADD [AdvertisingId] bigint NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015212242_AdvertisingCourses_3')
BEGIN
    ALTER TABLE [AdvertisingCourses] ADD [IsEnded] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015212242_AdvertisingCourses_3')
BEGIN
    CREATE INDEX [IX_Courses_AdvertisingId] ON [Courses] ([AdvertisingId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015212242_AdvertisingCourses_3')
BEGIN
    CREATE UNIQUE INDEX [IX_AdvertisingCourses_CourseId] ON [AdvertisingCourses] ([CourseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015212242_AdvertisingCourses_3')
BEGIN
    ALTER TABLE [Courses] ADD CONSTRAINT [FK_Courses_AdvertisingCourses_AdvertisingId] FOREIGN KEY ([AdvertisingId]) REFERENCES [AdvertisingCourses] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015212242_AdvertisingCourses_3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191015212242_AdvertisingCourses_3', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015213809_AdvertisingCourses_4')
BEGIN
    ALTER TABLE [Courses] DROP CONSTRAINT [FK_Courses_AdvertisingCourses_AdvertisingId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015213809_AdvertisingCourses_4')
BEGIN
    DROP INDEX [IX_Courses_AdvertisingId] ON [Courses];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015213809_AdvertisingCourses_4')
BEGIN
    DROP INDEX [IX_AdvertisingCourses_CourseId] ON [AdvertisingCourses];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015213809_AdvertisingCourses_4')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Courses]') AND [c].[name] = N'AdvertisingId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Courses] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Courses] DROP COLUMN [AdvertisingId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015213809_AdvertisingCourses_4')
BEGIN
    CREATE INDEX [IX_AdvertisingCourses_CourseId] ON [AdvertisingCourses] ([CourseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015213809_AdvertisingCourses_4')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191015213809_AdvertisingCourses_4', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015222220_AdvertisingCourses_5')
BEGIN
    ALTER TABLE [Courses] ADD [AdvertisingState] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191015222220_AdvertisingCourses_5')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191015222220_AdvertisingCourses_5', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184041_Comment')
BEGIN
    ALTER TABLE [Tenants] ADD [CommentId] bigint NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184041_Comment')
BEGIN
    CREATE TABLE [Comments] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [EntityName] nvarchar(max) NULL,
        [EntityId] bigint NOT NULL,
        [Content] nvarchar(max) NULL,
        [EducatorId] bigint NULL,
        [TenantId] bigint NULL,
        [CourseId] bigint NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comments_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Comments_Educators_EducatorId] FOREIGN KEY ([EducatorId]) REFERENCES [Educators] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Comments_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Tenants] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184041_Comment')
BEGIN
    CREATE INDEX [IX_Tenants_CommentId] ON [Tenants] ([CommentId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184041_Comment')
BEGIN
    CREATE INDEX [IX_Comments_CourseId] ON [Comments] ([CourseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184041_Comment')
BEGIN
    CREATE INDEX [IX_Comments_EducatorId] ON [Comments] ([EducatorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184041_Comment')
BEGIN
    CREATE INDEX [IX_Comments_TenantId] ON [Comments] ([TenantId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184041_Comment')
BEGIN
    ALTER TABLE [Tenants] ADD CONSTRAINT [FK_Tenants_Comments_CommentId] FOREIGN KEY ([CommentId]) REFERENCES [Comments] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184041_Comment')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191016184041_Comment', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    ALTER TABLE [Comments] DROP CONSTRAINT [FK_Comments_Courses_CourseId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    ALTER TABLE [Comments] DROP CONSTRAINT [FK_Comments_Educators_EducatorId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    ALTER TABLE [Comments] DROP CONSTRAINT [FK_Comments_Tenants_TenantId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    DROP INDEX [IX_Comments_CourseId] ON [Comments];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    DROP INDEX [IX_Comments_EducatorId] ON [Comments];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    DROP INDEX [IX_Comments_TenantId] ON [Comments];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Comments]') AND [c].[name] = N'CourseId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Comments] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Comments] DROP COLUMN [CourseId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Comments]') AND [c].[name] = N'EducatorId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Comments] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Comments] DROP COLUMN [EducatorId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Comments]') AND [c].[name] = N'TenantId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Comments] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Comments] DROP COLUMN [TenantId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    CREATE INDEX [IX_Comments_EntityId] ON [Comments] ([EntityId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Courses_EntityId] FOREIGN KEY ([EntityId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Educators_EntityId] FOREIGN KEY ([EntityId]) REFERENCES [Educators] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Tenants_EntityId] FOREIGN KEY ([EntityId]) REFERENCES [Tenants] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016184938_Comment_2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191016184938_Comment_2', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016193533_CommentOfUser')
BEGIN
    ALTER TABLE [Comments] ADD [UserId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016193533_CommentOfUser')
BEGIN
    CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016193533_CommentOfUser')
BEGIN
    ALTER TABLE [Comments] ADD CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016193533_CommentOfUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191016193533_CommentOfUser', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016222549_Score')
BEGIN
    ALTER TABLE [Tenants] ADD [Score] real NOT NULL DEFAULT CAST(0 AS real);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016222549_Score')
BEGIN
    ALTER TABLE [Educators] ADD [Score] real NOT NULL DEFAULT CAST(0 AS real);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016222549_Score')
BEGIN
    ALTER TABLE [Courses] ADD [Score] real NOT NULL DEFAULT CAST(0 AS real);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016222549_Score')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191016222549_Score', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016225259_Notifications')
BEGIN
    ALTER TABLE [Tenants] DROP CONSTRAINT [FK_Tenants_Comments_CommentId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016225259_Notifications')
BEGIN
    DROP INDEX [IX_Tenants_CommentId] ON [Tenants];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016225259_Notifications')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tenants]') AND [c].[name] = N'CommentId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Tenants] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Tenants] DROP COLUMN [CommentId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016225259_Notifications')
BEGIN
    CREATE TABLE [Notifications] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [Content] nvarchar(max) NULL,
        [Title] nvarchar(max) NULL,
        [SenderId] bigint NOT NULL,
        [SenderType] nvarchar(max) NULL,
        [OwnerId] bigint NOT NULL,
        [OwnerType] nvarchar(max) NULL,
        [UserId] bigint NULL,
        [EducatorId] bigint NULL,
        [TenantId] bigint NULL,
        [CourseId] bigint NULL,
        CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Notifications_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Notifications_Educators_EducatorId] FOREIGN KEY ([EducatorId]) REFERENCES [Educators] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Notifications_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Tenants] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Notifications_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016225259_Notifications')
BEGIN
    CREATE INDEX [IX_Notifications_CourseId] ON [Notifications] ([CourseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016225259_Notifications')
BEGIN
    CREATE INDEX [IX_Notifications_EducatorId] ON [Notifications] ([EducatorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016225259_Notifications')
BEGIN
    CREATE INDEX [IX_Notifications_TenantId] ON [Notifications] ([TenantId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016225259_Notifications')
BEGIN
    CREATE INDEX [IX_Notifications_UserId] ON [Notifications] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016225259_Notifications')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191016225259_Notifications', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    ALTER TABLE [Notifications] DROP CONSTRAINT [FK_Notifications_Courses_CourseId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    ALTER TABLE [Notifications] DROP CONSTRAINT [FK_Notifications_Educators_EducatorId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    ALTER TABLE [Notifications] DROP CONSTRAINT [FK_Notifications_Tenants_TenantId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    ALTER TABLE [Notifications] DROP CONSTRAINT [FK_Notifications_Users_UserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    DROP INDEX [IX_Notifications_CourseId] ON [Notifications];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    DROP INDEX [IX_Notifications_EducatorId] ON [Notifications];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    DROP INDEX [IX_Notifications_TenantId] ON [Notifications];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    DROP INDEX [IX_Notifications_UserId] ON [Notifications];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notifications]') AND [c].[name] = N'CourseId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Notifications] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Notifications] DROP COLUMN [CourseId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notifications]') AND [c].[name] = N'EducatorId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Notifications] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Notifications] DROP COLUMN [EducatorId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notifications]') AND [c].[name] = N'TenantId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Notifications] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Notifications] DROP COLUMN [TenantId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notifications]') AND [c].[name] = N'UserId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Notifications] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Notifications] DROP COLUMN [UserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    ALTER TABLE [Notifications] ADD [IsRead] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191016230124_Notify2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191016230124_Notify2', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191017192827_notify-json')
BEGIN
    EXEC sp_rename N'[Notifications].[Content]', N'_Content', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191017192827_notify-json')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191017192827_notify-json', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191017194217_Dictionary')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notifications]') AND [c].[name] = N'_Content');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Notifications] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Notifications] DROP COLUMN [_Content];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191017194217_Dictionary')
BEGIN
    ALTER TABLE [Notifications] ADD [Content] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191017194217_Dictionary')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191017194217_Dictionary', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191017195945_notifyLast')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191017195945_notifyLast', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191017211031_DictionaryRemove')
BEGIN
    ALTER TABLE [Notifications] ADD [ContentId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191017211031_DictionaryRemove')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191017211031_DictionaryRemove', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191017215909_notifyContentType')
BEGIN
    ALTER TABLE [TenantEducator] ADD [IsAccepted] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191017215909_notifyContentType')
BEGIN
    ALTER TABLE [Notifications] ADD [NotifyContentType] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191017215909_notifyContentType')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191017215909_notifyContentType', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191021223137_Editions')
BEGIN
    ALTER TABLE [Tenants] ADD [EditionId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191021223137_Editions')
BEGIN
    ALTER TABLE [Educators] ADD [EditionId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191021223137_Editions')
BEGIN
    CREATE TABLE [Editions] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CourseCount] int NOT NULL,
        [EventCount] int NOT NULL,
        [LiveSupport] bit NOT NULL,
        [Price] real NOT NULL,
        CONSTRAINT [PK_Editions] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191021223137_Editions')
BEGIN
    CREATE INDEX [IX_Tenants_EditionId] ON [Tenants] ([EditionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191021223137_Editions')
BEGIN
    CREATE INDEX [IX_Educators_EditionId] ON [Educators] ([EditionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191021223137_Editions')
BEGIN
    ALTER TABLE [Educators] ADD CONSTRAINT [FK_Educators_Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [Editions] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191021223137_Editions')
BEGIN
    ALTER TABLE [Tenants] ADD CONSTRAINT [FK_Tenants_Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [Editions] ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191021223137_Editions')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191021223137_Editions', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191021223634_Editions_2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191021223634_Editions_2', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191123205112_locationId')
BEGIN
    ALTER TABLE [Courses] ADD [Address] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191123205112_locationId')
BEGIN
    ALTER TABLE [Courses] ADD [LocationId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191123205112_locationId')
BEGIN
    CREATE TABLE [Locations] (
        [Id] bigint NOT NULL IDENTITY,
        [ModifiedDate] datetime2 NOT NULL,
        [CreatorUserId] nvarchar(max) NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Locations] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191123205112_locationId')
BEGIN
    CREATE INDEX [IX_Courses_LocationId] ON [Courses] ([LocationId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191123205112_locationId')
BEGIN
    ALTER TABLE [Courses] ADD CONSTRAINT [FK_Courses_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191123205112_locationId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191123205112_locationId', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124192255_locationId_events')
BEGIN
    EXEC sp_rename N'[Events].[Location]', N'Address', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124192255_locationId_events')
BEGIN
    ALTER TABLE [Events] ADD [LocationId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124192255_locationId_events')
BEGIN
    CREATE INDEX [IX_Events_LocationId] ON [Events] ([LocationId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124192255_locationId_events')
BEGIN
    ALTER TABLE [Events] ADD CONSTRAINT [FK_Events_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124192255_locationId_events')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191124192255_locationId_events', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124201010_ownerId_events')
BEGIN
    ALTER TABLE [Events] ADD [OwnerId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124201010_ownerId_events')
BEGIN
    ALTER TABLE [Events] ADD [OwnerType] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124201010_ownerId_events')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191124201010_ownerId_events', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124215845_ownerId_courses')
BEGIN
    ALTER TABLE [Courses] ADD [OwnerId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124215845_ownerId_courses')
BEGIN
    ALTER TABLE [Courses] ADD [OwnerType] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124215845_ownerId_courses')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191124215845_ownerId_courses', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129200500_price-to-decimal')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Courses]') AND [c].[name] = N'Price');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Courses] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Courses] ALTER COLUMN [Price] decimal(18,2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191129200500_price-to-decimal')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191129200500_price-to-decimal', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130200405_tenantadds')
BEGIN
    ALTER TABLE [Tenants] ADD [AboutUs] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130200405_tenantadds')
BEGIN
    ALTER TABLE [Tenants] ADD [Title] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130200405_tenantadds')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191130200405_tenantadds', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130200608_tenant_location')
BEGIN
    ALTER TABLE [Tenants] ADD [LocationId] bigint NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130200608_tenant_location')
BEGIN
    CREATE INDEX [IX_Tenants_LocationId] ON [Tenants] ([LocationId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130200608_tenant_location')
BEGIN
    ALTER TABLE [Tenants] ADD CONSTRAINT [FK_Tenants_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130200608_tenant_location')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191130200608_tenant_location', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130200704_tenantLocation_required')
BEGIN
    ALTER TABLE [Tenants] DROP CONSTRAINT [FK_Tenants_Locations_LocationId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130200704_tenantLocation_required')
BEGIN
    DROP INDEX [IX_Tenants_LocationId] ON [Tenants];
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tenants]') AND [c].[name] = N'LocationId');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Tenants] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Tenants] ALTER COLUMN [LocationId] bigint NOT NULL;
    CREATE INDEX [IX_Tenants_LocationId] ON [Tenants] ([LocationId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130200704_tenantLocation_required')
BEGIN
    ALTER TABLE [Tenants] ADD CONSTRAINT [FK_Tenants_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130200704_tenantLocation_required')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191130200704_tenantLocation_required', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130201144_tenant_location_changed')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191130201144_tenant_location_changed', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130202901_edition_removed')
BEGIN
    ALTER TABLE [Tenants] DROP CONSTRAINT [FK_Tenants_Editions_EditionId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130202901_edition_removed')
BEGIN
    DROP INDEX [IX_Tenants_EditionId] ON [Tenants];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130202901_edition_removed')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tenants]') AND [c].[name] = N'EditionId');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Tenants] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Tenants] DROP COLUMN [EditionId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130202901_edition_removed')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191130202901_edition_removed', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130203223_edition_eheh')
BEGIN
    ALTER TABLE [Educators] DROP CONSTRAINT [FK_Educators_Editions_EditionId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130203223_edition_eheh')
BEGIN
    DROP INDEX [IX_Educators_EditionId] ON [Educators];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130203223_edition_eheh')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Educators]') AND [c].[name] = N'EditionId');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Educators] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Educators] DROP COLUMN [EditionId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191130203223_edition_eheh')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191130203223_edition_eheh', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200208211022_blob')
BEGIN
    ALTER TABLE [Courses] ADD [ImagePath] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200208211022_blob')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200208211022_blob', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200212223303_course-entity')
BEGIN
    ALTER TABLE [Courses] ADD [Certificate] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200212223303_course-entity')
BEGIN
    ALTER TABLE [Courses] ADD [CertificateOfParticipation] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200212223303_course-entity')
BEGIN
    ALTER TABLE [Courses] ADD [DiscountPrice] decimal(18,2) NOT NULL DEFAULT 0.0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200212223303_course-entity')
BEGIN
    ALTER TABLE [Courses] ADD [Duration] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200212223303_course-entity')
BEGIN
    ALTER TABLE [Courses] ADD [OnlineVideo] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200212223303_course-entity')
BEGIN
    ALTER TABLE [Courses] ADD [Requirements] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200212223303_course-entity')
BEGIN
    ALTER TABLE [Courses] ADD [Teachings] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200212223303_course-entity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200212223303_course-entity', N'2.2.6-servicing-10079');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200219163711_fırst')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200219163711_fırst', N'2.2.6-servicing-10079');
END;

GO

