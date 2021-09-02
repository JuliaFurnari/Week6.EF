using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.EF.BookStore.Core.Models
{
    class Shelf
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
