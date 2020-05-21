using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicJournal_WEB.Controllers
{
    public class TeacherHomePageController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}