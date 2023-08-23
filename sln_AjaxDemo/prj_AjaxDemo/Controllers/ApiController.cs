using Microsoft.AspNetCore.Mvc;
using prj_AjaxDemo.Models;

namespace prj_AjaxDemo.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;
        public ApiController(DemoContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        public IActionResult Index()
        {
            //  System.Threading.Thread.Sleep(5000); //睡5秒鐘，再往下執行
            return Content("Hello Fetch!!");
        }

        public IActionResult GetDemo(UserViewModel user)  //public IActionResult GetDemo(string name, int age = 30)
        {
            if (string.IsNullOrEmpty(user.userName))
            {
                user.userName = "guest";
            }
            return Content($"Hello {user.userName}, You are {user.userage} years old.");
        }

        public IActionResult Register(Members member, IFormFile file)
        {

            //return Content($"{_host.ContentRootPath}");  //C:\shared\Ajax\MSIT150Site\
            //   return Content($"{_host.WebRootPath}");  //C:\shared\Ajax\MSIT150Site\wwwroot
            string filePath = Path.Combine(_host.WebRootPath, "uploads", file.FileName); //C:\shared\Ajax\MSIT150Site\wwwroot\uploads\walk.gif

            //上傳檔案到uploads資料夾中
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            //將圖轉成二進位
            byte[]? imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }

            //    return Content($"上傳檔案到 {filePath}");

            //  return Content($"{file.FileName} - {file.Length} - {file.ContentType}");

            member.FileName = file.FileName;
            member.FileData = imgByte;
            _context.Members.Add(member);
            _context.SaveChanges();

            return Content("新增成功!!");
        }

        public IActionResult GetImageByte(int id = 1)
        {
            Members? member = _context.Members.Find(id);
            byte[]? img = member.FileData;
            return File(img, "image/jpeg");

        }

        //回傳城市的JSON資料

        public IActionResult Cities()
        {
            var cities = _context.Address.Select(c => c.City).Distinct();
            //var cities = _context.Address.Select(c => new
            //{
            //    c.City
            //}).Distinct();
            return Json(cities);
        }

        //根據城市名稱，回傳城市的鄉鎮區JSON資料
        public IActionResult Districts(string city)
        {
            var districts = _context.Address.Where(a => a.City == city).Select(c => c.SiteId).Distinct();

            return Json(districts);
        }

        //根據鄉鎮區名稱，回傳鄉鎮區的路名JSON資料
        public IActionResult Roads(string siteId)
        {
            var roads = _context.Address.Where(a => a.SiteId == siteId).Select(c => c.Road).Distinct();

            return Json(roads);
        }
    }
}
