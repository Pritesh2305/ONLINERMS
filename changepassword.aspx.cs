using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class changepassword : System.Web.UI.Page
{
    SqlConnection mssqlcon = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if ((Session["rid"] == null) || (Session["rid"] + "" == ""))
            {
                Response.Redirect("index.html");
            }

            if ((Session["username"] == null) || (Session["username"] + "" == ""))
            {
                Response.Redirect("index.html");
            }

            //Username = Session["username"] + "";
            //Sername = Session["sername"] + "";
            //Dbname = Session["dbname"] + "";
            //Dbusername = Session["dbusername"] + "";
            //Dbpass = Session["dbpass"] + "";

            this.lblrestname.Text = "  " + Session["coinfo"] + "" + "".ToUpper();

            Page.MaintainScrollPositionOnPostBack = true;
        }
        catch (Exception)
        { }
    }

    protected void btnsubmit_click(object sender, EventArgs e)
    {
        try
        {
            this.lblinfo.Text = "";

            if (CheckValidation())
            {
                mssqlcon.ConnectionString = ConfigurationManager.ConnectionStrings["ONLINERMS"].ConnectionString;
                mssqlcon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM MSTUSERS WHERE USERNAME=@username AND ISNULL(ISBLOCK,0)=0", mssqlcon);
                cmd.Parameters.AddWithValue("@username", Session["username"] + "");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string pass1 = "";
                    pass1 = dt.Rows[0]["PASSWORD"] + "".Trim();

                    if (pass1 != this.txtcupass.Text.Trim())
                    {
                        this.lblinfo.Text = "Invalid Current Password.";
                        this.txtcupass.Focus();
                    }
                    else
                    {
                        string str1 = "";
                        str1 = "UPDATE MSTUSERS SET PASSWORD = '" + this.txtnepass.Text.Trim() + "'" + " WHERE RID = " + Session["rid"] + "";
                        SqlCommand cmd1 = new SqlCommand(str1,mssqlcon);
                        int retval = cmd1.ExecuteNonQuery();

                        if (retval > 0)
                        {
                            this.btnhome_click(sender, e);
                        }
                        else
                        {
                            this.lblinfo.Text = "Sorry, Some Problem Occured in Change Password Process. Please Try Again.";
                        }
                    }
                }
            }
            // Session.RemoveAll();
            // Response.Redirect("index.html");
        }
        catch (Exception)
        { }
    }

    private bool CheckValidation()
    {
        try
        {
            if (this.txtcupass.Text.Trim() == "")
            {
                this.txtcupass.Focus();
                this.lblinfo.Text = "Please Enter Current Password.";
                return false;
            }

            if (this.txtnepass.Text.Trim() == "")
            {
                this.txtnepass.Focus();
                this.lblinfo.Text = "Please Enter New Password.";
                return false;
            }

            if (this.txtcopass.Text.Trim() == "")
            {
                this.txtcopass.Focus();
                this.lblinfo.Text = "Please Enter Confirm Password.";
                return false;
            }

            if (this.txtnepass.Text.Trim() != this.txtcopass.Text.Trim())
            {
                this.txtcopass.Focus();
                this.lblinfo.Text = "Invalid Confirm Password.";
                return false;
            }




            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }



    protected void btnhome_click(object sender, EventArgs e)
    {
        try
        {
            Session.RemoveAll();
            Response.Redirect("index.html");
        }
        catch (Exception)
        { }
    }

}