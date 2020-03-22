using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GameStore.WebUI.Extensions;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		private readonly IGameRepository _repository;

		private IDictionary<Guid, string> Categories => _repository.GetCategories();

		private IDictionary<Guid, string> Publishers => _repository.GetPublishers();

		public AdminController(IGameRepository repository)
		{
			_repository = repository;
		}

		[Route("Index")]
		public ViewResult Index()
		{
			ViewBag.Count = _repository.GetCountOfGames();
			return View(_repository.GetAllGames());
		}

		[HttpGet]
		public ViewResult Edit(Guid gameId)
		{
			Game game = _repository.GetGameById(gameId);

			GameEditViewModel model = new GameEditViewModel
			{
				Game = game,
				Categories = Categories,
				Publishers = Publishers
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind] Game game, HttpPostedFileBase gameImage, HttpPostedFileBase fonImage)
		{
			Game currentGame = _repository.GetGameById(game.GameId);

			if (ModelState.IsValid)
			{
				game.GameImage = gameImage != null ? gameImage.ToBase64() : currentGame?.GameImage;
				game.BackImage = fonImage != null ? fonImage.ToBase64() : currentGame?.BackImage;

				if (!string.IsNullOrEmpty(game.PreviewLink))
				{
					game.PreviewLink = game.PreviewLink.PreviewLink();
				}

				await _repository.UpdateGameAsync(game);
				TempData["message"] = $"Game {game.Name} was saved";

				return RedirectToAction("Index");
			}

			GameEditViewModel model = new GameEditViewModel
			{
				Game = game,
				Categories = Categories,
				Publishers = Publishers
			};

			return View(model);
		}

		[HttpGet]
		public ViewResult AddRequirements()
		{
			var games = _repository.GetAllGames().Select(x => new { Value = x.GameId.ToString(), Text = x.Name }).ToList();

			var listItems = new List<SelectListItem>();

			games.ForEach(g => listItems.Add(new SelectListItem { Value = g.Value, Text = g.Text }));

			ViewBag.Games = listItems;

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddRequirements([Bind] Requirements requirements)
		{
			if (ModelState.IsValid)
			{
				var game = _repository.GetGameById(requirements.GameId);

				await _repository.AddRequirementsAsync(requirements);
				TempData["message"] = $"Game {game?.Name} requirements was added";

				return RedirectToAction("Index");
			}

			return View(requirements);
		}

		[HttpGet]
		public ViewResult AddGame()
		{
			GameEditViewModel model = new GameEditViewModel
			{
				Game = new Game { ReleaseDate = DateTime.Now },
				Categories = Categories,
				Publishers = Publishers
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> AddGame([Bind] Game game, HttpPostedFileBase gameImage, HttpPostedFileBase fonImage)
		{
			if (ModelState.IsValid)
			{
				game.GameImage = gameImage?.ToBase64();
				game.BackImage = fonImage?.ToBase64();

				if (!string.IsNullOrEmpty(game.PreviewLink))
				{
					game.PreviewLink = game.PreviewLink.PreviewLink();
				}

				await _repository.AddGameAsync(game);
				TempData["message"] = $"Game {game.Name} was added";

				return RedirectToAction("Index");
			}

			GameEditViewModel model = new GameEditViewModel
			{
				Game = game,
				Categories = Categories,
				Publishers = Publishers
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(Guid gameId)
		{
			var deletedGame = await _repository.DeleteGameAsync(gameId);

			if (deletedGame != null)
			{
				TempData["message"] = $"Game {deletedGame.Name} was deleted";
			}

			return RedirectToAction("Index");
		}
	}
}