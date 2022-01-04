<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feedbackreport.aspx.cs" Inherits="feedbackreport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ONLINE RMS FEEDBACK REPORTS</title>
    <link rel="shortcut icon" href="img/RMS.ico" />
    <a href="img/RMS.ico"></a>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet"
        type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Lato" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js" type="text/javascript"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"
        type="text/javascript"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <link href="css/feedbackreportstyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function CallButtonEvent() {
            document.getElementById("<%=btnSubmit.ClientID %>").click();
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpfeedbackdetailsdt").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpfeedbackdetailedt").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpchartfoodqualitysdt").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpchartfoodqualityedt").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpchartservicesdt").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpchartserviceedt").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpchartatmospheresdt").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpchartatmosphereedt").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpchartoverallsdt").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpchartoveralledt").datepicker();
        });
    </script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.22/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>
    <script type="text/javascript">
        function feedbackdetail2pdf() {
            html2canvas(document.getElementById('tblfeedbackdetail'), {
                onrendered: function (canvas) {
                    var data = canvas.toDataURL();
                    var docDefinition = {
                        content: [{
                            image: data,
                            width: 500
                        }]
                    };
                    pdfMake.createPdf(docDefinition).download("Feedbackdetail.pdf");
                }
            });
        }
    </script>
</head>
<body data-spy="scroll" data-target=".navbar" data-offset="50">
    <form id="form1" runat="server">
        <nav class="navbar navbar-inverse navbar-fixed-top">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <%--<a class="navbar-brand" href="#">Krupa Infotech</a>--%>
                    <a class="navbar-brand" href="#">
                        <asp:Label ID="lblcustinfo" runat="server" Text=""></asp:Label></a>
                </div>
                <div>
                    <div class="collapse navbar-collapse" id="myNavbar">
                        <ul class="nav navbar-nav">
                            <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Feedback<span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="#rptfeedbackdetail">Feedback Detail</a></li>
                                    <li><a href="#rptchartfoodquality">Food Quality Chart</a></li>
                                    <li><a href="#rptchartservice">Service Chart</a></li>
                                    <li><a href="#rptchartatmosphere">Atmosphere Chart</a></li>
                                    <li><a href="#rptchartoverall">Overall Experience Chart</a></li>
                                </ul>
                            </li>                            
                            <li>
                                <asp:Label ID="lblvalidity" runat="server" Text="" Font-Size="Large" ForeColor="Red"></asp:Label></li>
                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                            <li><a href="rmsnewreport.aspx" target="_self">Report</a></li> 
                            <li><a id="Logout" href="javascript:;" onclick="CallButtonEvent()">Logout </a></li>
                            <asp:Button ID="btnSubmit" Text="Logout" runat="server" OnClick="Logout_Click" Style="display: none" />
                        </ul>
                    </div>
                </div>
            </div>
        </nav>
        <div id="rptfeedbackdetail" class="container-fluid">
            <h3>Feedback Detail</h3>
            <h4>FROM :</h4>
            <asp:TextBox ID="dtpfeedbackdetailsdt" class="form-control input-lg" runat="server"
                Width="200px" ReadOnly="true">
            </asp:TextBox>
            <%--<br />--%>
            <h4>TO :</h4>
            <asp:TextBox ID="dtpfeedbackdetailedt" class="form-control input-lg" runat="server"
                Width="200px" ReadOnly="true">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnfeedbackdetail" runat="server" Text="Generate" Width="90px" class="generatebutton generatebuttoncss"
                OnClick="btnfeedbackdetail_Click" />
            &nbsp; &nbsp;
        <asp:Button ID="btnexcelfeedbackdetail" runat="server" class="excelbutton excelbuttoncss"
            Text="EXCEL" Width="90px" OnClick="feedbackdetail2excel" />
            &nbsp; &nbsp;
        <input type="button" id="btnpdffeedbackdetailreg" value="PDF" class="pdfbutton pdfbuttoncss"
            width="90px" onclick="feedbackdetail2pdf()" />
            <br />
            <%--<asp:Label ID="Label2" runat="server" Text=""></asp:Label>--%>
            <div id="divfeedbackdetail" class="table-responsive" height="400px" runat="server">
                <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
            </div>
        </div>
        <div id="rptchartfoodquality" class="container-fluid">
            <h3>Food Quality Chart</h3>
            <h4>FROM :</h4>
            <asp:TextBox ID="dtpchartfoodqualitysdt" class="form-control input-lg" runat="server"
                Width="200px" ReadOnly="true">
            </asp:TextBox>
            <h4>TO :</h4>
            <asp:TextBox ID="dtpchartfoodqualityedt" class="form-control input-lg" runat="server"
                Width="200px" ReadOnly="true">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnchartfoodquality" runat="server" Text="Generate" Width="90px"
                class="generatebutton generatebuttoncss" OnClick="btnchartfoodquality_Click" />
            &nbsp; &nbsp;
        <br />
            <div id="divchartfoodquality" class="table-responsive" height="400px" runat="server">
            </div>
        </div>
        <div id="rptchartservice" class="container-fluid">
            <h3>Service Chart</h3>
            <h4>FROM :</h4>
            <asp:TextBox ID="dtpchartservicesdt" class="form-control input-lg" runat="server"
                Width="200px" ReadOnly="true">
            </asp:TextBox>
            <h4>TO :</h4>
            <asp:TextBox ID="dtpchartserviceedt" class="form-control input-lg" runat="server"
                Width="200px" ReadOnly="true">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnchartservice" runat="server" Text="Generate" Width="90px" class="generatebutton generatebuttoncss"
                OnClick="btnchartservice_Click" />
            &nbsp; &nbsp;
        <br />
            <div id="divchartservice" class="table-responsive" height="400px" runat="server">
            </div>
        </div>
        <div id="rptchartatmosphere" class="container-fluid">
            <h3>Atmosphere Chart</h3>
            <h4>FROM :</h4>
            <asp:TextBox ID="dtpchartatmospheresdt" class="form-control input-lg" runat="server"
                Width="200px" ReadOnly="true">
            </asp:TextBox>
            <h4>TO :</h4>
            <asp:TextBox ID="dtpchartatmosphereedt" class="form-control input-lg" runat="server"
                Width="200px" ReadOnly="true">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnchartatmosphere" runat="server" Text="Generate" Width="90px" class="generatebutton generatebuttoncss"
                OnClick="btnchartatmosphere_Click" />
            &nbsp; &nbsp;
        <br />
            <div id="divchartatmosphere" class="table-responsive" height="400px" runat="server">
            </div>
        </div>
        <div id="rptchartoverall" class="container-fluid">
            <h3>Overall Experience Chart</h3>
            <h4>FROM :</h4>
            <asp:TextBox ID="dtpchartoverallsdt" class="form-control input-lg" runat="server"
                Width="200px" ReadOnly="true">
            </asp:TextBox>
            <h4>TO :</h4>
            <asp:TextBox ID="dtpchartoveralledt" class="form-control input-lg" runat="server"
                Width="200px" ReadOnly="true">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnchartoverall" runat="server" Text="Generate" Width="90px" class="generatebutton generatebuttoncss"
                OnClick="btnchartoverall_Click" />
            &nbsp; &nbsp;
        <br />
            <div id="divchartoverall" class="table-responsive" height="400px" runat="server">
            </div>
        </div>
    </form>
</body>

</html>
