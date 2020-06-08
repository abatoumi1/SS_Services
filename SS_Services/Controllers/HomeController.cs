using Microsoft.AspNetCore.Mvc;
using SS_Services.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace SS_Services.Controllers {

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
