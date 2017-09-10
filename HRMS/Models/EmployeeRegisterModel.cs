using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using HRMS.Repository;
using System.Data;

using System.ComponentModel.DataAnnotations;
namespace HRMS.Models
{
    public class EmployeeRegisterModel
    {


        private List<SelectListItem> _SiteList = new List<SelectListItem>();
        private List<SelectListItem> _PostList = new List<SelectListItem>();
        private List<SelectListItem> _StaffList = new List<SelectListItem>();
        private List<SelectListItem> _CategorieList = new List<SelectListItem>();

        public EmployeeRegisterModel()
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_GetALLMasterList");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(dr["TypeID"].ToString()) == 1)
                {
                    _SiteList.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["ID"].ToString() });
                }
                else if (Convert.ToInt32(dr["TypeID"].ToString()) == 2)
                {
                    _PostList.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["ID"].ToString() });
                }
                else if (Convert.ToInt32(dr["TypeID"].ToString()) == 3)
                {
                    _StaffList.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["ID"].ToString() });
                }
                else if (Convert.ToInt32(dr["TypeID"].ToString()) == 4)
                {
                    _CategorieList.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["ID"].ToString() });
                }
            }

        }

        public string EmpID { get; set; }

        [DefaultValue(0)]
        [Required(ErrorMessage = "Please Select Client Name/Site")]
        public int SiteID { get; set; }
        public List<SelectListItem> SiteList { get { return _SiteList; } }
        [DefaultValue(0)]
        [Required(ErrorMessage = "Please Select Post")]
        public int PostID { get; set; }
        public List<SelectListItem> PostList { get { return _PostList; } }
        [DefaultValue(0)]
        [Required(ErrorMessage = "Please Select Department")]
        public int StaffID { get; set; }
        public List<SelectListItem> StaffList { get { return _StaffList; } }

        [DefaultValue(0)]
        [Required(ErrorMessage = "Please Select Categorie")]
        public int CategoryID { get; set; }
        public List<SelectListItem> CategorieList { get { return _CategorieList; } }

        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Wages Amount")]
        public string Wages { get; set; }
        [Required(ErrorMessage = "Please Enter DA Amount")]
        public string DA { get; set; }
        public string UPassword { get; set; }



        public List<EmployeeRegisterModel> EmpList = new List<EmployeeRegisterModel>();
    }
    public class EmpBasicDetails
    {
        public string EmpID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public int GenderID { get; set; }
        public string MobileNo { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string AadharNo { get; set; }
        public string PANNo { get; set; }
        public string DOB { get; set; }
        public string PAN { get; set; }
        public string AadharNumber { get; set; }
        public string MaritalStatus { get; set; }
       

    }
    public class ForgotViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public bool Succeeded { get; set; }
    }
    [Serializable()]
    public class ResetPasswordViewModel
    {

        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the Password.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "Password must be 6 to 15 characters long. Must contain at least one lower case, one upper case letter, one digit and special character.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Confirm password.")]
        [Display(Name = "Confirm password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$", ErrorMessage = "Password must be 6 to 15 characters long. Must contain at least one lower case, one upper case letter, one digit and special character.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirm password do not match.")]
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public bool Succeeded { get; set; }

    }
}