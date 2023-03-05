CREATE TABLE [dbo].[BillingDepartments]
(
	[Id] INT NOT NULL , 
	[HospitalId] int NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_BillingDepartment] PRIMARY KEY ([Id])
)
