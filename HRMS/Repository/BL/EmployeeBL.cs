using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRMS.Models;
using HRMS.Repository.DA;
namespace HRMS.Repository.BL
{
    public class EmployeeBL
    {
        public static int SaveUpdateEmployee(EmployeeRegisterModel models)
        {
            return EmployeeDA.SaveUpdateEmployee(models);
        }
        public static List<EmployeeRegisterModel> GetEmpList()
        {
            return EmployeeDA.GetEmpList(); 
        }

        public static EmpBasicDetails GetEmpDetails(int EMPID)
        {
            return EmployeeDA.GetEmpDetails(EMPID); 
        }
        public static WagesModels GetWagesDetails()
        {
            return EmployeeDA.GetWagesDetails(); 
        }
        public static List<WagesModels> GetWagesListForAllCategory()
        {
            return EmployeeDA.GetWagesListForAllCategory(); 
        }
        public static EmpEducationDetails GetEmpEducation(int EMPID)
        {
            return EmployeeDA.GetEmpEducation(EMPID);
        }
        public static int UpdateEmpDetails(EmpBasicDetails Model)
        {
            return EmployeeDA.UpdateEmpDetails(Model);
        }
        public static int UpdateEmpEducation(EmpEducationDetails Model)
        {
            return EmployeeDA.UpdateEmpEducation(Model);
        }
        public static ForgotViewModel ForgotPassword(ForgotViewModel model)
        {
            return EmployeeDA.ForgotPassword(model);
        }
        public static ResetPasswordViewModel ResetPasswordAsync(ResetPasswordViewModel model)
        {
            return EmployeeDA.ResetPasswordAsync(model);
        }
    }
}