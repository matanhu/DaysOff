using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;

namespace Udi.ActiveDirectory
{
    public class ActiveDirectory
    {
        protected string Id { get; set; }
        public string Name;
        public ActiveDirectory() { }
        public string Get_Name(string _id, string prop)
        {
            using (DirectoryEntry entry = new DirectoryEntry("LDAP://bezeq.com", "amam\\spsv2t", "spsv2t"))
            {
                DirectorySearcher dSearcher = new DirectorySearcher(entry); // { Filter = string.Format("(cn={0})", _id) };
                dSearcher.Filter = "(&(objectClass=user)(cn=" + _id + "))";
                dSearcher.PropertiesToLoad.Add(prop);
                if (dSearcher.FindOne().Properties.Contains(prop))
                    return (dSearcher.FindOne().Properties[prop][0].ToString());
                else
                    return ("No such property as " + prop);
            }
        }

        // הפונקציה הזו מקבלת מספר תעודת זהות 8 ספרות ומחזירה את שם העובד 
        public string Get_Fixed_Name_id(string _id)
        {
            using (DirectoryEntry entry = new DirectoryEntry("LDAP://bezeq.com", "amam\\spsv2t", "spsv2t"))
            {
                DirectorySearcher dSearcher = new DirectorySearcher(entry);
                dSearcher.Filter = "(&(objectClass=user)(cn=" + _id + "))";
                dSearcher.PropertiesToLoad.Add("DisplayName");
                if (dSearcher.FindOne().Properties.Contains("DisplayName"))
                {
                    string result = dSearcher.FindOne().Properties["DisplayName"][0].ToString();
                    string[] result_AR = result.Split('-');
                    return (result_AR[0].Trim());
                }
                else
                    return ("Cant Get Fixed Name");
            }
        }

        // הפונקציה הזו מקבלת שם באנגלית ומחזירה את שם העובד 
        public string Get_Fixed_Name(string fullName)
        {
            
            using (DirectoryEntry entry = new DirectoryEntry("LDAP://bezeq.com", "amam\\spsv2t", "spsv2t"))
            {
                DirectorySearcher dSearcher = new DirectorySearcher(entry);
                //dSearcher.Filter = "(&(objectClass=user)(cn=" + _id + "))";
                //dSearcher.Filter = "(&(objectClass=user)(givenName="+fName+")(sn=" +sName+ "))";
                //dSearcher.Filter = "(&(objectCategory=person)(objectClass=user)(displayName=*" + fName + " " + sName + "*)(memberOf=CN=IPVPN_Nemo_Users,OU=IPVPN,OU=APG,OU=NEMO,DC=bezeq,DC=com))";
                dSearcher.Filter = "(&(objectCategory=person)(objectClass=user)(displayName=*" + fullName + "*)(memberOf=CN=Portal_IPVPN_Nemo_Users,OU=POR,OU=APG,OU=NEMO,DC=bezeq,DC=com))";
                dSearcher.PropertiesToLoad.Add("cn");
                dSearcher.PropertiesToLoad.Add("memberOf");
                SearchResultCollection userCollection = dSearcher.FindAll();
                if (userCollection.Count == 0)
                {
                    return ("Cant Get Fixed Name");
                }
                if (userCollection.Count == 1)
                {
                    foreach (SearchResult users in userCollection)
                    {

                        users.Properties["memberOf"].IndexOf("IPVPN");
                        if (users.Properties.Contains("cn"))
                        {
                            string result = users.Properties["cn"][0].ToString();
                            string[] result_AR = result.Split('-');
                            return (result_AR[0].Trim());
                        }
                    }

                }
                else
                {
                    return ("Get Multiple Fixed Name");
                }
                return ("Cant Get Fixed Name");
            }
        }
    }
}