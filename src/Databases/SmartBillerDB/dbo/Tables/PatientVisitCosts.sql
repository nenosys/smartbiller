CREATE TABLE [dbo].[PatientVisitCosts]
(
	[Id] INT NOT NULL identity(1,1), 
	[PatientVisitId] INT NOT NULL, 
	[BillingCategoryId] INT NOT NULL, 
    [Quantity] INT NOT NULL, 
    [StartDate] DATETIME NULL, 
    [EndDate] DATETIME NULL, 
    [ChargableUnits] INT NOT NULL, 
    CONSTRAINT [PK_PatientVisitCosts] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_PatientVisitCosts_PatientVisits] FOREIGN KEY ([PatientVisitId]) REFERENCES [PatientVisits]([Id]) on delete cascade, 
    CONSTRAINT [FK_PatientVisitCosts_BillingCategories] FOREIGN KEY ([BillingCategoryId]) REFERENCES [BillingCategories]([Id]) 
)
