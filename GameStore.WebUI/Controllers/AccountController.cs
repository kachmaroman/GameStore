using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using GameStore.Domain.Abstract;
using GameStore.Domain.Concrete;
using GameStore.Domain.Entities;

namespace GameStore.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountRepository _repository;

		public AccountController(IAccountRepository repository)
		{
			_repository = repository;
		}

		[Route("Login")]
		public ViewResult Login() => View();

		[Route("Login")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(Administrator model)
		{
			if (ModelState.IsValid)
			{
				string password = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "SHA1");

				Administrator admin = await _repository.LoginAsync(model, password);

				if (admin != null)
				{
					FormsAuthentication.SetAuthCookie(model.Login, false);

					return Redirect(Url.Action("Index", "Admin"));
				}

				ModelState.AddModelError("", @"Invalid login or password");
			}

			return View(model);
		}

		[Route("Logout")]
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();

			return RedirectToAction("Login");
		}

		[Route("Register")]
		[HttpGet]
		public ViewResult Register() => View();

		[Route("Register")]
		[HttpPost]
		public async Task<ActionResult> Register([Bind] Administrator administrator)
		{
			if (ModelState.IsValid)
			{
				string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(administrator.Password, "SHA1");

				Administrator admin = new Administrator
				{
					Login = administrator.Login,
					Password = pass
				};

				bool isAdminExist = await _repository.IsAdminExistsAsync(admin);

				if (isAdminExist)
				{
					ModelState.AddModelError("", @"An admin for that e-mail address already exists. Please enter a different e-mail address.");
					return View(administrator);
				}

				await _repository.AddAdminAsync(admin);


				return Redirect(Url.Action("Login"));
			}

			return View(administrator);
		}
	}
}