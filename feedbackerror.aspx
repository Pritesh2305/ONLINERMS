<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feedbackerror.aspx.cs" Inherits="feedbackerror" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet"
        id="bootstrapcss" />
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
    <form action="" class="form-inlin justify-content-center">
    <div class="form-group">
        <div id="cover-caption">
            <div id="container" class="container">
                <div class="row text-white">
                    <div class="col-sm-6 offset-sm-3 text-center">
                        <br />
                        <h6 class="display-5" style="color: Black">
                            Problem with your submission. Errors have been highlighted below.</h6>
                        <br />
                        <asp:Label ID="lblerrormsg" test="dcgfdfg" Style="color: Red" runat="server" Font-Bold="True"
                            Font-Names="Verdana" Font-Size="small"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
