﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestWeb
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectsURL.Value = ConfigurationManager.AppSettings["Projects_Service"].ToString();
            TasksURL.Value = ConfigurationManager.AppSettings["Task_Service"].ToString();

            txtIs_Billable.Items.Add("true");
            txtIs_Billable.Items.Add("false");

            txtIs_Active.Items.Add("true");
            txtIs_Active.Items.Add("false");
        }
    }
}