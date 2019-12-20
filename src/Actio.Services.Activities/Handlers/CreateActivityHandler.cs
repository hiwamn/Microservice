﻿using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _bus;
        private readonly IActivityService _activityService;
        private readonly ILogger _logger;

        public CreateActivityHandler(IBusClient bus,IActivityService activityService,ILogger<CreateActivity> logger)
        {
            _bus = bus;
            _activityService = activityService;
            _logger = logger;
        }
        public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine($"creating activity: {command.Name}");
            _logger.LogInformation($"creating activity: {command.Name}");
            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category, command.Name, command.Description, command.CreatedAt);
                await _bus.PublishAsync(new ActivityCreated(command.Id,command.UserId,command.Category,command.Name,command.Description));
                return;
            }
            catch (ActioException ex)
            {
                await _bus.PublishAsync(new CreateActivityRejected(command.Id,ex.Code,ex.Message));
            }
        }
    }
}
