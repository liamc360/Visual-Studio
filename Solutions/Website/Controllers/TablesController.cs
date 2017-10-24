using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Website.Models;

namespace Website.Controllers
{
    [Authorize]
    public class TablesController : Controller
    {
        /* EMPLOYEES WITH JSON */
        private IList<SharedClasses.Person.Employee> emp;

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

        public ActionResult AddEmployee()
        {
            return PartialView("_AddEmployee");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(SharedClasses.Person.Employee employee)
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content("Sorry, this method can't be called only from AJAX.");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    int result = SharedClasses.Person.Employee.AddEmployee(employee);
                    if (result > 0)
                    {
                        return Content("Record added successfully !");
                    }
                    else
                    {
                        return Content("Failed to add record !");
                    }
                }
                else
                {
                    StringBuilder strB = new StringBuilder(500);
                    foreach (ModelState modelState in ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            strB.Append(error.ErrorMessage + "<br/>");
                        }
                    }
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content(strB.ToString());
                }
            }
            catch (Exception ee)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content("Sorry, an error occured." + ee.Message);
            }
        }


        public ActionResult EditEmployee(int id)
        {
            SharedClasses.Person.Employee emp = SharedClasses.Person.Employee.GetEmployees(id).First();
            return PartialView("_EditEmployee", emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(SharedClasses.Person.Employee employee)
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content("Sorry, this method can't be called only from AJAX.");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    int result = SharedClasses.Person.Employee.UpdateEmployee(employee);
                    if (result > 0)
                    {
                        return Content("Record added successfully !");
                    }
                    else
                    {
                        return Content("Failed to add record !");
                    }
                }
                else
                {
                    StringBuilder strB = new StringBuilder(500);
                    foreach (ModelState modelState in ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            strB.Append(error.ErrorMessage + "<br/>");
                        }
                    }
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return Content(strB.ToString());
                }
            }
            catch (Exception ee)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Content("Sorry, an error occured." + ee.Message);
            }
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
