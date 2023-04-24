using ClassLibrary.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Tenta.Models.Customer
{
    public class CustomerVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please input a name")]
        [MinLength(2, ErrorMessage = "Name must contain at least 2 letters")]
        [MaxLength(60, ErrorMessage = "Name cannot contain more than 60 letters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please choose a country")]
        public string CountryLabel { get; set; } = null!;
        public List<SelectListItem>? Countries { get; set; }

        [Range(18, 100, ErrorMessage = "You have to between 18-100 years old!")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Please enter your birthday")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}
