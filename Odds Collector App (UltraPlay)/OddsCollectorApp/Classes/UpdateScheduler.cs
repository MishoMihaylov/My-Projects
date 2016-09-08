using Quartz;
using Quartz.Impl;

namespace OddsCollectorApp.Classes
{
    public class UpdateScheduler
    {
        public static void Start()
        {
            IScheduler dbUpdateScheduler = StdSchedulerFactory.GetDefaultScheduler();

            dbUpdateScheduler.Start();

            IJobDetail dbUpdate = JobBuilder.Create<BackgroundJob>().Build();

            ITrigger dbUpdateTrigger = TriggerBuilder.Create()
                 .WithIdentity("BackgroundJob", "Background")
                 .StartNow()
                 .WithSimpleSchedule(s => s
                       .WithIntervalInSeconds(60)
                       .RepeatForever())
                 .Build();

            dbUpdateScheduler.ScheduleJob(dbUpdate, dbUpdateTrigger);
        }
    }
}