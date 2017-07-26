CREATE TABLE [dbo].[Gigs]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[DateTime] [datetime] NOT NULL,
[Venue] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ArtistId] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[GenreId] [tinyint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Gigs] ADD CONSTRAINT [PK_dbo.Gigs] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_ArtistId] ON [dbo].[Gigs] ([ArtistId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_GenreId] ON [dbo].[Gigs] ([GenreId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Gigs] ADD CONSTRAINT [FK_dbo.Gigs_dbo.AspNetUsers_Artist_Id] FOREIGN KEY ([ArtistId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Gigs] ADD CONSTRAINT [FK_dbo.Gigs_dbo.Genres_Genre_Id] FOREIGN KEY ([GenreId]) REFERENCES [dbo].[Genres] ([Id]) ON DELETE CASCADE
GO
