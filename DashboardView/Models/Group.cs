using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardView.Models
{
    public class Group
    {
        public string Title { get; set; }
        public List<Build> Builds { get; set; }
    }
}