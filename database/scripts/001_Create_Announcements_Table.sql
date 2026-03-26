USE [BulletinBoard];
GO

CREATE TABLE [dbo].[Announcements]
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Title] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),    
    [Status] BIT NOT NULL DEFAULT 1,    
    [Category] INT NOT NULL,
    [SubCategory] INT NOT NULL,
    [AuthorId] NVARCHAR(256) NULL 
);
GO

CREATE NONCLUSTERED INDEX [IX_Announcements_Category] 
ON [dbo].[Announcements] ([Category], [SubCategory]);
GO