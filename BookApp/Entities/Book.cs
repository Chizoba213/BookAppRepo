using System.Collections.Generic;

namespace BookApp.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        // public int AuthorId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        // public ICollection<Author> Author { get; set; }
        public List <User> Users { get; set;}
        //public List <Author> Authors { get; set; }
        public int SoldCount { get; set; } = 0;

    } 
}
