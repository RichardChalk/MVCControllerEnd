using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Data
{
    public partial class Customer
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(60)]
        public string Name { get; set; } = null!;

        [Required]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;

        [Range(18,100,ErrorMessage = "You have to between 18-100 years old!")]
        public int? Age { get; set; }
        
        [Required(ErrorMessage ="Please enter your birthday")]

        public DateTime Birthday { get; set; }
    }
}
