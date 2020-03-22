using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Domain.Entities
{
	public class Game
	{
		[HiddenInput(DisplayValue = false)]
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid GameId { get; set; } = Guid.NewGuid();

		[Display(Name = "Name")]
		[MaxLength(100)]
		[Required(ErrorMessage = "Please, enter a game name")]
		public string Name { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Description")]
		[Required(ErrorMessage = "Please, enter the description")]
		public string Description { get; set; }

		public Guid CategoryId { get; set; }
		public Category Category { get; set; }

		[Display(Name = "Price")]
		[Required]
		[DataType(DataType.Currency)]
		[Range(0.01, double.MaxValue, ErrorMessage = "Please, enter correct price")]
		public decimal Price { get; set; }

		public Guid PublisherId { get; set; }
		public Publisher Publisher { get; set; }


		[DataType(DataType.Date)]
		[Display(Name = "Release Date")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Required(ErrorMessage = "Please, enter release date")]
		public DateTime ReleaseDate { get; set; }

		public string GameImage { get; set; }

		public string BackImage { get; set; }

		public string PreviewLink { get; set; }

		[Required]
		[Range(0, 500, ErrorMessage = "Quantity must be between 0 and 500")]
		public int Quantity { get; set; }

		[Index(IsUnique = true, IsClustered = false)]
		public int ShortId { get; set; } = Convert.ToInt32(DateTime.Now.ToFileTime().ToString().Substring(10));

		public ICollection<Order> Orders { get; set; }
	}
}
