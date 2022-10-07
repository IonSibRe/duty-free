CREATE TABLE [dbo].[Users] (
    [UserId]   INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [Email]    NVARCHAR (MAX) NOT NULL,
    [ImageUrl] NVARCHAR (MAX) NOT NULL,
    [Role]     INT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

