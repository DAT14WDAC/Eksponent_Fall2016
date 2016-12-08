using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eksponent_Fall2016.Models;

namespace Eksponent_Fall2016.Controllers
{
    public class EmployeeSkillsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmployeeSkills
        public ActionResult Index()
        {
            var employeesSkills = db.EmployeesSkills.Include(e => e.Employee).Include(e => e.Skill);
            return View(employeesSkills.ToList());
        }

        // GET: EmployeeSkills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSkill employeeSkill = db.EmployeesSkills.Find(id);
            if (employeeSkill == null)
            {
                return HttpNotFound();
            }
            return View(employeeSkill);
        }

        // GET: EmployeeSkills/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Firstname");
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Skillname");
            return View();
        }

        // POST: EmployeeSkills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeSkillId,Level,SkillId,EmployeeId")] EmployeeSkill employeeSkill)
        {
            if (ModelState.IsValid)
            {
                db.EmployeesSkills.Add(employeeSkill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Firstname", employeeSkill.EmployeeId);
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Skillname", employeeSkill.SkillId);
            return View(employeeSkill);
        }

        // GET: EmployeeSkills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSkill employeeSkill = db.EmployeesSkills.Find(id);
            if (employeeSkill == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Firstname", employeeSkill.EmployeeId);
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Skillname", employeeSkill.SkillId);
            return View(employeeSkill);
        }

        // POST: EmployeeSkills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeSkillId,Level,SkillId,EmployeeId")] EmployeeSkill employeeSkill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeSkill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Firstname", employeeSkill.EmployeeId);
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Skillname", employeeSkill.SkillId);
            return View(employeeSkill);
        }

        // GET: EmployeeSkills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSkill employeeSkill = db.EmployeesSkills.Find(id);
            if (employeeSkill == null)
            {
                return HttpNotFound();
            }
            return View(employeeSkill);
        }

        // POST: EmployeeSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeSkill employeeSkill = db.EmployeesSkills.Find(id);
            db.EmployeesSkills.Remove(employeeSkill);
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
