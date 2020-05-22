using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using MobileApplicationMonitoringService.Application.Kafka;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Options;
using NotificationService.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NotificationService
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = Configure();
            var consumer = new Consumer<Null, Message[]>(configuration.GetSection("Kafka").Get<KafkaOptions>());
            var ts = consumer.Consume("notification", messages =>
            {
                EmailOptions options = configuration.GetSection("EmailOptions").Get<EmailOptions>();

                foreach(var item in messages.Value)
                {
                    using (MailMessage email = new MailMessage())
                    {
                        email.From = new MailAddress(options.UserName);
                        email.To.Add("something@mail.ru");
                        email.Subject = "Notification";
                        email.Body = $"Node: {item.UserName}\n" +
                        $"Application Version: {item.AppVersion}\n" +
                        $"Event Name: {item.EventName}\n" +
                        $"Event Description: {item.EventName}\n" +
                        $"Date: {item.Date}";
                        using(SmtpClient smtpClient = new SmtpClient(options.SmtpHost, options.SmtpPort))
                        {
                            smtpClient.Credentials = new NetworkCredential(options.UserName, options.Password);
                            smtpClient.EnableSsl = options.SmtpEnableSsl;
                            smtpClient.Send(email);
                        }
                    }
                }
            });
            Task.WaitAny(ts);
        }
        public static IConfigurationRoot Configure()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration;
        }
    }
}
