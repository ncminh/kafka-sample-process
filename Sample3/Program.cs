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

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<IHttpClientFaker, HttpClientFaker>();
                    services.AddTransient<IKafkaService, KafkaService>();

                    services.AddHostedService<KafkaConsumerWorker>();
                })
                .Build();

            host.Run();
        }
    }
}