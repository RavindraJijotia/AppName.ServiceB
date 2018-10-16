using AppName.ServiceB.Models;
using AppName.ServiceB.Models.Configurations;
using AppName.ServiceB.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace AppName.ServiceB.Services.Implementations
{
    public class RabbitMqService: IRabbitMqService, IDisposable
    {
        private readonly IMessageValidatorService _messageValidatorService;
        private readonly RabbitMqOptions _rabbitMqOptions;
        private IModel _channel;

        public RabbitMqService(RabbitMqOptions rabbitMqOptions, IMessageValidatorService messageValidatorService)
        {
            _rabbitMqOptions = rabbitMqOptions;
            _messageValidatorService = messageValidatorService;
        }

        public void CreateConnection()
        {
            var connectionFactory =
                new ConnectionFactory { HostName = _rabbitMqOptions.HostName };
            var connection = connectionFactory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void StartMessageListener()
        {
            _channel.QueueDeclare(queue: _rabbitMqOptions.NameMessageQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var strMessage = Encoding.UTF8.GetString(body);
                NameMessage nameMessage = JsonConvert.DeserializeObject<NameMessage>(strMessage);

                if (nameMessage != null && _messageValidatorService.IsValidMessage(nameMessage.Message))
                    Console.WriteLine($"Hello {nameMessage.Message.Split(", ")[1]}, I am your receiver!");
                else
                    Console.WriteLine("Invalid Message.");
            };

            _channel.BasicConsume(queue: _rabbitMqOptions.NameMessageQueueName, autoAck: true, consumer: consumer);
        }
        
        public void CloseConnection()
        {
            if (!_channel.IsClosed)
                _channel.Close();
        }

        public void Dispose()
        {
            if (!_channel.IsClosed)
                _channel.Close();
        }
    }
}
