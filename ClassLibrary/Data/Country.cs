using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Data
{
    public partial class Country
    {
        public Country()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        
        [Required]
        public string CountryLabel { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
