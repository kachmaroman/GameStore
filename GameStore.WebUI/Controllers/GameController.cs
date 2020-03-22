using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Models;
using PagedList;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {       
        private const int PageSize = 10;
        private readonly IGameRepository _repository;

        public IEnumerable<string> Categories { get; }

        public GameController(IGameRepository repository)
        {
            _repository = repository;
            Categories = repository.GetCategoryNames();
        }
        
        [Route("Games")]
        public ViewResult Catalog(string name, string genre, string sortBy, int page = 1)
        {
	        List<Game> games = _repository.GetReleasedGames().ToList();

			if (!string.IsNullOrEmpty(genre))
            {
                genre = genre.ToLower();
                games = _repository.GetByCategory(genre).ToList();
            }
                
            if (!string.IsNullOrEmpty(name))
            {
                games = _repository.GetGamesByName(name).ToList();
                ViewBag.Name = name;
                ViewBag.Count = games.Count;
            }

            switch (sortBy)
            {
                case "price-asc":
                    games = games.OrderBy(x => x.Price).ToList();
                    break;
                case "price-desc":
                    games = games.OrderByDescending(x => x.Price).ToList();
                    break;
                case "date-asc":
	                games = games.OrderBy(x => x.ReleaseDate).ToList();
                    break;
                case "date-desc":
	                games = games.OrderByDescending(x => x.ReleaseDate).ToList();
                    break;
                case "title-asc":
	                games = games.OrderBy(x => x.Name).ToList();
                    break;
                case "title-desc":
	                games = games.OrderByDescending(x => x.Name).ToList();
                    break;
            }

            var model = new GamesListViewModel
            {
                PagedGames = games.ToPagedList(page, PageSize),
                Categories = Categories,
                Name = name,
                Genre = genre,
                SortBy = sortBy
            };

            return View(model);
        }

        public ActionResult AutocompleteSearch(string term = "")
        {
            term = term.Trim();

            if (string.IsNullOrEmpty(term))
                return Json(null);

            var models = _repository.GetGamesByName(term)
                .Select(x => new {id = x.ShortId, name = x.Name});

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        [Route("Games/Info/{shortId}")]
        public ActionResult Info(int shortId)
        {
            Game game = _repository.GetGameByShortId(shortId);
            
            if (game == null)
            {
	            return RedirectToAction("NotFound", "Error");
            }

            game.Publisher = _repository.GetPublisherById(game.PublisherId);
            game.Category = _repository.GetCategoryById(game.CategoryId);
            var requirements = _repository.GetRequirementsByGameId(game.GameId);

			ViewBag.Requirements = requirements;

			return View (game);
        }
    }
}