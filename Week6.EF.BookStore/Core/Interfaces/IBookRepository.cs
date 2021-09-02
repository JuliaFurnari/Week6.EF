using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.BookStore.Core.Models;

namespace Week6.EF.BookStore.Core.Interfaces
{
    interface IBookRepository : IRepository<Book>
    {
        //GetByIsbn
        Book GetByISBN(string isbn);
    }
}
