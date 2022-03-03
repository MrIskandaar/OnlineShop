namespace OnlineShop.Logics.UserManager.Commands
{
    public class UpdateUserCommand : IRequest<UserViewModel>
    {
        public UserRequestModel UserInfo { get; set; }
        public int UserId { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserViewModel>
        {
            private readonly ShopContext _context;
            public UpdateUserCommandHandler(ShopContext context)
            {
                _context = context;
            }

            public async Task<UserViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
                if (user == null) return null;
                user.FirstName = request.UserInfo.FirstName;
                user.LastName = request.UserInfo.LastName;
                user.UserName = request.UserInfo.UserName.ToLower().Trim();
                user.Password = request.UserInfo.Password;
                user.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync(cancellationToken);
                return user;
            }
        }
    }
}
