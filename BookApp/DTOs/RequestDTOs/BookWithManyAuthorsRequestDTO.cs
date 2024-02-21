using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookApp.DTOs.RequestDTOs
{
    public class BookWithManyAuthorsRequestDTO
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}
