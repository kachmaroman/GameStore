using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace GameStore.Domain.Entities
{
    public class Customer
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerId { get; set; }

		[MaxLength(50)]
        [Required(ErrorMessage = "Please, enter your full name")]
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Please, enter your email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

        [Required]
		[DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
		public string Phone { get; set; }

        public ICollection<Order> Orders { get; set; } 
    }
}