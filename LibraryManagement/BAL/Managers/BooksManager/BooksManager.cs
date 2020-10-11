using LibraryManagement.DAL.Repositories;
using LibraryManagement.DomainModels;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.BAL
{
    public class BooksManager : IBooksManager
    {
        readonly ILibraryManagementRepository<BookStore> libraryManagementRepository; 
        public BooksManager(ILibraryManagementRepository<BookStore> libraryManagementRepository)
        {
            this.libraryManagementRepository = libraryManagementRepository;
        }
        public int AddBook(BookViewModal addedBook)
        {
            BookStore book = new BookStore()
            {
                Title = addedBook.Title,
                Author = addedBook.Author
            };
            this.libraryManagementRepository.Add(book);
            var result = this.libraryManagementRepository.Save();
            return result;
        }

        public int DeleteBook(int Id)
        {
            BookStore book = this.libraryManagementRepository.FindBy(x => x.Id == Id).FirstOrDefault();
            if(book != null)
            {
                this.libraryManagementRepository.Delete(book);
                var result = this.libraryManagementRepository.Save();
                return result;
            }
            return 0;
        }

        public int UpdateBook(BookViewModal updatedBook)
        {
            var book = this.libraryManagementRepository.FindBy(x => x.Id == updatedBook.Id).FirstOrDefault();
            if(book != null)
            {
                book.Author = updatedBook.Author;
                book.Title = updatedBook.Title;
                this.libraryManagementRepository.Edit(book);
                var result = this.libraryManagementRepository.Save();
                return result;
            }
            return 0;
        }

        public List<BookViewModal> GetBooks()
        {
            return this.libraryManagementRepository.GetAll().Select(x => new BookViewModal() { 
                Id = x.Id,
                Author = x.Author,
                Title = x.Title
            }).ToList();
        }
    }
}
