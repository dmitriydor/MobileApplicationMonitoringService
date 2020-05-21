using Confluent.Kafka;
using MobileApplicationMonitoringService.Application.Options;
using System;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Kafka
{
    public class Consumer<TKey, TValue>
    {
        private readonly ConsumerConfig configuration;
        public delegate Task MessageConsumedDelegate(Message<TKey, TValue> message);

        public Consumer(KafkaOptions options)
        {
            configuration = new ConsumerConfig(options.Configuration);
        }
        public Task Consume(string topic, MessageConsumedDelegate messageConsumedDelegate)
        {
            return Task.Run(async () =>
               {
                   var consumer = new ConsumerBuilder<TKey, TValue>(configuration)
                       .SetValueDeserializer(new JsonDeserializer<TValue>())
                       .Build();
                   consumer.Subscribe(topic);

                   while(true)
                   {
                       ConsumeResult<TKey, TValue> consumed = consumer.Consume(TimeSpan.FromMilliseconds(50));
                       if (consumed != null)
                       {
                           await messageConsumedDelegate.Invoke(consumed.Message);
                           consumer.Commit(consumed);
                       }
                   }
               }
            );
        }
    }
}
