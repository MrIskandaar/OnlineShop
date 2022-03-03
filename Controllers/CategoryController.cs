using OnlineShop.Logics.CategoryManager.Commands;
using OnlineShop.Logics.CategoryManager.Queries;

namespace OnlineShopService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator? _mediator;

        protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return id <= 0 ? BadRequest() :
                Ok(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Add(string request)
        {
            return request == null || request.Length <= 3 ? BadRequest() :
                Ok(await Mediator.Send(new AddCategoryCommand { CategoryName = request}));
        }       

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryCommand request)
        {
            return request == null || request.Id <= 0 ? BadRequest() :
                Ok(await Mediator.Send(request));
        } 
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return id <= 0 ? BadRequest() :
                Ok(await Mediator.Send(new DeleteCategoryCommand { Id = id }));
        }
    }
}
