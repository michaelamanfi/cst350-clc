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

CREATE TABLE [dbo].[Status] (
    [StatusId] INT IDENTITY(1,1) NOT NULL,
    [Status] NVARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([StatusId] ASC)
);

INSERT INTO [dbo].[Status] ([Status]) VALUES
('Completed'),
('In Progress'),
('Failed'),
('Won');

GO
CREATE TABLE [dbo].[Game] (
    [GameId]   INT            IDENTITY (1, 1) NOT NULL,
    [UserId]   INT            NOT NULL,
    [StatusId] INT            NOT NULL,
    [Json]     NVARCHAR (MAX) NULL,
    [CreatedDate] DATETIME NOT NULL, 
    [ModifiedDate] DATETIME NOT NULL, 
    [DisplayName] NVARCHAR(100) NULL, 
    PRIMARY KEY CLUSTERED ([GameId] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]),
    FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([StatusId])
);

GO
-- Add indexes on foreign keys for better join performance
CREATE INDEX idx_userid ON [dbo].[Game] ([UserId]);
CREATE INDEX idx_statusid ON [dbo].[Game] ([StatusId]);

GO