using Confluent.Kafka;
using MobileApplicationMonitoringService.Application.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Kafka
{
    public class Producer
    {
        private readonly ProducerConfig configuration;
        public Producer(KafkaOptions options)
        {
            configuration = new ProducerConfig(options.Configuration);
        }
        //для сериализации
        //protobuf.net
        //json
        public async Task Produce<T>(T message, string topic = null)
        {
            using var producer = new ProducerBuilder<Null, T>(configuration)
                .SetValueSerializer(new JsonSerializer<T>())
                .Build();
            await producer.ProduceAsync(topic, new Message<Null, T> { Value = message });
        }
    }
}
