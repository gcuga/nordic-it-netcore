CREATE OR ALTER VIEW [dbo].[VW_ReminderItem]
      ([Id],[ContactId],[TargetDate],[Message],[StatusId],[CreatedDate],[UpdatedDate])
WITH SCHEMABINDING
AS
SELECT [Id],[ContactId],[TargetDate],[Message],[StatusId],[CreatedDate],[UpdatedDate]
  FROM [dbo].[ReminderItem]
GO

CREATE OR ALTER TRIGGER [dbo].[VW_ReminderItem_TRIOI]
   ON  [dbo].[VW_ReminderItem]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

    if (select top 1 [Id] from inserted where [Id] is not null) is not null
        THROW 51001, 'Insert statement mustn''t have Id', 1;

	DECLARE @now AS DATETIMEOFFSET = SYSDATETIMEOFFSET();

	INSERT INTO [dbo].[ReminderItem](
		[Id],
		[ContactId],
		[TargetDate],
		[Message],
		[StatusId],
		[CreatedDate],
		[UpdatedDate])
    SELECT NEWID()
         , i.[ContactId]
         , i.[TargetDate]
         , i.[Message]
         , i.[StatusId]
         , @now
         , @now
      FROM inserted i;
END
GO

CREATE OR ALTER TRIGGER [dbo].[VW_ReminderItem_TRIOU]
   ON  [dbo].[VW_ReminderItem]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	DECLARE @now AS DATETIMEOFFSET = SYSDATETIMEOFFSET();

	UPDATE r
       SET r.[ContactId] = i.[ContactId]
         , r.[TargetDate] = i.[TargetDate]
         , r.[Message] = i.[Message]
         , r.[StatusId] = i.[StatusId]
         , r.[UpdatedDate] = @now
      FROM [dbo].[ReminderItem] r inner join inserted i on i.Id = r.Id
END
GO

CREATE OR ALTER TRIGGER [dbo].[VW_ReminderItem_TRIOD]
   ON  [dbo].[VW_ReminderItem]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE r FROM [dbo].[ReminderItem] r inner join deleted d on d.Id = r.Id
END
GO
