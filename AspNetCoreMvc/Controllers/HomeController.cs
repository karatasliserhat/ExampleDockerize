using AspNetCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

namespace AspNetCoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileProvider _fileProvider;
        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IFileProvider fileProvider, IConfiguration configuration)
        {
            _logger = logger;
            _fileProvider = fileProvider;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.MssqlCon = _configuration["MssqlCon"];
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ImageSave()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ImageSave(IFormFile formFile)
        {
            if (formFile != null || formFile.Length > 0)
            {

                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);

                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            return View();
        }

        public IActionResult ImageShow()
        {
            var images = _fileProvider.GetDirectoryContents("wwwroot/images").ToList().Select(x => x.Name).ToList();
            return View(images);
        }

        [HttpPost]
        public IActionResult ImageShow(string name)
        {
            var deleteImage = _fileProvider.GetDirectoryContents("wwwroot/images").ToList().First(x => x.Name == name);

            System.IO.File.Delete(deleteImage.PhysicalPath);

            return RedirectToAction(nameof(ImageShow));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}