using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for Manager
/// </summary>
public class Manager
{
    private String employee_id;
    private String employee_name;
    private Boolean manager;
    private Boolean admin;

	public Manager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static Dictionary<String, String> getManagerDetails(String id)
    {
        Dictionary<String, String> managerDetails = new Dictionary<string, string>();
        int flag = 0;

        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            //MySqlCommand Command = new MySqlCommand("SELECT group_code FROM fortigate.customer where group_code like '" + groupCode + "';", Conn);
            MySqlCommand Command = new MySqlCommand("SELECT * FROM fortigate.Managers WHERE employee_id like '" + id + "';", Conn);
            Conn.Open();
            try
            {
                MySqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    flag++;
                    managerDetails.Add("employee_id", Reader["employee_id"].ToString());
                    managerDetails.Add("employee_name", Reader["employee_name"].ToString());
                    managerDetails.Add("manager", Reader["manager"].ToString());
                    managerDetails.Add("admin", Reader["admin"].ToString());
                }

            }
            catch { }
            finally
            { Conn.Close(); }
        }
        if (flag > 0)
        {
            return managerDetails;
        }
        else
        {
            return null;
        }
    }

}