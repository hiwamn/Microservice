using Actio.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    public class ActivitiesController : Controller
    {
        private readonly IBusClient bus;

        public ActivitiesController(IBusClient bus)
        {
            this.bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.Now;
            await bus.PublishAsync(command);
            return Accepted($"activities/{command.Id}");
        }


    }
}
