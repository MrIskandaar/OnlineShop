using Microsoft.IdentityModel.Tokens;
using Root.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineShop.Logics.AuthorizationManager.Commands
{
    public class AuthorizeCommand : IRequest<LoginResponse>
    {
        [Required(ErrorMessage = "username must be written!")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="password must be written!")]
        public string Password { get; set; }

        public class AuthorizeCommandHandler : IRequestHandler<AuthorizeCommand, LoginResponse>
        {
            private readonly ShopContext _context;
            public AuthorizeCommandHandler(ShopContext context)
            {
                _context = context;
            }

            public async Task<LoginResponse> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName.Trim().ToLower(), cancellationToken);
                if (user == null || user.Password != request.Password) return null;
                var refreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty);
                List<Claim> claims = new()
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.Id.ToString())
                };
                var signingKey = AuthorizationHelper.GetSecurityKey();
                JwtSecurityToken jwt = new(
                    issuer: AuthorizationHelper.ISSUER,
                    audience: AuthorizationHelper.AUDIENCE,
                    claims: claims,
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthorizationHelper.EXPIRE_MIN))
                    );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var expDate = DateTime.Now.Add(TimeSpan.FromMinutes(AuthorizationHelper.EXPIRE_MIN));
                return new LoginResponse()
                {
                    UserId = user.Id,
                    AccessToken = encodedJwt,
                    ExpDate = expDate,
                    UserName = user.UserName,
                    RefreshToken = refreshToken
                };
            }
        }
    }
}
