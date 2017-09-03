using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRMS.Models;
using System.Data.SqlClient;
using System.Data;
namespace HRMS.Repository.DA
{
    public class EmployeeDA
    {
        public static int SaveUpdateEmployee(EmployeeRegisterModel models)
        {

            SqlParameter[] pr = new SqlParameter[] 
            {
                new SqlParameter("@SiteID",models.SiteID),
                new SqlParameter("@PostID",models.PostID),
                new SqlParameter("@StaffID",models.StaffID),
                new SqlParameter("@CategoryID",models.CategoryID),
                new SqlParameter("@Wages",models.Wages),
                new SqlParameter("@DA",models.DA),
                new SqlParameter("@FirstName",models.FirstName),
                new SqlParameter("@LastName",models.LastName),
                new SqlParameter("@Email",models.Email),
                new SqlParameter("@UPassword",models.UPassword),
                new SqlParameter("@Mode","Save"),
            };
            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_SaveEmpRecord", pr);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return 1;

            }
            return 0;

        }
        public static List<EmployeeRegisterModel> GetEmpList()
        {
            List<EmployeeRegisterModel> EmpList = new List<EmployeeRegisterModel>();

            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "Select * from tbl_EmpMaster where IsActive=1");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    EmpList.Add(new EmployeeRegisterModel
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),

                        EmpID = dr["ID"].ToString(),

                    });
                }
            }
            return EmpList;

        }

        public static EmpBasicDetails GetEmpDetails(int EMPID)
        {
            EmpBasicDetails models = new EmpBasicDetails();

            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "Select * from tbl_EmpMaster where IsActive=1 AND ID =" + EMPID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                models.EmpID = ds.Tables[0].Rows[0]["ID"].ToString();
                models.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                models.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                models.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                models.FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString();

            }
            return models;

        }

    }
}