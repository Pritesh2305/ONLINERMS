using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class feedbackreport : System.Web.UI.Page
{
    protected string userid = "";
    protected SqlConnection mssqlcon = new SqlConnection();

    protected DataTable DtBillReg = new DataTable();
    protected DataTable DtBilldtl = new DataTable();
    protected DataTable DtitemsaleReg = new DataTable();
    protected DataTable Dtfeedbackdetail = new DataTable();

    protected DataGrid dgGrid = new DataGrid();

    private string _sername;
    private string _dbname;
    private string _dbusername;
    private string _dbpass;
    private string _coinfo;
    private string _username;

    public string Username
    {
        get { return _username; }
        set { _username = value; }
    }
    public string Coinfo
    {
        get { return _coinfo; }
        set { _coinfo = value; }
    }
    public string Dbpass
    {
        get { return _dbpass; }
        set { _dbpass = value; }
    }
    public string Dbusername
    {
        get { return _dbusername; }
        set { _dbusername = value; }
    }
    public string Dbname
    {
        get { return _dbname; }
        set { _dbname = value; }
    }
    public string Sername
    {
        get { return _sername; }
        set { _sername = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void ExportToExcel(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                string filename = "OnlineRmsReport.xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.Flush();
                Response.SuppressContent = true;
            }
        }
        catch (Exception)
        {
            //Log some trace info here
        }
    }

    protected void Logout_Click(object sender, EventArgs e)
    {
        try
        {
            Session.RemoveAll();
            Response.Redirect("index.html");
        }
        catch (Exception)
        { }
    }

    protected void btnfeedbackdetail_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_feedbackdetail();
        }
        catch (Exception)
        { }
    }

    protected void feedbackdetail2excel(object sender, EventArgs e)
    {
        try
        {
            this.Gen_feedbackdetail();
            this.ExportToExcel(Dtfeedbackdetail);

        }
        catch (Exception)
        { }
    }

    protected void btnchartfoodquality_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_ChartFoodQuality();
        }
        catch (Exception)
        { }
    }

    protected void btnchartservice_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_ChartService();
        }
        catch (Exception)
        { }
    }

    protected void btnchartatmosphere_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_ChartAtmosphere();
        }
        catch (Exception)
        { }
    }

    protected void btnchartoverall_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_ChartOverall();
        }
        catch (Exception)
        { }
    }

    #region ReportFunction

    private bool Gen_feedbackdetail()
    {
        string connstr = "";
        string qry = "";
        string sdt = "";
        string edt = "";

        try
        {
            connstr = "Data Source=" + Sername + ";Integrated Security=false;Initial Catalog=" + Dbname + ";User ID=" + Dbusername + ";Password=" + Dbpass;
            mssqlcon.ConnectionString = connstr;
            mssqlcon.Open();

            sdt = Request.Form[dtpfeedbackdetailsdt.UniqueID];
            edt = Request.Form[dtpfeedbackdetailedt.UniqueID];

            dtpfeedbackdetailsdt.Text = sdt;
            dtpfeedbackdetailedt.Text = edt;

            if ((sdt != null) && (edt != null))
            {
                qry = " SELECT FEEDBACKDATA.MOBNO,FEEDBACKDATA.GUESTNAME,FEEDBACKDATA.EMAIL,FEEDBACKDATA.OPTFOOD,FEEDBACKDATA.OPTSERVICE,FEEDBACKDATA.OPTATMO,FEEDBACKDATA.OPTOVER,FEEDBACKDATA.REMARK,CONVERT(VARCHAR,FEEDBACKDATA.FEEDDATE,103) AS FEEDDATE " +
                        " FROM FEEDBACKDATA WHERE ISNULL(FEEDBACKDATA.DELFLG,0)=0 AND FEEDBACKDATA.FEEDDATE BETWEEN @p_fromdate and @p_todate ";
                SqlCommand mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = mscmd;
                sda.Fill(Dtfeedbackdetail);
                mssqlcon.Close();

                string rptdtl = "";

                rptdtl += "<table id='tblfeedbackdetail' class='table table-hover'>";
                rptdtl += "<thead>";
                rptdtl += "<tr bgcolor='#ec891d'> <th scope='col'>MOBILE</th><th scope='col'>GUEST NAME</th><th scope='col'>E-MAIL</th><th scope='col'>FOOD RATING</th><th scope='col'>SERVICE RATING</th><th scope='col'>ATMOSPHERE RATING</th><th scope='col'>OVERALL RATING</th><th scope='col'>SUGGESTION</th><th scope='col'>FEEDBACK DATE</th>";
                rptdtl += "</tr>";
                rptdtl += "</thead>";
                rptdtl += "<tbody>";
                foreach (DataRow dr in Dtfeedbackdetail.Rows)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<th scope='row'>" + dr["MOBNO"].ToString() + "</th>";
                    rptdtl = rptdtl + "<td>" + dr["GUESTNAME"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["EMAIL"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["OPTFOOD"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["OPTSERVICE"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["OPTATMO"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["OPTOVER"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["REMARK"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["FEEDDATE"].ToString() + "</td>";
                }
                rptdtl = rptdtl + "</tbody>";
                rptdtl = rptdtl + "</table>";

                divfeedbackdetail.InnerHtml = rptdtl;

            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool Gen_ChartFoodQuality()
    {
        StringBuilder sb = new StringBuilder();

        string connstr = "";
        string qry = "";
        string sdt = "";
        string edt = "";
        SqlCommand mscmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dtchartfoodquality = new DataTable();

        try
        {
            connstr = "Data Source=" + Sername + ";Integrated Security=false;Initial Catalog=" + Dbname + ";User ID=" + Dbusername + ";Password=" + Dbpass;
            mssqlcon.ConnectionString = connstr;
            mssqlcon.Open();

            sdt = Request.Form[dtpchartfoodqualitysdt.UniqueID];
            edt = Request.Form[dtpchartfoodqualityedt.UniqueID];

            this.dtpchartfoodqualitysdt.Text = sdt;
            this.dtpchartfoodqualityedt.Text = edt;

            if ((sdt != null) && (edt != null))
            {
                qry = "SELECT SUM(CASE WHEN OPTFOOD=0 THEN 1 ELSE 0 END) AS STAR0, " +
                        " SUM(CASE WHEN OPTFOOD=1 THEN 1 ELSE 0 END) AS STAR1, " +
                        " SUM(CASE WHEN OPTFOOD=2 THEN 1 ELSE 0 END) AS STAR2, " +
                        " SUM(CASE WHEN OPTFOOD=3 THEN 1 ELSE 0 END) AS STAR3, " +
                        " SUM(CASE WHEN OPTFOOD=4 THEN 1 ELSE 0 END) AS STAR4, " +
                        " SUM(CASE WHEN OPTFOOD=5 THEN 1 ELSE 0 END) AS STAR5 " +
                        " FROM FEEDBACKDATA " +
                        " WHERE ISNULL(FEEDBACKDATA.DELFLG, 0) = 0 AND FEEDBACKDATA.FEEDDATE BETWEEN @p_fromdate and @p_todate ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtchartfoodquality);

                string star0 = "0";
                string star1 = "0";
                string star2 = "0";
                string star3 = "0";
                string star4 = "0";
                string star5 = "0";

                if (dtchartfoodquality.Rows.Count > 0)
                {
                    star0 = dtchartfoodquality.Rows[0]["STAR0"] + "";
                    star1 = dtchartfoodquality.Rows[0]["STAR1"] + "";
                    star2 = dtchartfoodquality.Rows[0]["STAR2"] + "";
                    star3 = dtchartfoodquality.Rows[0]["STAR3"] + "";
                    star4 = dtchartfoodquality.Rows[0]["STAR4"] + "";
                    star5 = dtchartfoodquality.Rows[0]["STAR5"] + "";

                    sb.Append("<script type='text/javascript' language='javascript'>");
                    sb.Append("google.charts.load('current', { 'packages': ['corechart'] });");
                    sb.Append("google.charts.setOnLoadCallback(drawChart);");
                    sb.Append("function drawChart() {");
                    sb.Append("var data = google.visualization.arrayToDataTable([");
                    sb.Append("['STAR', 'FEEDBACK'],['0 Star'," + star0 + "],['1 Star', " + star1 + "],['2 Star', " + star2 + "],['3 Star', " + star3 + "],['4 Star', " + star4 + "],['5 Star', " + star5 + "]]);");
                    sb.Append("var options = { 'title': 'FOOD QUALITY FEEDBACK CHART', 'width': 700, 'height': 700 };");
                    sb.Append("var chart = new google.visualization.PieChart(document.getElementById('divchartfoodquality'));");
                    sb.Append("chart.draw(data, options);");
                    sb.Append(" } </script>) ");

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "doSomething", sb.ToString());
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool Gen_ChartService()
    {
        StringBuilder sb = new StringBuilder();

        string connstr = "";
        string qry = "";
        string sdt = "";
        string edt = "";
        SqlCommand mscmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dtchartservice = new DataTable();

        try
        {
            connstr = "Data Source=" + Sername + ";Integrated Security=false;Initial Catalog=" + Dbname + ";User ID=" + Dbusername + ";Password=" + Dbpass;
            mssqlcon.ConnectionString = connstr;
            mssqlcon.Open();

            sdt = Request.Form[this.dtpchartservicesdt.UniqueID];
            edt = Request.Form[this.dtpchartserviceedt.UniqueID];

            this.dtpchartservicesdt.Text = sdt;
            this.dtpchartserviceedt.Text = edt;

            if ((sdt != null) && (edt != null))
            {
                qry = "SELECT SUM(CASE WHEN OPTSERVICE=0 THEN 1 ELSE 0 END) AS STAR0, " +
                        " SUM(CASE WHEN OPTSERVICE=1 THEN 1 ELSE 0 END) AS STAR1, " +
                        " SUM(CASE WHEN OPTSERVICE=2 THEN 1 ELSE 0 END) AS STAR2, " +
                        " SUM(CASE WHEN OPTSERVICE=3 THEN 1 ELSE 0 END) AS STAR3, " +
                        " SUM(CASE WHEN OPTSERVICE=4 THEN 1 ELSE 0 END) AS STAR4, " +
                        " SUM(CASE WHEN OPTSERVICE=5 THEN 1 ELSE 0 END) AS STAR5 " +
                        " FROM FEEDBACKDATA " +
                        " WHERE ISNULL(FEEDBACKDATA.DELFLG, 0) = 0 AND FEEDBACKDATA.FEEDDATE BETWEEN @p_fromdate and @p_todate ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtchartservice);

                string star0 = "0";
                string star1 = "0";
                string star2 = "0";
                string star3 = "0";
                string star4 = "0";
                string star5 = "0";

                if (dtchartservice.Rows.Count > 0)
                {
                    star0 = dtchartservice.Rows[0]["STAR0"] + "";
                    star1 = dtchartservice.Rows[0]["STAR1"] + "";
                    star2 = dtchartservice.Rows[0]["STAR2"] + "";
                    star3 = dtchartservice.Rows[0]["STAR3"] + "";
                    star4 = dtchartservice.Rows[0]["STAR4"] + "";
                    star5 = dtchartservice.Rows[0]["STAR5"] + "";

                    sb.Append("<script type='text/javascript' language='javascript'>");
                    sb.Append("google.charts.load('current', { 'packages': ['corechart'] });");
                    sb.Append("google.charts.setOnLoadCallback(drawChart);");
                    sb.Append("function drawChart() {");
                    sb.Append("var data = google.visualization.arrayToDataTable([");
                    sb.Append("['STAR', 'FEEDBACK'],['0 Star'," + star0 + "],['1 Star', " + star1 + "],['2 Star', " + star2 + "],['3 Star', " + star3 + "],['4 Star', " + star4 + "],['5 Star', " + star5 + "]]);");
                    sb.Append("var options = { 'title': 'SERVICE FEEDBACK CHART', 'width': 700, 'height': 700 };");
                    sb.Append("var chart = new google.visualization.PieChart(document.getElementById('divchartservice'));");
                    sb.Append("chart.draw(data, options);");
                    sb.Append(" } </script>) ");

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "doSomething", sb.ToString());
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool Gen_ChartAtmosphere()
    {
        StringBuilder sb = new StringBuilder();

        string connstr = "";
        string qry = "";
        string sdt = "";
        string edt = "";
        SqlCommand mscmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dtchartatmo = new DataTable();

        try
        {
            connstr = "Data Source=" + Sername + ";Integrated Security=false;Initial Catalog=" + Dbname + ";User ID=" + Dbusername + ";Password=" + Dbpass;
            mssqlcon.ConnectionString = connstr;
            mssqlcon.Open();

            sdt = Request.Form[this.dtpchartatmospheresdt.UniqueID];
            edt = Request.Form[this.dtpchartatmosphereedt.UniqueID];

            this.dtpchartservicesdt.Text = sdt;
            this.dtpchartserviceedt.Text = edt;

            if ((sdt != null) && (edt != null))
            {
                qry = "SELECT SUM(CASE WHEN OPTATMO=0 THEN 1 ELSE 0 END) AS STAR0, " +
                        " SUM(CASE WHEN OPTATMO=1 THEN 1 ELSE 0 END) AS STAR1, " +
                        " SUM(CASE WHEN OPTATMO=2 THEN 1 ELSE 0 END) AS STAR2, " +
                        " SUM(CASE WHEN OPTATMO=3 THEN 1 ELSE 0 END) AS STAR3, " +
                        " SUM(CASE WHEN OPTATMO=4 THEN 1 ELSE 0 END) AS STAR4, " +
                        " SUM(CASE WHEN OPTATMO=5 THEN 1 ELSE 0 END) AS STAR5 " +
                        " FROM FEEDBACKDATA " +
                        " WHERE ISNULL(FEEDBACKDATA.DELFLG, 0) = 0 AND FEEDBACKDATA.FEEDDATE BETWEEN @p_fromdate and @p_todate ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtchartatmo);

                string star0 = "0";
                string star1 = "0";
                string star2 = "0";
                string star3 = "0";
                string star4 = "0";
                string star5 = "0";

                if (dtchartatmo.Rows.Count > 0)
                {
                    star0 = dtchartatmo.Rows[0]["STAR0"] + "";
                    star1 = dtchartatmo.Rows[0]["STAR1"] + "";
                    star2 = dtchartatmo.Rows[0]["STAR2"] + "";
                    star3 = dtchartatmo.Rows[0]["STAR3"] + "";
                    star4 = dtchartatmo.Rows[0]["STAR4"] + "";
                    star5 = dtchartatmo.Rows[0]["STAR5"] + "";

                    sb.Append("<script type='text/javascript' language='javascript'>");
                    sb.Append("google.charts.load('current', { 'packages': ['corechart'] });");
                    sb.Append("google.charts.setOnLoadCallback(drawChart);");
                    sb.Append("function drawChart() {");
                    sb.Append("var data = google.visualization.arrayToDataTable([");
                    sb.Append("['STAR', 'FEEDBACK'],['0 Star'," + star0 + "],['1 Star', " + star1 + "],['2 Star', " + star2 + "],['3 Star', " + star3 + "],['4 Star', " + star4 + "],['5 Star', " + star5 + "]]);");
                    sb.Append("var options = { 'title': 'ATMOSPHERE FEEDBACK CHART', 'width': 700, 'height': 700 };");
                    sb.Append("var chart = new google.visualization.PieChart(document.getElementById('divchartatmosphere'));");
                    sb.Append("chart.draw(data, options);");
                    sb.Append(" } </script>) ");

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "doSomething", sb.ToString());
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool Gen_ChartOverall()
    {
        StringBuilder sb = new StringBuilder();

        string connstr = "";
        string qry = "";
        string sdt = "";
        string edt = "";
        SqlCommand mscmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dtchartover = new DataTable();

        try
        {
            connstr = "Data Source=" + Sername + ";Integrated Security=false;Initial Catalog=" + Dbname + ";User ID=" + Dbusername + ";Password=" + Dbpass;
            mssqlcon.ConnectionString = connstr;
            mssqlcon.Open();

            sdt = Request.Form[this.dtpchartoverallsdt.UniqueID];
            edt = Request.Form[this.dtpchartoveralledt.UniqueID];

            this.dtpchartservicesdt.Text = sdt;
            this.dtpchartserviceedt.Text = edt;

            if ((sdt != null) && (edt != null))
            {
                qry = "SELECT SUM(CASE WHEN OPTOVER=0 THEN 1 ELSE 0 END) AS STAR0, " +
                        " SUM(CASE WHEN OPTOVER=1 THEN 1 ELSE 0 END) AS STAR1, " +
                        " SUM(CASE WHEN OPTOVER=2 THEN 1 ELSE 0 END) AS STAR2, " +
                        " SUM(CASE WHEN OPTOVER=3 THEN 1 ELSE 0 END) AS STAR3, " +
                        " SUM(CASE WHEN OPTOVER=4 THEN 1 ELSE 0 END) AS STAR4, " +
                        " SUM(CASE WHEN OPTOVER=5 THEN 1 ELSE 0 END) AS STAR5 " +
                        " FROM FEEDBACKDATA " +
                        " WHERE ISNULL(FEEDBACKDATA.DELFLG, 0) = 0 AND FEEDBACKDATA.FEEDDATE BETWEEN @p_fromdate and @p_todate ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtchartover);

                string star0 = "0";
                string star1 = "0";
                string star2 = "0";
                string star3 = "0";
                string star4 = "0";
                string star5 = "0";

                if (dtchartover.Rows.Count > 0)
                {
                    star0 = dtchartover.Rows[0]["STAR0"] + "";
                    star1 = dtchartover.Rows[0]["STAR1"] + "";
                    star2 = dtchartover.Rows[0]["STAR2"] + "";
                    star3 = dtchartover.Rows[0]["STAR3"] + "";
                    star4 = dtchartover.Rows[0]["STAR4"] + "";
                    star5 = dtchartover.Rows[0]["STAR5"] + "";

                    sb.Append("<script type='text/javascript' language='javascript'>");
                    sb.Append("google.charts.load('current', { 'packages': ['corechart'] });");
                    sb.Append("google.charts.setOnLoadCallback(drawChart);");
                    sb.Append("function drawChart() {");
                    sb.Append("var data = google.visualization.arrayToDataTable([");
                    sb.Append("['STAR', 'FEEDBACK'],['0 Star'," + star0 + "],['1 Star', " + star1 + "],['2 Star', " + star2 + "],['3 Star', " + star3 + "],['4 Star', " + star4 + "],['5 Star', " + star5 + "]]);");
                    sb.Append("var options = { 'title': 'OVERALL FEEDBACK CHART', 'width': 700, 'height': 700 };");
                    sb.Append("var chart = new google.visualization.PieChart(document.getElementById('divchartoverall'));");
                    sb.Append("chart.draw(data, options);");
                    sb.Append(" } </script>) ");

                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "doSomething", sb.ToString());
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    #endregion
}