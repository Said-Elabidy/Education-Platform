using Education.Application.Services.JwtServices;
using Education.Domain.Entities;
using Education.Domain.Roles;
using EducationPlatform.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace EducationPlatform.Controllers
{

    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;
        public AccountsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
           
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                    return Unauthorized(new { message = "Invalid login attempt , email or password incorrect" });


                var result = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!result)
                    return Unauthorized(new { message = "Invalid login attempt , email or password incorrect" });




            var refreshToken = await _jwtService.GenerateRefreshToken(user);

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
                    Expires = DateTime.UtcNow.AddMinutes(_jwtService.RefreshTokenExpiryInMinutes)  // Expiry time of refresh token
                };



                Response.Cookies.Append("refreshToken", refreshToken, model.RememberMe ? CookieOptions : SessionCookieOptions);

                var accessToken = await _jwtService.GenerateAccessToken(user);

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




        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {

                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists is not null)
                    return Conflict(new { message = "User with this email already exists." });


                var role = await _roleManager.FindByNameAsync(MyRoles.User);

                if (role is null)
                    return BadRequest(new { message = "Role doesn't exist" });


                var user = new ApplicationUser { UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToArray();

                return BadRequest(new { errors });
            }

                  var roleAdded =   await _userManager.AddToRoleAsync(user, role.Name!);

                    if (!roleAdded.Succeeded)
                        return BadRequest(new { message = "Failed to Assign Role to User" });

                    var check = await _userManager.CheckPasswordAsync(user, model.Password);

                    if (!check)
                        return Unauthorized(new { message = "Invalid Credentials" });

                   
                    var refreshToken = await _jwtService.GenerateRefreshToken(user);

                    var SessionCookieOptions = new CookieOptions
                    {
                        HttpOnly = true,  // Can't be accessed via JS
                        Secure = true,    // Ensures cookies are sent over HTTPS
                        SameSite = SameSiteMode.None, // Prevents CSRF
                        Path = "/",
                        // Expires = DateTime.UtcNow.AddMinutes(_jwtService.RefreshTokenExpiryInMinutes)  // Expiry time of refresh token
                    };


                    Response.Cookies.Append("refreshToken", refreshToken, SessionCookieOptions);

                    var accessToken = await _jwtService.GenerateAccessToken(user);



                    return StatusCode(201, new
                    {
                        accessToken,
                        name = $"{user.FirstName} {user.LastName}",
                        id = user.Id,
                        email = user.Email,
                        role = role.Name!
                    });

                

            //List<string> errors = [];

            //foreach (var error in result.Errors)
            //    errors.Add(error.Description);

          
           

        }

        [HttpPost]
        [Route("Refresh")]
        public async Task<IActionResult> Refresh()
        {

            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized(new { message = "Refresh token not found" });

            var principal = _jwtService.ValidateRefreshToken(refreshToken, out bool expired);

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
            var accessToken = await _jwtService.GenerateAccessToken(user);

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

