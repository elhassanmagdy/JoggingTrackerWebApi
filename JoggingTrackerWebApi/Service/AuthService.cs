using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JoggingTrackerWebApi.Dto;
using JoggingTrackerWebApi.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace JoggingTrackerWebApi.Service
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName= dto.Username,
                Email= dto.Email,
            };

            var result= await _userManager.CreateAsync(user,dto.Password);
            if (!result.Succeeded)
            {
                return string.Join(", ", result.Errors.Select(e=>e.Description));
            }

            // assign role
            if (!string.IsNullOrEmpty(dto.Role))
            {
                await _userManager.AddToRoleAsync(user, dto.Role);
            }


            return "User registered successfully";
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {

            //check user
            var user= await _userManager.FindByNameAsync(dto.Username);
            if (user == null)
            {
                return "Invalid username or password";
            }

            //check password
            var IsValid =await _userManager.CheckPasswordAsync(user, dto.Password);
            {
                if (!IsValid)
                
                    return "Invalid username or password";
                
            }

            //create token
            var token =await GenerateJwtToken(user);
            return token;
        }

        public async Task<string> GenerateJwtToken(ApplicationUser user)
        {

            //claims
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, user.Id), // to connect user with his jogging entry
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //role
            var roles= await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role=> new Claim (ClaimTypes.Role, role)));

            //key
            var key = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_configuration["JWT:Key:Secret"]!));
            var cred =  new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            //create Token
            var token =new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                double.Parse(_configuration["JWT:DurationInMinutes"]!)
                ),
                signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
