using LibraryManagement.BAL.Managers.UserManager;
using LibraryManagement.DomainModals;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserManager userManager;
        public UserController(IApplicationUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddToFavourite([FromBody] UserBookViewModal userBook)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userManager.AddToFavourite(userBook);
                return this.Ok(result);
            }
            return this.BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MarkAsRead([FromBody] UserBookViewModal userBook)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userManager.MarkAsRead(userBook);
                return this.Ok(result);
            }

            return this.BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddReview([FromBody] UserBookViewModal userBook)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userManager.AddToReview(userBook);
                return this.Ok(result);
            }
            return this.BadRequest();
        }

    }
}
