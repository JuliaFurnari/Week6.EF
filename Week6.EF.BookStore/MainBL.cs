using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.BookStore.Core.Interfaces;
using Week6.EF.BookStore.Core.Models;

namespace Week6.EF.BookStore
{
    class MainBL //business layer più o meno
    {
        private IBookRepository _bookRepo;
        private IShelfRepository _shelfRepo;
        public MainBL(IBookRepository bookRepository, IShelfRepository shelfRepository)
        {
            _bookRepo = bookRepository;
            _shelfRepo = shelfRepository;
        }

        internal void AddBook(Book newBook)
        {
            //validazione
            if (newBook == null) throw new ArgumentNullException();

            _bookRepo.Add(newBook);
        }

        internal Book GetByIsbn(string isbn)
        {
            //validazione 
            if (string.IsNullOrEmpty(isbn)) throw new ArgumentNullException();

            var book = _bookRepo.GetByISBN(isbn);
            return book;
        }

        internal List<Book> FetchBooks()
        {
           var books = _bookRepo.Fetch();
            return books;
        }

        internal void DeleteBook(Book bookToDelete)
        {
            //validazione
            if (bookToDelete == null) throw new ArgumentNullException();

            _bookRepo.Delete(bookToDelete);
        }

        internal void UpdateBook(Book bookToUpdate)
        {
            //validazione input
            if (bookToUpdate == null) throw new ArgumentNullException();

            _bookRepo.Update(bookToUpdate);
        }

        internal List<Shelf> FetchShelves()
        {
            return _shelfRepo.Fetch();
        }

        internal Shelf GetByCode(string code)
        {
            //validazione
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException();

            var shelf = _shelfRepo.GetByCode(code);

            return shelf;
        }
    }
}
