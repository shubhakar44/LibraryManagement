using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DomainModels
{
    public class UserTokenModel
    {
        public string UserId { get; set; }

        public string EmailId { get; set; }

        public string Token { get; set; }
    }
}
