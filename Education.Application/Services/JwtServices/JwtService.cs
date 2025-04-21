using Education.Application.helpers;
using Education.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Education.Application.Services.JwtServices
{
    public class JwtService : IJwtService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwtOptions;

        public JwtService(UserManager<ApplicationUser> userManager, IOptions<JWT> jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public int RefreshTokenExpiryInMinutes => _jwtOptions.RefreshTokenLifetimeInMinutes;

        public async Task<string> GenerateRefreshToken(ApplicationUser user) => await GenerateJwtToken(user, _jwtOptions.RefreshTokenSecret, _jwtOptions.RefreshTokenLifetimeInMinutes);

         public async Task<string> GenerateAccessToken(ApplicationUser user) => await GenerateJwtToken(user, _jwtOptions.AccessTokenSecret, _jwtOptions.AccessTokenLifetimeInMinutes);

        private async Task<string> GenerateJwtToken(ApplicationUser user , string _secret , int _lifeTimeInMinutes )
        {
            var roles = await _userManager.GetRolesAsync(user);
            var secret = _secret;
            var encodedSecret = Encoding.UTF8.GetBytes(secret);
            var key = new SymmetricSecurityKey(encodedSecret);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var _user = new
            {
                name = $"{user.FirstName} {user.LastName}",
                id = user.Id,
                email = user.Email,
                role = roles
            };

            List<Claim> claims = [
                new (JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, user.Id),

    new ("name", $"{user.FirstName} {user.LastName}"),
    new ("email", user.Email!),
     new ("id", user.Id),
    new ("role", roles.FirstOrDefault()!)
                ];

            var token = new JwtSecurityToken
            (

                signingCredentials: creds,
                issuer: _jwtOptions.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_lifeTimeInMinutes),
                audience: _jwtOptions.Audience


            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? ValidateRefreshToken(string token, out bool isExpired)
        {
            isExpired = false;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.RefreshTokenSecret))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (securityToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    return null;


                if (securityToken is JwtSecurityToken _jwtToken)
                {

                    var exp = _jwtToken.Payload.Expiration;

                    if (exp.HasValue)
                    {
                        var expiryDate = DateTimeOffset.FromUnixTimeSeconds(exp.Value).UtcDateTime;

                        if (expiryDate < DateTime.UtcNow)
                        {
                            isExpired = true;
                            return null;
                        }
                    }

                    return principal;
                }

                return principal;
            }
            catch
            {
                return null;
            }

        }

      
    }
}
