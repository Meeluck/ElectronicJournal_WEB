using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicJournal_Library;
using ElectronicJournal_WEB.Context;
using ElectronicJournal_WEB.Models;
using ElectronicJournal_WEB.Models.DatabaseModel;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicJournal_WEB.Controllers
{
    public class AuthorizationController : Controller
    {
        ElectronicalJournalContext db;
        Password _password;
        Users _checkUser;

        public AuthorizationController()
        {
            db = new ElectronicalJournalContext();
            _password = new Password();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Authorization()
        {
            return Redirect("/Authorization/Index");
        }

        [HttpPost]
        public IActionResult Authorization(string login,string password)
        {

            var user = from us in db.Users
                       where us.Login == login
                       select new Users
                       {
                           UserId = us.UserId,
                           Login = us.Login,
                           AccessLevelId = us.AccessLevelId,
                           PasswordHash = us.PasswordHash,
                           PasswordSalt = us.PasswordSalt
                       };

            foreach(Users item in user)
                _checkUser = item;

            if (_checkUser == null)
            {
                ViewBag.ErrMessage = "Пользователь с таким логином не найден";
                return View();
            }

            //доступ для пользоавтелей закрыт
            if (_checkUser.AccessLevelId > 3)
            {
                ViewBag.ErrMessage = "В доступе отказано";
                return View();
            }

            _password.PasswordHash = _checkUser.PasswordHash;
            _password.PasswordSalt = _checkUser.PasswordSalt;

            if (!_password.ComparePassword(password))
            {
                ViewBag.ErrMessage = "Неверный пароль";
                return View();
            }

            UserSession.Instance(_checkUser.UserId);

            switch (_checkUser.AccessLevelId)
            {
                case 1:
                    return Redirect("~/StudentHomePage/Index");
                case 2:
                    return Redirect("~/StudentHomePage/Index");
                case 3:
                    return Redirect("~/TeacherHomePage/Index");
                default:
                    break;
            }
            return View();
        }

    }
}