namespace OnlineShop.Logics.UserManager.Queries
{
    public class GetUserByIdQuery : IRequest<UserViewModel>
    {
        public int UserId { get; set; }

        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
        {
            private readonly ShopContext _context;
            public GetUserByIdQueryHandler(ShopContext context)
            {
                _context = context;
            }
            public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
                return user ?? null;
            }
        }
    }
}
