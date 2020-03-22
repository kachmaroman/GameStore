using System;
using System.IO;
using System.Web;

namespace GameStore.WebUI.Extensions
{
	public static class GameExtensions
	{
		public static string PreviewLink(this string link)
		{
			int index = link.LastIndexOf("=", StringComparison.Ordinal);

			return link.Substring(++index);
		}

		public static string ToBase64(this HttpPostedFileBase image)
		{
			byte[] data;

			using (BinaryReader br = new BinaryReader(image.InputStream))
			{
				data = br.ReadBytes(image.ContentLength);
			}

			var base64 = Convert.ToBase64String(data);
			var imageSrc = $"data:image/{Path.GetExtension(image.FileName)};base64,{base64}";

			return imageSrc;
		}
	}
}