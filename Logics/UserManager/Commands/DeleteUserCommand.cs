namespace OnlineShop.Logics.UserManager.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
        {
            private readonly ShopContext _context;
            public DeleteUserCommandHandler(ShopContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
                if (user == null) return false;
                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
}
