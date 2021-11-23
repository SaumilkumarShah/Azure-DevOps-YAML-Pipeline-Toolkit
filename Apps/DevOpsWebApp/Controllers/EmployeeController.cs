using DevOpsWebApp.Models;
using DevOpsWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevOpsWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee/GetAllEmpDetails
        public ActionResult GetAllEmpDetails()
        {
            EmpRepository EmpRepo = new EmpRepository();
            return View(EmpRepo.GetAllEmployees());
        }
        // GET: Employee/AddEmployee
        public ActionResult AddEmployee()
        {
            return View();
        }
        // POST: Employee/AddEmployee
        [HttpPost]
        public ActionResult AddEmployee(EmployeeModel Emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmpRepository EmpRepo = new EmpRepository();
                    EmpRepo.AddEmployee(Emp);
                    ViewBag.Message = "Records added successfully.";
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        // GET: Bind controls to Update details
        public ActionResult EditEmpDetails(int id)
        {
            EmpRepository EmpRepo = new EmpRepository();
            return View(EmpRepo.GetAllEmployees().Find(Emp => Emp.Empid == id));
        }
        // POST:Update the details into database
        [HttpPost]
        public ActionResult EditEmpDetails(int id, EmployeeModel obj)
        {
            try
            {
                EmpRepository EmpRepo = new EmpRepository();
                EmpRepo.UpdateEmployee(obj);
                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return View();
            }
        }
        // GET: Delete  Employee details by id
        public ActionResult DeleteEmp(int id)
        {
            try
            {
                EmpRepository EmpRepo = new EmpRepository();
                if (EmpRepo.DeleteEmployee(id))
                {
                    ViewBag.AlertMsg = "Employee details deleted successfully";
                }
                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return RedirectToAction("GetAllEmpDetails");
            }
        }
    }
}
