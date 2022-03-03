using OnlineShop.Logics.ProductManager.Commands;
using OnlineShop.Logics.ProductManager.Queries;

namespace OnlineShopService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdudctController : ControllerBase
    {
        private readonly IMediator? _mediator;

        protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return id <= 0 ? BadRequest($"non acceptable id : {id}") :
                Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        [HttpGet("ByCategory/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            if (categoryId <= 0) return BadRequest("categoryId is incorrect");
            return Ok(await Mediator.Send(new GetProductsByCategoryQuery() { CategoryId = categoryId }));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductCommand command)
        {
            if (command == null) return BadRequest("information to add wasn't written!");
            if (!int.TryParse(User?.FindFirst("UserId")?.Value.ToString(), out var userId))
                return BadRequest("you are not registered");
            command.UserId = userId;
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommand command)
        {
            if (command == null) return BadRequest("information to update wasn't written");
            if (!int.TryParse(User?.FindFirst("UserId")?.Value.ToString(), out var userId))
                return BadRequest("you are not registered");
            command.UserId = userId;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return id <= 0 ? BadRequest($"non acceptable id : {id}") :
                Ok(await Mediator.Send(new DeleteProductCommand { ProductId = id }));
        }
    }
}
