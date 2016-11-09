using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Script.Services;
using System.Configuration;
using Udi.ProjectDetails;
using Udi.ProjectDocs;
using com.bezeq.bdeskws;
using System.Globalization;
using Udi.ActiveDirectory;

public partial class _Default : System.Web.UI.Page
{
    private static String user;

    protected void Page_Load(object sender, EventArgs e)
    {
        //user = User.Identity.Name.Substring(5);
    }



    public Dictionary<String, String> getEmployee(String _id)
    {
        return Employee.getEmployee(_id);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static Dictionary<String, String> getUserDetails()
    {
        Dictionary<String, String> userDetails;
        ActiveDirectory ad = new ActiveDirectory();
        String user = HttpContext.Current.User.Identity.Name.Substring(5);

        ad.Get_Fixed_Name_id(user);
        userDetails = Manager.getManagerDetails(user);
        if (userDetails != null)
        {
            return userDetails;
        }
        else
        {
            userDetails = new Dictionary<string, string>();
            userDetails.Add("employee_id", user);
            userDetails.Add("employee_name", ad.Get_Fixed_Name_id(user));
            userDetails.Add("manager", "False");
            userDetails.Add("admin", "False");
            return userDetails;
        }


    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static void addFreeDay(String _id, DateTime startDate, DateTime endDate, String Comment)
    {
        FreeDay freeDay = new FreeDay(_id, startDate, endDate, Comment);
        freeDay.addFreeDayToDB();
    }

    
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<Dictionary<String, String>> getAllFreeDays()
    {
        return FreeDay.getAllFreeDays();

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public static List<Dictionary<String, String>> getAllBlockDays()
    {
        return BlockDay.getAllBlockDays();

    }


    [WebMethod]
    public static List<Dictionary<String, String>> insertFreeDays(String fullName, String StartAt, String EndAt, String Comment)
    {
        return FreeDay.insertFreeDay(fullName , Convert.ToDateTime(StartAt), Convert.ToDateTime(EndAt), Comment);
    }

    [WebMethod]
    public static List<Dictionary<String, String>> insertFreeDayAlsoBlocks(String fullName, String StartAt, String EndAt, String Comment)
    {
        return FreeDay.insertFreeDayAlsoBlocks(fullName, Convert.ToDateTime(StartAt), Convert.ToDateTime(EndAt), Comment);
    }
    

    [WebMethod]
    public static List<Dictionary<String, String>> insertBlockDays(String ID, String StartAt, String EndAt, String Comment)
    {
        return BlockDay.insertBlockDay(ID, Convert.ToDateTime(StartAt), Convert.ToDateTime(EndAt), Comment);
    }

    [WebMethod]
    public static void deleteFreeDay(String ID, String remover_id)
    {
        FreeDay.deleteFreeDay(ID, remover_id);
    }

    [WebMethod]
    public static void deleteBlockDay(String ID, String remover_id)
    {
        BlockDay.deleteBlockDay(ID, remover_id);
    }

    [WebMethod]
    public static List<Dictionary<String, String>> updateFreeDays(String ID, String StartAt, String EndAt, String Comment)
    {
        return FreeDay.updateFreeDays(ID, Convert.ToDateTime(StartAt), Convert.ToDateTime(EndAt), Comment);
    }

    [WebMethod]
    public static List<Dictionary<String, String>> updateBlockDays(String ID, String StartAt, String EndAt, String Comment)
    {
        return BlockDay.updateBlockDays(ID, Convert.ToDateTime(StartAt), Convert.ToDateTime(EndAt), Comment);
    }
    

    


}