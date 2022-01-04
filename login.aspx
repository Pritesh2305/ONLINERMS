<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ONLINE RMS LOGIN</title>
    <link rel="shortcut icon" href="img/RMS.ico" />
    <a href="img/RMS.ico"></a>" />
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet"
        type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Lato" rel="stylesheet" type="text/css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="css/loginstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <nav class="navbar navbar-default navbar-fixed-top">
  <div class="container">
    <div class="navbar-header">
      <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>                        
      </button>
      <a class="navbar-brand" href="index.html">Krupa Infotech</a>
    </div>
    <div class="collapse navbar-collapse" id="myNavbar">
      <ul class="nav navbar-nav">
        <%--<li><a href="#login">LOGIN</a></li>
        <li><a href="#product">PRODUCT</a></li>        
        <li><a href="#contact">CONTACT</a></li>--%>
      </ul>
      <!--<ul class="nav navbar-nav navbar-right">        
        <li><a href="login.aspx">Login </a></li>
      </ul>-->
    </div>
  </div>
</nav>
        <div class="container-fluid bg-grey">
            <h2 class="text-center">
                ONLINE RMS LOGIN
            </h2>
            <div class="row">
                <div class="col-sm-4">
                    <h1>
                        USER LOGIN DETAIL</h1>
                </div>
                <div class="col-sm-8">
                    <h3>
                        Username</h3>
                    <asp:TextBox ID="txtusername" placeholder="Username" Text="" class="form-control input-lg"
                        runat="server"></asp:TextBox>
                    <br />
                    <h3>
                        Password</h3>
                    <asp:TextBox ID="txtpassword" placeholder="Password" TextMode="Password" class="form-control input-lg"
                        runat="server"></asp:TextBox>
                    <br />
                    <%--<asp:Button ID="Button1" runat="server" Text="Login" OnClick="btnlogin_click" />--%>
                    <asp:Button ID="Button1" runat="server" Text="Login" class="btn btn-primary btn-lg"
                        OnClick="btnlogin_click" />
                    <br />
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
