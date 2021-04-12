using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WINTEX.DAL;
using WINTEX.Models.ViewModels;

namespace WINTEX.Controllers
{
    [Authorize(Roles = "Admin, Researcher")]
    [ValidateAntiForgeryToken]
    public class ResearcherController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ResearcherController(ILogger logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // GET: /AddMummy
        [HttpGet]
        public IActionResult AddMummy()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View(new AddMummyViewModel());
        }

        public IActionResult AddMummy(AddMummyViewModel newMummy)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpGet]
        public IActionResult AddShaftLocation()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View(new AddLocation());
        }

        [HttpPost]
        public IActionResult AddShaftLocation(AddLocation newShaft)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpGet]
        public IActionResult AddTombLocation()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View(new AddTomb());
        }

        [HttpPost]
        public IActionResult AddTombLocation(AddTomb newTomb)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpGet]
        public IActionResult EditMummy()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpPost]
        public IActionResult EditMummy(object editMummy)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpGet]
        public IActionResult DeleteMummy()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpPost]
        public IActionResult DeleteMummy(object deleteMummy)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }
    }
}
