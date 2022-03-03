namespace OnlineShop.Logics.CategoryManager.Commands
{
    public class UpdateCategoryCommand : IRequest<Category>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
        {
            private readonly ShopContext _context;
            public UpdateCategoryCommandHandler(ShopContext context)
            {
                _context = context;
            }
            public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == request.Id);
                if (category == null) return null;
                category.CategoryName = request.CategoryName;
                category.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync(cancellationToken);
                return category;
            }
        }
    }
}
