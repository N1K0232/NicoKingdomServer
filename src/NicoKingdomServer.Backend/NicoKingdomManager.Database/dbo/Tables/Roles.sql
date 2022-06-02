CREATE TABLE [dbo].[Roles] (
    [Id]               UNIQUEIDENTIFIER CONSTRAINT [DF_Roles_Id] DEFAULT (newid()) NOT NULL,
    [Name]             NVARCHAR (100)   NOT NULL,
    [Color]            NVARCHAR (10)    NOT NULL,
    [CreatedDate]      DATE         NOT NULL,
    [LastModifiedDate] DATE         NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

