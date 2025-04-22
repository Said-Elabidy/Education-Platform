

using Education.Application.helpers;
using Education.Domain.Entities;
using System.Security.Claims;

namespace Education.Application.Services.JwtServices
{
    public interface IJwtService
    {
        public int RefreshTokenExpiryInMinutes { get; }
        public Task<string> GenerateAccessToken(ApplicationUser user);
        public  Task<string> GenerateRefreshToken(ApplicationUser user);
       // Task<string> GenerateJwtToken(ApplicationUser user);
        ClaimsPrincipal? ValidateRefreshToken(string token, out bool isExpired);
    }
}
