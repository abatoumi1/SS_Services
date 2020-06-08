using Microsoft.AspNetCore.Mvc;
using SportsServices.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace SportsServices.Controllers {

    [EnableCors("AllowAnyOrigin")]
    public class HomeController : Controller {
        private DataContext context;

        public HomeController(DataContext ctx) {
            context = ctx;
        }

        public IActionResult Index() {
            ViewBag.Message = "Sports Store App";
            return View(context.Products.First());
        }

        [Authorize]
        public string Protected() {
            return "You have been authenticated";
        }
    }
}
