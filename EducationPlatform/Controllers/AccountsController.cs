using Education.Domain.Entities;
using Education.Domain.Roles;
using EducationPlatform.helpers;
using EducationPlatform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace EducationPlatform.Controllers
{

    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwtOptions;
        public AccountsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtOptions = jwtOptions.Value!;
        }


        private async Task<string> GenerateJwt(ApplicationUser user, JWT options)
        {

            var roles = await _userManager.GetRolesAsync(user);
            var secret = options.Secret;
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
                issuer: options.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(options.LifetimeInMinutes),
                audience: options.Audience


            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsPrincipal? ValidateRefreshToken(string token, JWT options, out bool isExpired)
        {
            isExpired = false;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidIssuer = options.Issuer,
                ValidateAudience = true,
                ValidAudience = options.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(options.Secret)),



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

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {


                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                    return Unauthorized(new { message = "Invalid login attempt , email or password incorrect" });


                var result = await _userManager.CheckPasswordAsync(user, model.Password);

                if (!result)
                    return Unauthorized(new { message = "Invalid login attempt , email or password incorrect" });



                var expiry = 60 * 24 * 15;
                var refreshToken = await GenerateJwt(user, _jwtOptions with { LifetimeInMinutes = expiry });

                var SessionCookieOptions = new CookieOptions
                {
                    HttpOnly = true,  // Can't be accessed via JS
                    Secure = true,    // Ensures cookies are sent over HTTPS
                    SameSite = SameSiteMode.None, // Prevents CSRF
                    Path = "/",
                    // Expires = DateTime.UtcNow.AddMinutes(expiry)  // Expiry time of refresh token
                };

                var CookieOptions = new CookieOptions
                {
                    HttpOnly = true,  // Can't be accessed via JS
                    Secure = true,    // Ensures cookies are sent over HTTPS
                    SameSite = SameSiteMode.None, // Prevents CSRF
                    Path = "/",
                    Expires = DateTime.UtcNow.AddMinutes(1)  // Expiry time of refresh token
                };



                Response.Cookies.Append("refreshToken", refreshToken, model.RememberMe ? CookieOptions : SessionCookieOptions);

                var accessToken = await GenerateJwt(user, _jwtOptions);

                var roles = await _userManager.GetRolesAsync(user);
                return Ok(new
                {
                    accessToken,
                    name = $"{user.FirstName} {user.LastName}",
                    id = user.Id,
                    email = user.Email,
                    role = roles.FirstOrDefault()!
                });


            }


            return BadRequest();
        }




        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {





                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists is not null)
                    return Conflict(new { message = "User with this email already exists." });


                var role = await _roleManager.FindByNameAsync(MyRoles.Admin);

                if (role is null)
                    return UnprocessableEntity(new { message = "Role doesn't exist" });


                var user = new ApplicationUser { UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, role.Name!);

                    var check = await _userManager.CheckPasswordAsync(user, model.Password);

                    if (!check)
                        return Unauthorized();

                    var expiry = 60 * 24 * 15;
                    var refreshToken = await GenerateJwt(user, _jwtOptions with { LifetimeInMinutes = expiry });

                    var SessionCookieOptions = new CookieOptions
                    {
                        HttpOnly = true,  // Can't be accessed via JS
                        Secure = true,    // Ensures cookies are sent over HTTPS
                        SameSite = SameSiteMode.None, // Prevents CSRF
                        Path = "/",
                        // Expires = DateTime.UtcNow.AddMinutes(expiry)  // Expiry time of refresh token
                    };


                    Response.Cookies.Append("refreshToken", refreshToken, SessionCookieOptions);

                    var accessToken = await GenerateJwt(user, _jwtOptions);



                    return StatusCode(201, new
                    {
                        accessToken,
                        name = $"{user.FirstName} {user.LastName}",
                        id = user.Id,
                        email = user.Email,
                        role = role.Name!
                    });

                }

                List<IdentityError> errors = [];

                foreach (var error in result.Errors)
                    errors.Add(error);

                return BadRequest(new { errors });
            }

            return BadRequest();

        }

        [HttpPost]
        [Route("Refresh")]
        public async Task<IActionResult> Refresh()
        {

            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized(new { message = "Refresh token not found" });

            var principal = ValidateRefreshToken(refreshToken, _jwtOptions, out bool expired);

            if (expired is true)
                return Unauthorized(new { message = "Refresh token expired" });
            if (principal is null)
                return Unauthorized(new { message = "Invalid refresh token" });


            var userId = principal.FindFirst("id")?.Value;
            if (userId is null)
                return Unauthorized(new { message = "user was not found" });
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Unauthorized(new { message = "user was not found" });
            var accessToken = await GenerateJwt(user, _jwtOptions);

            return Ok(new { accessToken });
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()

        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Conflict(new { message = "User is already logged Out" });


            await _signInManager.SignOutAsync();
            Response.Cookies.Delete("refreshToken");
            return Ok(new { message = "user logged out successfully" });
        }
    }
}

