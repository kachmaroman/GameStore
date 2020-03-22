using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
	public class Publisher
	{
		[Key]
		public Guid Id { get; set; }

		[MaxLength(50)]
		public string Name { get; set; }

		public ICollection<Game> Games { get; set; }
	}
}