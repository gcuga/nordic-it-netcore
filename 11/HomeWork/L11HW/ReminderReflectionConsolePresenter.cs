using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace L11HW
{
    class ReminderReflectionConsolePresenter
    {
        public static void PrintMembers(ReminderItem reminderItem)
        {
            Type t = reminderItem.GetType();
            Console.WriteLine(t);
            FieldInfo[] fielsdInfo = t.GetFields(BindingFlags.Instance
                    | BindingFlags.Static
                    | BindingFlags.Public
                    | BindingFlags.NonPublic);

            foreach (var item in fielsdInfo)
            {
                string tmpString = (item.IsPublic ? "public " : item.IsPrivate ? "private " : "")
                    + (item.IsStatic ? "static " : "")
                    + ((item.IsLiteral && !item.IsInitOnly) ? "const " : item.IsInitOnly ? "readonly " : "")
                    + $"{item.FieldType} {item.Name}";
                tmpString = tmpString.Replace("System.", string.Empty);
                Console.WriteLine(tmpString);
            }

            Console.WriteLine();




        }
    }
}
