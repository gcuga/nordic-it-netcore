using System;
using System.Collections.Generic;
using System.Text;

namespace L11HW
{
    class ReminderItem
    {
        private static int _reminderCount = 0;
        private const string _defaultAlarmMessage = "Reminder";
        private string _alarmMessage;
        public static int ReminderCount { get => _reminderCount; }
        public int ReminderNumber { get; }
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

        public ReminderItem(DateTimeOffset alarmDate, string alarmMessage = null)
        {
            _reminderCount++;
            ReminderNumber = _reminderCount;
            AlarmDate = alarmDate;
            AlarmMessage = alarmMessage;
        }

        public string GetDefaultAlarmMessage()
        {
            return $"{_defaultAlarmMessage} {ReminderNumber}";
        }

        public void WriteProperties()
        {
            // С одной стороны красиво, с другой стороны пример
            // скрытых проблем, свойства IsOutdated и TimeToAlarm
            // связаны между собой, поскольку каждый раз идет обращение
            // к разным состояниям, IsOutdated, TimeToAlarm.Days,
            // TimeToAlarm.Hours, TimeToAlarm.Seconds
            // неконсистентны друг другу и если TimeToAlarm можно 
            // побороть присвоив сначала его значение локальной 
            // переменной и таким образом зафиксировав состояние,
            // внутри данной функции, то свойство IsOutdated права
            // на жизнь не имеет совсем, его состояние и состояние
            // TimeToAlarm нельзя согласовать в принципе, оба свойства
            // нарушают принцип единичной ответственности, потому что
            // для их функционирования требуется переменный внешний
            // для данного класса параметр - текущая дата и время
            // т.е. свойствами этого класса они не имеют права быть
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{nameof(ReminderNumber)} : {ReminderNumber}");
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
            Console.WriteLine(sb.ToString());
        }

    }
}
