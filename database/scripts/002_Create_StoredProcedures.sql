USE [BulletinBoard];
GO

-- =============================================
-- 1. CREATE
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[sp_CreateAnnouncement]
    @Title NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @Category INT,
    @SubCategory INT,
    @AuthorId NVARCHAR(256) = NULL,
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Announcements] 
        ([Title], [Description], [Category], [SubCategory], [AuthorId], [Status], [CreatedDate])
    VALUES 
        (@Title, @Description, @Category, @SubCategory, @AuthorId, 1, GETUTCDATE());

    SET @NewId = SCOPE_IDENTITY();
END
GO

-- =============================================
-- 2. READ
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[sp_GetAnnouncements]
    @Category INT = NULL,
    @SubCategory INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Id], [Title], [Description], [CreatedDate], [Status], 
        [Category], [SubCategory], [AuthorId]
    FROM 
        [dbo].[Announcements]
    WHERE 
        (@Category IS NULL OR [Category] = @Category) AND
        (@SubCategory IS NULL OR [SubCategory] = @SubCategory)
    ORDER BY 
        [CreatedDate] DESC;
END
GO

-- =============================================
-- 2.1 READ
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[sp_GetAnnouncementById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        [Id], [Title], [Description], [CreatedDate], [Status], 
        [Category], [SubCategory], [AuthorId]
    FROM 
        [dbo].[Announcements]
    WHERE 
        [Id] = @Id;
END
GO

-- =============================================
-- 3. UPDATE
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[sp_UpdateAnnouncement]
    @Id INT,
    @Title NVARCHAR(100),
    @Description NVARCHAR(MAX),
    @Category INT,
    @SubCategory INT,
    @Status BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Announcements]
    SET 
        [Title] = @Title,
        [Description] = @Description,
        [Category] = @Category,
        [SubCategory] = @SubCategory,
        [Status] = @Status
    WHERE 
        [Id] = @Id;
END
GO

-- =============================================
-- 4. DELETE
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[sp_DeleteAnnouncement]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[Announcements]
    WHERE [Id] = @Id;
END
GO