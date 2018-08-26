using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskList.Data;
using TaskList.Domain.Models;
using TaskList.Helpers;

namespace TaskList.Controllers
{
    public class TasksController : Controller
    {
        private TaskListContext db = new TaskListContext();

        // GET: Tasks
        public ActionResult Index(int view)
        {
            HttpCookie userId = HttpContext.Request.Cookies[Constant.UserIdCookie];
            int id = int.Parse(userId.Value);
            HttpCookie firstName = HttpContext.Request.Cookies[Constant.FirstNameCookie];
            ViewBag.FirstName = firstName.Value;

            var tasks = db.Tasks.Include(t => t.User);
            if (view == 1)
            {
                var userTasksDate = db.Tasks.Where(task => task.UserId == id).OrderBy(d => d.DueDate);
                return View(userTasksDate);
            }

            if (view == 2)
            {
                var userTasksComplete = db.Tasks.Where(task => task.UserId == id).OrderBy(d => d.Complete);
                return View(userTasksComplete);
            }
            var userTasks = db.Tasks.Where(task => task.UserId == id);
            return View(userTasks);

        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            //left in here to show as example of structure in future
            //ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");

            HttpCookie userId = HttpContext.Request.Cookies[Constant.UserIdCookie];
            ViewBag.UserId = int.Parse(userId.Value);
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Description,DueDate,TimeLeft,Complete")] Task task)
        {
            int view = 0;
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index", new { view });
            }

            //left in here to show as example of structure in future
            //ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", task.UserId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            HttpCookie userId = HttpContext.Request.Cookies[Constant.UserIdCookie];
            ViewBag.UserId = int.Parse(userId.Value);

            //left in here to show as example of structure in future
            //ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", task.UserId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Description,DueDate,TimeLeft,Complete")] Task task)
        {
            if (ModelState.IsValid)
            {
                int view = 0;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { view });
            }
            HttpCookie userId = HttpContext.Request.Cookies[Constant.UserIdCookie];
            ViewBag.UserId = int.Parse(userId.Value);
            //ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", task.UserId);
            return View(task);
        }

        public ActionResult Complete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            HttpCookie userId = HttpContext.Request.Cookies[Constant.UserIdCookie];
            ViewBag.UserId = int.Parse(userId.Value);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Complete([Bind(Include = "Id,UserId,Description,DueDate,TimeLeft,Complete")]
            Task task)
        {
            if (ModelState.IsValid)
            {
                int view = 0;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { view });
            }
            HttpCookie userId = HttpContext.Request.Cookies[Constant.UserIdCookie];
            ViewBag.UserId = int.Parse(userId.Value);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int view = 0;
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index", new { view });
        }

        [HttpPost]
        public ActionResult Search(string seachQuery)
        {
            var searchTerm = db.Tasks.Where(per => per.Description.Contains(seachQuery));
            return View(searchTerm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
