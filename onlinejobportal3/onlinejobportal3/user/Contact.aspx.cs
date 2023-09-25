using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace onlinejobportal3.user
{
    public partial class Contact : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(str);
                string query = "Insert into Contact values(@Name,@Email,@Subject,@Message)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name",name.Value.Trim());
                cmd.Parameters.AddWithValue("@Email",email .Value.Trim());
                cmd.Parameters.AddWithValue("@Subject",subject.Value.Trim());
                cmd.Parameters.AddWithValue("@Message",message.Value.Trim());
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if(r > 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Thanks for reaching out will look your query";
                    lblmsg.CssClass = "alert alert-success";
                    Clear();
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "cannot save message right now, please try after some time";
                    lblmsg.CssClass = "alert alert-danger";
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close();
            }
        }

        private void Clear()
        {
            name.Value = string.Empty;
            email.Value = string.Empty;
            subject.Value = string.Empty;
            message.Value = string.Empty;
        }
    }
}