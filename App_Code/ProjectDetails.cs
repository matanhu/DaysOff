using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProjectDetails
/// </summary>
/// 
namespace Udi.ProjectDetails
{
    public class ProjectDetails
    {
        public string ProjectManagerID { get; set; }
        public string ProjectManagerName { get; set; }
        public string ProjectManagerPhone { get; set; }

        public string CustomerManagerID { get; set; }
        public string CustomerManagerName { get; set; }
        public string CustomerManagerPhone { get; set; }

        public string PreSaleAdviserID { get; set; }
        public string PreSaleAdviserName { get; set; }
        public string PreSaleAdviserPhone { get; set; }

        public string ReferantName { get; set; }
        public string CustomerName { get; set; }
        public ProjectDetails()
        {
        }
    }
}