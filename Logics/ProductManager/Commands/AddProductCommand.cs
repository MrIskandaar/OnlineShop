namespace OnlineShop.Logics.ProductManager.Commands
{
    public class AddProductCommand : IRequest<Product>
    {
        public ProductRequestModel Product { get; set; }
        public int UserId { get; set; }

        public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Product>
        {
            private readonly ShopContext _context;
            public AddProductCommandHandler(ShopContext context)
            {
                _context = context;
            }
            public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
            {
                var product = new Product
                {
                    ProductName = request.Product.ProductName,
                    Price = request.Product.Price,
                    CategoryId = request.Product.CategoryId
                };
                await _context.AddAsync(product, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return product;
            }
        }
    }
}
