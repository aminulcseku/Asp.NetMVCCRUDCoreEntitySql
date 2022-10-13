using Asp.NetMVCcrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asp.NetMVCcrud.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        public ActionResult Index()
        {

            using (DBModel dc = new DBModel())
            {
                var employees = dc.employees.OrderBy(a => a.name).ToList();
                //return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
                return View(employees);
            }

            
        }


        public ActionResult GetData()
        {
            using (DBModel dc = new DBModel())
            {
                var employees = dc.employees.OrderBy(a => a.name).ToList();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult Save(int id)
        {
            using (DBModel dc = new DBModel())
            {
                var v = dc.employees.Where(a => a.employeeid == id).FirstOrDefault();
                return View(v);
            }
        }



        [HttpPost]
        public ActionResult Save(employee emp)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (DBModel dc = new DBModel()) 
                {
                    if (emp.employeeid > 0)
                    {
                        //Edit 
                        var v = dc.employees.Where(a => a.employeeid == emp.employeeid).FirstOrDefault();
                        if (v != null)
                        {
                            v.name = emp.name;
                            v.position = emp.position;
                            v.office = emp.office;
                            v.age = emp.age;
                            v.salary = emp.salary;
                        }
                    }
                    else
                    {
                        //Save
                        dc.employees.Add(emp);
                    }
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        

	}
}