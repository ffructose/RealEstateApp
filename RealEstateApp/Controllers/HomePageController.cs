using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
