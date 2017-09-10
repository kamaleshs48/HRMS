using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using HRMS.Repository.BL;
using HRMS.Repository.DA;
using HRMS.Repository;
using HRMS.Filters;
namespace HRMS.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/ Update by 

        public ActionResult Index()
        {
            return View();
        }
        [UrlCopyAttribute]
        [HttpGet]
        public ActionResult EmpRegister()
        {
            EmployeeRegisterModel models = new EmployeeRegisterModel();

            return View(models);
        }
        [UrlCopyAttribute]
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
        [UrlCopyAttribute]
        public ActionResult DashBoard()
        {
            return View();
        }
        [UrlCopyAttribute]
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
        [UrlCopyAttribute]
        [HttpGet]
        public ActionResult WagesMaster()
        {
            WagesModels models = new WagesModels();
            models = EmployeeBL.GetWagesDetails();
            return View(models);
        }
        [UrlCopyAttribute]
        [HttpPost]
        public ActionResult WagesMaster(WagesModels models)
        {
             string Qry="";
            if (ModelState.IsValid)
            {

                Qry += " Update  tbl_WagesMaster Set Wages=" + Convert.ToInt32(models.BasicWages_1) + " ,DA =" + Convert.ToInt32(models.DA_1) + " WHERE CID = 1; ";
                Qry += " Update  tbl_WagesMaster Set Wages=" + Convert.ToInt32(models.BasicWages_2) + " ,DA =" + Convert.ToInt32(models.DA_2) + " WHERE CID = 2; ";
                Qry += " Update  tbl_WagesMaster Set Wages=" + Convert.ToInt32(models.BasicWages_3) + " ,DA =" + Convert.ToInt32(models.DA_3) + " WHERE CID = 3; ";
                Qry += " Update  tbl_WagesMaster Set Wages=" + Convert.ToInt32(models.BasicWages_4) + " ,DA =" + Convert.ToInt32(models.DA_4) + " WHERE CID = 4; ";
                Qry += " Update  tbl_WagesMaster Set Wages=" + Convert.ToInt32(models.BasicWages_5) + " ,DA =" + Convert.ToInt32(models.DA_5) + " WHERE CID = 5; ";
                Qry += " Update  tbl_WagesMaster Set Wages=" + Convert.ToInt32(models.BasicWages_6) + " ,DA =" + Convert.ToInt32(models.DA_6) + " WHERE CID = 6; ";
                CommanFunction.ExecuteNonQuery(Qry);

                ViewBag.Message = "Data Updated Successfully.";
                models = EmployeeBL.GetWagesDetails();
            }
            return View(models);
        }


        public ActionResult Attendence()
        {
            return View();
        }

    }
}
