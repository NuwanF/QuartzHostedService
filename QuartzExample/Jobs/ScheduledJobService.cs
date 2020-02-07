using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using QuartzExample.Helpers;
using QuartzExample.Jobs.SampleJob;
using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzExample.ScheduledJobs
{
    public class ScheduledJobService : IHostedService, IDisposable
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var SampleJobEnabled = ConfigurationHelper.Configuration["AppSettings:Quartz:Sample:SampleJobEnabled"].ToString();
            var SampleJobName = ConfigurationHelper.Configuration["AppSettings:Quartz:Sample:Job"].ToString();
            var SampleTriggerName = ConfigurationHelper.Configuration["AppSettings:Quartz:Sample:Trigger"].ToString();
            var SampleGroupName = ConfigurationHelper.Configuration["AppSettings:Quartz:Sample:Group"].ToString();
            var SampleSchedule = ConfigurationHelper.Configuration["AppSettings:Quartz:Sample:SampleJobSchedule"].ToString();

            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            if (SampleJobEnabled.ToLower() == "true")
            {

                IJobDetail job = JobBuilder.Create<SampleJob>()
                    .WithIdentity(SampleJobName, SampleGroupName)
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(SampleTriggerName, SampleGroupName)
                    .StartNow()
                    .WithCronSchedule(SampleSchedule)
                .Build();

                await sched.ScheduleJob(job, trigger);
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
