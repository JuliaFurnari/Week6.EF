using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.BookStore.Core.Interfaces;
using Week6.EF.BookStore.Core.Models;

namespace Week6.EF.BookStore.EF.Repositories
{
    class EFShelfRepository : IShelfRepository
    {
        private readonly BookContext ctx;
        public EFShelfRepository()
        {
            ctx = new BookContext();
        }
        public bool Add(Shelf item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Shelf item)
        {
            throw new NotImplementedException();
        }

        public List<Shelf> Fetch()
        {
            throw new NotImplementedException();
        }

        public Shelf GetByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            try
            {
                var shelf = ctx.Shelves.Where(s => s.Code == code).FirstOrDefault();
                var books = ctx.Books.Include(b => b.Shelf).ToList();
                return shelf;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Funzione che ritorna la lista di libri in uno scaffale

        public List<Book> BooksInShelf(Shelf s)
        {

        }

        public Shelf GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Shelf item)
        {
            throw new NotImplementedException();
        }
    }
}
