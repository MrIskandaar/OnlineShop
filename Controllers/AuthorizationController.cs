using OnlineShop.Logics.AuthorizationManager.Commands;

namespace OnlineShopService.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> Login(AuthorizeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
