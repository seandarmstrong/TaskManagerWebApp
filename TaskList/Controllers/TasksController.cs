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
        public ActionResult Index()
        {

            HttpCookie userId = HttpContext.Request.Cookies[Constant.UserIdCookie];
            int id = int.Parse(userId.Value);
            HttpCookie firstName = HttpContext.Request.Cookies[Constant.FirstNameCookie];
            ViewBag.FirstName = firstName.Value;

            var tasks = db.Tasks.Include(t => t.User);
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

            HttpCookie firstName = HttpContext.Request.Cookies[Constant.FirstNameCookie];
            ViewBag.FirstName = firstName.Value;
            HttpCookie userId = HttpContext.Request.Cookies[Constant.UserIdCookie];
            ViewBag.UserId = int.Parse(userId.Value);
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Description,DueDate,TimeLeft,Complete")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", task.UserId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Description,DueDate,TimeLeft,Complete")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", task.UserId);
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
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
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
