using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Udi.ActiveDirectory;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for BlockDay
/// </summary>
public class BlockDay
{

    private String id;
    private DateTime startDate;
    private DateTime endDate;
    private String Comment;
    private ActiveDirectory activeDirectory;

    public BlockDay()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public BlockDay(String _id, DateTime _startDate, DateTime _endDate, String _comment)
    {
        this.id = _id;
        this.startDate = _startDate;
        this.endDate = _endDate;
        this.Comment = _comment;
    }

    static public List<Dictionary<String, String>> insertBlockDay(String _id, DateTime _startDate, DateTime _endDate, String _Comment)
    {


        ActiveDirectory activeDirectory = new ActiveDirectory();

        String employeeName = activeDirectory.Get_Fixed_Name_id(_id);

        employeeName = employeeName.Replace("'", "''");

        String startDate = _startDate.ToString("yyyy-MM-dd HH:mm:ss");
        String endDate = _endDate.ToString("yyyy-MM-dd HH:mm:ss");
        String dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            MySqlCommand Command = new MySqlCommand(@"Insert into fortigate.FreeDayBlock (employee_id, employee_name, startDate, endDate, comment, initialDate) 
                                                    Values('" + _id + "','" + employeeName + "','" + startDate + "','" + endDate + "','" + _Comment + "','" + dateNow + "');", Conn);
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

    static public List<Dictionary<String, String>> getAllBlockDays()
    {
        List<Dictionary<String, String>> allBlockDaysTmp = new List<Dictionary<string, string>>();

        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            //MySqlCommand Command = new MySqlCommand("SELECT group_code FROM fortigate.customer where group_code like '" + groupCode + "';", Conn);
            MySqlCommand Command = new MySqlCommand("SELECT * FROM fortigate.FreeDayBlock;", Conn);
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
                    freeDayTmp.Add("startsAt", Reader["startDate"].ToString());
                    freeDayTmp.Add("endsAt", Reader["endDate"].ToString());
                    freeDayTmp.Add("comment", Reader["comment"].ToString());
                    freeDayTmp.Add("deletable", "true");
                    freeDayTmp.Add("editable", "true");
                    freeDayTmp.Add("endOpen", "True");
                    freeDayTmp.Add("startOpen", "false");
                    allBlockDaysTmp.Add(freeDayTmp);
                }

            }
            catch { }
            finally
            { Conn.Close(); }
        }


        return allBlockDaysTmp;
    }

    static public void deleteBlockDay(string id, String manager)
    {
        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            //MySqlCommand Command = new MySqlCommand("SELECT group_code FROM fortigate.customer where group_code like '" + groupCode + "';", Conn);
            MySqlCommand Command = new MySqlCommand("delete FROM fortigate.FreeDayBlock where id =" + id + ";", Conn);
            Conn.Open();
            try
            {
                Command.ExecuteNonQuery();
            }
            catch { }
            finally
            { Conn.Close(); }
        }
    }

    static public List<Dictionary<String, String>> updateBlockDays(String _id, DateTime _startDate, DateTime _endDate, String _Comment)
    {

        String startDate = _startDate.ToString("yyyy-MM-dd HH:mm:ss");
        String endDate = _endDate.ToString("yyyy-MM-dd HH:mm:ss");
        String dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            MySqlCommand Command = new MySqlCommand(@"UPDATE fortigate.FreeDayBlock SET startDate = '" + startDate +
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

}
