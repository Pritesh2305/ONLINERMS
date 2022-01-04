using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

public partial class login : System.Web.UI.Page
{
    SqlConnection mssqlcon = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnlogin_click(object sender, EventArgs e)
    {
        try
        {
            //Response.Write("before conn str");
            mssqlcon.ConnectionString = ConfigurationManager.ConnectionStrings["ONLINERMS"].ConnectionString;
            mssqlcon.Open();
            //Response.Write("after conn open");
            SqlCommand cmd = new SqlCommand("Select * from MSTUSERS where USERNAME=@username AND PASSWORD=@word AND ISNULL(ISBLOCK,0)=0", mssqlcon);
            cmd.Parameters.AddWithValue("@username", txtusername.Text);
            cmd.Parameters.AddWithValue("word", txtpassword.Text);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //Response.Write("after data fill");

            int i = cmd.ExecuteNonQuery();
            mssqlcon.Close();
            if (dt.Rows.Count > 0)
            {
                Session["rid"] = dt.Rows[0]["RID"] + "".Trim();
                Session["username"] = dt.Rows[0]["username"] + "".Trim();
                Session["sername"] = dt.Rows[0]["sername"] + "".Trim();
                Session["dbname"] = dt.Rows[0]["dbname"] + "".Trim();
                Session["dbusername"] = dt.Rows[0]["dbusername"] + "".Trim();
                Session["dbpass"] = dt.Rows[0]["dbpass"] + "".Trim();
                Session["coinfo"] = dt.Rows[0]["coinfo"] + "".Trim();
                Session["uptodate"] = dt.Rows[0]["uptodate"] + "".Trim();

                DateTime dtuptodate;
                int result = 0;
                DateTime.TryParse(Session["uptodate"] + "", out dtuptodate);
                result = DateTime.Compare(dtuptodate, DateTime.Now.Date);

                if (result > 0)
                {
                    Server.Transfer("rmsnewreport.aspx");
                }
                else
                {
                    Label1.Text = "Your Validity Expired. Please Contact to Krupa Infotech Support Team.";
                    Label1.ForeColor = System.Drawing.Color.Red;
                }

                // Response.Redirect("page2.aspx", false);
            }
            else
            {
                Label1.Text = "Invalid Username or Password";
                Label1.ForeColor = System.Drawing.Color.Red;
            }

        }
        catch (Exception ex)
        {
            Response.Write("Error : " + ex.Message.ToString());
        }
    }

    //protected void Logout_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Session.RemoveAll();
    //        Response.Redirect("index.html");
    //    }
    //    catch (Exception)
    //    { }
    //}
}