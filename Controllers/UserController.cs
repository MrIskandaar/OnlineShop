using OnlineShop.Logics.UserManager.Commands;
using OnlineShop.Logics.UserManager.Queries;

namespace OnlineShopService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery()));
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return id <= 0 ? BadRequest($"non acceptable id : {id}") :
                Ok(await Mediator.Send(new GetUserByIdQuery() { UserId = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserCommand command) 
        {
            return command == null ? BadRequest("info to add wasn't written") : 
                Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return id <+ 0 ? BadRequest($"non acceptable id : {id}") :
                Ok(await Mediator.Send(new DeleteUserCommand() { UserId = id }));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            return command == null ? BadRequest("info to update wasn't written") :
                Ok(await Mediator.Send(command));
        }
    }
}
