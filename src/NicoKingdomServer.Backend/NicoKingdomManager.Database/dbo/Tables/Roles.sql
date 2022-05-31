CREATE TABLE [dbo].[Roles] (
    [Id]               UNIQUEIDENTIFIER CONSTRAINT [DF_Roles_Id] DEFAULT (newid()) NOT NULL,
    [Name]             NVARCHAR (100)   NOT NULL,
    [Color]            NVARCHAR (10)    NOT NULL,
    [CreatedDate]      DATETIME         NOT NULL,
    [LastModifiedDate] DATETIME         NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

