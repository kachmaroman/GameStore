using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace GameStore.Domain.Entities
{
    public class Order
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Game Game { get; set; }

        public Guid GameId { get; set; }

        public Customer Customer { get; set; }

        public Guid CustomerId { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
