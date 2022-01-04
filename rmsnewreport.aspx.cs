using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rmsnewreport : System.Web.UI.Page
{
    protected string userid = "";
    protected SqlConnection mssqlcon = new SqlConnection();

    protected DataTable DtBillReg = new DataTable();
    protected DataTable DtBilldtl = new DataTable();
    protected DataTable DtitemsaleReg = new DataTable();
    protected DataTable Dtbillmod = new DataTable();
    protected DataTable Dtkotmod = new DataTable();
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
        try
        {
            DateTime dtvalidity;

            if (Session["rid"] == null)
            {
                Response.Redirect("index.html");
            }
            Username = Session["username"] + "";
            Sername = Session["sername"] + "";
            Dbname = Session["dbname"] + "";
            Dbusername = Session["dbusername"] + "";
            Dbpass = Session["dbpass"] + "";
            Coinfo = Session["coinfo"] + "";

            this.lblcustinfo.Text = "  " + Coinfo + "".ToUpper();

            DateTime.TryParse(Session["uptodate"] + "", out dtvalidity);
            this.lblvalidity.Text = "VALID UPTO " + dtvalidity.ToString("dd/MM/yyyy");

            Page.MaintainScrollPositionOnPostBack = true;
        }
        catch (Exception)
        { }
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

    #region Buttonclick

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

    protected void btnbusinesssummary_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_BusinessSummary();
        }
        catch (Exception)
        { }
    }

    protected void btnbillreg_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_BillRegister();
        }
        catch (Exception)
        { }
    }

    protected void btnexportbillreg_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_BillRegister();
            this.ExportToExcel(DtBillReg);
        }
        catch (Exception)
        { }
    }

    protected void btnbilldtl_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_Billdtl();
        }
        catch (Exception)
        { }
    }

    protected void billdtl2excel(object sender, EventArgs e)
    {
        try
        {
            this.Gen_Billdtl();
            this.ExportToExcel(DtBilldtl);

        }
        catch (Exception)
        { }
    }

    protected void btnitemsalereg_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_ItemSaleRegister();
        }
        catch (Exception)
        { }
    }

    protected void itemsalereg2excel(object sender, EventArgs e)
    {
        try
        {
            this.Gen_ItemSaleRegister();
            this.ExportToExcel(DtitemsaleReg);

        }
        catch (Exception)
        { }
    }

    protected void btnbillmodreg_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_BillModRegister();
        }
        catch (Exception)
        { }
    }

    protected void btnkotmodreg_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_KotModRegister();
        }
        catch (Exception)
        { }
    }

    protected void btnexcelbillmod_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_BillModRegister();
            this.ExportToExcel(Dtbillmod);

        }
        catch (Exception)
        { }
    }

    protected void btnexcelkotmod_Click(object sender, EventArgs e)
    {
        try
        {
            this.Gen_KotModRegister();
            this.ExportToExcel(Dtkotmod);

        }
        catch (Exception)
        { }
    }

    #endregion

    #region ReportFunction

    private bool Gen_BusinessSummary()
    {
        string connstr = "";
        string qry = "";
        string qry1 = "";
        string sdt = "";
        string edt = "";
        SqlCommand mscmd = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dtbusinesssummary = new DataTable();
        DataTable dtbusinesssummary1 = new DataTable();
        try
        {
            connstr = "Data Source=" + Sername + ";Integrated Security=false;Initial Catalog=" + Dbname + ";User ID=" + Dbusername + ";Password=" + Dbpass;
            mssqlcon.ConnectionString = connstr;
            mssqlcon.Open();

            sdt = Request.Form[dtpbusinesssdt.UniqueID];
            edt = Request.Form[dtpbusinessedt.UniqueID];

            dtpbusinesssdt.Text = sdt;
            dtpbusinessedt.Text = edt;

            if ((sdt != null) && (edt != null))
            {
                string rptdtl = "";

                rptdtl += "<table id='tblbusinesssummary' class='table table-hover'>";
                rptdtl += "<thead>";
                rptdtl += "<tr bgcolor='#89DA59'> <th scope='row'>BUSINESS SUMMARY</th>";
                rptdtl += "<td>" + "" + "</td>";
                rptdtl += "</tr>";
                rptdtl += "</thead>";
                rptdtl += "<tbody>";

                ///// BILLING BREAKUP DATA ////////////////////////////////////////////////////////////////////////////////////////////////////////////

                qry = "SELECT SUM(BILLPAX) AS TOTPAX," +
                        " count(rid) AS Totbill " +
                        " FROM BILL " +
                        " where isnull(BILL.ISREVISEDBILL, 0) = 0  And isnull(BILL.delflg, 0) = 0 AND BILL.BILLDATE BETWEEN @p_fromdate and @p_todate ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtbusinesssummary);

                if (dtbusinesssummary.Rows.Count > 0)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#ec891d'>";
                    rptdtl = rptdtl + "<th scope='row'>" + "BILLING BREAKUP SUMMARY " + "</th>";
                    rptdtl = rptdtl + "<td>" + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "BILL " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["Totbill"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "PAX " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTPAX"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";
                }
                dtbusinesssummary = new DataTable();

                ///// BILLING TYPE SUMMARY  ////////////////////////////////////////////////////////////////////////////////////////////////////////////

                qry = "SELECT " +
                       " sum(case when billtype='Table' and isnull(ISPARCELBILL,0) = 0 then Netamount else 0 end)  As TableAmt ," +
                       " sum(case when billtype='Parcel' OR isnull(ISPARCELBILL,0) = 1 then Netamount else 0 end)  As ParcelAmt ," +
                       " sum(case when billtype='CASH' And isnull(ISPARCELBILL,0)=0 then Netamount else 0 end)  As CashAmt,  " +
                       " sum(case when billtype='QUICKBILL' And isnull(ISPARCELBILL,0)=0  then Netamount else 0 end)  As QuickAmt, " +
                       " sum(Netamount) as NetAmt from bill " +
                       " where isnull(BILL.ISREVISEDBILL, 0) = 0  And isnull(BILL.delflg, 0) = 0 AND BILL.BILLDATE BETWEEN @p_fromdate and @p_todate ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtbusinesssummary);

                if (dtbusinesssummary.Rows.Count > 0)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#ec891d'>";
                    rptdtl = rptdtl + "<th scope='row'>" + "BILLING TYPE SUMMARY " + "</th>";
                    rptdtl = rptdtl + "<td>" + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "DINE IN " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TableAmt"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "PARCEL " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["ParcelAmt"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "CASHMEMO " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["CASHAmt"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "NET AMOUNT " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["NetAmt"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";
                }
                dtbusinesssummary = new DataTable();

                ///// BILLING DETAILS SUMMARY  ////////////////////////////////////////////////////////////////////////////////////////////////////////////

                qry = "Select " +
                         " sum(Totamount)  As Totamount, " +
                         " sum(TOTADDVATAMOUNT) As TOTADDVATAMOUNT, " +
                         " sum(TOTDISCAMOUNT) As TOTDISCAMOUNT," +
                         " sum(TOTADDDISCAMT) As TOTADDDISCAMT," +
                         " sum(TOTSERCHRAMT) As TOTSERCHRAMT, " +
                         " sum(TOTVATAMOUNT) As TOTVATAMOUNT," +
                         " sum(TOTBEVVATAMT) As TOTBEVVATAMT," +
                         " sum(TOTLIQVATAMT) As TOTLIQVATAMT," +
                         " sum(TOTGSTAMT) As TOTGSTAMT," +
                         " sum(CGSTAMT) As TOTCGSTAMT," +
                         " sum(SGSTAMT) As TOTSGSTAMT," +
                         " sum(IGSTAMT) As TOTIGSTAMT," +
                         " sum(TOTROFF)  As TOTROFF, " +
                         " sum(NETAMOUNT)  As NETAMOUNT " +
                         " From Bill" +
                         " where isnull(BILL.ISREVISEDBILL, 0) = 0  And isnull(BILL.delflg, 0) = 0 AND BILL.BILLDATE BETWEEN @p_fromdate and @p_todate ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtbusinesssummary);

                if (dtbusinesssummary.Rows.Count > 0)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#ec891d'>";
                    rptdtl = rptdtl + "<th scope='row'>" + "BILLING DETAILS " + "</th>";
                    rptdtl = rptdtl + "<td>" + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "TOTAL AMOUNT " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["Totamount"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "DISCOUNT " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTDISCAMOUNT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "SERVICE CHARGE " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTSERCHRAMT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "FOOD VAT " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTVATAMOUNT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "BEV. VAT " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTBEVVATAMT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "LIQ. VAT " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTLIQVATAMT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "CGST " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTCGSTAMT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "SGST " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTSGSTAMT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "IGST " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTIGSTAMT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "TOTAL GST " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTGSTAMT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "ROUND OFF " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTROFF"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "ADD. DISC. " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["TOTADDDISCAMT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "NET AMOUNT" + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["NETAMOUNT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";
                }
                dtbusinesssummary = new DataTable();
                ///// COUPON DISCOUNT  ////////////////////////////////////////////////////////////////////////////////////////////////////////////

                qry = "Select " +
                       " ((SUM(ISNULL(BILLDTL.IAMT,0))) * -1) AS COUPONDISC " +
                       " From Bill" +
                       " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID = Bill.RID) " +
                       " WHERE isnull(BILL.ISREVISEDBILL, 0) = 0  And isnull(BILL.delflg, 0) = 0 AND BILL.BILLDATE BETWEEN @p_fromdate and @p_todate AND ISNULL(BILLDTL.IQTY,0)< 0 ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtbusinesssummary);

                if (dtbusinesssummary.Rows.Count > 0)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "COUPON DISCOUNT " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["COUPONDISC"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";
                }
                dtbusinesssummary = new DataTable();

                ///// SETTLEMENT TYPE DETAILS  ////////////////////////////////////////////////////////////////////////////////////////////////////////////

                qry = " SELECT " +
                    " sum(case when setletype='CASH' then setleamount else 0 end)  As cashAmt , " +
                    " sum(case when setletype='CHEQUE' then setleamount else 0 end)  As chequeAmt , " +
                    " sum(case when setletype='CREDIT CARD' then setleamount else 0 end)  As creditcardAmt, " +
                    " sum(case when setletype='OTHER' then setleamount else 0 end)  As otherAmt, " +
                    " sum(case when setletype='ROOM CREDIT' then setleamount else 0 end)  As roomcreditamt, " +
                    " sum(case when setletype='COMPLEMENTARY' then setleamount else 0 end)  As Complyment, " +
                    " sum(isnull(adjamt,0)) as adjustamt," +
                    " sum(setleamount) as NetAmt, " +
                    " (sum(isnull(adjamt,0)) + sum(setleamount)) as Grandtotal " +
                    " from settlement " +
                    " where isnull(delflg,0)=0 AND SETTLEMENT.SETLEDATE BETWEEN @p_fromdate and @p_todate ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtbusinesssummary);

                qry1 = " SELECT SUM(CUSTCREDIT.PENDINGAMT) as CUSTCREDIT " +
                          " FROM (" +
                          " SELECT BILL.NETAMOUNT,SETTLEINFO.SETLEAMT,(ISNULL(BILL.NETAMOUNT,0) - ISNULL(SETTLEINFO.SETLEAMT,0)) AS PENDINGAMT" +
                          " FROM BILL" +
                          " LEFT JOIN (" +
                                      " SELECT SUM(ISNULL(SETLEAMOUNT,0) + ISNULL(ADJAMT,0)) AS SETLEAMT,SETTLEMENT.BILLRID" +
                                          " FROM SETTLEMENT " +
                                          " WHERE ISNULL(SETTLEMENT.DELFLG, 0) = 0 AND isnull(SETTLEMENT.BILLRID,0)>0 " +
                                          " GROUP BY SETTLEMENT.BILLRID" +
                                    " ) AS SETTLEINFO ON (SETTLEINFO.BILLRID = BILL.RID)  " +
                          " WHERE ISNULL(BILL.DELFLG,0)=0 AND BILL.BILLDATE BETWEEN @p_fromdate and @p_todate " +
                          " AND BILL.NETAMOUNT > SETTLEINFO.SETLEAMT" +
                          " )  AS CUSTCREDIT";

                mscmd = new SqlCommand(qry1, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtbusinesssummary1);

                decimal custcramt1 = 0;
                Decimal grtot1 = 0;

                if (dtbusinesssummary.Rows.Count > 0)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#ec891d'>";
                    rptdtl = rptdtl + "<th scope='row'>" + "SETTLEMENT SUMMARY " + "</th>";
                    rptdtl = rptdtl + "<td>" + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "CASH " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["CashAmt"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "CHEQUE " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["chequeAmt"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "CREDIT CARD " + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["creditcardAmt"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "OTHER" + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["otherAmt"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "ROOM CREDIT" + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["roomcreditamt"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "COMPLEMENTRY" + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["Complyment"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "ADJUST AMOUNT" + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["adjustamt"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "NET AMOUNT" + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["NetAmt"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    if (dtbusinesssummary1.Rows.Count > 0)
                    {
                        rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                        rptdtl = rptdtl + "<td>" + "CUST.CREDIT" + "</td>";
                        rptdtl = rptdtl + "<td>" + dtbusinesssummary1.Rows[0]["CUSTCREDIT"] + "" + "</td>";
                        Decimal.TryParse(dtbusinesssummary1.Rows[0]["CUSTCREDIT"] + "", out custcramt1);
                        rptdtl = rptdtl + "</tr>";
                    }

                    Decimal.TryParse(dtbusinesssummary.Rows[0]["Grandtotal"] + "", out grtot1);
                    grtot1 = grtot1 + custcramt1;

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "GRAND TOTAL" + "</td>";
                    rptdtl = rptdtl + "<td>" + grtot1 + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";
                }
                dtbusinesssummary = new DataTable();
                dtbusinesssummary1 = new DataTable();

                ///// OTHER PAYMENT DETAILS  ////////////////////////////////////////////////////////////////////////////////////////////////////////////

                qry = " SELECT SETTLEMENT.SETLETYPE,SETTLEMENT.OTHERPAYMENTBY, SUM(SETTLEMENT.SETLEAMOUNT) AS SETLEAMT " +
                       " FROM SETTLEMENT " +
                       " LEFT JOIN BILL ON (BILL.RID=SETTLEMENT.BILLRID) " +
                       " WHERE ISNULL(SETTLEMENT.DELFLG,0)=0 AND SETTLEMENT.SETLETYPE='OTHER'  " +
                       " AND  SETTLEMENT.SETLEDATE BETWEEN @p_fromdate and @p_todate " +
                       " GROUP BY SETTLEMENT.SETLETYPE,SETTLEMENT.OTHERPAYMENTBY " +
                       " ORDER BY SETTLEMENT.OTHERPAYMENTBY ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtbusinesssummary);

                if (dtbusinesssummary.Rows.Count > 0)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#ec891d'>";
                    rptdtl = rptdtl + "<th scope='row'>" + "OTHER PAYMENT SUMMARY " + "</th>";
                    rptdtl = rptdtl + "<td>" + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    foreach (DataRow dr in dtbusinesssummary.Rows)
                    {
                        rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                        rptdtl = rptdtl + "<td>" + dr["OTHERPAYMENTBY"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["SETLEAMT"].ToString() + "</td>";
                        rptdtl = rptdtl + "</tr>";
                    }
                }
                dtbusinesssummary = new DataTable();

                ///// INCOME DETAILS  ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                qry = "Select " +
                      " SUM(INAMOUNT) AS INAMOUNT " +
                      " FROM INCOME " +
                      " WHERE isnull(INCOME.delflg, 0) = 0 AND INCOME.INDATE BETWEEN @p_fromdate and @p_todate ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtbusinesssummary);

                if (dtbusinesssummary.Rows.Count > 0)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#ec891d'>";
                    rptdtl = rptdtl + "<th scope='row'>" + "INCOME SUMMARY " + "</th>";
                    rptdtl = rptdtl + "<td>" + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "INCOME AMOUNT" + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["INAMOUNT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";
                }
                dtbusinesssummary = new DataTable();

                ///// EXPENCES DETAILS  ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                qry = "Select " +
                       " SUM(EXAMOUNT) AS EXAMOUNT " +
                       " FROM EXPENCE " +
                       " WHERE isnull(EXPENCE.delflg, 0) = 0 AND EXPENCE.EXDATE BETWEEN @p_fromdate and @p_todate ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtbusinesssummary);

                if (dtbusinesssummary.Rows.Count > 0)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#ec891d'>";
                    rptdtl = rptdtl + "<th scope='row'>" + "EXPENCE SUMMARY " + "</th>";
                    rptdtl = rptdtl + "<td>" + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "EXPENCE AMOUNT" + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["EXAMOUNT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";
                }
                dtbusinesssummary = new DataTable();

                ////////
                // CASH ON HAND ENTERY INFORMATION             

                qry = "  SELECT " +
                        " SUM(CASE WHEN CASHSTATUS = 'PLUS' THEN CASHAMT ELSE 0 END) AS 'PLUSAMT' ," +
                        " SUM(CASE WHEN CASHSTATUS = 'MINUS' THEN CASHAMT ELSE 0 END) AS 'MINUSAMT'," +
                        " SUM(CASHAMT) AS NETAMT from CASHONHAND " +
                        " WHERE isnull(delflg,0)=0 AND CASHONHAND.CASHDATE BETWEEN @p_fromdate and @p_todate ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtbusinesssummary);

                if (dtbusinesssummary.Rows.Count > 0)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#ec891d'>";
                    rptdtl = rptdtl + "<th scope='row'>" + "CASH ON HAND ENTRY DETAILS " + "</th>";
                    rptdtl = rptdtl + "<td>" + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "PLUS CASH" + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["PLUSAMT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + "MINUS CASH" + "</td>";
                    rptdtl = rptdtl + "<td>" + dtbusinesssummary.Rows[0]["MINUSAMT"] + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";
                }
                dtbusinesssummary = new DataTable();

                ///////////////////////////////////////////////////////////
                // TIEUP COMPANY INFORMATION

                qry = " SELECT MSTTIEUPCOMPANY.RID,MSTTIEUPCOMPANY.COMPNAME,SUM(BILL.NETAMOUNT) AS NETAMOUNT " +
                        " FROM BILL" +
                        " LEFT JOIN MSTTIEUPCOMPANY ON (MSTTIEUPCOMPANY.RID=BILL.MSTTIEUPCOMPRID) " +
                        " WHERE isnull(BILL.MSTTIEUPCOMPRID,0)>0 AND isnull(BILL.ISREVISEDBILL, 0) = 0 " +
                        " AND isnull(BILL.ISREVISEDBILL,0)=0 AND BILL.BILLDATE BETWEEN @p_fromdate and @p_todate " +
                        " GROUP BY MSTTIEUPCOMPANY.RID,MSTTIEUPCOMPANY.COMPNAME ";

                mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                sda.SelectCommand = mscmd;
                sda.Fill(dtbusinesssummary);

                if (dtbusinesssummary.Rows.Count > 0)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#ec891d'>";
                    rptdtl = rptdtl + "<th scope='row'>" + "TIEUP COMPANY INFORMATION" + "</th>";
                    rptdtl = rptdtl + "<td>" + "" + "</td>";
                    rptdtl = rptdtl + "</tr>";

                    foreach (DataRow row in dtbusinesssummary.Rows)
                    {
                        string compnm1 = "";
                        compnm1 = (row["COMPNAME"] + "");
                        rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                        rptdtl = rptdtl + "<td>" + compnm1 + "</td>";
                        rptdtl = rptdtl + "<td>" + row["NETAMOUNT"] + "" + "</td>";
                        rptdtl = rptdtl + "</tr>";
                    }
                }

                rptdtl = rptdtl + "</tbody>";
                rptdtl = rptdtl + "</table>";

                divbusinesssummary.InnerHtml = rptdtl;
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool Gen_BillRegister()
    {
        string connstr = "";
        string qry = "";
        string sdt = "";
        string edt = "";

        try
        {
            if (Username.ToLower() == "msfho")
            {
                connstr = "Data Source=" + Sername + ";Integrated Security=false;Initial Catalog=" + Dbname + ";User ID=" + Dbusername + ";Password=" + Dbpass;
                mssqlcon.ConnectionString = connstr;
                mssqlcon.Open();

                sdt = Request.Form[dtpbillregsdt.UniqueID];
                edt = Request.Form[dtpbillregedt.UniqueID];

                dtpbillregsdt.Text = sdt;
                dtpbillregedt.Text = edt;

                if ((sdt != null) && (edt != null))
                {
                    SqlCommand mscmd = new SqlCommand("SP_BRANCHWISESUMMARY", mssqlcon);
                    mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                    mscmd.Parameters.AddWithValue("@p_todate", edt);
                    mscmd.CommandType = CommandType.StoredProcedure;
                    mscmd.CommandTimeout = 0;
                    SqlDataAdapter sda = new SqlDataAdapter(mscmd);
                    sda.Fill(DtBillReg);
                    mssqlcon.Close();

                    string rptdtl = "";

                    rptdtl += "<table id='tblbillregister' class='table table-hover'>";
                    rptdtl += "<thead>";
                    rptdtl += "<tr bgcolor='#ec891d'> <th scope='col'>BRANCH NAME</th><th scope='col'>NET AMOUNT</th>";
                    rptdtl += "</tr>";
                    rptdtl += "</thead>";
                    rptdtl += "<tbody>";
                    foreach (DataRow dr in DtBillReg.Rows)
                    {
                        rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                        rptdtl = rptdtl + "<th scope='row'>" + dr["BRANCHNM"].ToString() + "</th>";
                        rptdtl = rptdtl + "<td>" + dr["NETAMOUNT"].ToString() + "</td>";                       
                    }

                    rptdtl = rptdtl + "</tbody>";
                    rptdtl = rptdtl + "</table>";

                    divbillregister.InnerHtml = rptdtl;
                }
            }            
            else
            {
                connstr = "Data Source=" + Sername + ";Integrated Security=false;Initial Catalog=" + Dbname + ";User ID=" + Dbusername + ";Password=" + Dbpass;
                mssqlcon.ConnectionString = connstr;
                mssqlcon.Open();

                sdt = Request.Form[dtpbillregsdt.UniqueID];
                edt = Request.Form[dtpbillregedt.UniqueID];

                dtpbillregsdt.Text = sdt;
                dtpbillregedt.Text = edt;

                if ((sdt != null) && (edt != null))
                {
                    qry = "SELECT CONVERT(VARCHAR,BILL.billdate,103) AS BILLDATE,BILL.BILLNO,MSTTABLE.TABLENAME,bill.billpax," +
                            " BILL.totamount,BILL.totdiscamount,BILL.CGSTAMT,BILL.SGSTAMT,BILL.TOTROFF,BILL.TOTADDDISCAMT, " +
                            " BILL.netamount " +
                            " FROM BILL " +
                            " LEFT JOIN MSTTABLE ON (MSTTABLE.rid = BILL.tablerid) " +
                            " WHERE bill.billDATE between @p_fromdate and @p_todate  " +
                            " AND bill.delflg=0 and isnull(BILL.ISREVISEDBILL,0)=0 " +
                            " ORDER BY bill.billDATE,bill.rid";

                    SqlCommand mscmd = new SqlCommand(qry, mssqlcon);
                    mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                    mscmd.Parameters.AddWithValue("@p_todate", edt);
                    mscmd.CommandType = CommandType.Text;
                    mscmd.CommandTimeout = 0;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = mscmd;
                    sda.Fill(DtBillReg.DataSet);
                    mssqlcon.Close();

                    string rptdtl = "";

                    rptdtl += "<table id='tblbillregister' class='table table-hover'>";
                    rptdtl += "<thead>";
                    rptdtl += "<tr bgcolor='#ec891d'> <th scope='col'>DATE</th><th scope='col'>BILL NO</th><th scope='col'>TABLE NO</th><th scope='col'>PAX</th><th scope='col'>BASIC AMT</th><th scope='col'>DISC AMT</th><th scope='col'>CGST AMT</th><th scope='col'>SGST AMT</th><th scope='col'>R.OFF</th><th scope='col'>ADD.DISC</th><th scope='col'>NET AMT</th>";
                    rptdtl += "</tr>";
                    rptdtl += "</thead>";
                    rptdtl += "<tbody>";
                    foreach (DataRow dr in DtBillReg.Rows)
                    {
                        rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                        rptdtl = rptdtl + "<th scope='row'>" + dr["billdate"].ToString() + "</th>";
                        rptdtl = rptdtl + "<td>" + dr["billno"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["tablename"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["billpax"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["totamount"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["totdiscamount"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["cgstamt"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["sgstamt"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["totroff"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["totadddiscamt"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["netamount"].ToString() + "</td>";
                    }

                    rptdtl = rptdtl + "</tbody>";
                    rptdtl = rptdtl + "</table>";

                    divbillregister.InnerHtml = rptdtl;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message.ToString());
            return false;
        }
    }

    private bool Gen_Billdtl()
    {
        string connstr = "";
        string qry = "";
        string sdt = "";
        string edt = "";

        DataTable dtreport = new DataTable();
        DataTable dtbillres = new DataTable();
        DataTable dtsett = new DataTable();
        DataTable dtitem = new DataTable();

        try
        {

            dtreport.Columns.Add("RID", typeof(String));
            dtreport.Columns.Add("BILLDATE", typeof(String));
            dtreport.Columns.Add("BILLTIME", typeof(String));
            dtreport.Columns.Add("BILLNO", typeof(String));
            dtreport.Columns.Add("CUSTNM", typeof(String));
            dtreport.Columns.Add("TIEUP", typeof(String));
            dtreport.Columns.Add("ORDNO", typeof(String));
            dtreport.Columns.Add("BASICAMT", typeof(String));
            dtreport.Columns.Add("DISCAMT", typeof(String));
            dtreport.Columns.Add("CGSTAMT", typeof(String));
            dtreport.Columns.Add("SGSTAMT", typeof(String));
            dtreport.Columns.Add("NETAMT", typeof(String));
            dtreport.Columns.Add("PAYMENT", typeof(String));
            dtreport.Columns.Add("PAYMENTAMT", typeof(String));
            dtreport.Columns.Add("ITEMGROUP", typeof(String));
            dtreport.Columns.Add("ITEMNAME", typeof(String));
            dtreport.Columns.Add("ITEMQTY", typeof(String));

            connstr = "Data Source=" + Sername + ";Integrated Security=false;Initial Catalog=" + Dbname + ";User ID=" + Dbusername + ";Password=" + Dbpass;
            mssqlcon.ConnectionString = connstr;
            mssqlcon.Open();

            sdt = Request.Form[dtpbilldtlsdt.UniqueID];
            edt = Request.Form[dtpbilldtledt.UniqueID];

            dtpbilldtlsdt.Text = sdt;
            dtpbilldtledt.Text = edt;

            if ((sdt != null) && (edt != null))
            {

                qry = "SELECT BILL.RID,BILL.BILLDATE,convert(char(5), BILL.BILLTIME, 108) AS BILLTIME,BILL.BILLNO,MSTCUST.CUSTNAME," +
                             " MSTTIEUPCOMPANY.COMPNAME,BILL.COUPONNO,BILL.TOTAMOUNT,BILL.TOTDISCAMOUNT,BILL.CGSTAMT,BILL.SGSTAMT,BILL.NETAMOUNT" +
                             " FROM BILL" +
                             " LEFT JOIN MSTCUST ON (MSTCUST.RID=BILL.CUSTRID)" +
                             " LEFT JOIN MSTTIEUPCOMPANY ON (MSTTIEUPCOMPANY.RID=BILL.MSTTIEUPCOMPRID)" +
                             " WHERE ISNULL(BILL.DELFLG,0)=0 AND isnull(BILL.ISREVISEDBILL,0)=0" +
                             " AND bill.billDATE between @p_fromdate and @p_todate  " +
                             " ORDER BY BILL.BILLDATE,BILL.RID";

                SqlCommand mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = mscmd;
                sda.Fill(dtbillres);

                if (dtbillres.Rows.Count > 0)
                {
                    foreach (DataRow row in dtbillres.Rows)
                    {
                        Int64 billrid1 = 0;
                        Int64.TryParse(row["RID"] + "", out billrid1);

                        string billdate1;
                        string billtime1;
                        string billno1 = "";
                        string custnm1 = "";
                        string tieupcompnm1 = "";
                        string orderno1 = "";
                        Decimal basicamt1 = 0;
                        Decimal discamt1 = 0;
                        Decimal cgstamt1 = 0;
                        Decimal sgstamt1 = 0;
                        Decimal netamt1 = 0;

                        billdate1 = row["BILLDATE"] + "";
                        billtime1 = row["BILLTIME"] + "";
                        billno1 = row["BILLNO"] + "".Trim();
                        custnm1 = row["CUSTNAME"] + "".Trim();
                        tieupcompnm1 = row["COMPNAME"] + "".Trim();
                        orderno1 = row["COUPONNO"] + "".Trim();
                        Decimal.TryParse(row["TOTAMOUNT"] + "", out basicamt1);
                        Decimal.TryParse(row["TOTDISCAMOUNT"] + "", out discamt1);
                        Decimal.TryParse(row["CGSTAMT"] + "", out cgstamt1);
                        Decimal.TryParse(row["SGSTAMT"] + "", out sgstamt1);
                        Decimal.TryParse(row["NETAMOUNT"] + "", out netamt1);

                        dtitem = new DataTable();
                        // ITEM DETAILS
                        qry = " SELECT BILLDTL.BILLRID,BILLDTL.RID,MSTITEMGROUP.IGNAME,MSTITEM.INAME,BILLDTL.IQTY " +
                                    " FROM BILLDTL" +
                                    " LEFT JOIN MSTITEM ON (MSTITEM.RID=BILLDTL.IRID)" +
                                    " LEFT JOIN MSTITEMGROUP ON (MSTITEMGROUP.RID=MSTITEM.IGRPRID)" +
                                    " WHERE BILLRID=" + billrid1 + " AND ISNULL(BILLDTL.DELFLG,0)=0";

                        SqlCommand mscmd1 = new SqlCommand(qry, mssqlcon);
                        mscmd1.Parameters.AddWithValue("@p_fromdate", sdt);
                        mscmd1.Parameters.AddWithValue("@p_todate", edt);
                        mscmd1.CommandType = CommandType.Text;
                        mscmd1.CommandTimeout = 0;
                        SqlDataAdapter sda1 = new SqlDataAdapter();
                        sda1.SelectCommand = mscmd1;
                        sda1.Fill(dtitem);

                        /// PAYMENT 
                        /// 
                        dtsett = new DataTable();
                        qry = "SELECT SETTLEMENT.RID,SETTLEMENT.SETLETYPE,SETTLEMENT.SETLEAMOUNT " +
                                    " FROM SETTLEMENT" +
                                    " WHERE ISNULL(SETTLEMENT.DELFLG,0)=0" +
                                    " AND SETTLEMENT.BILLRID=" + billrid1 +
                                    " AND ISNULL(SETTLEMENT.SETLEAMOUNT,0)>0";
                        SqlCommand mscmd2 = new SqlCommand(qry, mssqlcon);
                        mscmd2.CommandType = CommandType.Text;
                        mscmd2.CommandTimeout = 0;
                        SqlDataAdapter sda2 = new SqlDataAdapter();
                        sda2.SelectCommand = mscmd2;
                        sda2.Fill(dtsett);

                        int cntsett = 0;
                        int cntitem = 0;
                        int loopcnt1 = 0;
                        int cnt1 = 0;

                        if (dtsett.Rows.Count <= 0)
                        {
                            dtsett.Rows.Add("0", "CUSTOMER CREDIT", netamt1);
                        }

                        cntsett = dtsett.Rows.Count;
                        cntitem = dtitem.Rows.Count;

                        loopcnt1 = cntitem;

                        if (cntsett > cntitem)
                        {
                            loopcnt1 = cntsett;
                        }

                        while (loopcnt1 > cnt1)
                        {
                            string igrp1 = "";
                            string inm1 = "";
                            Decimal iqty1 = 0;
                            string payment1 = "";
                            Decimal payamt1 = 0;

                            if (dtitem.Rows.Count >= (cnt1 + 1))
                            {
                                igrp1 = dtitem.Rows[cnt1]["IGNAME"] + "".Trim();
                                inm1 = dtitem.Rows[cnt1]["INAME"] + "".Trim();
                                Decimal.TryParse(dtitem.Rows[cnt1]["IQTY"] + "", out iqty1);
                            }
                            if (dtsett.Rows.Count >= (cnt1 + 1))
                            {
                                payment1 = dtsett.Rows[cnt1]["SETLETYPE"] + "".Trim();
                                Decimal.TryParse(dtsett.Rows[cnt1]["SETLEAMOUNT"] + "", out payamt1);
                            }

                            /// add row
                            /// 
                            DataRow dtreportrow = dtreport.NewRow();
                            dtreportrow["RID"] = "";
                            dtreportrow["BILLDATE"] = billdate1;
                            dtreportrow["BILLTIME"] = billtime1;
                            dtreportrow["BILLNO"] = billno1;
                            dtreportrow["CUSTNM"] = custnm1;
                            dtreportrow["TIEUP"] = tieupcompnm1;
                            dtreportrow["ORDNO"] = orderno1;

                            if (basicamt1 > 0)
                            {
                                dtreportrow["BASICAMT"] = basicamt1;
                            }
                            if (discamt1 > 0)
                            {
                                dtreportrow["DISCAMT"] = discamt1;
                            }
                            if (cgstamt1 > 0)
                            {
                                dtreportrow["CGSTAMT"] = cgstamt1;
                            }
                            if (sgstamt1 > 0)
                            {
                                dtreportrow["SGSTAMT"] = sgstamt1;
                            }
                            if (netamt1 > 0)
                            {
                                dtreportrow["NETAMT"] = netamt1;
                            }

                            dtreportrow["PAYMENT"] = payment1;
                            if (payamt1 > 0)
                            {
                                dtreportrow["PAYMENTAMT"] = payamt1;
                            }
                            dtreportrow["ITEMGROUP"] = igrp1;
                            dtreportrow["ITEMNAME"] = inm1;
                            if (iqty1 > 0)
                            {
                                dtreportrow["ITEMQTY"] = iqty1;
                            }

                            dtreport.Rows.Add(dtreportrow);

                            cnt1 = cnt1 + 1;

                            basicamt1 = 0;
                            discamt1 = 0;
                            cgstamt1 = 0;
                            sgstamt1 = 0;
                            netamt1 = 0;
                            payment1 = "";
                            payamt1 = 0;
                            igrp1 = "";
                            inm1 = "";
                            iqty1 = 0;
                        }
                    }
                }

                mssqlcon.Close();

                string rptdtl = "";

                rptdtl += "<table id='tblbilldtl' class='table table-hover'>";
                rptdtl += "<thead>";
                rptdtl += "<tr bgcolor='#ec891d'> <th scope='col'>DATE</th><th scope='col'>TIME</th><th scope='col'>BILL NO</th><th scope='col'>CUSTOMER</th><th scope='col'>TIEUP</th><th scope='col'>ORDER NO</th><th scope='col'>BASIC AMT</th><th scope='col'>DISC AMT</th><th scope='col'>CGST</th><th scope='col'>SGST</th><th scope='col'>NET AMT</th><th scope='col'>PAYMENT</th><th scope='col'>PAYMENT AMT</th><th scope='col'>GROUP</th><th scope='col'>ITEM NAME</th><th scope='col'>QTY</th>";
                rptdtl += "</tr>";
                rptdtl += "</thead>";
                rptdtl += "<tbody>";

                DtBilldtl = dtreport;

                foreach (DataRow dr in dtreport.Rows)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<td>" + dr["BILLDATE"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["BILLTIME"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td style='color:#D72C16; font-weight:bold;'>" + dr["BILLNO"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["CUSTNM"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td style='color:#002C54; font-weight:bold;'>" + dr["TIEUP"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["ORDNO"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["BASICAMT"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["DISCAMT"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["CGSTAMT"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["SGSTAMT"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td style='color:blue; font-weight:bold;'>" + dr["NETAMT"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td style='color:#CF3721;font-weight:bold;'>" + dr["PAYMENT"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td style='color:#CF3721;font-weight:bold;'>" + dr["PAYMENTAMT"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["ITEMGROUP"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["ITEMNAME"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["ITEMQTY"].ToString() + "</td>";
                }

                rptdtl = rptdtl + "</tbody>";
                rptdtl = rptdtl + "</table>";

                divbilldtl.InnerHtml = rptdtl;
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool Gen_ItemSaleRegister()
    {
        string connstr = "";
        string qry = "";
        string sdt = "";
        string edt = "";

        try
        {

            if (Username.ToLower() == "msfho")
            {
                connstr = "Data Source=" + Sername + ";Integrated Security=false;Initial Catalog=" + Dbname + ";User ID=" + Dbusername + ";Password=" + Dbpass;
                mssqlcon.ConnectionString = connstr;
                mssqlcon.Open();

                sdt = Request.Form[dtpitemsalesdt.UniqueID];
                edt = Request.Form[dtpitemsaleedt.UniqueID];

                dtpitemsalesdt.Text = sdt;
                dtpitemsaleedt.Text = edt;

                if ((sdt != null) && (edt != null))
                {
                    SqlCommand mscmd = new SqlCommand("SP_BRANCHWISEITEMGROUPSALE", mssqlcon);
                    mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                    mscmd.Parameters.AddWithValue("@p_todate", edt);
                    mscmd.CommandType = CommandType.StoredProcedure;
                    mscmd.CommandTimeout = 0;
                    SqlDataAdapter sda = new SqlDataAdapter(mscmd);
                    sda.Fill(DtitemsaleReg);
                    mssqlcon.Close();

                    string rptdtl = "";

                    rptdtl += "<table id='tblitemsalereg' class='table table-hover'>";
                    rptdtl += "<thead>";
                    rptdtl += "<tr bgcolor='#ec891d'> <th scope='col'>BRANCH NAME</th><th scope='col'>TOTAL QTY</th>";
                    rptdtl += "</tr>";
                    rptdtl += "</thead>";
                    rptdtl += "<tbody>";
                    foreach (DataRow dr in DtitemsaleReg.Rows)
                    {
                        rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                        rptdtl = rptdtl + "<th scope='row'>" + dr["IGNAME"].ToString() + "</th>";
                        rptdtl = rptdtl + "<td>" + dr["TOTQTY"].ToString() + "</td>";
                    }

                    rptdtl = rptdtl + "</tbody>";
                    rptdtl = rptdtl + "</table>";

                    divitemsalereg.InnerHtml = rptdtl;
                }
            }
            else
            { 

            connstr = "Data Source=" + Sername + ";Integrated Security=false;Initial Catalog=" + Dbname + ";User ID=" + Dbusername + ";Password=" + Dbpass;
            mssqlcon.ConnectionString = connstr;
            mssqlcon.Open();

            sdt = Request.Form[dtpitemsalesdt.UniqueID];
            edt = Request.Form[dtpitemsaleedt.UniqueID];

            dtpitemsalesdt.Text = sdt;
            dtpitemsaleedt.Text = edt;

                if ((sdt != null) && (edt != null))
                {
                    qry = "SELECT MSTITEM.RID AS IRID,MSTITEMGROUP.IGCODE,MSTITEMGROUP.IGNAME,MSTITEM.ICODE,MSTITEM.INAME, " +
                            " (ISNULL(BILLINFO.TOTQTY,0) + ISNULL(COMPINFO.COMPQTY,0)) AS TOTALQTY, " +
                            " ISNULL(BILLINFO.TOTQTY,0) AS TOTQTY,ISNULL(BILLINFO.TOTIAMT,0) AS TOTIAMT,ISNULL(BILLINFO.IQTY,0) AS IQTY, " +
                            " ABS(ISNULL(BILLINFO.DISQTY,0)) AS DISQTY, " +
                            " ISNULL(COMPINFO.COMPQTY,0) AS COMPQTY,ISNULL(COMPINFO.COMPAMT,0) AS COMPAMT, " +
                            " ISNULL(BILLINFO.IAMT,0) AS IAMT " +
                            " FROM MSTITEM " +
                            " LEFT JOIN MSTITEMGROUP ON (MSTITEMGROUP.RID = MSTITEM.IGRPRID) " +
                            " LEFT JOIN (  " +
                                " SELECT MSTITEM.RID as IRID,  " +
                                " SUM(KOTDTL.IQTY) AS COMPQTY, " +
                                " SUM(ISNULL(KOTDTL.IAMT,0)) AS COMPAMT " +
                                " FROM KOT  " +
                                " LEFT JOIN KOTDTL ON (KOTDTL.KOTRID = KOT.RID) " +
                                " LEFT JOIN MSTITEM on (MSTITEM.rid = KOTDTL.IRID)" +
                                " WHERE ISNULL(KOT.DELFLG,0) = 0 AND ISNULL(KOTDTL.DELFLG,0) = 0  " +
                                  " AND ISNULL(KOT.ISCOMPKOT,0)=1 AND ISNULL(KOTDTL.ICOMPITEM,0)=1 " +
                                  " AND KOT.KOTDATE BETWEEN @p_fromdate and @p_todate   " +
                                  " GROUP BY MSTITEM.RID " +
                                  " ) AS COMPINFO ON (COMPINFO.IRID = MSTITEM.RID)  " +
                            " LEFT JOIN (	  " +
                                  " SELECT " +
                                      " BILLDTL.IRID,  " +
                                        " SUM(ABS(BILLDTL.IQTY)) as TOTQTY, " +
                                        " SUM(ABS(BILLDTL.IAMT)) as TOTIAMT,  " +
                                        " SUM(CASE WHEN BILLDTL.IQTY > 0 THEN BILLDTL.IQTY ELSE 0 END) AS IQTY,   " +
                                        " SUM(CASE WHEN BILLDTL.IQTY < 0 THEN BILLDTL.IQTY ELSE 0 END) AS DISQTY,  " +
                                        " SUM(CASE WHEN BILLDTL.IAMT > 0 THEN BILLDTL.IAMT ELSE 0 END) AS IAMT   " +
                                   " FROM BILL " +
                                      " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID = BILL.RID)  	 " +
                                      " WHERE (ISNULL(BILL.DELFLG,0)=0 and ISNULL(BILLDTL.DELFLG,0)=0)  " +
                                              " AND ISNULL(BILL.ISREVISEDBILL,0)=0   " +
                                              " AND BILL.BILLDATE BETWEEN @p_fromdate and @p_todate " +
                                      " GROUP BY BILLDTL.IRID   " +
                                      " ) AS BILLINFO ON (BILLINFO.IRID = MSTITEM.RID) " +
                            " WHERE (ISNULL(BILLINFO.TOTQTY,0) + ISNULL(COMPINFO.COMPQTY,0))> 0" +
                            " ORDER BY ISNULL(BILLINFO.IQTY,0) DESC ";

                    SqlCommand mscmd = new SqlCommand(qry, mssqlcon);
                    mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                    mscmd.Parameters.AddWithValue("@p_todate", edt);
                    mscmd.CommandType = CommandType.Text;
                    mscmd.CommandTimeout = 0;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = mscmd;
                    sda.Fill(DtitemsaleReg);
                    mssqlcon.Close();

                    string rptdtl = "";

                    rptdtl += "<table id='tblitemsalereg' class='table table-hover'>";
                    rptdtl += "<thead>";
                    rptdtl += "<tr bgcolor='#ec891d'> <th scope='col'>ITEM GORUP</th><th scope='col'>ITEM NAME</th><th scope='col'>QTY</th><th scope='col'>COMP.QTY</th><th scope='col'>DISC.QTY</th><th scope='col'>AMOUNT</th>";
                    rptdtl += "</tr>";
                    rptdtl += "</thead>";
                    rptdtl += "<tbody>";
                    foreach (DataRow dr in DtitemsaleReg.Rows)
                    {
                        rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                        rptdtl = rptdtl + "<th scope='row'>" + dr["IGNAME"].ToString() + "</th>";
                        rptdtl = rptdtl + "<td>" + dr["INAME"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["IQTY"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["COMPQTY"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["DISQTY"].ToString() + "</td>";
                        rptdtl = rptdtl + "<td>" + dr["IAMT"].ToString() + "</td>";
                    }
                    rptdtl = rptdtl + "</tbody>";
                    rptdtl = rptdtl + "</table>";


                    divitemsalereg.InnerHtml = rptdtl;
                }
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool Gen_BillModRegister()
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

            sdt = Request.Form[dtpbillmodsdt.UniqueID];
            edt = Request.Form[dtpbillmodedt.UniqueID];

            dtpbillmodsdt.Text = sdt;
            dtpbillmodedt.Text = edt;

            if ((sdt != null) && (edt != null))
            {
                qry = "SELECT BILL.RID,BILL.BILLNO,BILL.REFBILLNO,BILL.BILLDATE,BILLTIME,BILL.CUSTNAME, " +
                                " MSTTABLE.TABLENAME,BILL.NETAMOUNT,BILL.BILLREMARK," +
                                " (CASE WHEN ISNULL(BILL.DELFLG,0)= 1 THEN 'DELETED' ELSE 'MODIFIED' END) AS STATUS, " +
                                " BILL.BILLINFO," +
                                " BILL.BILLPREPBY,ADDUSER.USERNAME" +
                                " FROM BILL" +
                                " LEFT JOIN MSTTABLE ON (MSTTABLE.RID=BILL.TABLERID)" +
                                " LEFT JOIN MSTUSERS ADDUSER ON (ADDUSER.RID=BILL.AUSERID)" +
                                " WHERE " +
                                " (RTRIM(LTRIM(BILL.BILLINFO))!='' OR ISNULL(BILL.DELFLG,0)=1) " +
                                " AND BILL.BILLDATE between @p_fromdate and @p_todate";

                SqlCommand mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = mscmd;
                sda.Fill(Dtbillmod);
                mssqlcon.Close();

                string rptdtl = "";

                rptdtl += "<table id='tblbillmodreg' class='table table-hover'>";
                rptdtl += "<thead>";
                rptdtl += "<tr bgcolor='#ec891d'> <th scope='col'>BILL NO</th><th scope='col'>REF.BILLNO</th><th scope='col'>BILL TIME</th><th scope='col'>STATUS</th><th scope='col'>INFORMATION</th>";
                rptdtl += "</tr>";
                rptdtl += "</thead>";
                rptdtl += "<tbody>";
                foreach (DataRow dr in Dtbillmod.Rows)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<th scope='row'>" + dr["BILLNO"].ToString() + "</th>";
                    rptdtl = rptdtl + "<td>" + dr["REFBILLNO"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["BILLTIME"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["STATUS"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td> <div style=word-break:break-all;>" + dr["BILLINFO"].ToString() + "</div> </td>";
                }
                rptdtl = rptdtl + "</tbody>";
                rptdtl = rptdtl + "</table>";

                divbillmod.InnerHtml = rptdtl;

            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private bool Gen_KotModRegister()
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

            sdt = Request.Form[dtpkotmodsdt.UniqueID];
            edt = Request.Form[dtpkotmodedt.UniqueID];

            dtpkotmodsdt.Text = sdt;
            dtpkotmodedt.Text = edt;

            if ((sdt != null) && (edt != null))
            {
                qry = "SELECT KOT.RID,KOT.KOTNO,KOT.REFKOTNO,KOT.KOTDATE,KOT.KOTTIME,KOT.CUSTNAME, " +
                        " MSTTABLE.TABLENAME,KOT.KOTREMARK," +
                        " (CASE WHEN ISNULL(KOT.DELFLG,0)= 1 THEN 'DELETED' ELSE 'MODIFIED' END) AS STATUS," +
                        " KOT.KOTINFO,ADDUSER.USERNAME" +
                        " FROM KOT" +
                        " LEFT JOIN MSTTABLE ON(MSTTABLE.RID = KOT.KOTTABLEID)" +
                        " LEFT JOIN MSTUSERS ADDUSER ON(ADDUSER.RID = KOT.AUSERID)" +
                        " WHERE" +
                        " (RTRIM(LTRIM(KOT.KOTINFO)) != '' OR ISNULL(KOT.DELFLG, 0) = 1)" +
                        " AND KOT.KOTDATE between @p_fromdate and @p_todate";

                SqlCommand mscmd = new SqlCommand(qry, mssqlcon);
                mscmd.Parameters.AddWithValue("@p_fromdate", sdt);
                mscmd.Parameters.AddWithValue("@p_todate", edt);
                mscmd.CommandType = CommandType.Text;
                mscmd.CommandTimeout = 0;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = mscmd;
                sda.Fill(Dtkotmod);
                mssqlcon.Close();

                string rptdtl = "";

                rptdtl += "<table id='tblkotmodreg' class='table table-hover'>";
                rptdtl += "<thead>";
                rptdtl += "<tr bgcolor='#ec891d'> <th scope='col'>KOT NO</th><th scope='col'>REF.KOTNO</th><th scope='col'>KOT TIME</th><th scope='col'>STATUS</th><th scope='col'>INFORMATION</th>";
                rptdtl += "</tr>";
                rptdtl += "</thead>";
                rptdtl += "<tbody>";
                foreach (DataRow dr in Dtkotmod.Rows)
                {
                    rptdtl = rptdtl + "<tr bgcolor='#fffacd'>";
                    rptdtl = rptdtl + "<th scope='row'>" + dr["KOTNO"].ToString() + "</th>";
                    rptdtl = rptdtl + "<td>" + dr["REFKOTNO"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["KOTTIME"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td>" + dr["STATUS"].ToString() + "</td>";
                    rptdtl = rptdtl + "<td> <div style=word-break:break-all;>" + dr["KOTINFO"].ToString() + "</div> </td>";
                }
                rptdtl = rptdtl + "</tbody>";
                rptdtl = rptdtl + "</table>";

                divkotmod.InnerHtml = rptdtl;

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