CREATE TABLE [dbo].[SignupItems] (
    -- This must be a string suitable for a GUID
    [Id]            NVARCHAR (128)     NOT NULL,
 
    -- These are the system properties
    [Version]       ROWVERSION         NOT NULL,
    [CreatedAt]     DATETIMEOFFSET (7) NOT NULL,
    [UpdatedAt]     DATETIMEOFFSET (7) NULL,
    [Deleted]       BIT                NOT NULL,
 
    -- These are the properties of our DTO not included in EntityFramework
    [GroupCode]                NVARCHAR (10)      NOT NULL,
    [EventDate]     DATETIMEOFFSET (7) NOT NULL,
    [UserID]                NVARCHAR (MAX)     NOT NULL,
[Name]                        NVARCHAR (MAX)     NOT NULL,
[Email]                        NVARCHAR (MAX)     NOT NULL,
);
 
CREATE CLUSTERED INDEX [IX_CreatedAt]
    ON [dbo].[SignupItems]([CreatedAt] ASC);
 
ALTER TABLE [dbo].[SignupItems]
    ADD CONSTRAINT [PK_dbo.Examples] PRIMARY KEY NONCLUSTERED ([Id] ASC);
 
CREATE TRIGGER [TR_dbo_Examples_InsertUpdateDelete] ON [dbo].[SignupItems]
    AFTER INSERT, UPDATE, DELETE AS
    BEGIN
        UPDATE [dbo].[SignupItems]
            SET [dbo].[SignupItems].[UpdatedAt] = CONVERT(DATETIMEOFFSET, SYSUTCDATETIME())
            FROM INSERTED WHERE inserted.[Id] = [dbo].[SignupItems].[Id]
    END;
 
ALTER TABLE [dbo].[SignupItems]
ADD CONSTRAINT SignupItems_Default_Id DEFAULT CONVERT(NVARCHAR(128), NEWID()) FOR [Id];
 
ALTER TABLE [dbo].[SignupItems]
ADD CONSTRAINT Signups_Default_CreatedAt DEFAULT CONVERT(DATETIMEOFFSET, SYSUTCDATETIME()) FOR [CreatedAt];
