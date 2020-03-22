using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Domain.Entities;

namespace GameStore.WebUI.Models
{
	public class GameInfoViewModel
	{
		public Game Game { get; set; }
		public Requirements Requirements { get; set; }
		public string Publisher { get; set; }
	}
}