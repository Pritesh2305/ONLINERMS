using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for feedbackdatabal
/// </summary>
public class feedbackdatabal
{
    private string _sernm;

    public string Sernm
    {
        get { return _sernm; }
        set { _sernm = value; }
    }
    private string _dbname;

    public string Dbname
    {
        get { return _dbname; }
        set { _dbname = value; }
    }
    private string _dbusername;

    public string Dbusername
    {
        get { return _dbusername; }
        set { _dbusername = value; }
    }
    private string _dbpass;

    public string Dbpass
    {
        get { return _dbpass; }
        set { _dbpass = value; }
    }

    private Int64 _formmode;
    private Int64 _rid;
    private DateTime _feeddate;

    public DateTime Feeddate
    {
        get { return _feeddate; }
        set { _feeddate = value; }
    }
    private string _guestname;

    public string Guestname
    {
        get { return _guestname; }
        set { _guestname = value; }
    }
    private string _mobno;

    public string Mobno
    {
        get { return _mobno; }
        set { _mobno = value; }
    }
    private string _email;

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }
    private int _optfood;

    public int Optfood
    {
        get { return _optfood; }
        set { _optfood = value; }
    }
    private int _optservice;

    public int Optservice
    {
        get { return _optservice; }
        set { _optservice = value; }
    }
    private int _optatmo;

    public int Optatmo
    {
        get { return _optatmo; }
        set { _optatmo = value; }
    }

    private int _optover;

    public int Optover
    {
        get { return _optover; }
        set { _optover = value; }
    }

    private string _remark;

    public string Remark
    {
        get { return _remark; }
        set { _remark = value; }
    }
    private string _osnm;

    public string Osnm
    {
        get { return _osnm; }
        set { _osnm = value; }
    }
    private string _bronm;

    public string Bronm
    {
        get { return _bronm; }
        set { _bronm = value; }
    }
    private string _devicenm;

    public string Devicenm
    {
        get { return _devicenm; }
        set { _devicenm = value; }
    }
    private long _loginuserid = 0;
    private string _errstr = "";
    private long _retval = 0;
    private long _id = 0;

	public feedbackdatabal()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Property

    public Int64 FormMode
    {
        get { return this._formmode; }
        set { this._formmode = value; }
    }

    public Int64 Rid
    {
        get { return _rid; }
        set { _rid = value; }
    }

    public long LoginUserId
    {
        get { return this._loginuserid; }
        set { this._loginuserid = value; }
    }

    public string Errstr
    {
        get { return this._errstr; }
        set { this._errstr = value; }
    }

    public long Retval
    {
        get { return this._retval; }
        set { this._retval = value; }
    }

    public long Id
    {
        get { return this._id; }
        set { this._id = value; }
    }

    #endregion

    public Int64 Db_Operation_FEEDBACKDATA(feedbackdatabal objbal1)
    {
        try
        {
            feedbackdatadal objdal1 = new feedbackdatadal();
            bool ret1 = objdal1.Db_Operation(objbal1);

            if (FormMode == 1)
            {
                objbal1.Id = objbal1.Rid;
            }

            return objbal1.Id;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    //public bool Delete_FEEDBACKDATA()
    //{
    //    try
    //    {
    //        feedbackdatadal objdal1 = new feedbackdatadal();
    //        bool ret1 = objdal1.DeleteRecord(_rid, _loginuserid);
    //        return ret1;
    //    }
    //    catch (Exception)
    //    {
    //        return false;
    //    }
    //}
}