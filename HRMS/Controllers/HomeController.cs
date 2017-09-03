using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using System.Data;
using HRMS.Repository;
namespace HRMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {

            // Test From Local


            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModels models)
        {
            if (ModelState.IsValid)
            {
                DataSet ds = new DataSet();
                ds = CommanFunction.GetDataSet("Select * from tbl_EmpMaster where EMail='" + models.EmailID + "' AND UPassword='" + models.Password + "'");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Session[SessionVariable.UserID] = ds.Tables[0].Rows[0]["ID"].ToString();
                    Session[SessionVariable.UserName] = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    Session[SessionVariable.UserEMail] = ds.Tables[0].Rows[0]["EMail"].ToString();
                    Session[SessionVariable.UserType] = ds.Tables[0].Rows[0]["UserType"].ToString();

                    if (ds.Tables[0].Rows[0]["UserType"].ToString() == "admin")
                    {
                        return RedirectToAction("DashBoard", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("BasicDetails", "Member");
                    }

                }
            }
            ModelState.AddModelError("", "Please Enter Valid Email ID and Password.");
            return View(models);
        }
    }
}
