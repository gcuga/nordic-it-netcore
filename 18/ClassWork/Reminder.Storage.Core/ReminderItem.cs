﻿using System;
using System.Text;

namespace Reminder.Storage.Core
{
    public enum ReminderItemStatus
    {
        Awaiting,
        ReadyToSend,
        SuccessfullySend,
        Failed
    }

    public class ReminderItem
    {
        private static int _reminderCount = 0;
        private const string _defaultAlarmMessage = "Reminder";

        public Guid Id { get; }
        public string ContactId { get; set; }
        private string _alarmMessage;
        public static int ReminderCount { get => _reminderCount; }
        public int ReminderNumber { get; }
        public ReminderItemStatus Status { get; set; } = ReminderItemStatus.Awaiting;

        public bool IsTimeToSend => TimeToAlarm <= TimeSpan.Zero;

        public TimeSpan TimeToAlarm
        {
            get
            {
                return AlarmDate - DateTimeOffset.Now;
            }
        }
        public bool IsOutdated
        {
            get
            {
                return TimeToAlarm.TotalMilliseconds < 0;
            }
        }

        public DateTimeOffset AlarmDate { get; set; }
        public string AlarmMessage
        {
            get
            {
                return _alarmMessage;
            }
            set
            {
                _alarmMessage = value ?? GetDefaultAlarmMessage();
            }
        }

        public ReminderItem(Guid id, DateTimeOffset alarmDate, string alarmMessage = null)
        {
            Id = id;
            _reminderCount++;
            ReminderNumber = _reminderCount;
            AlarmDate = alarmDate;
            AlarmMessage = alarmMessage;
        }

        public string GetDefaultAlarmMessage()
        {
            return $"{_defaultAlarmMessage} {ReminderNumber}";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{nameof(ReminderNumber)} : {ReminderNumber}");
            sb.AppendLine($"{nameof(Id)} : {Id}");
            sb.AppendLine($"{nameof(ContactId)} : {ContactId}");
            sb.AppendLine($"{nameof(AlarmDate)} : {AlarmDate}");
            sb.AppendLine($"{nameof(AlarmMessage)} : {AlarmMessage}");
            sb.Append($"{nameof(TimeToAlarm)} :");
            if (IsOutdated) { sb.Append(" is outdated for"); }
            if (Math.Abs(TimeToAlarm.TotalMilliseconds) > 1000)
            {
                if (TimeToAlarm.Days != 0) { sb.Append($" {Math.Abs(TimeToAlarm.Days)} days"); }
                if (TimeToAlarm.Hours != 0) { sb.Append($" {Math.Abs(TimeToAlarm.Hours)} hours"); }
                if (TimeToAlarm.Seconds != 0) { sb.Append($" {Math.Abs(TimeToAlarm.Seconds)} seconds"); }
            }
            else
            {
                sb.Append($" {Math.Abs(TimeToAlarm.Ticks)} ticks");
            }
            sb.AppendLine();
            sb.Append($"{nameof(IsOutdated)} : {IsOutdated}");
            return sb.ToString();
        }

        public void WriteProperties()
        {
            Console.WriteLine($"Type - {GetType().Name}\n" + ToString());
        }
    }
}
