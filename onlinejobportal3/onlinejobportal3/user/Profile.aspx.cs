﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace onlinejobportal3.user
{
    
    public partial class Profile1 : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        private string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                ShowUserProfile();
            }
        }

        private void ShowUserProfile()
        {
            con = new SqlConnection(str);
            string query = "Select UserId,Username,Name,Address,Mobile,Email,Country,Resume from [User] where Username=@username";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", Session["user"]);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                dlProfile.DataSource = dt;
                dlProfile.DataBind();
            }
            else
            {
                Response.Write("<script>alert('please do login again with your latest username')</script>");
            }
           
        }

        protected void dlProfile_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "EditUserProfile")
            {
                Response.Redirect("ResumeBuild.aspx?id=" + e.CommandArgument.ToString());
            }
        }
    }
}