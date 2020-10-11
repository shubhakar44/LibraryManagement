using LibraryManagement.DomainModels;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.BAL
{
    public interface IBooksManager
    {
        int AddBook(BookViewModal book);

        int DeleteBook(int Id);

        int UpdateBook(BookViewModal book);

        List<BookViewModal> GetBooks();
    }
}
