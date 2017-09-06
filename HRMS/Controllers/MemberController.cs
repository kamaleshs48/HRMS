using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using HRMS.Repository.BL;
using System.IO;
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
            models.EmpID = Session[SessionVariable.UserID].ToString();
            int a = EmployeeBL.UpdateEmpDetails(models);
            return View(models);
        }
        [HttpGet]
        public ActionResult EducationDetails()
        {
            EmpEducationDetails models = new EmpEducationDetails();
            models = EmployeeBL.GetEmpEducation(Convert.ToInt32(Session[SessionVariable.UserID].ToString()));
            return View(models);
        }
        [HttpPost]
        public ActionResult EducationDetails(EmpEducationDetails model, FormCollection frm)
        {
            String roleValue1 = Request["file1"];
            //string newMarketingTypeName = Request.Form["file2"].ToString();
            //String roleValue2 = frm["file2"].ToString();
            String roleValue3 = frm["Grth"];
            String roleValue4 = frm["pgFile"];
            model.ID = Convert.ToInt32(Session[SessionVariable.UserID]);
            if (Request.Files != null)
            {
                int count = 1;
                foreach (string fileRequest in Request.Files)
                {
                    HttpPostedFileBase fileData = Request.Files[fileRequest];
                    if (fileData.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(fileData.FileName);
                        fileName = model.ID + "_" + fileName;
                        string path = Path.Combine(Server.MapPath("~/EducationCer/"), fileName);
                        fileData.SaveAs(path);
                        if (count == 1)
                        {
                            model.cr10 = fileName;
                            count++;
                            continue;
                        }
                        if (count == 2)
                        {
                            model.cr12 = fileName;
                            count++;
                            continue;
                        }
                        if (count == 3)
                        {
                            model.crGr = fileName;
                            count++;
                            continue;
                        }
                        if (count == 4)
                        {
                            model.crPoGr = fileName;
                            count++;
                            continue;
                        }
                    }
                }
            }

            int a = EmployeeBL.UpdateEmpEducation(model);
            return View(model);
        }

        public ActionResult PFDetails()
        {
            return View();
        }
    }
}
