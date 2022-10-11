CREATE TABLE [dbo].[Products] (
    [ProductId]   INT            IDENTITY (1, 1) NOT NULL,
    [DateCreated] DATETIME2 (7)  NOT NULL,
    [CreatedBy]   INT            NOT NULL,
    [DateUpdated] DATETIME2 (7)  NULL,
    [UpdatedBy]   INT            NULL,
    [isDeleted]   BIT            NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Price]       INT            NOT NULL,
    [Quantity]    INT            NOT NULL,
    [ImageUrl]    NVARCHAR (MAX) NOT NULL,
    [Discount] INT NULL, 
    [CategoryName] NCHAR(10) NULL, 
    [isNew] BIT NULL, 
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

