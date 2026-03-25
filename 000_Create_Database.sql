IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BulletinBoard')
BEGIN
    CREATE DATABASE [BulletinBoard];
END
GO

USE [BulletinBoard];
GO