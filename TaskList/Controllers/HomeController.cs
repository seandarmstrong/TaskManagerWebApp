using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskList.Data;
using TaskList.Helpers;
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

        int ID = 0;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login)
        {
            var user = db.Users
                    .Where(p => p.Email == login.Email)
                    .Select(p => new { p.Id, p.Password, p.Email, p.FirstName });

            foreach (var column in user)
            {
                if (login.Password == column.Password && login.Email == column.Email)
                {
                    HttpCookie userIdCookie;
                    if (Request.Cookies[Constant.UserIdCookie] == null)
                    {
                        userIdCookie = new HttpCookie(Constant.UserIdCookie);
                        userIdCookie.Value = "0";
                        userIdCookie.Expires = DateTime.UtcNow.AddYears(1);
                    }
                    else
                    {
                        userIdCookie = Request.Cookies[Constant.UserIdCookie];
                    }

                    ID = column.Id;
                    userIdCookie.Value = ID.ToString();
                    Response.Cookies.Add(userIdCookie);

                    HttpCookie firstNameCookie;
                    if (Request.Cookies[Constant.FirstNameCookie] == null)
                    {
                        firstNameCookie = new HttpCookie(Constant.FirstNameCookie);
                        firstNameCookie.Value = "";
                        firstNameCookie.Expires = DateTime.UtcNow.AddYears(1);
                    }
                    else
                    {
                        firstNameCookie = Request.Cookies[Constant.FirstNameCookie];
                    }

                    firstNameCookie.Value = column.FirstName;
                    Response.Cookies.Add(firstNameCookie);

                    int view = 0;
                    return RedirectToAction("Index", "Tasks", new { view });
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