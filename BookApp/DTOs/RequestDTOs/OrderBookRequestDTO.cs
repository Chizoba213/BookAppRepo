using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.DTOs.RequestDTOs
{
    public class OrderBookRequestDTO
    {
        //[Required(ErrorMessage = "Title is required")]
        //public string Title { get; set; }

       // [Index("TitleIndex", IsUnique = true)]

        [Required(ErrorMessage = "Name is required")]
       // [Index("Email", IsUnique = true)]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string CustomerEmail { get; set; }
        //public string AuthorName { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}
