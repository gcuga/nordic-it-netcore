﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Reminder.Storage.SqlServer.ADO.Tests.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Reminder.Storage.SqlServer.ADO.Tests.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на -- drop shemabinding view
        ///DROP VIEW IF EXISTS [dbo].[VW_ReminderItem]
        ///GO
        ///
        ///-- drop foreign key
        ///IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N&apos;[dbo].[ReminderItem]&apos;) AND type in (N&apos;U&apos;))
        ///ALTER TABLE [dbo].[ReminderItem] DROP CONSTRAINT IF EXISTS [FK_ReminderItem_StatusId]
        ///GO
        ///--
        ///-- (Re-)create [dbo].[ReminderItem]
        ///IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N&apos;[dbo].[ReminderItem]&apos;) AND type in (N&apos;U&apos;))
        ///DROP TABLE [dbo].[ReminderItem]
        ///GO
        ///CREATE TABLE [dbo].[R [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string DatabaseSchema {
            get {
                return ResourceManager.GetString("DatabaseSchema", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на DROP PROCEDURE IF EXISTS [dbo].[AddReminderItem]
        ///GO
        ///CREATE PROCEDURE [dbo].[AddReminderItem] (
        ///	@contactId AS VARCHAR(50),
        ///	@targetDate AS DATETIMEOFFSET,
        ///	@message AS VARCHAR(200),
        ///	@status AS TINYINT
        ///)
        ///AS BEGIN
        ///	SET NOCOUNT ON
        ///
        ///	DECLARE
        ///		@now AS DATETIMEOFFSET,
        ///		@tempReminderId AS UNIQUEIDENTIFIER
        ///	
        ///	SELECT 
        ///		@now = SYSDATETIMEOFFSET(),
        ///		@tempReminderId = NEWID(); 
        ///
        ///	INSERT INTO [dbo].[ReminderItem](
        ///		[Id],
        ///		[ContactId],
        ///		[TargetDate],
        ///		[Message],
        ///		[StatusId],
        ///		[CreatedD [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string DatabaseSPs {
            get {
                return ResourceManager.GetString("DatabaseSPs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на CREATE OR ALTER VIEW [dbo].[VW_ReminderItem]
        ///      ([Id],[ContactId],[TargetDate],[Message],[StatusId],[CreatedDate],[UpdatedDate])
        ///WITH SCHEMABINDING
        ///AS
        ///SELECT [Id],[ContactId],[TargetDate],[Message],[StatusId],[CreatedDate],[UpdatedDate]
        ///  FROM [dbo].[ReminderItem]
        ///GO
        ///
        ///CREATE OR ALTER TRIGGER [dbo].[VW_ReminderItem_TRIOI]
        ///   ON  [dbo].[VW_ReminderItem]
        ///   INSTEAD OF INSERT
        ///AS 
        ///BEGIN
        ///	SET NOCOUNT ON;
        ///
        ///    if (select top 1 [Id] from inserted where [Id] is not null) is not null
        ///        THROW  [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string DatabaseVWs {
            get {
                return ResourceManager.GetString("DatabaseVWs", resourceCulture);
            }
        }
    }
}
