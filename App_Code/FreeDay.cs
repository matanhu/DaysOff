using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Udi.ActiveDirectory;
using System.Text;
using System.Net.Mail;

/// <summary>
/// Summary description for FreeDay
/// </summary>
/// 


public class FreeDay
{
    public struct ResultCheckBlockDays
    {
        public Boolean flag;
        public List<Dictionary<String, String>> BlockDays;
    }

    private String id;
    private DateTime startDate;
    private DateTime endDate;
    private String Comment;
    private ActiveDirectory activeDirectory;


    public FreeDay()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public FreeDay(String _id, DateTime _startDate, DateTime _endDate, String _comment)
    {
        this.id = _id;
        this.startDate = _startDate;
        this.endDate = _endDate;
        this.Comment = _comment;
    }

    static public List<Dictionary<String, String>> insertFreeDay(String fullName, DateTime _startDate, DateTime _endDate, String _Comment)
    {

        ResultCheckBlockDays blockDays = checkBlockDays(_startDate, _endDate);

        if (blockDays.flag == false)
        {
            ActiveDirectory activeDirectory = new ActiveDirectory();


            String _id = activeDirectory.Get_Fixed_Name(fullName);
            if (_id != "Cant Get Fixed Name" && _id != "Get Multiple Fixed Name")
            {
                String employeeName = activeDirectory.Get_Fixed_Name_id(_id);
                String manager_id = activeDirectory.Get_Name(_id, "manager").Split('=').ToArray<String>()[1].Substring(0, 8);
                String manager_name = activeDirectory.Get_Fixed_Name_id(manager_id);

                employeeName = employeeName.Replace("'", "''");

                String startDate = _startDate.ToString("yyyy-MM-dd HH:mm:ss");
                String endDate = _endDate.ToString("yyyy-MM-dd HH:mm:ss");
                String dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
                {
                    MySqlCommand Command = new MySqlCommand(@"Insert into fortigate.FreeDayEmployee (employee_id, employee_name, manager_name, startDate, endDate, comment, initialDate) 
                                                    Values('" + _id + "','" + employeeName + "','" + manager_name + "','" + startDate + "','" + endDate + "','" + _Comment + "','" + dateNow + "');", Conn);
                    Conn.Open();
                    try
                    {
                        Command.ExecuteNonQuery();

                    }
                    catch { }
                    finally { Conn.Close(); }
                }
                return null;
            }
            else if (_id == "Cant Get Fixed Name")
            {
                List<Dictionary<String, String>> error_message_list = new List<Dictionary<String, String>>();
                Dictionary<String, String> error_message = new Dictionary<String, String>();
                error_message.Add("error_message", "Cant Get Fixed Name");
                error_message_list.Add(error_message);
                return error_message_list;
            }
            else if (_id == "Get Multiple Fixed Name")
            {
                List<Dictionary<String, String>> error_message_list = new List<Dictionary<String, String>>();
                Dictionary<String, String> error_message = new Dictionary<String, String>();
                error_message.Add("error_message", "Get Multiple Fixed Name");
                error_message_list.Add(error_message);
                return error_message_list;
            }
            return null;

        }
        else
        {
            return blockDays.BlockDays;
        }
    }

    static public List<Dictionary<String, String>> insertFreeDayAlsoBlocks(String fullName, DateTime _startDate, DateTime _endDate, String _Comment)
    {

        ActiveDirectory activeDirectory = new ActiveDirectory();


            String _id = activeDirectory.Get_Fixed_Name(fullName);
            if (_id != "Cant Get Fixed Name" && _id != "Get Multiple Fixed Name")
            {
                String employeeName = activeDirectory.Get_Fixed_Name_id(_id);
                String manager_id = activeDirectory.Get_Name(_id, "manager").Split('=').ToArray<String>()[1].Substring(0, 8);
                String manager_name = activeDirectory.Get_Fixed_Name_id(manager_id);

                employeeName = employeeName.Replace("'", "''");

                String startDate = _startDate.ToString("yyyy-MM-dd HH:mm:ss");
                String endDate = _endDate.ToString("yyyy-MM-dd HH:mm:ss");
                String dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
                {
                    MySqlCommand Command = new MySqlCommand(@"Insert into fortigate.FreeDayEmployee (employee_id, employee_name, manager_name, startDate, endDate, comment, initialDate) 
                                                    Values('" + _id + "','" + employeeName + "','" + manager_name + "','" + startDate + "','" + endDate + "','" + _Comment + "','" + dateNow + "');", Conn);
                    Conn.Open();
                    try
                    {
                        Command.ExecuteNonQuery();

                    }
                    catch { }
                    finally { Conn.Close(); }
                }
                return null;
            }
            else if (_id == "Cant Get Fixed Name")
            {
                List<Dictionary<String, String>> error_message_list = new List<Dictionary<String, String>>();
                Dictionary<String, String> error_message = new Dictionary<String, String>();
                error_message.Add("error_message", "Cant Get Fixed Name");
                error_message_list.Add(error_message);
                return error_message_list;
            }
            else if (_id == "Get Multiple Fixed Name")
            {
                List<Dictionary<String, String>> error_message_list = new List<Dictionary<String, String>>();
                Dictionary<String, String> error_message = new Dictionary<String, String>();
                error_message.Add("error_message", "Get Multiple Fixed Name");
                error_message_list.Add(error_message);
                return error_message_list;
            }
            return null;

        }
        
    


    public void addFreeDayToDB()
    {
        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            MySqlCommand Command = new MySqlCommand(@"Insert into fortigate.FreeDayEmployee (id_emlpoyee, startDate, endTime, Comment) 
                                                Values('" + this.id + "','" + this.startDate + "','" + this.endDate + "','" + this.Comment + "');", Conn);
            Conn.Open();
            try
            {
                Command.ExecuteNonQuery();

            }
            catch { }
            finally { Conn.Close(); }
        }
    }

    static public List<Dictionary<String, String>> getAllFreeDays()
    {
        List<Dictionary<String, String>> allFreeDaysTmp = new List<Dictionary<string, string>>();

        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            //MySqlCommand Command = new MySqlCommand("SELECT group_code FROM fortigate.customer where group_code like '" + groupCode + "';", Conn);
            MySqlCommand Command = new MySqlCommand("SELECT * FROM fortigate.FreeDayEmployee;", Conn);
            Conn.Open();
            try
            {
                MySqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Dictionary<String, String> freeDayTmp = new Dictionary<string, string>();
                    freeDayTmp.Add("id", Reader["id"].ToString());
                    freeDayTmp.Add("title", Reader["employee_name"].ToString());
                    freeDayTmp.Add("employee_id", Reader["employee_id"].ToString());
                    freeDayTmp.Add("manager_name", Reader["manager_name"].ToString());
                    freeDayTmp.Add("startsAt", Reader["startDate"].ToString());
                    freeDayTmp.Add("endsAt", Reader["endDate"].ToString());
                    freeDayTmp.Add("comment", Reader["comment"].ToString());
                    freeDayTmp.Add("deletable", "False");
                    freeDayTmp.Add("editable", "False");
                    freeDayTmp.Add("endOpen", "False");
                    freeDayTmp.Add("startOpen", "False");
                    allFreeDaysTmp.Add(freeDayTmp);
                }

            }
            catch { }
            finally
            { Conn.Close(); }
        }


        return allFreeDaysTmp;
    }

    static public void deleteFreeDay(string id, String manager)
    {
        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            //MySqlCommand Command = new MySqlCommand("SELECT group_code FROM fortigate.customer where group_code like '" + groupCode + "';", Conn);
            MySqlCommand Command = new MySqlCommand("delete FROM fortigate.FreeDayEmployee where id =" + id + ";", Conn);
            Conn.Open();
            try
            {
                SendMail(id, manager);
                Command.ExecuteNonQuery();
            }
            catch { }
            finally
            { Conn.Close(); }
        }
    }

    static public List<Dictionary<String, String>> updateFreeDays(String _id, DateTime _startDate, DateTime _endDate, String _Comment)
    {
        ResultCheckBlockDays blockDays = checkBlockDays(_startDate, _endDate);

        if (blockDays.flag == false)
        {
            String startDate = _startDate.ToString("yyyy-MM-dd HH:mm:ss");
            String endDate = _endDate.ToString("yyyy-MM-dd HH:mm:ss");
            String dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
            {
                MySqlCommand Command = new MySqlCommand(@"UPDATE fortigate.FreeDayEmployee SET startDate = '" + startDate +
                                                        "', endDate = '" + endDate +
                                                        "', comment = '" + _Comment +
                                                        "', initialDate = '" + dateNow +
                                                        "' WHERE id like '" + _id + "';", Conn);

                Conn.Open();
                try
                {
                    Command.ExecuteNonQuery();

                }
                catch { }
                finally { Conn.Close(); }
            }
            return null;
        }
        else
        {
            return blockDays.BlockDays;
        }
    }

    static public ResultCheckBlockDays checkBlockDays(DateTime _startDate, DateTime _endDate)
    {
        ResultCheckBlockDays result;
        List<Dictionary<String, String>> blockDays = new List<Dictionary<string, string>>();
        int flag = 0;
        String startDate = _startDate.ToString("yyyy-MM-dd HH:mm:ss");
        String endDate = _endDate.ToString("yyyy-MM-dd HH:mm:ss");


        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            //MySqlCommand Command = new MySqlCommand("SELECT group_code FROM fortigate.customer where group_code like '" + groupCode + "';", Conn);
            MySqlCommand Command = new MySqlCommand("SELECT * FROM fortigate.FreeDayBlock where ('" + startDate + "'>= startDate and '" + startDate + "' <= endDate) or ('" + endDate + "' >= startDate and '" + endDate + "' <= endDate) or ('" + startDate + "' <= startDate and '" + endDate + "'>= endDate);", Conn);
            Conn.Open();
            try
            {
                MySqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    flag++;
                    Dictionary<String, String> freeDayTmp = new Dictionary<string, string>();
                    freeDayTmp.Add("id", Reader["id"].ToString());
                    freeDayTmp.Add("employee_name", Reader["employee_name"].ToString());
                    freeDayTmp.Add("employee_id", Reader["employee_id"].ToString());
                    freeDayTmp.Add("startsAt", Reader["startDate"].ToString());
                    freeDayTmp.Add("endsAt", Reader["endDate"].ToString());
                    freeDayTmp.Add("comment", Reader["comment"].ToString());
                    blockDays.Add(freeDayTmp);
                }
            }
            catch { }
            finally
            { Conn.Close(); }

            if (flag > 0)
            {
                result.flag = true;
                result.BlockDays = blockDays;
                return result;
            }
            else
            {
                result.flag = false;
                result.BlockDays = blockDays;
                return result;
            }
        }

    }

    static private void SendMail(String id, String remover_id)
    {
        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            ActiveDirectory activeDirectory = new ActiveDirectory();

            MySqlCommand Command = new MySqlCommand("Select * FROM fortigate.FreeDayEmployee where id =" + id + ";", Conn);
            Conn.Open();
            try
            {
                MySqlDataReader Reader = Command.ExecuteReader();
                Reader.Read();

                String employee_name = Reader["employee_name"].ToString();
                String manager_name = Reader["manager_name"].ToString();
                String startDate = Reader["startDate"].ToString();
                String endDate = Reader["endDate"].ToString();
                String comment = Reader["comment"].ToString();

                String manager_id = activeDirectory.Get_Fixed_Name(manager_name);
                String manager_mail = activeDirectory.Get_Name(manager_id, "mail");
                String remover_name = activeDirectory.Get_Fixed_Name_id(remover_id);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<html dir='rtl'><head>");
                sb.AppendLine("<style type='text/css'>");
                sb.AppendLine("BODY {font-family:arial;font-size:14px;}");
                sb.AppendLine("#tblData TD{ border:solid 1px #2050a0;}");
                sb.AppendLine("</style><body>");
                sb.AppendLine("<table align=\"right\" cellpadding=\"0\" cellapacing=\"0\" dir=\"rtl\"><tr><td>");
                sb.AppendLine(manager_name + " שלום רב,<br /><br />");
                sb.AppendLine("בוצעה כרגע מחיקת חופשה, <br /><br /></td></tr>");
                sb.AppendLine("<tr><td style=\"font-weight:bold;color:#2050a0;text-decoration:underline;border-radius:10px\">פרטי החופשה שנמחקה</td></tr>");
                sb.AppendLine("<tr><td><table cellpadding=\"3\" cellspacing=\"0\"  id=\"tblData\" border=\"1\" style=\"border:1px solid #000070\"><tr>");
                sb.AppendLine("<td style=\"width:120px\">שם העובד:</td><td style=\"width:160px;color:#ff0000;font-weight:bold\">" + employee_name + "</td></tr>");
                sb.AppendLine("<tr><td>תחילת החופשה: </td><td>" + startDate + "</td></tr>");
                sb.AppendLine("<tr><td align=\"right\" style=\"dir:rtl\">סוף החופשה: </td><td>" + endDate + "</td></tr>");
                sb.AppendLine("<tr><td>הערות החופשה:</td><td>" + comment + "</td></tr>");
                sb.AppendLine("<td style=\"width:120px\">נמחק על ידי:</td><td style=\"width:160px;color:#ff0000;font-weight:bold\">" + remover_name + "</td></tr>");
                sb.AppendLine("</table></td></tr></table>");
                sb.AppendLine("</body></html>");


                ActiveDirectory AD = new ActiveDirectory();
                MailMessage Msg = new MailMessage();
                SmtpClient MailServer = new SmtpClient("relay.bezeq.com");
                Msg.From = new MailAddress("FreeDays@bezeq.co.il");

                Msg.To.Add(manager_mail);
                //Msg.CC.Add(AD.Get_Name(User.Identity.Name.Substring(User.Identity.Name.IndexOf("\\") + 1), "mail"));
                Msg.CC.Add("matanho@bezeq.co.il");
                Msg.Subject = "מחיקת יום חופש במערכת ניהול חופשים ";
                Msg.IsBodyHtml = true;
                Msg.Body = sb.ToString();

                try
                {
                    MailServer.Send(Msg);
                }
                catch { };

            }
            catch { }
            finally
            { Conn.Close(); }
        }
    }
}
