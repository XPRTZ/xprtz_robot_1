using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using robot2.DataStructures;
using robot2.Models;

namespace robot2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobotProgramController : ControllerBase
    {
        private readonly RobotProgramResolver _programResolver;
        private readonly CommandQueue _commandQueue;

        public RobotProgramController(RobotProgramResolver programResolver, CommandQueue commandQueue)
        {
            _programResolver = programResolver;
            _commandQueue = commandQueue;
        }
        
        [HttpPut("{name}")]
        public IActionResult LoadProgram(string name, CancellationToken cancellationToken)
        {
            var program = _programResolver(name);
            _commandQueue.LoadProgram(program);
            return Ok();
        }
    }
}
