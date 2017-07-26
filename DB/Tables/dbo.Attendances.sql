CREATE TABLE [dbo].[Attendances]
(
[GigId] [int] NOT NULL,
[AttendeeId] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Attendances] ADD CONSTRAINT [PK_dbo.Attendances] PRIMARY KEY CLUSTERED  ([GigId], [AttendeeId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_AttendeeId] ON [dbo].[Attendances] ([AttendeeId]) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_GigId] ON [dbo].[Attendances] ([GigId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Attendances] ADD CONSTRAINT [FK_dbo.Attendances_dbo.AspNetUsers_AttendeeId] FOREIGN KEY ([AttendeeId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Attendances] ADD CONSTRAINT [FK_dbo.Attendances_dbo.Gigs_GigId] FOREIGN KEY ([GigId]) REFERENCES [dbo].[Gigs] ([Id])
GO
