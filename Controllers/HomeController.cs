using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WINTEX.DAL;
using WINTEX.Infrastructure;
using WINTEX.Models;
using WINTEX.Models.ViewModels;

namespace WINTEX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private FEGBExcavationContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, FEGBExcavationContext context)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NavigationPage()
        {
            return View();
        }
        


        // GET: /ListMummies

       /*

        [HttpGet]
        public IActionResult BurialDetails(int mummyId)
        {
            _logger.LogInformation("{Protocol} {Method} {Path} : mummyId {mummyId}", Request.Protocol, Request.Method, Request.Path, mummyId);
            return View();
        }
       */


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }








        public IActionResult Privacy()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }
    }
}
