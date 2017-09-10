using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using System.Data;
using HRMS.Repository;
using HRMS.Repository.BL;
namespace HRMS.Controllers
{
    public class HomeController : Controller
    {
        string _URL = "";
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

            // Test From Local  and publish vs2013


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

         [HttpGet]
        public ActionResult ForgotPassword()
        {
            ForgotViewModel model = new ForgotViewModel();
            return View(model);

        }
        [HttpGet]
        public ActionResult ResetPassword(string id)
        {

            ResetPasswordViewModel model = new ResetPasswordViewModel();
            DataSet ds = new DataSet();
            model.Email = CommanFunction.UrlDecode(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            _URL = HttpContext.Request.Url.Port != 0 ? HttpContext.Request.Url.AbsoluteUri.Replace(":" + HttpContext.Request.Url.Port, string.Empty) : HttpContext.Request.Url.AbsoluteUri;
            _URL.Replace(_URL.Substring(_URL.LastIndexOf("/")), "");
            _URL = _URL.Replace("ResetPassword", "");
            if (ModelState.IsValid)
            {
                //model.Password = CommanFunction.Encrypt(model.Password);
                var result = EmployeeBL.ResetPasswordAsync(model);


                string mailSub = "Password changed on the HRMS";
                string mailBody = "Dear " + result.UserName + ",<br /><br />";
                mailBody += "Your password has been changed on the HRMS. " + "Your details are:<br /><br/>";
                mailBody += "Email: " + model.Email + "<br />";
                mailBody += "<br/><br/>Your password is not shown here for security reasons.<br />";
                mailBody += "If you did not change your password, please notify Support immediately.<br />";

                mailBody += "<br/>Please <a href='" + _URL + "/index'>click here</a> to the HRMS " + "<br/>";
                string body = mailBody + CommanFunction.GetEmailFooter();
                CommanFunction.SendMailByBW(model.Email, mailSub, body);
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult ForgotPassword(ForgotViewModel model)
        {
            _URL = HttpContext.Request.Url.Port != 0 ? HttpContext.Request.Url.AbsoluteUri.Replace(":" + HttpContext.Request.Url.Port, string.Empty) : HttpContext.Request.Url.AbsoluteUri;
            _URL.Replace(_URL.Substring(_URL.LastIndexOf("/")), "");
            _URL = _URL.Replace("ForgotPassword", "");
            if (ModelState.IsValid)
            {
                string pass = DateTime.Now.ToString("ddMM") + CommanFunction.GenerateUniqueCode(60, 5);
                model.password = pass;
                var user = EmployeeBL.ForgotPassword(model);


                string mailBody = "Dear <b>" + user.UserName + "</b>,<br /><br/>";

                mailBody += "<span style='font-size:11pt;font-family:Times New Roman'>A request has been made to reset your password on the HRMS.</span><br /><br />";
                mailBody += "<span style='font-size:11pt;font-family:Times New Roman'>Your details are as below:</span><br />";
                mailBody += "<b>Full name:</b> " + user.UserName + "<br />";
                mailBody += "<b>Email:</b> " + model.Email + "<br />";
                mailBody += "<b>Temporary password:</b> " + pass + "<br /><br />";

                mailBody += "<span style='font-size:11pt;font-family:Times New Roman'>" + "Please login to the service using this password. You will have to change your password when you first login." + "</span><br />";
                mailBody += "<span style='font-size:11pt;font-family:Times New Roman'>If you did not request to reset your password, please notify Support immediately." + "</span><br /><br />";
                mailBody += "<span style='font-size:11pt;font-family:Times New Roman'>Please <a href='" + _URL + "/index'>click here</a> to login into the HRMS." + "</span><br/><br/>";
                string mailSubject = "Temporary password for the HRMS";
                string body = mailBody + CommanFunction.GetEmailFooter();
                CommanFunction.SendMailByBW(model.Email, mailSubject, body);
                //Message.Destination = model.Email;
                if (user.Succeeded)
                {
                    ViewBag.JavascriptToRun = "ShowErrorPopup()";
                    ViewBag.Error = "Password has been reset and sent to your email.";
                    //  Response.Write("<script>alert('Password has been changed and sent to your email.');  window.location ='" + _URL + "/index';</script>");
                }
                else
                {
                    ViewBag.JavascriptToRun = "ShowErrorPopup()";
                    ViewBag.Error = "Email address does not exist.";
                    // Response.Write("<script>alert('Emial address does not exist.');</script>");
                    //  ModelState.AddModelError("", "Emial address does not exist.");
                }
            }
            return View(model);
        }
        public static string GenerateUniqueCode(int maxValue, int length)
        {
            string chars = "123456789abcdefghiABCDEFGHIjklmnopqrJKLMNOPQRstuvwxyzSTUVWXYZ";
            char[] stringChars = new char[length];
            Random random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(0, maxValue)];
            }
            return new String(stringChars);
        }
    }
}
