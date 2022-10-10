CREATE TABLE [dbo].[Orders] (
    [OrderId]     INT            IDENTITY (1, 1) NOT NULL,
    [DateCreated] DATETIME       NOT NULL,
    [Name]        NVARCHAR (512) NOT NULL,
    [Price]       INT            NOT NULL,
    [UsedId]      INT            NULL,
    [ProductId]   INT            NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC),
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]),
    FOREIGN KEY ([UsedId]) REFERENCES [dbo].[Users] ([UserId])
);

