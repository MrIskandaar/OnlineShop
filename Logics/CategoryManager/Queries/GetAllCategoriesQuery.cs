namespace OnlineShop.Logics.CategoryManager.Queries
{
    public class GetAllCategoriesQuery : IRequest<IList<Category>>
    {
        public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IList<Category>>
        {
            private readonly ShopContext _context;
            public GetAllCategoriesQueryHandler(ShopContext context)
            {
                _context = context;
            }
            public async Task<IList<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categories = await _context.Categories.ToListAsync(cancellationToken);
                return categories;
            }
        }
    }
}
