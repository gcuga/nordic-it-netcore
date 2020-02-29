using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace L17CW
{
    class Worker
    {
        public event Action<int, WorkType> OnWorkComplete;
        public event Action<int, int, WorkType> OnWorkHourPassed;

        public void DoWork(int hours, WorkType workType)
        {
            Console.WriteLine($"[{nameof(DoWork)}] Work of type {workType} : {hours} hour");
            for (int i = 0; i < hours; i++)
            {
               
                OnWorkHourPassed?.Invoke(hours, i ,WorkType.Work);
            }

            if (OnWorkComplete != null)
            {
                OnWorkComplete(hours, workType);
            }
        }
    }
}
