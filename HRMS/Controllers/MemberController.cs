using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using HRMS.Repository.BL;
namespace HRMS.Controllers
{
    public class MemberController : Controller
    {


        public ActionResult DashBoard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult BasicDetails()
        {
            EmpBasicDetails models = new EmpBasicDetails();

            models = EmployeeBL.GetEmpDetails(Convert.ToInt32(Session[SessionVariable.UserID].ToString()));

            return View(models);
        }
        [HttpPost]
        public ActionResult BasicDetails(EmpBasicDetails models)
        {
            return View(models);
        }
        public ActionResult PFDetails()
        {
            return View();
        }
    }
}
