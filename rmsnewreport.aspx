<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rmsnewreport.aspx.cs" Inherits="rmsnewreport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ONLINE RMS REPORTS</title>
    <link rel="shortcut icon" href="img/RMS.ico" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>

    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Lato" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js" type="text/javascript"></script>
    <%--<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" type="text/javascript"></script>--%>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.22/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>
    <link href="css/rmsreportstyle.css" rel="stylesheet" />
    <link href="css/rmsnewreportstyle.css" rel="stylesheet" />

    <style>
        /*#rptbillmod {
            padding-top: 50px;
            height: 700px;
            color: Black;
            background-color: #87cefa;
        }*/
    </style>

    <!-- PDF FUNCTION -->
    <script type="text/javascript">
        function business2pdf() {
            html2canvas(document.getElementById('tblbusinesssummary'), {
                onrendered: function (canvas) {
                    var data = canvas.toDataURL();
                    var docDefinition = {
                        content: [{
                            image: data,
                            width: 500
                        }]
                    };
                    pdfMake.createPdf(docDefinition).download("businesssummary.pdf");
                }
            });
        }
        <script type="text/javascript">
            function billregister2pdf() {
                html2canvas(document.getElementById('tblbillregister'), {
                    onrendered: function (canvas) {
                        var data = canvas.toDataURL();
                        var docDefinition = {
                            content: [{
                                image: data,
                                width: 500
                            }]
                        };
                        pdfMake.createPdf(docDefinition).download("billregister.pdf");
                    }
                });
        }
    </script>
    <script type="text/javascript">
        function billdtl2pdf() {
            html2canvas(document.getElementById('tblbilldtl'), {
                onrendered: function (canvas) {
                    var data = canvas.toDataURL();
                    var docDefinition = {
                        content: [{
                            image: data,
                            width: 500
                        }]
                    };
                    pdfMake.createPdf(docDefinition).download("billdtl.pdf");
                }
            });
        }
    </script>
    <script type="text/javascript">
        function itemsalereg2pdf() {
            html2canvas(document.getElementById('tblitemsalereg'), {
                onrendered: function (canvas) {
                    var data = canvas.toDataURL();
                    var docDefinition = {
                        content: [{
                            image: data,
                            width: 500
                        }]
                    };
                    pdfMake.createPdf(docDefinition).download("itemsaleregister.pdf");
                }
            });
        }
    </script>


    <!-- DATETIME FUNCTION -->
    <script type="text/javascript">
        function LogoutButtonclickEvent() {
            document.getElementById("<%=btnLogout.ClientID %>").click();
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpbilldtlsdt").datepicker()
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpbilldtledt").datepicker()
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpbillregsdt").datepicker()
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpbillregedt").datepicker()
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpitemsalesdt").datepicker()
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpitemsaleedt").datepicker()
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpbusinesssdt").datepicker()
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpbusinessedt").datepicker()
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpkotmodsdt").datepicker()
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpkotmodedt").datepicker()
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpbillmodsdt").datepicker()
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dtpbillmodedt").datepicker()
        });
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
                    <a class="navbar-brand" href="#">
                        <asp:Label ID="lblcustinfo" runat="server" Text=""></asp:Label></a>
                </div>
                <div>
                    <div class="collapse navbar-collapse" id="myNavbar">
                        <ul class="nav navbar-nav">
                            <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Report<span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="#rptbusinesssummary">Business Summary</a></li>
                                    <li><a href="#rptbillregister">Bill Register</a></li>
                                    <li><a href="#rptbilldetail">Bill Detail</a></li>
                                    <li><a href="#rptitemsale">Item Sale</a></li>
                                </ul>
                            </li>
                            <li class="dropdown"><a class="dropdown-toggle" data-toggle="dropdown" href="#">Admin Report<span class="caret"></span></a>
                                <ul class="dropdown-menu">
                                    <li><a href="#rptkotmod">KOT Modified Detail</a></li>
                                    <li><a href="#rptbillmod">Bill Modified Detail</a></li>
                                </ul>
                            </li>
                            <li>
                                <asp:Label ID="lblvalidity" runat="server" Text="" Font-Size="Large" ForeColor="Red"></asp:Label></li>
                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                            <li><a href="feedbackreport.aspx" target="_self">Feedback Report</a></li>
                            <li><a href="changepassword.aspx" target="_self">Change Password</a></li>
                            <li><a id="Logout" href="javascript:;" onclick="LogoutButtonclickEvent()">Logout </a></li>
                            <asp:Button ID="btnLogout" Text="Logout" runat="server" OnClick="Logout_Click" Style="display: none" />
                        </ul>
                    </div>
                </div>
            </div>
        </nav>

        <div id="rptbusinesssummary" class="container-fluid">
            <h3>Business Summary</h3>
            <h4>FROM :</h4>
            <asp:TextBox ID="dtpbusinesssdt" class="form-control input-lg" runat="server" Width="200px"
                ReadOnly="true">
            </asp:TextBox>
            <h4>TO :</h4>
            <asp:TextBox ID="dtpbusinessedt" class="form-control input-lg" runat="server" Width="200px"
                ReadOnly="true">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnbusinesssummary" runat="server" Text="Generate" Width="90px" class="generatebutton generatebuttoncss" OnClick="btnbusinesssummary_Click" />
            &nbsp; &nbsp;       
        <input type="button" id="btnpdfbusinesssummary" value="PDF" class="pdfbutton pdfbuttoncss"
            width="90px" onclick="business2pdf()" />
            <br />
            <div id="divbusinesssummary" class="table-responsive" height="400px" runat="server">
            </div>
        </div>
        <div id="rptbillregister" class="container-fluid">
            <h3>Bill Register</h3>
            <h4>FROM :</h4>
            <asp:TextBox ID="dtpbillregsdt" class="form-control input-lg" runat="server" Width="200px"
                ReadOnly="true">
            </asp:TextBox>
            <h4>TO :</h4>
            <asp:TextBox ID="dtpbillregedt" class="form-control input-lg" runat="server" Width="200px"
                ReadOnly="true">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnbillreg" runat="server" Text="Generate" Width="90px" class="generatebutton generatebuttoncss" OnClick="btnbillreg_Click" />
            &nbsp;
    <asp:Button ID="btnexcelbillreg" runat="server" class="excelbutton excelbuttoncss"
        Text="EXCEL" Width="90px" OnClick="btnexportbillreg_Click" />
            &nbsp; &nbsp;
    <input type="button" id="btnpdfbillreg" value="PDF" class="pdfbutton pdfbuttoncss"
        width="90px" onclick="billregister2pdf()" />
            <br />
            <div id="divbillregister" class="table-responsive" height="400px" runat="server">
            </div>
        </div>
        <div id="rptbilldetail" class="container-fluid">
            <h3>Bill Detail</h3>
            <h4>FROM :</h4>
            <asp:TextBox ID="dtpbilldtlsdt" class="form-control input-lg" runat="server" Width="200px"
                ReadOnly="true">
            </asp:TextBox>
            <h4>TO :</h4>
            <asp:TextBox ID="dtpbilldtledt" class="form-control input-lg" runat="server" Width="200px"
                ReadOnly="true">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnbilldtl" runat="server" Text="Generate" Width="90px" class="generatebutton generatebuttoncss" OnClick="btnbilldtl_Click" />
            &nbsp; &nbsp;
    <asp:Button ID="btnexcelbilldtl" runat="server" class="excelbutton excelbuttoncss"
        Text="EXCEL" Width="90px" OnClick="billdtl2excel" />
            &nbsp; &nbsp;
    <input type="button" id="btnpdfbilldtl" value="PDF" class="pdfbutton pdfbuttoncss"
        width="90px" onclick="billdtl2pdf()" />
            <br />
            <div id="divbilldtl" class="table-responsive" height="400px" runat="server">
            </div>
        </div>
        <div id="rptitemsale" class="container-fluid">
            <h3>Item Sale Register</h3>
            <h4>FROM :</h4>
            <asp:TextBox ID="dtpitemsalesdt" class="form-control input-lg" runat="server" Width="200px" ReadOnly="true">
            </asp:TextBox>
            <h4>TO :</h4>
            <asp:TextBox ID="dtpitemsaleedt" class="form-control input-lg" runat="server" Width="200px" ReadOnly="true">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnitemsalereg" runat="server" Text="Generate" Width="90px" class="generatebutton generatebuttoncss" OnClick="btnitemsalereg_Click" />
            &nbsp; &nbsp;
            <asp:Button ID="btnexcelitemsalereg" runat="server" class="excelbutton excelbuttoncss" Text="EXCEL" Width="90px" OnClick="itemsalereg2excel" />
            &nbsp; &nbsp;
            <input type="button" id="btnpdfitemsalereg" value="PDF" class="pdfbutton pdfbuttoncss" width="90px" onclick="business2pdf()" />
            <br />
            <div id="divitemsalereg" class="table-responsive" height="400px" runat="server">
            </div>
        </div>
        <div id="rptkotmod" class="container-fluid">
            <h3>KOT Modified</h3>
            <h4>FROM :</h4>
            <asp:TextBox ID="dtpkotmodsdt" class="form-control input-lg" runat="server" Width="200px" ReadOnly="true">
            </asp:TextBox>
            <h4>TO :</h4>
            <asp:TextBox ID="dtpkotmodedt" class="form-control input-lg" runat="server" Width="200px" ReadOnly="true">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnkotmod" runat="server" Text="Generate" Width="90px" class="generatebutton generatebuttoncss" OnClick="btnkotmodreg_Click"/>
            <%----%> 
            &nbsp; &nbsp;
            <asp:Button ID="btnexcelkotmod" runat="server" class="excelbutton excelbuttoncss" Text="EXCEL" Width="90px" OnClick="btnexcelkotmod_Click"/>
            <%----%>
            &nbsp; &nbsp;
            <%--<input type="button" id="btnpdfkotmod" value="PDF" class="pdfbutton pdfbuttoncss" width="90px" />onclick="business2pdf()" --%>
            <br />
            <div id="divkotmod" class="table-responsive" height="400px" runat="server">
            </div>
        </div>
        <div id="rptbillmod" class="container-fluid">
            <h3>Bill Modified</h3>
            <h4>FROM :</h4>
            <asp:TextBox ID="dtpbillmodsdt" class="form-control input-lg" runat="server" Width="200px" ReadOnly="true">
            </asp:TextBox>
            <h4>TO :</h4>
            <asp:TextBox ID="dtpbillmodedt" class="form-control input-lg" runat="server" Width="200px" ReadOnly="true">
            </asp:TextBox>
            <br />
            <asp:Button ID="btnbillmod" runat="server" Text="Generate" Width="90px" class="generatebutton generatebuttoncss" OnClick="btnbillmodreg_Click" />
            &nbsp; &nbsp;
            <asp:Button ID="btnexcelbillmod" runat="server" class="excelbutton excelbuttoncss" Text="EXCEL" Width="90px" OnClick="btnexcelbillmod_Click"/>            
            &nbsp; &nbsp;
            <%--<input type="button" id="btnpdfbillmod" value="PDF" class="pdfbutton pdfbuttoncss" width="90px" />onclick="business2pdf()" --%>
            <br />
            <div id="divbillmod" class="table-responsive" height="400px" runat="server">
            </div>
        </div>
    </form>
</body>
</html>
