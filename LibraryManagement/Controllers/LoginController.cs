using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.BAL.Managers.LoginManager;
using LibraryManagement.DomainModels;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UserManager<ApplicationUser> userManager;

        SignInManager<ApplicationUser> signInManager; 

        LoginManager loginManager;
        public LoginController(UserManager<ApplicationUser> userManager,LoginManager loginManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.signInManager = signInManager;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel login)
        {
            if (String.IsNullOrEmpty(login.EmailId) || String.IsNullOrEmpty(login.Password))
            {
                return this.BadRequest("No EmailId or Password");
            }

            var user = await userManager.FindByEmailAsync(login.EmailId);

            if(user != null)
            {
                var validPassword = await this.userManager.CheckPasswordAsync(user, login.Password);
                if(validPassword)
                {
                    var token = await this.loginManager.GenerateJWTToken(user);
                    await signInManager.SignInAsync(user, true);
                    return this.Ok(token);
                }
                else
                {
                    return this.BadRequest("Invalid Password");
                }
            } 
            else
            {
                return this.NotFound("User not found");
            }
        }
    }
}
