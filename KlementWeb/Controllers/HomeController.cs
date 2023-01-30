using KlementWeb.Business.Interfaces;
using KlementWeb.Classes;
using KlementWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace KlementWeb.Controllers
{
    [ExceptionsToMessageFilter]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
