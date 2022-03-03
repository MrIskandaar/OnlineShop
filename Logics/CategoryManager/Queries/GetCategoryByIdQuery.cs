namespace OnlineShop.Logics.CategoryManager.Queries
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public int Id { get; set; }

        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
        {
            private readonly ShopContext _context;

            public GetCategoryByIdQueryHandler(ShopContext context)
            {
                _context = context;
            }
            public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
                if (category == null) return null;
                return category;
            }
        }
    }
}
