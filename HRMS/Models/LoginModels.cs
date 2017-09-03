using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HRMS.Models
{
    public class LoginModels
    {
        public int? UserID { get; set; }
        [Required(ErrorMessage = Message.EmailRequired)]
        public string EmailID { get; set; }
        public string Password { get; set; }
        public HRMSUserType UserType { get; set; }
        public string UType { get; set; }
    }
    public enum HRMSUserType
    {
        Admin,
        Employee,
        Superwiser
    }

   

}