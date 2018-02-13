using DbStorage.Models;
using DbStorage.NHibernate.Repositiry;
using DbStorage.Repository;
using FileStorage.Models;
using System.Linq;
using System.Web.Mvc;

namespace FileStorage.Controllers
{

    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        protected IUserRepository UserRepository { get; set; }

        public AdminController()
        {
            UserRepository = new NHUserRepository();
        }

        // GET: Admin
        public ActionResult Index()
        {
            var users = UserRepository.GetAll()
                .Where(u => u.Status != UserStatus.Deleted);
            return View(users);
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

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Ban(long id)
        {
            var user = UserRepository.Get(id);
            if (user == null)
                return new JsonResult()
                {
                    Data = new
                    {
                        success = false,
                        error = "Операция не возможна"
                    }
                };

            user.Status = user.Status == UserStatus.Active
                ? UserStatus.Blocked
                : UserStatus.Active;
            UserRepository.Save(user);

            return new JsonResult()
            {
                Data = new
                {
                    success = true,
                    message = user.Status == UserStatus.Active ? "Забанить" : "Разбанить"
                }
            };
        }

        public ActionResult Delete(long id)
        {
            var user = UserRepository.Get(id);
            if (user == null)
                return RedirectToAction("Index");

            user.Status = UserStatus.Deleted;
            UserRepository.Save(user);

            return RedirectToAction("Index");
        }
    }
}