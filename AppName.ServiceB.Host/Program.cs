using AppName.ServiceB.Host.Helpers;
using AppName.ServiceB.Models.Configurations;
using AppName.ServiceB.Services.Implementations;
using AppName.ServiceB.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;


namespace AppName.ServiceB.Host
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static ServiceProvider Container { get; set; }
        private const string ExitOption = "q";

        static void Main(string[] args)
        {
            Console.Title = "Receiver";

            //Register DI & load configuration from json file.
            var services = new ServiceCollection();
            ConfigHelper.LoadConfig(args);
            ConfigureServices(services);
            Container = services.BuildServiceProvider();

            var rabbitMqService = Container.GetService<IRabbitMqService>();

            rabbitMqService.CreateConnection();
            rabbitMqService.StartMessageListener();
            

            Console.WriteLine("Press enter or any key then enter to Exit");
            Console.WriteLine("Listening for messages...");

            var userInput = Console.ReadLine();

            if (userInput != null)
            {
                rabbitMqService.CloseConnection();

                Console.WriteLine("Stopping...");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(ConfigHelper.Configuration.GetSection("RabbitMQConfiguration")
                .Get<RabbitMqOptions>());
            
            services.AddTransient<IMessageValidatorService, MessageValidatorService>();
            services.AddTransient<IRabbitMqService, RabbitMqService>();
        }
        
    }
}

