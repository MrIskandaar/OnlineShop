namespace OnlineShop.Logics.UserManager.Commands
{
    public class AddUserCommand : IRequest<UserViewModel>
    {
        public UserRequestModel UserInfo { get; set; }

        public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserViewModel>
        {
            private readonly ShopContext _context;
            public AddUserCommandHandler(ShopContext context)
            {
                _context = context;
            }
            public async Task<UserViewModel> Handle(AddUserCommand request, CancellationToken cancellationToken)
            {
                User user = new ()
                {
                    FirstName = request.UserInfo.FirstName,
                    LastName = request.UserInfo.LastName,
                    UserName = request.UserInfo.UserName.Trim().ToLower(),
                    Password = request.UserInfo.Password
                };
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return user;
            }
        }
    }
}
