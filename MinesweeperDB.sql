-- Create MinesweeperDB database
CREATE DATABASE MinesweeperDB;
GO

-- Switch to the MinesweeperDB database
USE MinesweeperDB;
GO

-- Create Users table
CREATE TABLE [dbo].[Users]
(
    UserId INT IDENTITY PRIMARY KEY,
    FirstName NVARCHAR(50) NULL,
    LastName NVARCHAR(50) NULL,
    Sex NVARCHAR(10) NULL,
    Age INT NOT NULL,
    State NVARCHAR(50),
    EmailAddress NVARCHAR(100) NOT NULL UNIQUE,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL
);
GO
