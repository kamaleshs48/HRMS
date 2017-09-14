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
using HiQPdf;
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
                    GenerateOLPDF(models.FirstName);
                    return RedirectToAction("EmpList", "Admin");
                }
            }
            ModelState.AddModelError("", "Somthing wrong please contact IT Team.");
            return View();
        }


        public string GenerateOLPDF(string EMPID)
        {
            // create the HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
            // create the HTML to PDF converter
            string serialno = "ImpLc3JG-RG5LQFBD-UFsUDBIC-EwIQAhsa-FAIREwwT-EAwbGxsb";

            htmlToPdfConverter.StopSlowScripts = true;
            htmlToPdfConverter.HtmlLoadedTimeout = 0;
            // set a demo serial number
           // htmlToPdfConverter.SerialNumber = serialno;

            // set browser width
            htmlToPdfConverter.BrowserWidth = 1200;

            // set HTML Load timeout
            htmlToPdfConverter.HtmlLoadedTimeout = 120;

            // set PDF page size and orientation
            htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
            //  htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Landscape;

            // set the PDF standard used by the document
            htmlToPdfConverter.Document.PdfStandard = PdfStandard.Pdf;

            // set PDF page margins
            htmlToPdfConverter.Document.Margins = new PdfMargins(5);

            // set whether to embed the true type font in PDF
            htmlToPdfConverter.Document.FontEmbedding = true;

            // set triggering mode; for WaitTime mode set the wait time before convert
            htmlToPdfConverter.TriggerMode = ConversionTriggerMode.Auto;

            // set the document security
            htmlToPdfConverter.Document.Security.AllowPrinting = true;
            
           
            // convert HTML to PDF
            byte[] pdfBuffer = null;



            //if (radioButtonConvertUrl.Checked)
            //{
            //    // convert URL to a PDF memory buffer
            //    string url = textBoxUrl.Text;

            string fullUrl = Url.Action("EMPOL", "ADMIN", new { id = 5 }, Request.Url.Scheme);

            pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(fullUrl);
            //}
            //else
            //{
            //    // convert HTML code
            //    string htmlCode = textBoxHtmlCode.Text;
            //    string baseUrl = textBoxBaseUrl.Text;

            //    // convert HTML code to a PDF memory buffer
            //    pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);
            //}

            // inform the browser about the binary data format
            //HttpContext.Response.AddHeader("Content-Type", "application/pdf");

            //// let the browser know how to open the PDF document, attachment or inline, and the file name
            //HttpContext.Response.AddHeader("Content-Disposition", String.Format("{0}; filename=HtmlToPdf.pdf; size={1}",
            //     "inline", pdfBuffer.Length.ToString()));

            //// write the PDF buffer to HTTP response
            //HttpContext.Response.BinaryWrite(pdfBuffer);

            //// call End() method of HTTP response to stop ASP.NET page processing
            //HttpContext.Response.End();


            string Newfilename =  EMPID + "_OL_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm") + ".pdf";
            System.IO.File.WriteAllBytes(Server.MapPath(@"~\upload\" + Newfilename), pdfBuffer);


            return "";
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
            string Qry = "";
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
        public ActionResult EmpDoc()
        {

            return View();
        }
        public ActionResult EMPOL(string ID)
        {
            EmpBasicDetails models = new EmpBasicDetails();
            return View(models);
        }


    }
}
