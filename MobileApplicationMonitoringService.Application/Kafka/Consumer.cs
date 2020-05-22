using Confluent.Kafka;
using MobileApplicationMonitoringService.Application.Options;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace MobileApplicationMonitoringService.Application.Kafka
{
    public class Consumer<TKey, TValue>
    {
        private readonly ConsumerConfig configuration;
        

        public Consumer(KafkaOptions options)
        {
            configuration = new ConsumerConfig(options.Configuration);
        }
        public Task Consume(string topic, Action<Message<TKey,TValue>> messageConsumedDelegate)
        {
            return Task.Run(() =>
               {
                   var consumer = new ConsumerBuilder<TKey, TValue>(configuration)
                       .SetValueDeserializer(new JsonDeserializer<TValue>())
                       .Build();
                   consumer.Subscribe(topic);

                   while(true)
                   {
                       ConsumeResult<TKey, TValue> consumed = consumer.Consume();
                       if (consumed != null)
                       {
                           messageConsumedDelegate.Invoke(consumed.Message);
                           consumer.Commit(consumed);
                       }
                   }
               }
            );
        }
    }
}
