CREATE TABLE [dbo].[BillingCategories]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
	[HospitalId] int NOT NULL,
    [BillingDepartmentId] INT NOT NULL, 
    [Name] NVARCHAR(100) NULL, 
    [BillingRateTypeId] INT NOT NULL, 
    [ChargePerUnit] DECIMAL(18, 2) NULL, 
    [ChargeUpperBound] INT NULL, 
    [ChargeAfterUpperBound] DECIMAL(18, 2) NULL, 
    [Deleted] BIT NULL , 
    CONSTRAINT [FK_BillingCategories_Hospitals] FOREIGN KEY ([HospitalId]) REFERENCES [Hospitals]([Id]), 
    CONSTRAINT [FK_BillingCategories_BillingDepartments] FOREIGN KEY ([BillingDepartmentId]) REFERENCES [BillingDepartments]([Id]), 
    CONSTRAINT [FK_BillingCategories_BillingRateTypes] FOREIGN KEY ([BillingRateTypeId]) REFERENCES [BillingRateTypes]([Id])
)
