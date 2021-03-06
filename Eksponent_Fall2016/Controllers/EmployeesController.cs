﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eksponent_Fall2016.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Eksponent_Fall2016.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {    
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)); 
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var employee = db.Employees.Where(i => i.ApplicationUserId == currentUser.Id).FirstOrDefault();
            return View(employee);
        }

        public ActionResult EmployeeSkills(int? id)
        {
           return RedirectToAction("EmployeeSkills", "Skills", new { id = id });
        }


        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,Firstname,Lastname,Profileimage,CompanyId,ApplicationUserId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", employee.CompanyId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", employee.CompanyId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,Firstname,Lastname,Profileimage,CompanyId,ApplicationUserId")] Employee employee,
             HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var path = Server != null ? Server.MapPath("~") : "";
                employee.SaveImage(image, path, "/ProfileImages/");
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetEmployees");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", employee.CompanyId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            var user = db.Users.Find(employee.ApplicationUserId);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("GetEmployees");
        }

        // GET: Employees/GET LIST/
        public ActionResult GetEmployees()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var currentUser = userManager.FindById(User.Identity.GetUserId());

            Company company = db.Companies.Where(i => i.ApplicationUserId == currentUser.Id).FirstOrDefault();
            List<Employee> e = new List<Employee>();

            e = company.IEmployee.ToList();
            return View(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
            public ActionResult GetSkills()
        {

            //Fetching UserManager
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //Get User from Database based on userId 
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            
            Employee e = db.Employees.Where(x => x.ApplicationUserId == currentUser.Id).Single();

            Company company = db.Companies.Find(e.CompanyId);

            var list = new List<Skill>();
            list = db.Skills.Where(x => x.CompanyId == company.CompanyId).ToList();

            var model = new EmployeeSkillViewModel
            {
                SkillList = list.Select(a => new SelectListItem
                {
                    Text = a.Skillname,
                    Value = a.SkillId.ToString(),
                })
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult GetEmployeeSkills(IEnumerable<int> skillIds)
        {
            //Fetching UserManager
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //Get User from Database based on userId 
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            Employee emp = db.Employees.Where(x => x.ApplicationUserId == currentUser.Id).Single();

            Company company = db.Companies.Find(emp.CompanyId);

            var eSList = new List<EmployeeSkill>();

            if (ModelState.IsValid)
            {
                foreach (var skill in skillIds)
                {
                    var result = db.EmployeesSkills.Include(e => e.Employee).Include(e => e.Skill)
                   .Where(x => x.SkillId == skill).ToList();
                    eSList.AddRange(result);
                }
            }
            return View(eSList);
        }
    }
}
