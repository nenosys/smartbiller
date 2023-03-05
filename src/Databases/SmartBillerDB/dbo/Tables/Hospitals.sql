CREATE TABLE [dbo].[Hospitals] (
    [Id]   INT            NOT NULL Identity(1,1),
    [Name] NVARCHAR (100) NULL, 
    CONSTRAINT [PK_Hospitals] PRIMARY KEY ([Id])
);

