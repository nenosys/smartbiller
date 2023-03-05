CREATE TABLE [dbo].[Patients]
(
	[Id] INT NOT NULL Identity(1,1), 
	[HospitalId] int NOT NULL,
    [FirstName] NVARCHAR(100) NOT NULL, 
    [LastName] NVARCHAR(100) NOT NULL, 
    [Address] NVARCHAR(500) NULL, 
    [AddedBy] INT NOT NULL, 
    [LastUpdatedBy] INT NULL, 
    [AddedDate] DATETIME NOT NULL, 
    [LastUpdateDate] DATETIME NULL, 
    [Age] INT NOT NULL, 
    [Identifier] VARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_Patients] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Patients_Hospitals] FOREIGN KEY ([HospitalId]) REFERENCES [Hospitals]([Id]), 
    CONSTRAINT [FK_Patients_AddedUser] FOREIGN KEY ([AddedBy]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_Patients_UpdatedUser] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [Users]([Id]) 
)
