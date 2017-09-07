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
        // GET: /Admin/ Update by 

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

        [HttpGet]
        public ActionResult WagesMaster()
        {
            WagesModels models = new WagesModels();
            models = EmployeeBL.GetWagesDetails();
            return View(models);
        }
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

    }
}
