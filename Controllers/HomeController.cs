﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WINTEX.DAL;
using WINTEX.Infrastructure;
using WINTEX.Models;
using WINTEX.Models.ViewModels;

namespace WINTEX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("{Protocol} {Method} {Path}", Request.Protocol, Request.Method, Request.Path);
            return View();
        }

        // GET: /ListMummies
        public IActionResult ListMummies(int pageNum, int itemsPerPage = 20)
        {
            _logger.LogInformation("{Protocol} {Method} {Path} : pageNum {pageNum}, itemsPerPage {itemsPerPage}", Request.Protocol, Request.Method, Request.Path, pageNum, itemsPerPage);
            var paginator = new Paginator<Mummy>(itemsPerPage, _unitOfWork.Mummies.GetAll(m => m.PostExhumationDatum, m => m.Shaft));
            var pageModel = new List<MummyListView>();
            foreach(Mummy m in paginator.GetItems(pageNum))
            {
                pageModel.Add(m);
            }
            return View(pageModel);
        }

        [HttpGet]
        public IActionResult MummyDetails(int mummyId)
        {
            _logger.LogInformation("{Protocol} {Method} {Path} : mummyId {mummyId}", Request.Protocol, Request.Method, Request.Path, mummyId);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
