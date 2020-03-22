using System.Linq;
using System.Web.Mvc;
using GameStore.Domain.Abstract;
namespace GameStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameRepository _repository;

        public HomeController(IGameRepository repository)
        {
	        _repository = repository;
        }

        public ViewResult Index()
        {
			return View(_repository.GetComingSoonGames().ToList());
        }

        [Route("About")]
        public ViewResult About()
        {
			return View();
		}
    }
}