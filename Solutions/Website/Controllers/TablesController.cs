﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    [Authorize]
    public class TablesController : Controller
    {
        /* EMPLOYEES WITH JSON */
        private List<SharedClasses.Person.Employee> emp;

        public JsonResult EmployeeData(int empNo, String birthDate, String firstName, String lastName, String gender, String hireDate)
        {
            emp = SharedClasses.Person.Employee.GetEmployees(empNo, birthDate, firstName, lastName, gender, hireDate);
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Employees()
        {
            return View();
        }
        public ActionResult SearchEmployees()
        {
            return PartialView("_SearchEmployees");
        }


        // GET: Tables
        public ActionResult Tickets()
        {
            return View(emp);
        }

        public ActionResult WebGrid()
        {
            return View(emp);
        }

        // GET: Tables/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tables/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tables/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tables/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tables/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tables/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
