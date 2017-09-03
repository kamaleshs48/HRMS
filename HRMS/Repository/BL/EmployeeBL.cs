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

    }
}