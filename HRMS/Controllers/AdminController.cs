using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using HRMS.Repository.BL;
using HRMS.Repository.DA;
using HRMS.Repository;
namespace HRMS.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EmpRegister()
        {
            EmployeeRegisterModel models = new EmployeeRegisterModel();

            return View(models);
        }
        [HttpPost]
        public ActionResult EmpRegister(EmployeeRegisterModel models)
        {
            if (ModelState.IsValid)
            {
                models.UPassword = CommanFunction.UrlEncode(DateTime.Now.ToString("mmSSMM"));
                int r = EmployeeBL.SaveUpdateEmployee(models);
                if (r == 1)
                {

                    string body = " Hello " + models.FirstName + ",</br> Your Passowrd is -" + models.UPassword;


                    CommanFunction.SendMailByBW(models.Email, "Best Care Password ", body);

                    return RedirectToAction("EmpList", "Admin");
                }
            }
            ModelState.AddModelError("", "Somthing wrong please contact IT Team.");
            return View();
        }
        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult EmpList()
        {
            EmployeeRegisterModel model = new EmployeeRegisterModel();
            model.EmpList = EmployeeBL.GetEmpList();
            return View(model);
        }
        public ActionResult OnlineRegister()
        {
            return View();
        }

        public ActionResult WagesMaster()
        {
            return View();
        }

    }
}
