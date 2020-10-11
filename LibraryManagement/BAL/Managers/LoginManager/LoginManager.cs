using LibraryManagement.DomainModels;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BAL.Managers.LoginManager
{
    public class LoginManager
    {
        IConfiguration configuration;

        UserManager<ApplicationUser> userManager;
        public LoginManager(IConfiguration configuration, UserManager<ApplicationUser> userManager) {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<UserTokenModel> GenerateJWTToken(ApplicationUser user)
        {
            var role = await userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration.GetSection("jwt").GetSection("SecretKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, role.First())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToken = new UserTokenModel()
            {
                EmailId = user.Email,
                UserId = user.Id,
                Token = tokenHandler.WriteToken(token)
            };

            return userToken;
        }
    }
}
