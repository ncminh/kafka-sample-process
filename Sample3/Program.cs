using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample3.Interfaces;

namespace Sample3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Using DI to inject neccessary classes to the program.

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<IHttpClientFaker, HttpClientFaker>();
                    services.AddTransient<IKafkaService, KafkaService>();

                    /// Register the background service ( KafkaWorker )
                    services.AddHostedService<KafkaConsumerWorker>();
                })
                .Build();

            host.Run();
        }
    }
}