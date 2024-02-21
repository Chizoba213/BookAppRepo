using BookApp.Constants.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookApp.DTOs.RequestDTOs
{
    public class AuthorRequestDTO
    {
        [Required(ErrorMessage = "Author name is required")]
        [MaxLength (15)]
        public string AuthorName { get; set; }
        //[RegularExpression("M|F")]
        [EnumDataType(typeof(GenderEnum))]
        public string Gender { get; set; }
        public int[] Book { get; set; }
        public List<BookRequestDTO> BookRequestDTOs { get; set; } 
      // public int[] BookIds { get; set; }

    }
}
