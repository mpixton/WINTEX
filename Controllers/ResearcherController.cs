using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WINTEX.Controllers
{
    public class ResearcherController : Controller
    {
        // GET: /AddMummy
        [HttpGet]
        public ActionResult AddMummy()
        {
            return View();
        }
    }
}
