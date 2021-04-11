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
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        public IActionResult ListBurials()
        {
            return View(_context);
        }

        public IActionResult BurialDetails()
        {
            return View(_context.Mummies);
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        public IActionResult ListMummies(int pageNum, int itemsPerPage = 20)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            var mummyList = _unitOfWork.Mummies.GetAll(m => m.PostExhumationDatum, m => m.Shaft).ToList().Cast<MummyListView>();
            var pageModel = new Paginator<MummyListView>(itemsPerPage, mummyList);
            return View(pageModel.GetItems(pageNum));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
