using LibraryManagement.DAL.Repositories;
using LibraryManagement.DomainModals;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.BAL.Managers.UserManager
{
    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly ILibraryManagementRepository<BookStore> libraryManagementRepository;

        private readonly ILibraryManagementRepository<UserBook> userBookRepo;

        readonly UserManager<ApplicationUser> userManager;
        public ApplicationUserManager(ILibraryManagementRepository<BookStore> libraryManagementRepository,
            ILibraryManagementRepository<UserBook> userBookRepo, UserManager<ApplicationUser> userManager)
        {
            this.libraryManagementRepository = libraryManagementRepository;
            this.userBookRepo = userBookRepo;
            this.userManager = userManager;
        }

        public async Task<int> AddToFavourite(UserBookViewModal userBookViewModal)
        {
            var isBookExists = this.userBookRepo.FindBy(x => x.BookId == userBookViewModal.BookId && x.UserId == userBookViewModal.UserId).FirstOrDefault(); ;

            if(isBookExists == null)
            {
                var user = await userManager.FindByIdAsync(userBookViewModal.UserId);
                var book = libraryManagementRepository.FindBy(x => x.Id == userBookViewModal.BookId).FirstOrDefault();
                if(user != null && book != null) {
                    var userBook = new UserBook()
                    {
                        UserId = user.Id,
                        BookId = book.Id,
                        BookStores = book,
                        Users = user
                    };

                    this.userBookRepo.Add(userBook);
                    var result = this.userBookRepo.Save();
                    return result;
                }
                return 0;
            }
            return 0;
        }

        public async Task<int> MarkAsRead(UserBookViewModal userBookViewModal)
        {
            var user = await userManager.FindByIdAsync(userBookViewModal.UserId);
            if(user == null)
            {
                return 0;
            }
            var userBook = this.userBookRepo.FindBy(x => x.BookId == userBookViewModal.BookId && x.UserId == user.Id).FirstOrDefault(); ;

            if (userBook != null)
            {
                userBook.IsRead = userBookViewModal.IsRead;
                this.userBookRepo.Edit(userBook);
                var result = this.userBookRepo.Save();
                return result;
            }
            return 0;
        }

        public async Task<int> AddToReview(UserBookViewModal userBookViewModal)
        {
            var user = await userManager.FindByIdAsync(userBookViewModal.UserId);
            if (user == null)
            {
                return 0;
            }
            var userBook = this.userBookRepo.FindBy(x => x.BookId == userBookViewModal.BookId && x.UserId == user.Id).FirstOrDefault(); ;

            if (userBook != null && userBook.IsRead)
            {
                userBook.Review = userBookViewModal.Review;
                this.userBookRepo.Edit(userBook);
                var result = this.userBookRepo.Save();
                return result;
            }
            return 0;
        }
    }
}
