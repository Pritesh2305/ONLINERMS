<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="changepassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet"
        id="bootstrapcss">
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="scripts/JScript1.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="cover-caption">
        <div id="container" class="container">
            <%--<div class="row text-white">--%>
            <div class="row text-green">
                <div class="col-sm-6 offset-sm-3 text-center">
                    <asp:Label ID="lblrestname" Font-Size="XX-Large" Font-Bold="true" Style="color: Blue"
                        runat="server"></asp:Label>
                    <br />
                    <h3 class="display-12" style="color: green">
                        Change Paasword Form</h3>
                    <div class="info-form">
                        <form action="" class="form-inlin justify-content-center" style="color: #200080">
                        <div class="form-group">
                            <label class="sr-only">
                                Current Password</label>
                            <asp:TextBox ID="txtcupass" placeholder="Current Password" Text="" class="form-control"
                                MaxLength="100" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcupass"
                                ErrorMessage="!!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="group1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="sr-only">
                                New Password</label>
                            <asp:TextBox ID="txtnepass" placeholder="New Password" Text="" class="form-control"
                                MaxLength="50" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtnepass"
                                ErrorMessage="!!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="group1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label class="sr-only">
                                Confirm Paasord</label>
                            <asp:TextBox ID="txtcopass" placeholder=" Confirm Password" Text="" class="form-control"
                                MaxLength="200" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtcopass"
                                ErrorMessage="!!" ForeColor="Red" SetFocusOnError="True" ValidationGroup="group1"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtnepass"
                                ControlToValidate="txtcopass" ErrorMessage="password not same !!" ForeColor="Red" ValidationGroup="group1"></asp:CompareValidator>
                        </div>
                        <asp:Label ID="lblinfo" runat="server"></asp:Label>
                        <br />
                        <asp:Button ID="btnsubmit" runat="server" Text="Change Password" ValidationGroup="group1" class="btn btn-primary btn-lg"
                            OnClick="btnsubmit_click" />
                        <asp:Button ID="btnhome" runat="server" Text="Home" class="btn btn-success btn-lg"
                            OnClick="btnhome_click" />
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
