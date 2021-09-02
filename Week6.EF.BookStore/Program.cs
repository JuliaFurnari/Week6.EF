using System;
using Week6.EF.BookStore.Client;

namespace Week6.EF.BookStore
{
    class Program
    {
        //
        static void Main(string[] args)
        {
            //Gestione del magazzino della libreria
            //L'utente magazziniere, all'accesso al sistema, deve poter:
            //- Aggiungere un libro
            //- Eliminare un libro
            //- Visualizzare tutti i libri
            //- Aggiornare la quantità di un libro in magazzino

            //Entità:
            //Libro

            Menu.Start();

            //Menu.ShowBooks();
        }
    }
}
