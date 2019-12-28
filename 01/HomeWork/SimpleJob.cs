using System;
using Quartz;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SimpleJob : IJob
    {
        async Task IJob.Execute(IJobExecutionContext context)
        {
            Console.WriteLine("\n================================\n" +
                    "Job executed " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "\n" +
                    "================================\n");
            //throw new NotImplementedException();
        }
    }
}
