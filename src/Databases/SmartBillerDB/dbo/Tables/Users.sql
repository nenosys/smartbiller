CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL identity(1,1), 
    [FirstName] NVARCHAR(100) NULL, 
    [LastName] NVARCHAR(100) NULL, 
    [AspNetUserId] NVARCHAR(128) NOT NULL, 
    [HospitalId] INT NOT NULL, 
    CONSTRAINT [FK_Users_AspNetUsers] FOREIGN KEY ([AspNetUserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_Users_Hospitals] FOREIGN KEY ([HospitalId]) REFERENCES [Hospitals]([Id]), 
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]) 
)
