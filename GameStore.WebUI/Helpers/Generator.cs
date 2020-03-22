using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Helpers
{
	public static class Generator
	{
		public static int Generate()
		{
			return Convert.ToInt32(DateTime.Now.ToFileTime().ToString().Substring(10));
		}
	}
}