using DataApp.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ServiceApp.IServices;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEducationDataService _educationDataService;
        private readonly IConfiguration _config;

        public HomeController(IEducationDataService educationDataService, IConfiguration config)
        {
            _educationDataService = educationDataService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetPartialViewShowDiem(string? SBDOrMaHS)
        {
            var year = _config.GetValue<int>("DefaultYear");
            return PartialView("_ShowDiemPartialview", await _educationDataService.LookUpScoreInformationBySBDOrMaHSAsync(SBDOrMaHS, year));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
