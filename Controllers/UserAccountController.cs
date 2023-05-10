using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlanningPokerWebAPI.ApplicationLayer.DTOs;
using PlanningPokerWebAPI.Infrastructure;
using PlanningPokerWebAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private UserManager<User> userManager;
        private readonly ApplicationSettings appSettings;
        private PokerAppDbContext context;

        public UserAccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            IOptions<ApplicationSettings> appSettings, PokerAppDbContext context)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
            this.context = context;
        }

        //[HttpPost]
        //[EnableCors("AllowSpecificOrigin")]
        //[Route("Register")]
        //public async Task<object> Register(UserLoginDto user)
        //{
        //    //user.Role = "User";
        //    var newUser = new User()
        //    {
        //        UserName = user.UserName                
        //    };
        //    try
        //    {
        //        var result = await userManager.CreateAsync(newUser, user.Password);
        //        //await userManager.AddToRoleAsync(newUser, user.Role);
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //[HttpPost]
        //[EnableCors("AllowSpecificOrigin")]
        //[Route("Login")]
        //public async Task<IActionResult> Login(UserLoginDto user)
        //{
        //    var loggedInUser = await userManager.FindByNameAsync(user.UserName);
        //    if (loggedInUser != null && await userManager.CheckPasswordAsync(loggedInUser, user.Password))
        //    {
        //        //var role = await userManager.GetRolesAsync(loggedInUser);
        //        //IdentityOptions options = new IdentityOptions();
        //        var tokenDescriptor = new SecurityTokenDescriptor()
        //        {
        //            Subject = new ClaimsIdentity(new Claim[]
        //            {
        //                new Claim("UserID", loggedInUser.Id),
        //                //new Claim(options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
        //            }),
        //            Expires = DateTime.UtcNow.AddDays(2),
        //            SigningCredentials = new SigningCredentials(
        //                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt_Secret)),
        //                SecurityAlgorithms.HmacSha256Signature)
        //        };
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        //        var token = tokenHandler.WriteToken(securityToken);
        //        return Ok(new { token });
        //    }
        //    return BadRequest(new { message = "Username or password is incorrect." });
        //}


        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        [Route("Register")]
        public async Task<object> RegisterUser([FromBody]UserLoginDto user)
        {
            var newUser = new User()
            {
                UserName = user.UserName
            };
            try
            {
                var result = await userManager.CreateAsync(newUser, user.Password);
                //await userManager.AddToRoleAsync(newUser, user.Role);
                //context.ApplicationUsers.Add(newUser);
                //context.SaveChanges();
                //return Ok(result);
                return Ok(true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    

    [HttpPost]
    [EnableCors("AllowSpecificOrigin")]
    [Route("Login")]
    public async Task<LoggedInUser> Login([FromBody] UserLoginDto user)
    {
        var loggedInUser = await userManager.FindByNameAsync(user.UserName);
        if (loggedInUser != null && await userManager.CheckPasswordAsync(loggedInUser, user.Password))
        {
            //var role = await userManager.GetRolesAsync(loggedInUser);
            IdentityOptions options = new IdentityOptions();
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(options.ClaimsIdentity.UserIdClaimType, loggedInUser.Id.ToString()),
                    //new Claim(options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                });
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt_Secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //  new ClaimsPrincipal(claimsIdentity));
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            //return Ok(new { token });
            return new LoggedInUser()
            {
                user = context.ApplicationUsers.Where(x => x.UserName == user.UserName).FirstOrDefault(),
                auth_token = token
            };
        }
        return null;
        //return BadRequest(new { message = "Username or password is incorrect." });
    }

    [HttpGet]
    [EnableCors("AllowSpecificOrigin")]
    [Route("Login")]
    public async Task<User> GetCurrentUser([FromBody] UserLoginDto user)
    {
        return context.ApplicationUsers.Where(x => x.UserName == user.UserName).FirstOrDefault();
    }

    public class LoggedInUser
    {
        public User user { get; set; }
        public string auth_token { get; set; }
    }
}
}
