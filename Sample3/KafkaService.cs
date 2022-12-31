using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sample3.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sample3
{
    public class KafkaService : IKafkaService
    {
        readonly ClientConfig _kConfig;
        readonly ILogger<KafkaService> _logger;
        readonly IHttpClientFaker _clientFaker;

        public KafkaService(ILogger<KafkaService> logger, IHttpClientFaker clientFaker)
        {
            _logger = logger;
            _clientFaker = clientFaker;

            _kConfig = new ClientConfig
            {
                BootstrapServers = "localhost:9092"
            };
        }

        public void Consume(string topic)
        {
            var consumerCfg = new ConsumerConfig(_kConfig)
            {
                GroupId = "sample3-group-1",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            CancellationTokenSource cancelTokenSrc = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancelTokenSrc.Cancel();
            };

            using (var consumer = new ConsumerBuilder<string, string>(consumerCfg).Build())
            {
                consumer.Subscribe(topic);
                var data = new List<string>();
                try
                {
                    while (true)
                    {
                        var consumeResult = consumer.Consume(cancelTokenSrc.Token);
                        if (string.IsNullOrEmpty(consumeResult.Message.Key)) continue;

                        if (data.Contains(consumeResult.Message.Value)) continue;

                        data.Add(consumeResult.Message.Value);

                        var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(consumeResult.Message.Value);

                        if (result != null)
                        {
                            var clients = _clientFaker.GetClients(5);

                            foreach (var client in clients)
                            {
                                result["clientid"] = client.Id;
                                // Produce the message for each clientid
                                Produce("second_topic", result);
                            }
                        }
                    }
                }
                catch (OperationCanceledException ex)
                {
                    var msg = ex.Message;
                    // User pressed Ctrl+C to cancel.
                }
                finally
                {
                    consumer.Close();
                }
            }
        }

        public void Produce(string topic, Dictionary<string, object> data)
        {
            using (var producer = new ProducerBuilder<string, string>(_kConfig).Build())
            {
                var val = JObject.FromObject(data).ToString((Newtonsoft.Json.Formatting)Formatting.None);

                producer.Produce(topic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = val }, (deliveryReport) =>
                {
                    if (deliveryReport.Error.Code != ErrorCode.NoError)
                    {
                        _logger.LogError($"Failed to delivery message, error: {deliveryReport.Error.Reason}.");
                    }
                    else
                    {
                        _logger.LogInformation($"Produced message successfully to: {deliveryReport.TopicPartitionOffset}");
                    }
                });

                producer.Flush(TimeSpan.FromSeconds(10));
            }
        }
    }
}
