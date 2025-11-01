using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
