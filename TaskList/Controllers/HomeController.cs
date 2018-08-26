using System.Linq;
using System.Web.Mvc;
using TaskList.Data;
using TaskList.Models;

namespace TaskList.Controllers
{
    public class HomeController : Controller
    {
        private TaskListContext db = new TaskListContext();

        public ActionResult Index(string alert)
        {
            ViewBag.Message = alert;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login)
        {
            var user = db.Users
                    .Where(p => p.Email == login.Email)
                    .Select(p => new { p.Id, p.Password, p.Email });

            foreach (var column in user)
            {
                if (login.Password == column.Password || login.Email == column.Email)
                {
                    int id = column.Id;
                    return RedirectToAction("Index", "Tasks", new { id });
                }
            }
            string alert = "The username or password is incorrect. Please try again!";
            return RedirectToAction("Index", "Home", new { alert });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}