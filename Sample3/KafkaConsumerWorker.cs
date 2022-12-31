using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample3
{
    public class KafkaConsumerWorker : BackgroundService
    {
        readonly ILogger<KafkaConsumerWorker> _logger;
        readonly IKafkaService _kService;

        public KafkaConsumerWorker(IKafkaService kSvc, ILogger<KafkaConsumerWorker> logger)
        {
            _kService = kSvc;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Logging message from background worker: {DateTime.Now.ToString()}");
                _kService.Consume("first_topic");
                await Task.Delay(20000, stoppingToken);
            }            
        }
    }
}
