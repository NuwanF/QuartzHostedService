using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuartzExample.ScheduledJobs;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzExample
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Starting!");

            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging();
                services.AddHostedService<ScheduledJobService>();

            });

            if (isService)
            {
                await builder.RunAsServiceAsync();
            }
            else
            {
                await builder.RunConsoleAsync();
            }
        }
    }
}
