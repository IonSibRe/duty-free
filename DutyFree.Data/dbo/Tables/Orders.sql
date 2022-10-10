CREATE TABLE [dbo].[Orders] (
    [OrderId]     INT            IDENTITY (1, 1) NOT NULL,
    [DateCreated] DATETIME       NOT NULL,
    [Name]        NVARCHAR (512) NOT NULL,
    [Price]       INT            NOT NULL,
    [UserId]      INT            NULL,
    [ProductId]   INT            NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC),
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);

