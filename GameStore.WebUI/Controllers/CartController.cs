using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using GameStore.Domain.Entities;
using GameStore.Domain.Abstract;
using GameStore.Domain.Concrete;

namespace GameStore.WebUI.Controllers
{
	public class CartController : Controller
	{
		private readonly IGameRepository _repository;

		public CartController(IGameRepository repository) => _repository = repository;

		[Route("Checkout")]
		public ViewResult Checkout() => View(new Customer());

		[Route("Checkout")]
		[HttpPost]
		public async Task<ViewResult> Checkout(Cart cart, Customer customer)
		{
			if (!cart.Lines.Any())
			{
				ModelState.AddModelError("", @"Sorry, your cart list is empty!");
			}

			if (cart.Lines.Any(x => x.Game.Quantity < x.Quantity))
			{
				var games = cart.Lines.Where(x => x.Game.Quantity < x.Quantity);

				ModelState.AddModelError("", @"Sorry, You can buy only: ");

				foreach (CartLine line in games)
				{
					ModelState.AddModelError("", $@"{line.Game.Quantity} {(line.Game.Quantity == 1 ? "item" : "items")} of {line.Game.Name}");
				}
			}

			if (ModelState.IsValid)
			{
				WebMail.SmtpServer = EmailInfo.ServerName;
				WebMail.SmtpPort = EmailInfo.ServerPort;
				WebMail.SmtpUseDefaultCredentials = EmailInfo.SmtpUseCredentials;
				WebMail.EnableSsl = EmailInfo.UseSsl;
				WebMail.UserName = EmailInfo.Username;
				WebMail.Password = EmailInfo.Password;
				WebMail.From = EmailInfo.MailFromAddress;

				var toMail = EmailInfo.MailToAddress;
				var subject = EmailInfo.Subject;
				var body = EmailInfo.GetMessage(cart, customer);
				var fromMail = EmailInfo.MailFromAddress;

				try
				{
					WebMail.Send(toMail, subject, body, fromMail);
					ViewBag.Status = "Order Sent Successfully.";

					await _repository.AddOrderAsync(customer, cart);
				}
				catch
				{
					ViewBag.Status = "Problem while processing order. Please try again.";
				}

				return View("Completed");
			}

			return View(customer);
		}

		[Route("Cart")]
		public ViewResult List(Cart cart) => View(cart);

		public PartialViewResult Table(Cart cart) => PartialView(cart);

		public PartialViewResult Summary(Cart cart) => PartialView(cart);

		[HttpPost]
		public JsonResult AddToCart(Cart cart, Guid gameId)
		{
			Game game = _repository.GetGameById(gameId);

			if (game != null)
			{
				cart.AddItem(game, 1);
			}

			return Json(JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public JsonResult RemoveFromCart(Cart cart, Guid gameId)
		{
			var game = _repository.GetGameById(gameId);

			if (game != null)
				cart.RemoveLine(game);

			return Json(JsonRequestBehavior.AllowGet);
		}
	}
}