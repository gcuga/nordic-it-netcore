using System;
using Quartz;
using Quartz.Impl;


namespace ConsoleApp1
{
    class MySchedule
    {
        private string group;
        private string job;
        private string triggerName;
        private int triggerSeq;

        private ISchedulerFactory schedFact;
        private IScheduler sched;
//        private IJobDetail jobDetail;

        public MySchedule()
        {
            this.group = "group1";
            this.job = "job1";
            this.triggerName = "trigger";
            this.triggerSeq = 0;

            // construct a scheduler factory
            this.schedFact = new StdSchedulerFactory();

            // get a scheduler, start the schedular before triggers or anything else
            this.sched = schedFact.GetScheduler().GetAwaiter().GetResult();
            sched.Start();

            // create job
//            this.jobDetail = JobBuilder.Create<SimpleJob>()
//                    .WithIdentity(this.job, this.group)
//                    .Build();
        }

        public void CreateTrigger(DateTimeOffset executionDateTime)
        {
            this.triggerSeq++;

            // Несколько триггеров к одному заданию почему то прикрепить не получилось
            // никаких ошибок, триггреры создаются, но срабатывает только первый
            IJobDetail jobDetail;
            jobDetail = JobBuilder.Create<SimpleJob>()
                .WithIdentity(this.job + this.triggerSeq, this.group)
                .Build();

            // create trigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(this.triggerName + this.triggerSeq, this.group)
                .StartAt(executionDateTime).WithSimpleSchedule(x => x.WithRepeatCount(0))
                .Build();

            // Schedule the job using the job and trigger 
//            sched.ScheduleJob(this.jobDetail, trigger);
            sched.ScheduleJob(jobDetail, trigger);

            //Console.WriteLine(trigger.ToString());
        }
    }
}
