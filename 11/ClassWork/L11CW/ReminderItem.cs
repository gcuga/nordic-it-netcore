using System;
using System.Collections.Generic;
using System.Text;

namespace L11CW
{
    internal class ReminderItem
    {
        private const string _defaultAlarmMessage = "Alarm";
        private string _alarmMessage;
        public DateTimeOffset AlarmDate { get; set; }
        public string AlarmMessage 
        {   
            get
            {
                return _alarmMessage;
            }
            set
            {
                _alarmMessage = value ?? _defaultAlarmMessage;
            }
        }
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

        public ReminderItem(DateTimeOffset alarmDate, string alarmMessage = _defaultAlarmMessage)
        {
            AlarmDate = alarmDate;
            AlarmMessage = alarmMessage ?? _defaultAlarmMessage;
        }

        public void WriteProperties()
        {
            Console.WriteLine($"{nameof(AlarmDate)} : {AlarmDate}" +
                $"\n{nameof(AlarmMessage)} : {AlarmMessage}" +
                $"\n{nameof(TimeToAlarm)} : {(int)TimeToAlarm.TotalMinutes} мин." +
                $"\n{nameof(IsOutdated)} : {IsOutdated}");
        }

    }
}
