using Actio.Common.Commands;
using Actio.Common.Events;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient bus;

        public CreateActivityHandler(IBusClient bus)
        {
            this.bus = bus;
        }
        public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine($"creating activity: {command.Name}");
            await bus.PublishAsync(new ActivityCreated(command.Id,command.UserId,command.Category,command.Name,command.Description));
        }
    }
}
