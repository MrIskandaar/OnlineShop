namespace OnlineShop.Logics.ProductManager.Commands
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public ProductUpdateModel Product { get; set; }

        public int UserId { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
        {
            private readonly ShopContext _context;
            public UpdateProductCommandHandler(ShopContext context)
            {
                _context = context;
            }

            public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Product.Id, cancellationToken);
                if (product == null) return null;
                product.Price = request.Product.Price;
                product.CategoryId = request.Product.CategoryId;
                product.UserId = request.UserId;
                await _context.SaveChangesAsync(cancellationToken);
                return product;
            }
        }
    }
}
