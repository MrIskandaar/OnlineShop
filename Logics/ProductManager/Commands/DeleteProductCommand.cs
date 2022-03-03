namespace OnlineShop.Logics.ProductManager.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
        {
            private readonly ShopContext _context;

            public DeleteProductCommandHandler(ShopContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
                if (product == null) return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
}
