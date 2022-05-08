CREATE TABLE [dbo].[users] (
    [id]   INT          NOT NULL,
    [name] VARCHAR (50) NOT NULL,
    [signed_in] INT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([id] ASC)
);

