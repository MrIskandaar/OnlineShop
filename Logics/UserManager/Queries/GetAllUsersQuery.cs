namespace OnlineShop.Logics.UserManager.Queries
{
    public class GetAllUsersQuery : IRequest<IList<UserViewModel>>
    {
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IList<UserViewModel>>
        {
            private readonly ShopContext _context;
            public GetAllUsersQueryHandler(ShopContext context)
            {
                _context = context;
            }
            public async Task<IList<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var users = await _context.Users.ToListAsync(cancellationToken);
                return users.Select(u => (UserViewModel)u).ToList();
            }
        }
    }
}
