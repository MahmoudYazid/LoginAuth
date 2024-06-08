using LoginAuth.command;
using LoginAuth.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IMediator Mediator { get; set; }
        public UserController ( IMediator mediator ) { 
        
            Mediator = mediator;
        }


        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> MakeNewUser([FromForm] CreateNewUserCommand createNewUserCommand_) {

            var Command = await Mediator.Send(createNewUserCommand_);
            return Ok(Command);
        
        
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginQuery loginQuery)
        {

            var Command = await Mediator.Send(loginQuery);
            return Ok(Command);


        }

    }
}
