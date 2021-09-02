using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week6.EF.BookStore.Core.Models
{
    class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(10), MinLength(10)]
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }

        public Shelf Shelf { get; set; }
        public int ShelfId { get; set; }

        //public Book (int id, string isbn, string title, string author, int quantity)
        //{
        //    Id = id;
        //    ISBN = isbn;
        //    Title = title;
        //    Author = author;
        //    Quantity = quantity;
        //}

        //public Book()
        //{

        //}
    }
}
