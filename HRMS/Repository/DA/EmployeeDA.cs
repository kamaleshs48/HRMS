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
                models.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                models.GenderID = Convert.ToInt32(ds.Tables[0].Rows[0]["GenderID"]);
                models.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                models.AadharNumber = ds.Tables[0].Rows[0]["AadharNumber"].ToString();
                models.PANNo = ds.Tables[0].Rows[0]["PAN"].ToString();
                models.MaritalStatus = ds.Tables[0].Rows[0]["MaritalStatus"].ToString();
                models.PresentAddress = ds.Tables[0].Rows[0]["PresentAddress"].ToString();
                models.PermanentAddress = ds.Tables[0].Rows[0]["PermanentAddress"].ToString();


            }
            return models;

        }
        public static int UpdateEmpDetails(EmpBasicDetails Model)
        {
            int status = 0;
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] prm = new SqlParameter[]
            {
                new SqlParameter("@Mode","Update"),
                new SqlParameter("@FirstName",Model.FirstName),
                new SqlParameter("@LastName",Model.LastName),
                new SqlParameter("@Email",Model.Email),
                new SqlParameter("@FatherName",Model.FatherName),
                new SqlParameter("@DOB",Model.DOB),
                new SqlParameter("@GenderID",Model.GenderID),
                new SqlParameter("@MobileNo",Model.MobileNo),
                new SqlParameter("@AadharNumber",Model.AadharNumber),
                new SqlParameter("@PAN",Model.PANNo),
                new SqlParameter("@MaritalStatus",Model.MaritalStatus),
                new SqlParameter("@PermanentAddress",Model.PermanentAddress),
                new SqlParameter("@PresentAddress",Model.PresentAddress),
                new SqlParameter("@EmpID",Model.EmpID),
            };

                status = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_SaveEmpRecord", prm);

            }
            catch (Exception ex)
            {
            }
            return status;



        }
        public static EmpEducationDetails GetEmpEducation(int EMPID)
        {
            EmpEducationDetails models = new EmpEducationDetails();

            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "Select * from tbl_Education where EmpID =" + EMPID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                models.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["EmpID"]);
                models.cr10 = ds.Tables[0].Rows[0]["10th"].ToString();
                models.cr12 = ds.Tables[0].Rows[0]["12th"].ToString();
                models.crGr = ds.Tables[0].Rows[0]["Gr"].ToString();
                models.crPoGr = ds.Tables[0].Rows[0]["POGR"].ToString();

            }
            return models;

        }
        public static int UpdateEmpEducation(EmpEducationDetails Model)
        {
            int status = 0;
            try
            {
                DataSet ds = new DataSet();
                SqlParameter[] prm = new SqlParameter[]
            {
            new SqlParameter("@Mode","UpdateEducation"),
            new SqlParameter("@cr10",Model.cr10),
             new SqlParameter("@cr12",Model.cr12),
              new SqlParameter("@crGr",Model.crGr),
               new SqlParameter("@crPoGr",Model.crPoGr),
                 new SqlParameter("@EmpID",Model.ID),
            };

                status = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "sp_SaveEmpRecord", prm);

            }
            catch (Exception ex)
            {
            }
            return status;

            return 0;

        }

        public static WagesModels GetWagesDetails()
        {
            WagesModels models = new WagesModels();

            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "Select * from tbl_WagesMaster order by id ");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                models.BasicWages_1 = ds.Tables[0].Rows[0]["Wages"].ToString();
                models.BasicWages_2 = ds.Tables[0].Rows[1]["Wages"].ToString();
                models.BasicWages_3 = ds.Tables[0].Rows[2]["Wages"].ToString();
                models.BasicWages_4 = ds.Tables[0].Rows[3]["Wages"].ToString();
                models.BasicWages_5 = ds.Tables[0].Rows[4]["Wages"].ToString();
                models.BasicWages_6 = ds.Tables[0].Rows[5]["Wages"].ToString();

                models.DA_1 = ds.Tables[0].Rows[0]["DA"].ToString();
                models.DA_2 = ds.Tables[0].Rows[1]["DA"].ToString();
                models.DA_3 = ds.Tables[0].Rows[2]["DA"].ToString();
                models.DA_4 = ds.Tables[0].Rows[3]["DA"].ToString();
                models.DA_5 = ds.Tables[0].Rows[4]["DA"].ToString();
                models.DA_6 = ds.Tables[0].Rows[5]["DA"].ToString();

            }
            return models;
        }

        public static List<WagesModels> GetWagesListForAllCategory()
        {
            List<WagesModels> lst = new List<WagesModels>();

            DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, "Select * from tbl_WagesMaster order by id ");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lst.Add(new WagesModels { CID = Convert.ToInt32(dr["CID"].ToString()), BasicWages_1 = dr["Wages"].ToString(), DA_1 = dr["DA"].ToString() });
                }

            }
            return lst;
        }

        public static ForgotViewModel ForgotPassword(ForgotViewModel model)
        {
            ForgotViewModel result = new ForgotViewModel();
            try
            {
                SqlParameter[] pr = new SqlParameter[]
                {
                 new SqlParameter("@EmailId",model.Email),
                 new SqlParameter("@Password",model.password),
                };
                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "Sp_ForgotPassword", pr);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0]["Status"].ToString() == "Success")
                {
                    result.Succeeded = true;
                    result.UserName = ds.Tables[0].Rows[0]["FirstName"].ToString() + " " + ds.Tables[0].Rows[0]["LastName"].ToString();
                }
                else
                {
                    result.Succeeded = false;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public static ResetPasswordViewModel ResetPasswordAsync(ResetPasswordViewModel model)
        {
            ResetPasswordViewModel result = new ResetPasswordViewModel();
            try
            {
                SqlParameter[] pr = new SqlParameter[]
                {
                 new SqlParameter("@EmailId",model.Email),
                 new SqlParameter("@Password",model.Password),
                };
                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.StoredProcedure, "Sp_ResetPassword", pr);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows[0]["Status"].ToString() == "Success")
                {
                    result.Succeeded = true;
                    result.UserName = ds.Tables[0].Rows[0]["FirstName"].ToString() + " " + ds.Tables[0].Rows[0]["LastName"].ToString();
                }
                else
                {
                    result.Succeeded = false;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

    }
}