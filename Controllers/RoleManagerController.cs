using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WINTEX.Controllers
{
    ///This Controller is for managing the roles that users can be assigned
    ///it allows for new roles to be made
    public class RoleManagerController : Controller
    {
        private readonly IDiagnosticContext diagnosticContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleManagerController(IDiagnosticContext diagnosticContext, RoleManager<IdentityRole> roleManager)
        {
            this.diagnosticContext = diagnosticContext;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }
    }
}
