CREATE TABLE [dbo].[GroupItems] (
    -- This must be a string suitable for a GUID
    [Id]            NVARCHAR (128)     NOT NULL,

    -- These are the system properties
    [Version]       ROWVERSION         NOT NULL,
    [CreatedAt]     DATETIMEOFFSET (7) NOT NULL,
    [UpdatedAt]     DATETIMEOFFSET (7) NULL,
    [Deleted]       BIT                NOT NULL

    -- These are the properties of our DTO not included in EntityFramework
    [StringField]   NVARCHAR (MAX)     NULL,
    [IntField]      INT                NOT NULL,
    [DoubleField]   FLOAT (53)         NOT NULL,
    [DateTimeField] DATETIMEOFFSET (7) NOT NULL,
);

CREATE CLUSTERED INDEX [IX_CreatedAt]
    ON [dbo].[GroupItems]([CreatedAt] ASC);

ALTER TABLE [dbo].[GroupItems]
    ADD CONSTRAINT [PK_dbo.Examples] PRIMARY KEY NONCLUSTERED ([Id] ASC);

CREATE TRIGGER [TR_dbo_Examples_InsertUpdateDelete] ON [dbo].[GroupItems]
    AFTER INSERT, UPDATE, DELETE AS
    BEGIN
        UPDATE [dbo].[GroupItems]
            SET [dbo].[GroupItems].[UpdatedAt] = CONVERT(DATETIMEOFFSET, SYSUTCDATETIME())
            FROM INSERTED WHERE inserted.[Id] = [dbo].[GroupItems].[Id]
    END;

ALTER TABLE [dbo].[GroupItems]
    ALTER COLUMN [Id] SET DEFAULT CONVERT(NVARCHAR(128), NEWID());

ALTER TABLE [dbo].[GroupItems]
ADD CONSTRAINT Default_Id DEFAULT CONVERT(NVARCHAR(128), NEWID()) FOR [Id];
 
ALTER TABLE [dbo].[GroupItems]
ADD CONSTRAINT Default_CreatedAt DEFAULT CONVERT(DATETIMEOFFSET, SYSUTCDATETIME()) FOR [CreatedAt];
