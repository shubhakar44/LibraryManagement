using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.DAL.Repositories
{
    public interface ILibraryManagementRepository<T> : IGenericRepository<T> where T : class
    {
    }
}
