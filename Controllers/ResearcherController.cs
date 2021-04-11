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

        [HttpGet]
        public ActionResult AddShaftLocation()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View(new AddLocation());
        }

        [HttpPost]
        public ActionResult AddShaftLocations(AddLocation newShaft)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        public ActionResult AddTombLocation()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View(new AddTomb());
        }
    }
}
