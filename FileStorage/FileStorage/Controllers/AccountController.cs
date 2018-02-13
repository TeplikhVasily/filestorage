using DbStorage.NHibernate.Repositiry;
using DbStorage.Repository;
using FileStorage.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace FileStorage.Controllers
{
    public class AccountController : Controller
    {
        #region Protected Members

        protected IUserRepository UserRepository { get; set; }

        #endregion

        public AccountController()
        {
            UserRepository = new NHUserRepository();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (model.Password == "12345678")
            {
                ModelState.AddModelError("password", "Слишком простой пароль");
                return View();
            }

            if (UserRepository.Check(model.Login, model.Password))
            {
                // аутентификация
                FormsAuthentication.SetAuthCookie(model.Login, true);
                return RedirectToAction("Documents", "Document");
            }

            return View();
        }
        //выход из записи
        public void Logoff()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}