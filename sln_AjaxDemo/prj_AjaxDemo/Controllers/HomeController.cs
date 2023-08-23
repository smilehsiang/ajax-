using Microsoft.AspNetCore.Mvc;
using prj_AjaxDemo.Models;
using System.Diagnostics;

namespace prj_AjaxDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Partial1()
        {
            
            return PartialView();
        }
        public IActionResult Partial2()
        {
            ViewBag.message = "來自Partial2的內容";
            return PartialView();
        }
        public IActionResult jquery()
        { 
        return View();
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult First()
        {
            return View();
        }

        public IActionResult GetDemo()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }

        public IActionResult Promise()
        {
            return View();
        }

        public IActionResult Fetch()
        {
            return View();
        }


        public IActionResult Travel()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}