using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DomainModals
{
    public class UserBookViewModal
    {
        public int BookId { get; set; }

        public string UserId { get; set; }

        public string Review { get; set; }

        public bool IsRead { get; set; }

    }
}
