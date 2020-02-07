using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzExample.Jobs.SampleJob
{
    class SampleJob : IJob
    {
        public static bool _IsRunning = false;

        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => ExecuteService());
        }

        public void ExecuteService()
        {
            try
            {
                if (_IsRunning)
                    return;

                _IsRunning = true;

                StartService();

                _IsRunning = false;

                Console.WriteLine("Sample service finished");
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Sample job error: {0}", ex));
            }            
        }

        public void StartService()
        {
            Console.WriteLine("Sample service started");
        }
    }
}
