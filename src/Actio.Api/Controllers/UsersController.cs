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
    public class UsersController : Controller
    {
        private readonly IBusClient bus;

        public UsersController(IBusClient bus)
        {
            this.bus = bus;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await bus.PublishAsync(command);
            return Accepted();
        }


    }
}
