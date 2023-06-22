using AttendanceControlSystem.Entity;
using AttendanceControlSystem.Interfaces;
using AttendanceControlSystem.Models.UserModels;
using AttendanceControlSystem.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AttendanceControlSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IMapper mapper, IUserService userService)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);

            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(userModel.Password);

            user.PasswordHash = passwordHash;
            user.Role = RoleConstants.Teacher;

            try
            {
                await _userService.CreateAsync(user);
            }
            catch (MongoWriteException ex) 
            {
                if (ex.WriteError.Category == ServerErrorCategory.DuplicateKey) 
                {
                    return BadRequest($"User with such user name '{userModel.UserName}' exists");
                }
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserModel userModel)
        {
            var user = await _userService.GetByUserNameAsync(userModel.UserName);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(userModel.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                /*new Claim(ClaimTypes.Role, user.Role),*/
                new Claim(ClaimTypes.Role, RoleConstants.Admin),
                new Claim(ClaimTypes.Role, RoleConstants.Teacher),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
