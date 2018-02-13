using DbStorage.Models;
using DbStorage.NHibernate.Repositiry;
using DbStorage.Repository;
using FileStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileStorage.Controllers
{
    public class CreateUserController : Controller
    {
        protected IUserRepository UserRepository { get; set; }

        public CreateUserController()
        {
            UserRepository = new NHUserRepository();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var nonUniq = UserRepository.GetAll()
                    .Any(u => u.Login == model.Login || u.Email == model.Login);

                if (nonUniq)
                {
                    ModelState.AddModelError("", "Такой логин уже занят");
                    return View();
                }

                var user = new User()
                {
                    FirstName = model.Firstname,
                    LastName = model.Lastname,
                    Email = model.Email,
                    Login = model.Login,
                    Password = model.Password,
                    Status = UserStatus.Active
                };
                UserRepository.Save(user);

                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}