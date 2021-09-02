using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6.EF.BookStore.Core.Models;
using Week6.EF.BookStore.EF.Repositories;

namespace Week6.EF.BookStore.Client
{
    public class Menu
    {
        private static MainBL mainBL = new MainBL(new EFBookRepository(), new EFShelfRepository());

        internal static void Start()
        {
            Console.WriteLine("Benvenuto!");

            char choice;

            do
            {
                Console.WriteLine("Premi 1 per aggiungere un libro");
                Console.WriteLine("Premi 2 per eliminare un libro");
                Console.WriteLine("Premi 3 per visualizzare tutti i libri in magazzino");
                Console.WriteLine("Premi 4 per modificare quantità di un libro in magazzino");
                Console.WriteLine("Premi 5 per visualizzare tutti i libri di uno scaffale");

                Console.WriteLine("Premi Q per uscire");

                choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        //Aggiungi libro
                        AddNewBook();
                        Console.WriteLine();
                        break;
                    case '2':
                        //elimina libro
                        DeleteBook();
                        Console.WriteLine();
                        break;
                    case '3':
                        //visualizzare tutti i libri
                        ShowBooks();
                        Console.WriteLine();
                        break;
                    case '4':
                        //modificare la quantità di un libro
                        UpdateQuantity();
                        Console.WriteLine();
                        break;
                    case '5':
                        //visualizzare i libri di uno scaffale
                        
                        ShowBooksInShelf();
                        Console.WriteLine();
                        break;
                    case 'Q':
                        return;
                    default:
                        Console.WriteLine("Scelta non disponibile");
                        break;
                }
            }
            while (!(choice == 'Q'));
        }

        private static void ShowBooksInShelf()
        {
            Console.WriteLine("Digita il codice dello scaffale di cui vuoi visualizzare i libri.");
            string codeShelf = Console.ReadLine();
            while(codeShelf == string.Empty)
            {
                Console.Write("Stringa vuota. Digita una stringa di caratteri.");
                codeShelf = Console.ReadLine();
            }
            try
            {
                Shelf shelf = mainBL.GetByCode(codeShelf);
                List<Book> books = shelf.Books;
                foreach(var b in books)
                {
                    Console.WriteLine($"ISBN: {b.ISBN} Titolo: {b.Title} " +
                         $"Autore: {b.Author} Quantità: {b.Quantity}");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Il codice inserito non corrisponde a nessun scaffale.");
            }

        }

        private static void ShowBooks()
        {
            var books = mainBL.FetchBooks();

            if (books.Count != 0)
            {
                Console.WriteLine("Libri in magazzino:");
                foreach (var b in books)
                {
                    Console.WriteLine($"ISBN: {b.ISBN} Titolo: {b.Title} " +
                        $"Autore: {b.Author} Quantità: {b.Quantity}");
                }
            }
            else
            {
                Console.WriteLine("\nNon ci sono Libri in magazzino");
            }
        }

        private static void UpdateQuantity()
        {
            string isbn;

            Console.WriteLine("Digita il codice ISBN del libro di cui vuoi modificare la quantità");
            ShowBooks();

            do
            {
                Console.Write("Inserisci il codice ISBN di 10 cifre:");
                isbn = Console.ReadLine();
            }
            while (isbn.Length != 10);

            var bookToUpdate = GetBookByISBN(isbn);

            if (bookToUpdate != null)
            {
                Console.Write("Inserisci la quantità:");

                int quantity = 0;
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
                {
                    Console.WriteLine("Devi inserire un valore valido");
                }

                bookToUpdate.Quantity = quantity;
                mainBL.UpdateBook(bookToUpdate);
            }
            else
            {
                Console.WriteLine("Non c'è un libro in magazzino con questo codice ISBN");
            }
        }

        private static void DeleteBook()
        {
            string isbn;
            Console.WriteLine("Digita il codice ISBN del libro che vuoi eliminare");
            ShowBooks();

            do
            {
                Console.Write("Inserisci il codice ISBN di 10 cifre:");
                isbn = Console.ReadLine();
            }
            while (isbn.Length != 10);

            var bookToDelete = GetBookByISBN(isbn);

            if (bookToDelete != null)
            {
                mainBL.DeleteBook(bookToDelete);
            }
            else
            {
                Console.WriteLine("Non c'è un libro in magazzino con questo codice ISBN");
            }
        }

        private static Book GetBookByISBN(string isbn)
        {
            var book = mainBL.GetByIsbn(isbn);

            return book;
        }

        public static void AddNewBook()
        {
            //interazione con utente
            string title, author;
            string isbn;

            do
            {
                Console.Write("Inserisci il codice ISBN di 10 cifre:");
                isbn = Console.ReadLine();
            }
            while (isbn.Length != 10);

            if (GetBookByISBN(isbn) == null)
            {
                do
                {
                    Console.Write("Inserisci il titolo:");
                    title = Console.ReadLine();
                }
                while (title.Length == 0);

                do
                {
                    Console.Write("Inserisci l'autore:");
                    author = Console.ReadLine();
                } while (author.Length == 0);

                Console.Write("Inserisci la quantità che sarà disponibile in magazzino:");
                int quantity = 0;
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
                {
                    Console.WriteLine("Devi inserire un valore valido");

                }

                Shelf shelf;
                do
                {
                    Console.WriteLine("Inserisci il codice dello scaffale in cui posizionare il libro.");
                    ShowSelves();
                    string code = Console.ReadLine();

                    //Recupero lo scaffale con il codice inserito
                    //Se esiste, ok. Altrimenti mi richiede di inserire il codice.
                    shelf = GetShelfByCode(code);
                } while (shelf == null);

                Book newBook = new Book
                {
                    ISBN = isbn,
                    Title = title,
                    Author = author,
                    Quantity = quantity,
                };

                mainBL.AddBook(newBook);

                Console.WriteLine($"Libro aggiunto. ISBN: {newBook.ISBN} Titolo: {newBook.Title} Autore: {newBook.Author} Quantità: {newBook.Quantity}");
            }
            else
            {
                Console.WriteLine("Esiste già un libro con questo ISBN in magazzino");
            }
        }

        private static Shelf GetShelfByCode(string code)
        {
            var shelf = mainBL.GetByCode(code);

            return shelf;
        }

        private static void ShowSelves()
        {
            //recupera dati dagli scaffali dal db
            var shelves = mainBL.FetchShelves();
            if (shelves.Count != 0)
            {
                Console.WriteLine("Scaffali");
                foreach (var s in shelves)
                {
                    Console.WriteLine(s.Code);
                }
            }
            else Console.WriteLine("Non ci sono scaffali.");
            }
        
    }

}
