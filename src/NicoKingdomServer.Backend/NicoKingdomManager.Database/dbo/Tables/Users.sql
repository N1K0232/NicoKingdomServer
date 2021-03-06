CREATE TABLE [dbo].[Users] (
    [Id]               UNIQUEIDENTIFIER CONSTRAINT [DF_Users_Id] DEFAULT (newid()) NOT NULL,
    [RoleId]           UNIQUEIDENTIFIER NOT NULL,
    [UserName]         NVARCHAR (100)   NOT NULL,
    [NickName]         NVARCHAR (100)   NOT NULL,
    [CreatedDate]      DATETIME         NOT NULL,
    [LastModifiedDate] DATETIME         NULL,

    FOREIGN KEY(RoleId) REFERENCES Roles(Id),
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);