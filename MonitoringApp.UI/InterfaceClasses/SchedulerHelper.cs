using Quartz.Impl;
using Quartz;

namespace MonitoringApp.UI.InterfaceClasses
{
    public static class SchedulerHelper
    {
        public static async void SchedulerSetup(int intervalTime)
        {
            var _scheduler = await new StdSchedulerFactory().GetScheduler();
            await _scheduler.Start();

            var appControlJob = JobBuilder.Create<AppControl>()
                .WithIdentity("AppControl")
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("AppControl")
                .StartNow()
                .WithSimpleSchedule(builder => builder.WithIntervalInSeconds(intervalTime).RepeatForever()) //.WithCronSchedule("*/1 * * * *")
                .Build();

            var jobIsExists = await _scheduler.CheckExists(appControlJob.Key);
            if (!jobIsExists)
            {
                var result = await _scheduler.ScheduleJob(appControlJob, trigger);

            }
        }
    }
}
