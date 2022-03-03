namespace OnlineShop.Logics.ProductManager.Queries
{
    public class GetProductByIdQuery : IRequest<ProductViewModel>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductViewModel>
        {
            private readonly ShopContext _context;

            public GetProductByIdQueryHandler(ShopContext context)
            {
                _context = context;
            }

            public async Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await _context.Products
                    .Include(p => p.User)
                    .Include(p => p.Category)
                    .Where(p => !p.IsDeleted)
                    .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                return product ?? null;
            }
        }
    }
}
