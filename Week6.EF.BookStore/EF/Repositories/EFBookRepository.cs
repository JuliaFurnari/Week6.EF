using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.BookStore.Core.Interfaces;
using Week6.EF.BookStore.Core.Models;

namespace Week6.EF.BookStore.EF.Repositories
{
    class EFBookRepository : IBookRepository
    {

        private readonly BookContext bookCtx;

        public EFBookRepository()
        {
            bookCtx = new BookContext();
        }

        public bool Add(Book newBook)
        {
            if (newBook == null)
                return false;

            try
            {
                bookCtx.Books.Add(newBook);
                bookCtx.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Book bookToDelete)
        {
            if (bookToDelete == null)
                return false;

            try
            {
                var book = bookCtx.Books.Find(bookToDelete.Id);

                if (book != null)
                    bookCtx.Books.Remove(book);

                bookCtx.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Book> Fetch()
        {
            try
            {
                var books = bookCtx.Books.ToList();
                return books;
            }
            catch (Exception)
            {
                return new List<Book>();
            }
        }

        public Book GetById(int id)
        {
            if (id <= 0)
                return null;

            return bookCtx.Books.Find(id);
        }

        public Book GetByISBN(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return null;

            try
            {
                var book = bookCtx.Books.Where(b => b.ISBN == isbn).FirstOrDefault();

                return book;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Update(Book updatedBook)
        {
            if (updatedBook == null)
                return false;

            try
            {
                bookCtx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
