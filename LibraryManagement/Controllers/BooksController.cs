using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.BAL;
using LibraryManagement.DomainModels;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        private readonly IBooksManager booksManager;

        public BooksController(ILogger<BooksController> logger , IBooksManager booksManager)
        {
            _logger = logger;
            this.booksManager = booksManager;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(BookViewModal book)
        {
            if (ModelState.IsValid)
            {
                var result = this.booksManager.AddBook(book);
                return this.Ok(result);
            }
            return this.BadRequest();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAll()
        {
            var result = this.booksManager.GetBooks();
            return this.Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(BookViewModal book)
        {
            if (ModelState.IsValid)
            {
                var result = this.booksManager.UpdateBook(book);
                return this.Ok(result);
            }
            return this.BadRequest();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromQueryAttribute] int Id)
        {
            if (ModelState.IsValid)
            {
                var result = this.booksManager.DeleteBook(Id);
                return this.Ok(result);
            }
            return this.BadRequest();
        }
    }
}
