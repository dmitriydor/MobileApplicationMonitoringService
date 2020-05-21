using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;

namespace NotificationService
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        private static IConfigurationRoot Configure()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration;
        }
    }
}
