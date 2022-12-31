using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample3.Interfaces
{
    public interface IKafkaService
    {
        /// <summary>
        /// Consume Kafka message
        /// </summary>
        /// <param name="topic"></param>
        void Consume(string topic);

        /// <summary>
        /// Produce Kafka message
        /// </summary>
        /// <param name="topic"></param>
        void Produce(string topic, Dictionary<string, object> data);
    }
}
