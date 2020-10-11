using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Repositories
{
    public class LibraryManagementRepository<T> : GenericRepository<T> , ILibraryManagementRepository<T>  where T : class, new()
    {
        public LibraryManagementRepository(ApplicationDbContext dbContext) : base(dbContext){
            
        }
    }
}
