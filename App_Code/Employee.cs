using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for Employee
/// </summary>
public class Employee
{
    private string id;
    private string name;
    private string manager;

    public Employee()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Employee(String _id, String _name, String _manager)
    {
        if (!checkEmployee(_id))
        {
            this.id = _id;
            this.name = _name;
            this.manager = _manager;
            using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
            {
                MySqlCommand Command = new MySqlCommand("Insert into fortigate.Employee (id, name, manager) Values('" + _id + "','" + _name + "','" + _manager + "');", Conn);
                Conn.Open();
                try
                {
                    Command.ExecuteNonQuery();

                }
                catch { }
                finally { Conn.Close(); }
            }
        }
        else
        {
            Dictionary<String, String> employee = getEmployee(_id);
            this.id = employee["id"];
            this.name = employee["name"];
            this.manager = employee["manager"];            
        }
    }

    static public Dictionary<String, String> getEmployee(String _id)
    {
        Dictionary<String, String> employeeDetailsTmp = new Dictionary<string, string>();
        List<Dictionary<String, String>> employee = new List<Dictionary<string, string>>();

        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            MySqlCommand Command = new MySqlCommand("SELECT * FROM fortigate.Employee where id like '" + _id + "';", Conn);
            Conn.Open();
            try
            {
                MySqlDataReader Reader = Command.ExecuteReader();
                Reader.Read();
                employeeDetailsTmp.Add("id", Reader["id"].ToString());
                employeeDetailsTmp.Add("name", Reader["name"].ToString());
                employeeDetailsTmp.Add("manager", Reader["manager"].ToString());
            }
            catch { }
            finally { Conn.Close(); }

            return employeeDetailsTmp;
        }
    }

    static public Boolean checkEmployee(String _id)
    {
        Boolean flag = new Boolean();
        using (MySqlConnection Conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["customersConnectionString"].ConnectionString))
        {
            MySqlCommand Command = new MySqlCommand("SELECT count(*) FROM fortigate.Employee where id like '" + _id + "';", Conn);
            Conn.Open();
            try
            {
                MySqlDataReader Reader = Command.ExecuteReader();
                Reader.Read();
                flag = (Convert.ToInt32(Reader["count"].ToString()) > 0 ? true : false);
            }
            catch { }
            finally { Conn.Close(); }
        }

        return flag;
    }

    public String getId()
    {
        return this.id;
    }

    public void setId(String _id)
    {
        this.id = _id;
    }

    
}