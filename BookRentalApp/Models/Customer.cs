using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress, RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }

}
