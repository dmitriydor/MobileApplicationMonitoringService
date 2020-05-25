using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MobileApplicationMonitoringService.Application.Kafka;
using MobileApplicationMonitoringService.Application.Models;
using MobileApplicationMonitoringService.Application.Options;
using NotificationService.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationService
{
    public class NotificationSandingService : IHostedService
    {
        private readonly IOptions<KafkaOptions> kafkaOptions;
        private readonly IOptions<EmailOptions> emailOptions;
        public NotificationSandingService( IOptions<KafkaOptions> kafkaOptions, IOptions<EmailOptions> emailOptions)
        {
            this.kafkaOptions = kafkaOptions;
            this.emailOptions = emailOptions;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new Consumer<Null, Message[]>(kafkaOptions.Value);
            var ts = consumer.Consume("notification", messages =>
            {
                EmailOptions options = emailOptions.Value;

                foreach (var item in messages.Value)
                {
                    try
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
                            using (SmtpClient smtpClient = new SmtpClient(options.SmtpHost, options.SmtpPort))
                            {
                                smtpClient.Credentials = new NetworkCredential(options.UserName, options.Password);
                                smtpClient.EnableSsl = options.SmtpEnableSsl;
                                smtpClient.Send(email);
                            }
                        }
                    } 
                    catch(SmtpFailedRecipientsException smptException)
                    {
                        Console.WriteLine(smptException.Message);
                    } 
                    catch(SmtpException smtpExeption)
                    {
                        Console.WriteLine(smtpExeption.Message);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            });
            Task.WaitAny(ts);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
