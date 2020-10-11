using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class UserBook
    {
        public int BookId { get; set; }

        public BookStore BookStores { get; set; }

        public string UserId { get; set; }

        public ApplicationUser Users { get; set; }
        
        [MaxLength(500)]
        public string Review { get; set; }

        public bool IsRead { get; set; }

    }
}
