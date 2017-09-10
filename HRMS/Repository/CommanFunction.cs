using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using HRMS.Models;
using System.Data;
using System.Data.SqlClient;
using HRMS.Repository.DA;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Mail;
namespace HRMS.Repository
{
    public static class CommanFunction
    {
        private static readonly System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        public static string UrlEncode(string URL)
        {

            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(URL.ToString());
            return Convert.ToBase64String(encoded);
        }

        public static string UrlEncode(int encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe.ToString());
            return Convert.ToBase64String(encoded);
        }


        public static string UrlDecode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }


        public static DataSet GetDataSet(string Qry)
        {
            return SqlHelper.ExecuteDataset(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
        }
        public static int ExecuteNonQuery(string Qry)
        {
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStr(), CommandType.Text, Qry);
        }

        public static string GetResponse(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                // Set the Method property of the request to POST.
                request.Method = "GET";
                // Get the request stream.
                Stream dataStream = default(Stream);
                // = request.GetRequestStream();
                // Write the data to the request stream.
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                // Console.WriteLine(DirectCast(response, HttpWebResponse).StatusDescription)
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                // Console.WriteLine(responseFromServer)
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception ex)
            {
                //throw new Exception(string.Format("Error Parsing Column Name : {0}", ex.ToString()));
                return "Error";
            }
        }
        public static DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            try
            {
                string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
                List<string> ColumnsName = new List<string>();

                foreach (string jSA in jsonStringArray)
                {
                    string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                    foreach (string ColumnsNameData in jsonStringData)
                    {
                        try
                        {
                            int idx = ColumnsNameData.IndexOf(":");
                            string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                            if (!ColumnsName.Contains(ColumnsNameString))
                            {
                                ColumnsName.Add(ColumnsNameString);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                        }
                    }
                    break; // TODO: might not be correct. Was : Exit For
                }
                foreach (string AddColumnName in ColumnsName)
                {
                    dt.Columns.Add(AddColumnName);
                }
                foreach (string jSA in jsonStringArray)
                {
                    string[] RowData__1 = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                    DataRow nr = dt.NewRow();
                    foreach (string rowData__2 in RowData__1)
                    {
                        try
                        {
                            int idx = rowData__2.IndexOf(":");
                            string RowColumns = rowData__2.Substring(0, idx - 1).Replace("\"", "");
                            string RowDataString = rowData__2.Substring(idx + 1).Replace("\"", "");
                            nr[RowColumns] = RowDataString;

                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error Parsing Column Name : {0}", ex.ToString()));
                        }
                    }
                    dt.Rows.Add(nr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error Parsing Column Name : {0}", ex.ToString()));
                //  return dt;
            }
        }




        public static bool SendMail(string mailSub, string body, string emailTo)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(emailTo);
                string fromEmail = "kamaleshs48@gmail.com";
                string mailPass = "8802699574k";
                string mailServer = System.Configuration.ConfigurationSettings.AppSettings["MailHost"].ToString(); //;
                string mailPort = System.Configuration.ConfigurationSettings.AppSettings["MailPort"].ToString();//
                mail.From = new MailAddress(fromEmail, "E-Exam", System.Text.Encoding.UTF8);
                mail.Subject = mailSub;
                mail.Body = body;

                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(fromEmail, mailPass);
                client.Port = Convert.ToInt32(mailPort);
                client.Host = mailServer;
                client.EnableSsl = false;
                client.Send(mail);
                HttpContext.Current.Session["Error"] = "Success";
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Session["Error"] = ex.ToString();

                return false;
                //Response.Write("<script>alert('" + ex.Message + "')</script>");
            }

        }
        public static bool SendMailByBW1(string from, string to, string subject, string body)
        {
            bool isSent = false;
            try
            {
                ////////////Create the msg object to be sent
                //////////MailMessage msg = new MailMessage();
                ////////////Add your email address to the recipients
                //////////msg.To.Add(to);
                ////////////Configure the address we are sending the mail from
                //////////MailAddress address = new MailAddress(from, "Anirubha Homes");
                //////////msg.From = address;
                //////////msg.Subject = subject;
                //////////msg.Body = body;
                //////////msg.IsBodyHtml = true;
                //////////// msg.CC.Add("");
                ////////////Configure an SmtpClient to send the mail.            
                //////////SmtpClient client = new SmtpClient();
                //////////client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //////////client.EnableSsl = true ;
                //////////client.Host = "smtp.gmail.com";
                //////////client.Port = 587;

                ////////////Setup credentials to login to our sender email address ("UserName", "Password")
                //////////NetworkCredential credentials = new NetworkCredential("info@anirubha.com", "kalpna45@");
                //////////client.UseDefaultCredentials = true;
                //////////client.Credentials = credentials;

                ////////////Send the msg
                //////////client.Send(msg);

                const string SERVER = "relay-hosting.secureserver.net";
                System.Web.Mail.MailMessage oMail = new System.Web.Mail.MailMessage();
                oMail.From = "info@e-exam.in";
                oMail.To = to;
                oMail.Cc = "kamaleshs48@gmail.com";
                oMail.Subject = subject;
                oMail.Body = body;
                oMail.BodyFormat = System.Web.Mail.MailFormat.Html;// enumeration
                oMail.Priority = System.Web.Mail.MailPriority.High;// enumeration





                //oMail.Body = "Sent at: " + DateTime.Now;
                System.Web.Mail.SmtpMail.SmtpServer = SERVER;

                System.Web.Mail.SmtpMail.Send(oMail);
                oMail = null;// free up resources


                isSent = true;
                //Display some feedback to the user to let them know it was sent

            }
            catch (Exception ex)
            {
                // throw ex;
                //If the message failed at some point, let the user know
                isSent = false;
                //"Your message failed to send, please try again."
            }



            return isSent;
        }
        //Update By Kamlesh
        public static bool SendMailByBW( string to, string subject, string body)
        {
            try
            {

                //Encoed
                byte[] bytes = Encoding.UTF8.GetBytes(body);
                string _body = Convert.ToBase64String(bytes);
                //decode

                string base64 = "YWJjZGVmPT0=";
                byte[] bytes1 = Convert.FromBase64String(base64);
                string str = Encoding.UTF8.GetString(bytes1);


                var values = new Dictionary<string, string>
{
            { "FromEmail", System.Configuration.ConfigurationSettings.AppSettings["EEmail"].ToString()},
            { "FromPassword",  System.Configuration.ConfigurationSettings.AppSettings["EPassword"].ToString() },
            { "toEmail", to },
            { "EmailBody",body },
            { "MailSubject", subject},
            { "MailDisplayName", "Best Cares" },
};

                var content = new System.Net.Http.FormUrlEncodedContent(values);

                var response = client.PostAsync("http://t1.roken4life.com/sendemail.aspx", content);//http://localhost:57867/SendEmail.aspx    http://t1.roken4life.com/sendemail.aspx

                var responseString = response.Result.Content.ReadAsStringAsync();// ReadAsStringAsync();




                return true;
            }
            catch (Exception ex)
            {
                var s = ex.ToString();
                return false;
            }
        }
        public static string GetEmailFooter()
        {

            string mailfooterThankYou = "";
            mailfooterThankYou += "<br/><span style='font-size:11pt;font-family:Times New Roman'>Thank you.</span><br/><br/>";
            mailfooterThankYou += "-----------------------------------------------------------------------------------------------------------------<br/>";
            mailfooterThankYou += "<span style='font-family:Times New Roman'><b>XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX</b></span><br/>";
            mailfooterThankYou += "-----------------------------------------------------------------------------------------------------------------<br/><br/>";

            string mailfooter = "";
            //mailfooter += "<a href='https://XXXXXX.com'><img id='imgLogo' src='https://XXXXXXXXX.com/images/logos/logo.PNG' /></a><br/>";
            mailfooter += "<span style='font-size:10px;font-family:Times New Roman'>This is an automated message sent by our service. Please do not reply to this email. If you have a support query please contact us by emailing us at support@XXXXXX.com.</span>" + "<br /><br />";
            mailfooter += "<span style='font-size:10px;font-family:Times New Roman'>XXXXXX | XXXXXXXX | XXXXXXXXX | XXXX | XXXXXXXXX</span>" + "<br />";
            mailfooter += "<span style='font-size:10px;font-family:Times New Roman'>XXXXXXXXXXXXXXXX | XXXXXXXXXX</span>" + "<br /><br/>";
            mailfooter += "<span style='font-size:10px;font-family:Times New Roman'>XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX</span>" + "<br/><br/>";
            mailfooter += "<span style='font-size:10px;font-family:Times New Roman'>XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX</span>" + "<br/><br/>";


            mailfooter += "<span style='font-size: 10pt; font-family:Times New Roman'>********************************************************************************************************************";


            return mailfooterThankYou + mailfooter;
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