
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using f2u.API.Data;
using f2u.API.DTO;
using f2u.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;

namespace f2u.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthenticationRepository _repo;


        public AuthController(IAuthenticationRepository repo, IConfiguration config)
        {
            _config = config;
            this._repo = repo;
        }


        //public async Task<IActionResult> Register([FromBody]UserRegisterDto userRegister)
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegister)
        {
            //if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var usrName = userRegister.UserName.ToLower();
            if (await _repo.UserExists(userRegister.UserName.ToLower()))
            {
                return BadRequest(string.Format("User name '{0}' already exists.", userRegister.UserName));
            }
            var user2Create = new User()
            {
                UserName = usrName
            };
            var createdUser = await _repo.Register(user2Create, userRegister.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {

            var userFromRepo = await _repo.Login(userLoginDto.UserName.ToLower(), userLoginDto.Password);
            if (userFromRepo == null) { Unauthorized(); }
            var claims = new[]{
               new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.UserName),
           };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
            });

        }
    }
}
