using System;
using System.Globalization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            int choice;
            MySchedule schedule = new MySchedule();

            PrintInstructions();
            while (!quit) {
                Console.WriteLine("Enter your choice: ");
                string readLine = Console.ReadLine();
                if (!int.TryParse(readLine, out choice)) {
                    choice = -1;
                }

                switch (choice) {
                    case 0:
                        quit = true;
                        break;
                    case 1:
                        PrintInstructions();
                        break;
                    case 2:
                        AddExecutionTime(schedule);
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        PrintInstructions();
                        break;
                }
            }
        }

        public static void PrintInstructions() {
            Console.WriteLine("\nPress");
            Console.WriteLine("\t0 - To quit the application.");
            Console.WriteLine("\t1 - To print instructions.");
            Console.WriteLine("\t2 - To add the execution time of the job.");
        }

        public static void AddExecutionTime(MySchedule schedule) {
            Console.WriteLine("Enter the execution time (for example " +
                    DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ") :");
            string readLine = Console.ReadLine();

            DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            dtfi.ShortDatePattern = "dd.MM.yyyy";
            dtfi.LongDatePattern = "dd MMMM yyyy";
            dtfi.ShortTimePattern = "HH:mm";
            dtfi.LongTimePattern = "HH:mm:ss";
            dtfi.FullDateTimePattern = "dd.MM.yyyy HH:mm:ss";

            DateTimeOffset executionTime;
            if (DateTimeOffset.TryParse(readLine, dtfi, DateTimeStyles.None, out executionTime)) {
                schedule.CreateTrigger(executionTime);
                Console.WriteLine("The job's execution time added\n");
            }
            else {
                Console.WriteLine("Date format error\n");
            }
        }
    }

}
