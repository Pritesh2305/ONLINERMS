using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class feedback : System.Web.UI.Page
{
    string regusernm1;
    SqlConnection mssqlcon = new SqlConnection();
    feedbackdatabal bal = new feedbackdatabal();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            regusernm1 = Request.QueryString["feed"] + "";

            if (regusernm1 != "")
            {
                regusernm1 = regusernm1.Replace('"', ' ').Trim();

                mssqlcon.ConnectionString = ConfigurationManager.ConnectionStrings["ONLINERMS"].ConnectionString;
                mssqlcon.Open();
                SqlCommand cmd = new SqlCommand("Select * FROM MSTUSERS WHERE FEEDID=@feeduser AND ISNULL(ISBLOCK,0)=0", mssqlcon);
                //SqlCommand cmd = new SqlCommand("Select * FROM MSTUSERS WHERE ISNULL(ISBLOCK,0)=0", mssqlcon);
                cmd.Parameters.AddWithValue("@feeduser", regusernm1);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

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
                        this.lblrestname.Text = Session["coinfo"] + "";
                    }
                    else
                    {
                        this.lblinfo.Text = "Your Validity Expired. Please Contact to Krupa Infotech Support Team.";
                        this.lblinfo.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    Server.Transfer("page404.htm");
                    //Response.Redirect("page404.htm");
                }
            }
            else
            {
                Server.Transfer("page404.htm");
                //Response.Redirect("page404.htm");
            }

        }
        catch (Exception)
        {
            Server.Transfer("page404.htm");
        }
    }

    private bool IsPhoneNumber(string number)
    {
        try
        {
            //return Regex.Match(number, @"^[0-9]{10}$").Success;            
            return Regex.Match(number, @"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}").Success;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool CheckValidForm()
    {
        string name1 = "";
        string mobno1 = "";

        try
        {
            name1 = Request.Form["txtname"] + "".Trim();
            mobno1 = Request.Form["txtmobno"] + "".Trim();

            if (name1 == "")
            {
                this.lblinfo.Text = "Please Write Name.";
                this.lblinfo.ForeColor = System.Drawing.Color.Red;
                this.lblinfo.Font.Bold = true;
                this.txtname.Focus();
                return false;
            }

            if (mobno1 == "")
            {
                this.lblinfo.Text = "Please Write Mobile No.";
                this.lblinfo.ForeColor = System.Drawing.Color.Red;
                this.lblinfo.Font.Bold = true;
                this.txtmobno.Focus();
                return false;
            }

            if (!IsPhoneNumber(mobno1))
            {
                this.lblinfo.Text = "Please Write Valid Mobile No.";
                this.lblinfo.ForeColor = System.Drawing.Color.Red;
                this.lblinfo.Font.Bold = true;
                this.txtmobno.Focus();
                return false;
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected void btnsubmit_click(object sender, EventArgs e)
    {
        Int64 FeedRid1 = 0;
        try
        {
            Page.MaintainScrollPositionOnPostBack = true;

            if (CheckValidForm())
            {
                FeedRid1 = this.Save_Feedback();

                if (FeedRid1 > 0)
                {
                    Server.Transfer("feedbackthanks.aspx");
                }
                else
                {
                    Session["FeedError"] = "Problem in Saving Feedback or You have already submitted feedback.";
                    Server.Transfer("feedbackerror.aspx");
                }
            }
        }
        catch (Exception)
        { }
    }

    private Int64 Save_Feedback()
    {
        Int64 Rid1 = 0;
        string name1 = "";
        string mobno1 = "";
        string email1 = "";
        string WebBrowserName = string.Empty;
        string OSName = string.Empty;
        string Platform = "";
        int food1 = 0;
        int service1 = 0;
        int atmo = 0;
        int over = 0;
        string remark1 = "";

        try
        {

            name1 = Request.Form["txtname"] + "".Trim();
            mobno1 = Request.Form["txtmobno"] + "".Trim();
            email1 = Request.Form["txtemail"] + "".Trim();
            int.TryParse(Request.Form["selected_rating1"] + "", out food1);
            int.TryParse(Request.Form["selected_rating2"] + "", out service1);
            int.TryParse(Request.Form["selected_rating3"] + "", out atmo);
            int.TryParse(Request.Form["selected_rating4"] + "", out over);
            remark1 = Request.Form["txtsuggestion"] + "".Trim();

            WebBrowserName = HttpContext.Current.Request.Browser.Browser;
            OSName = HttpContext.Current.Request.Browser.Platform;
            if (Request.Browser.IsMobileDevice)
            {
                Platform = "Mobile";
            }
            else
            {
                Platform = "PC";
            }

            // Save Feedback //
            bal.Sernm = Session["sername"] + "";
            bal.Dbname = Session["dbname"] + "";
            bal.Dbusername = Session["dbusername"] + "";
            bal.Dbpass = Session["dbpass"] + "";

            bal.Rid = 0;
            bal.Feeddate = DateTime.Today.Date;
            bal.Guestname = name1;
            bal.Mobno = mobno1;
            bal.Email = email1;
            bal.Optfood = food1;
            bal.Optservice = service1;
            bal.Optatmo = atmo;
            bal.Optover = over;
            bal.Remark = remark1;
            bal.Osnm = OSName;
            bal.Bronm = WebBrowserName;
            bal.Devicenm = Platform;
            bal.FormMode = 0;
            bal.LoginUserId = 1;

            Rid1 = bal.Db_Operation_FEEDBACKDATA(bal);



            return Rid1;
        }
        catch (Exception)
        {
            return 0;
        }
    }
}