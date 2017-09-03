using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Data;
namespace HRMS.Repository
{
    public class mySQLCommanFunction
    {

        public static bool Addd()
        {
            MySqlCommand sqlcom = new MySqlCommand();
            sqlcom.CommandText = "INSERT INTO `tb_baobei` (`tb_Name`,`tb_Price`,`tb_Image`,`tb_Url`)" +
                    " VALUES(@tb_Name,@tb_Price,@tb_Image,@tb_Url);";
            MySqlParameter[] commandParameters = new MySqlParameter[]{
                        new MySqlParameter("@tb_Name",""),
                        new MySqlParameter("@tb_Price",""),
                        new MySqlParameter("@tb_Image",""),
                        new MySqlParameter("@tb_Url","")
                      };
            return MySqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, MySqlHelper.ConnectionStringManager, sqlcom.CommandText, commandParameters);
        }

    }
}