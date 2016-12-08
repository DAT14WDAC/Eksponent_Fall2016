﻿using System;
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
    public class SkillFocusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SkillFocus
        public ActionResult Index()
        {
            var skillFocus = db.SkillFocus.Include(s => s.Employee).Include(s => s.Skill);
            return View(skillFocus.ToList());
        }

        // GET: SkillFocus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillFocus skillFocus = db.SkillFocus.Find(id);
            if (skillFocus == null)
            {
                return HttpNotFound();
            }
            return View(skillFocus);
        }

        // GET: SkillFocus/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Firstname");
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Skillname");
            return View();
        }

        // POST: SkillFocus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SkillFocusId,EmployeeId,SkillId,Startdate")] SkillFocus skillFocus)
        {
            if (ModelState.IsValid)
            {
                db.SkillFocus.Add(skillFocus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Firstname", skillFocus.EmployeeId);
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Skillname", skillFocus.SkillId);
            return View(skillFocus);
        }

        // GET: SkillFocus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillFocus skillFocus = db.SkillFocus.Find(id);
            if (skillFocus == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Firstname", skillFocus.EmployeeId);
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Skillname", skillFocus.SkillId);
            return View(skillFocus);
        }

        // POST: SkillFocus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SkillFocusId,EmployeeId,SkillId,Startdate")] SkillFocus skillFocus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skillFocus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Firstname", skillFocus.EmployeeId);
            ViewBag.SkillId = new SelectList(db.Skills, "SkillId", "Skillname", skillFocus.SkillId);
            return View(skillFocus);
        }

        // GET: SkillFocus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillFocus skillFocus = db.SkillFocus.Find(id);
            if (skillFocus == null)
            {
                return HttpNotFound();
            }
            return View(skillFocus);
        }

        // POST: SkillFocus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SkillFocus skillFocus = db.SkillFocus.Find(id);
            db.SkillFocus.Remove(skillFocus);
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