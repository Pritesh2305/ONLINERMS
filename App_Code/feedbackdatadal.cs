using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for feedbackdatadal
/// </summary>
public class feedbackdatadal
{
    protected SqlConnection mssqlcon = new SqlConnection();

    public feedbackdatadal()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool Db_Operation(feedbackdatabal objbal1)
    {
        bool funRetval;
        string connstr = "";

        try
        {
            funRetval = true;

            connstr = "Data Source=" + objbal1.Sernm + ";Integrated Security=false;Initial Catalog=" + objbal1.Dbname + ";User ID=" + objbal1.Dbusername + ";Password=" + objbal1.Dbpass;
            mssqlcon.ConnectionString = connstr;
            mssqlcon.Open();

            //CHECK DUPLICATED FEEDBACK.
            SqlCommand cmd = new SqlCommand("SELECT MOBNO FROM FEEDBACKDATA where MOBNO=@mobno AND ISNULL(DELFLG,0)=0", mssqlcon);
            cmd.Parameters.AddWithValue("@mobno", objbal1.Mobno);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                objbal1.Errstr = "FEEDBACK ALREADY REGISTED.";
                objbal1.Retval = 0;
                objbal1.Id = 0;
                funRetval = false;
            }
            else
            {
                SqlCommand mscmd = new SqlCommand("sp_feedbackdata", mssqlcon);

                mscmd.Parameters.Add("@p_mode", SqlDbType.BigInt);
                mscmd.Parameters.Add("@p_rid", SqlDbType.BigInt);
                mscmd.Parameters.Add("@p_feeddate", SqlDbType.DateTime);
                mscmd.Parameters.Add("@p_guestname", SqlDbType.NVarChar);
                mscmd.Parameters.Add("@p_mobno", SqlDbType.NVarChar);
                mscmd.Parameters.Add("@p_email", SqlDbType.NVarChar);
                mscmd.Parameters.Add("@p_optfood", SqlDbType.Int);
                mscmd.Parameters.Add("@p_optservice", SqlDbType.Int);
                mscmd.Parameters.Add("@p_optatmo", SqlDbType.Int);
                mscmd.Parameters.Add("@p_optover", SqlDbType.Int);
                mscmd.Parameters.Add("@p_remark", SqlDbType.NVarChar);
                mscmd.Parameters.Add("@p_osnm", SqlDbType.NVarChar);
                mscmd.Parameters.Add("@p_bronm", SqlDbType.NVarChar);
                mscmd.Parameters.Add("@p_devicenm", SqlDbType.NVarChar);
                mscmd.Parameters.Add("@p_userid", SqlDbType.BigInt);

                SqlParameter param_errstr = new SqlParameter("@p_errstr", SqlDbType.NVarChar, 500);
                param_errstr.Direction = ParameterDirection.Output;
                mscmd.Parameters.Add(param_errstr);

                SqlParameter param_retval = new SqlParameter("@p_retval", SqlDbType.BigInt);
                param_retval.Direction = ParameterDirection.Output;
                mscmd.Parameters.Add(param_retval);

                SqlParameter param_id = new SqlParameter("@p_id", SqlDbType.BigInt);
                param_id.Direction = ParameterDirection.Output;
                mscmd.Parameters.Add(param_id);

                mscmd.CommandType = CommandType.StoredProcedure;

                mscmd.Parameters["@p_mode"].Value = objbal1.FormMode;
                mscmd.Parameters["@p_rid"].Value = objbal1.Rid;
                mscmd.Parameters["@p_feeddate"].Value = objbal1.Feeddate;
                mscmd.Parameters["@p_guestname"].Value = objbal1.Guestname;
                mscmd.Parameters["@p_mobno"].Value = objbal1.Mobno;
                mscmd.Parameters["@p_email"].Value = objbal1.Email;
                mscmd.Parameters["@p_optfood"].Value = objbal1.Optfood;
                mscmd.Parameters["@p_optservice"].Value = objbal1.Optservice;
                mscmd.Parameters["@p_optatmo"].Value = objbal1.Optatmo;
                mscmd.Parameters["@p_optover"].Value = objbal1.Optover;
                mscmd.Parameters["@p_remark"].Value = objbal1.Remark;
                mscmd.Parameters["@p_osnm"].Value = objbal1.Osnm;
                mscmd.Parameters["@p_bronm"].Value = objbal1.Bronm;
                mscmd.Parameters["@p_devicenm"].Value = objbal1.Devicenm;

                mscmd.Parameters["@p_userid"].Value = objbal1.LoginUserId;

                if (mssqlcon.State == ConnectionState.Closed)
                {
                    mssqlcon.Open();
                }

                int ret = mscmd.ExecuteNonQuery();

                objbal1.Errstr = mscmd.Parameters["@p_Errstr"].Value.ToString();
                objbal1.Retval = Convert.ToInt32(mscmd.Parameters["@p_RetVal"].Value);
                objbal1.Id = Convert.ToInt32(mscmd.Parameters["@p_id"].Value);

                funRetval = true;

                if (objbal1.Retval > 0)
                {
                    funRetval = false;
                }
            }

            mssqlcon.Close();

            return funRetval;
        }
        catch (Exception)
        {
            //MessageBox.Show(ex.Message.ToString() + " Error occures in Db_Operation())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
    }

    //public bool DeleteRecord(Int64 Rid, long UserId)
    //{
    //    bool funRetval;

    //    DataTable dt1 = new DataTable();
    //    try
    //    {
    //        funRetval = false;

    //        clssql.OpenMsSqlConnection();

    //        funRetval = clssql.ExecuteMsSqlCommand("Update TABLERESERVATION set DelFlg = 1,DUSERID = " + UserId + " , DDATETIME=getdate() WHERE Rid = " + Rid);

    //        //if (funRetval)
    //        //{
    //        //    funRetval = clssql.ExecuteMsSqlCommand("Update BQBOOKINGDTL set DelFlg = 1,DUSERID = " + UserId + " , DDATETIME=getdate() WHERE bqborid = " + Rid);
    //        //}

    //        return funRetval;
    //    }

    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.Message.ToString() + " Error occures in DeleteRecord())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    //        return false;
    //    }
    //}
}