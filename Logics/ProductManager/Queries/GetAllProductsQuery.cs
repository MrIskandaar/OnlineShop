namespace OnlineShop.Logics.ProductManager.Queries
{
    public class GetAllProductsQuery : IRequest<IList<Product>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IList<Product>>
        {
            private readonly ShopContext _context;
            public GetAllProductsQueryHandler(ShopContext context)
            {
                _context = context;
            }
            public async Task<IList<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var products = await _context.Products
                    .Include(p => p.User)
                    .Include(p => p.Category)
                    .Where(p => !p.IsDeleted)
                    .ToListAsync(cancellationToken);

                return products;
            }
        }
    }
}

