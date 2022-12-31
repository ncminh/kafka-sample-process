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
    /// <summary>
    /// This is a background service run every 20 seconds.
    /// It will try to consume the topic name first_topic.
    /// Then append a field called client id to the result and publish the message to another topic.
    /// </summary>
    public class KafkaConsumerWorker : BackgroundService
    {
        /// <summary>
        /// The logger for this background service.
        /// </summary>
        readonly ILogger<KafkaConsumerWorker> _logger;

        /// <summary>
        /// The Kafka service that will handle the task of consuming and producing message.
        /// </summary>
        readonly IKafkaService _kService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kSvc"></param>
        /// <param name="logger"></param>
        public KafkaConsumerWorker(IKafkaService kSvc, ILogger<KafkaConsumerWorker> logger)
        {
            _kService = kSvc;
            _logger = logger;
        }

        /// <summary>
        /// Execute the task every 20 seconds.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Logging message from background worker: {DateTime.Now.ToString()}");
                _kService.Consume("first_topic");
                await Task.Delay(20000, stoppingToken); // This line decide the interval of the worker process to call KafkaService.
            }            
        }
    }
}
