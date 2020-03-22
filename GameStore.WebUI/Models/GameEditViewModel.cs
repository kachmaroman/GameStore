using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Domain.Entities;

namespace GameStore.WebUI.Models
{
	public class GameEditViewModel
	{
		public Game Game { get; set; }
		public IDictionary<Guid, string> Publishers { get; set; }
		public IDictionary<Guid, string> Categories { get; set; }
	}
}