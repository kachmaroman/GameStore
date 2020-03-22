using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace GameStore.Domain.Entities
{
    public class Administrator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AdministratorId { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Username is required")]
        public string Login { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
