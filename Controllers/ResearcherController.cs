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
        public IActionResult AddMummyQuick()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View(new AddMummyViewModel());
        }

        public IActionResult AddMummyQuick(AddMummyViewModel newMummy)
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }
    }
}
