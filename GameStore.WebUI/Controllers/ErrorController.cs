using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HandleError]
        public ActionResult NotFound(string aspxerrorpath)
        {
            if (!string.IsNullOrWhiteSpace(aspxerrorpath))
                return RedirectToAction("NotFound");

            return View();
        }
    }
}