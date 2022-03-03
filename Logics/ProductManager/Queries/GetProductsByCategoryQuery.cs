namespace OnlineShop.Logics.ProductManager.Queries
{
    public class GetProductsByCategoryQuery : IRequest<IList<ProductViewModel>>
    {
        public int CategoryId { get; set; }

        public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, IList<ProductViewModel>>
        {
            private readonly ShopContext _context;
            public GetProductsByCategoryQueryHandler(ShopContext context)
            {
                _context = context;
            }
            public async Task<IList<ProductViewModel>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
            {
                var products = await _context.Products.Where(p => p.CategoryId == request.CategoryId)
                    .Include(p => p.User)
                    .Include(p => p.Category)
                    .ToListAsync(cancellationToken);
                return products.Select(p => (ProductViewModel)p).ToList() ?? null;
            }
        }
    }
}
