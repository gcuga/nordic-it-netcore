using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace L11HW
{
    class ReminderReflectionConsolePresenter
    {
        public static void PrintType(ReminderItem reminderItem)
        {
            Type t = reminderItem.GetType();
            Console.WriteLine(t);
            Console.WriteLine();
        }

        public static void PrintFields(ReminderItem reminderItem)
        {
            Type t = reminderItem.GetType();
            Console.WriteLine("Fields:");
            FieldInfo[] fielsdInfo = t.GetFields(BindingFlags.Instance
                    | BindingFlags.Static
                    | BindingFlags.NonPublic
                    | BindingFlags.Public);

            foreach (var item in fielsdInfo)
            {
                string tmpString = (item.IsPublic ? "public " : item.IsPrivate ? "private " : "")
                    + (item.IsStatic ? "static " : "")
                    + ((item.IsLiteral && !item.IsInitOnly) ? "const " : item.IsInitOnly ? "readonly " : "")
                    + $"{item.FieldType} {item.Name}"
                    + $" = {item.GetValue(reminderItem)}";
                tmpString = tmpString.Replace("System.", string.Empty);
                Console.WriteLine(tmpString);
            }

            Console.WriteLine();
        }

        public static void PrintProperties(ReminderItem reminderItem)
        {
            Type t = reminderItem.GetType();
            Console.WriteLine("Properties:");
            PropertyInfo[] propertyInfos = t.GetProperties(BindingFlags.Instance
                    | BindingFlags.Static
                    | BindingFlags.NonPublic
                    | BindingFlags.Public);

            foreach (var item in propertyInfos)
            {
                string tmpString = string.Empty;
                if (item.GetGetMethod() != null)
                {
                    if (item.GetGetMethod().IsPublic) { tmpString += "public"; }
                    if (item.GetGetMethod().IsStatic) { tmpString += " static"; }
                }

                tmpString += $" {item.PropertyType}";
                if (!item.CanWrite)
                {
                    tmpString += " readonly";
                }

                tmpString += $" {item.Name}";
                if (item.GetSetMethod() != null && item.GetSetMethod().ToString() != string.Empty)
                {
                    tmpString += $" {item.GetSetMethod()}";
                }

                tmpString += $" = {item.GetValue(reminderItem)}";
                tmpString = tmpString.Replace("System.", string.Empty);
                Console.WriteLine(tmpString);
            }

            Console.WriteLine();
        }

        public static void PrintPropertiesValues(ReminderItem reminderItem)
        {
            DateTimeOffset dateTimeOffsetNow = DateTimeOffset.Now;
            Console.WriteLine("Now: {0}", dateTimeOffsetNow);

            Type t = reminderItem.GetType();
            StringBuilder sb = new StringBuilder();
            string propertyName;

            propertyName = "ReminderNumber";
            int reminderNumber = (int)t.GetProperty(propertyName).GetValue(reminderItem);
            sb.AppendLine($"{propertyName} : {reminderNumber}");

            propertyName = "AlarmDate";
            DateTimeOffset alarmDate = (DateTimeOffset)t.GetProperty(propertyName).GetValue(reminderItem);
            sb.AppendLine($"{propertyName} : {alarmDate}");

            propertyName = "AlarmMessage";
            string alarmMessage = (string)t.GetProperty(propertyName).GetValue(reminderItem);
            alarmMessage = alarmMessage.Replace("Reminder", "Reflexed reminder");
            t.GetProperty(propertyName).SetValue(reminderItem, alarmMessage);
            alarmMessage = (string)t.GetProperty(propertyName).GetValue(reminderItem);
            sb.AppendLine($"{propertyName} : {alarmMessage}");

            TimeSpan timeToAlarm = alarmDate - dateTimeOffsetNow;
            bool isOutdated = timeToAlarm.TotalMilliseconds < 0;

            sb.Append("True TimeToAlarm :");
            if (isOutdated) { sb.Append(" is outdated for"); }
            if (Math.Abs(timeToAlarm.TotalMilliseconds) > 1000)
            {
                if (timeToAlarm.Days != 0) { sb.Append($" {Math.Abs(timeToAlarm.Days)} days"); }
                if (timeToAlarm.Hours != 0) { sb.Append($" {Math.Abs(timeToAlarm.Hours)} hours"); }
                if (timeToAlarm.Seconds != 0) { sb.Append($" {Math.Abs(timeToAlarm.Seconds)} seconds"); }
            }
            else
            {
                sb.Append($" {Math.Abs(timeToAlarm.Ticks)} ticks");
            }
            sb.AppendLine();
            sb.Append($"True IsOutdated : {isOutdated}");
            Console.WriteLine(sb.ToString());
        }
    }
}
