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
        public ActionResult AddMummy()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View(new AddMummyViewModel());
        }

        public ActionResult AddMummy(AddMummyViewModel newMummy)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpGet]
        public ActionResult AddShaftLocation()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View(new AddLocation());
        }

        [HttpPost]
        public ActionResult AddShaftLocation(AddLocation newShaft)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpGet]
        public ActionResult AddTombLocation()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View(new AddTomb());
        }

        [HttpPost]
        public ActionResult AddTombLocation(AddTomb newTomb)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpGet]
        public ActionResult EditMummy()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpPost]
        public ActionResult EditMummy(object editMummy)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpGet]
        public ActionResult DeleteMummy()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        [HttpPost]
        public ActionResult DeleteMummy(object deleteMummy)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }
    }
}
