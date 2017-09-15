using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace HRMS.Models
{
    public class EmpAttendanceModels
    {
       
        [Required(ErrorMessage="Please Select Employee Name")]
        public string EMPID { get; set; }
          [Required(ErrorMessage = "Please Select Month")]
        public int MonthID { get; set; }
        public DateTime AttDate { get; set; }
        public string AttMonth { get; set; }
        public string AttValue { get; set; }
        public List<SelectListItem> MonthList = new List<SelectListItem>();
        public List<SelectListItem> EmpList = new List<SelectListItem>();
        public List<Att> MonthsDayList = new List<Att>();

    }
    public class Att
    {
        public string D {get;set;}
        public string DayID {get;set;}
        public string DayValues {get;set;}

    }
}