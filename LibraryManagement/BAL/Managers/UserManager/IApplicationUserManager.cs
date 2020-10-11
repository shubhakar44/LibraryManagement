using LibraryManagement.DomainModals;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.BAL.Managers.UserManager
{
    public interface IApplicationUserManager
    {
        Task<int> AddToFavourite(UserBookViewModal book);

        Task<int> MarkAsRead(UserBookViewModal book);

        Task<int> AddToReview(UserBookViewModal book);
    }
}
