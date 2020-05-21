using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicJournal_WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicJournal_WEB.Controllers
{
    public class StudentHomePageController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.AccessLevelId = UserSession.AccessLevelId;

            return View();
        }
    }
}