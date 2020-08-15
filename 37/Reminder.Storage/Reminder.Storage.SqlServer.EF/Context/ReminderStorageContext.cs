using System;
using System.Collections.Generic;
using System.Text;
using Reminder.Storage.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Reminder.Storage.SqlServer.EF.Context
{
    public class ReminderStorageContext : DbContext
    {
        public DbSet<ReminderItemDto> ReminderItems { get; set; }

        public ReminderStorageContext(DbContextOptions<ReminderStorageContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReminderItemDto>(entity =>
            {
                entity
                    .ToTable("ReminderItem")
                    .HasIndex(e => e.Status);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ContactId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TargetDate)
                    .IsRequired()
                    .HasColumnType("datetimeoffset(7)");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("StatusId")
                    .HasConversion(new EnumToNumberConverter<ReminderItemStatus, int>());

                entity.Property(e => e.CreatedDate)
                    .IsRequired()
                    .HasColumnType("datetimeoffset(7)")
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedDate)
                    .IsRequired()
                    .HasColumnType("datetimeoffset(7)")
                    .HasDefaultValueSql("SYSDATETIMEOFFSET()")
                    .ValueGeneratedOnAddOrUpdate();
            });
        }
    }
}
