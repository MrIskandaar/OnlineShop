namespace OnlineShop.Logics.CategoryManager.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
        {
            private readonly ShopContext _context;
            public DeleteCategoryCommandHandler(ShopContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
                if (category == null) return false;
                //category.IsDeleted = true; //if I want to save info
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
}
