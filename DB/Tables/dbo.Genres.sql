CREATE TABLE [dbo].[Genres]
(
[Id] [tinyint] NOT NULL,
[Name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Genres] ADD CONSTRAINT [PK_dbo.Genres] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
