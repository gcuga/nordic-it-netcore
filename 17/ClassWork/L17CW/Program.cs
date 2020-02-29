using System;
using System.IO;

namespace L17CW
{
    delegate void WorkPerformedEventHandler(int hours, WorkType workType);
    

    class Program
    {
        static void Main(string[] args)
        {
            //WorkPerformedEventHandler wpeh = WorkPerformed1;
            //wpeh += WorkPerformed2;
            //wpeh(10, 0);
            //wpeh -= WorkPerformed1;
            //wpeh -= WorkPerformed1;
            //wpeh -= WorkPerformed1;
            //wpeh -= WorkPerformed3;
            //wpeh(5, WorkType.DoNothing);

            Worker worker = new Worker();
            worker.OnWorkComplete += (int hours, WorkType workType) =>
            {
                Console.WriteLine("Complete");
            };

            worker.OnWorkHourPassed += (int totalHours, int hoursPassed, WorkType workType) =>
            {
                Console.WriteLine($"{workType} {hoursPassed} {totalHours}");
            };

            worker.DoWork(3, WorkType.Work);
            Console.WriteLine("==============================================");
            worker.DoWork(2, WorkType.DoNothing);

            Console.WriteLine("==============================================");
            Console.WriteLine();

            var generator = new RandomDataGenerator();
            generator.RandomDataGenerated += (int bytesDone, int totalBytes) =>
            {
                Console.WriteLine($"Generated {bytesDone} from {totalBytes} byte(s)...");
            };

            generator.RandomDataGenerationDone += (object sender, EventArgs e) =>
            {
                Console.WriteLine($"{sender.GetHashCode()} : I'm done!");
            };

            byte[] data = generator.GetRandomData(1024 * 1024, 1024 * 256);

            const string sourceFileName = @"\Zip\test_bytes.txt";
            const string zipFileName = @"\Zip\test_bytes.zip";


            File.WriteAllBytes(@"\Zip\test_bytes.txt", data);



        }

        private static void Generator_RandomDataGenerated(int bytesDone, int totalBytes)
        {
            throw new NotImplementedException();
        }

        private static void Worker_OnWorkHourPassed(int arg1, int arg2, WorkType arg3)
        {
            throw new NotImplementedException();
        }

        private static void WorkPerformed1(int hours, WorkType workType)
        {
            Console.WriteLine($"[{nameof(WorkPerformed1)}] Work of type {workType} : {hours} hours");
        }

        private static void WorkPerformed2(int hours, WorkType workType)
        {
            Console.WriteLine($"[{nameof(WorkPerformed2)}] Work of type {workType} : {hours} hours");
        }

        private static void WorkPerformed3(int hours, WorkType workType)
        {
            Console.WriteLine($"[{nameof(WorkPerformed3)}] Work of type {workType} : {hours} hours");
        }
    }

    public enum WorkType
    {
        Work,
        DoNothing
    }
}
