using Actio.Common.Commands;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RawRabbit.Pipe;
using System.Reflection;
using Actio.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RawRabbit.Instantiation;

namespace Actio.Common.RabbitMq
{
    public static class Extensions
    {
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, 
            ICommandHandler<TCommand> handler) where TCommand : ICommand
        => bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
            ctx=>ctx.UseConsumerConfiguration(cfg=>
            cfg.FromDeclaredQueue(q=>q.WithName(GetQueueName<TCommand>()))));

        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus, 
            IEventHandler<TEvent> handler) where TEvent : IEvent
        => bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
            ctx=>ctx.UseConsumerConfiguration(cfg=>
            cfg.FromDeclaredQueue(q=>q.WithName(GetQueueName<TEvent>()))));

        private static string GetQueueName<T>() => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMq(this IServiceCollection service, IConfiguration config)
        {
            var option = new RabbitMqOptions();
            var section = config.GetSection("rabbitmq");
            section.Bind(option);
            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = option
            });
            service.AddSingleton<IBusClient>(_=> client);
        }
    }
}
