namespace OnlineShop.Logics.CategoryManager.Commands
{
    public class AddCategoryCommand : IRequest<Category>
    {
        public string? CategoryName { get; set; }

        public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Category>
        {
            private readonly ShopContext _context;
            public AddCategoryCommandHandler(ShopContext context)
            {
                _context = context;
            }
            public async Task<Category> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
            {
                if (request.CategoryName == null || request.CategoryName.Length == 0) return null;
                var category = new Category { CategoryName = request.CategoryName };
                await _context.Categories.AddAsync(category, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return category;
            }
        }
    }
}
