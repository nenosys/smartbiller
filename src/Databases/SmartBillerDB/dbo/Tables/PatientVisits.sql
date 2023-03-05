CREATE TABLE [dbo].[PatientVisits]
(
	[Id] INT NOT NULL Identity(1,1), 
    [PatientId] INT NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NULL, 
    [Settled] BIT NOT NULL, 
	[AddedBy] INT NOT NULL, 
    [LastUpdatedBy] INT NULL, 
    [AddedDate] DATETIME NOT NULL, 
    [LastUpdateDate] DATETIME NULL, 
    CONSTRAINT [PK_PatientVisits] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_PatientVisits_AddedUser] FOREIGN KEY ([AddedBy]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_PatientVisits_UpdatedUser] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_PatientVisits_Patients] FOREIGN KEY ([PatientId]) REFERENCES [Patients]([Id]) on delete cascade 
)
