using ClassLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.DTOs
{
    public partial class CustomerDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please input a name")]
        [MinLength(2, ErrorMessage = "Name must contain at least 2 letters")]
        [MaxLength(60, ErrorMessage = "Name cannot contain more than 60 letters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please choose a country")]
        public string CountryLabel { get; set; } = null!;

        [Range(18, 100, ErrorMessage = "You have to between 18-100 years old!")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Please enter your birthday")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}
