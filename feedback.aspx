<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="feedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Feedback Form</title>
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
                    <h3 class="display-12" style="color: green" >
                        Feedback Form</h3>
                    <div class="info-form">
                        <form action="" class="form-inlin justify-content-center" style="color: #200080">
                        <div class="form-group">
                            <label class="sr-only">
                                Name</label>
                            <asp:TextBox ID="txtname" placeholder="Name" Text="" class="form-control" MaxLength="100"
                                runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="sr-only">
                                Mobile</label>
                            <asp:TextBox ID="txtmobno" placeholder="Mobile" Text="" class="form-control" MaxLength="50"
                                runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="sr-only">
                                E-Mail</label>
                            <asp:TextBox ID="txtemail" placeholder="E-Mail" Text="" class="form-control" MaxLength="200"
                                runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="control-label" for="rating">
                                <h4 class="field-label-header" style="color: Red">
                                    Please Rate us on</h4>
                            </label>
                            <div class="ratingcnt">
                                <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
                                    rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN"
                                    crossorigin="anonymous">
                                <div class="form-group" id="rating-ability-wrapper">
                                    <label class="control-label" for="rating">
                                        <span class="field-label-header" style="color: #000080"><b>Food Quality</b></span>
                                    </label>
                                    <br />
                                    <button type="button" class="btnrating1 btn btn-default .btn-md" data-attr="1" id="rating-star-1">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating1 btn btn-default btn-md" data-attr="2" id="rating-star-2">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating1 btn btn-default btn-md" data-attr="3" id="rating-star-3">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating1 btn btn-default btn-md" data-attr="4" id="rating-star-4">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating1 btn btn-default btn-md" data-attr="5" id="rating-star-5">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <input type="hidden" id="selected_rating1" name="selected_rating1" value="" />
                                </div>
                            </div>
                            <div class="ratingcnt2">
                                <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
                                    rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN"
                                    crossorigin="anonymous">
                                <div class="form-group" id="Div1">
                                    <label class="control-label" for="rating">
                                        <span class="field-label-header" style="color: #000080"><b>Service</b></span>
                                    </label>
                                    </br>
                                    <button type="button" class="btnrating2 btn btn-default .btn-md" data-attr="1" id="rating2-star-1">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating2 btn btn-default btn-md" data-attr="2" id="rating2-star-2">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating2 btn btn-default btn-md" data-attr="3" id="rating2-star-3">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating2 btn btn-default btn-md" data-attr="4" id="rating2-star-4">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating2 btn btn-default btn-md" data-attr="5" id="rating2-star-5">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <input type="hidden" id="selected_rating2" name="selected_rating2" value="" />
                                </div>
                            </div>
                            <div class="ratingcnt3">
                                <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
                                    rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN"
                                    crossorigin="anonymous">
                                <div class="form-group" id="Div2">
                                    <label class="control-label" for="rating">
                                        <span class="field-label-header" style="color: #000080"><b>Atmosphere</b></span>
                                    </label>
                                    </br>
                                    <button type="button" class="btnrating3 btn btn-default .btn-md" data-attr="1" id="rating3-star-1">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating3 btn btn-default btn-md" data-attr="2" id="rating3-star-2">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating3 btn btn-default btn-md" data-attr="3" id="rating3-star-3">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating3 btn btn-default btn-md" data-attr="4" id="rating3-star-4">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating3 btn btn-default btn-md" data-attr="5" id="rating3-star-5">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <input type="hidden" id="selected_rating3" name="selected_rating3" value="" />
                                </div>
                            </div>
                            <div class="ratingcnt4">
                                <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
                                    rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN"
                                    crossorigin="anonymous">
                                <div class="form-group" id="Div3">
                                    <label class="control-label" for="rating">
                                        <span class="field-label-header" style="color: #000080"><b>Overall Experience</b></span>
                                    </label>
                                    </br>
                                    <button type="button" class="btnrating4 btn btn-default .btn-md" data-attr="1" id="rating4-star-1">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating4 btn btn-default btn-md" data-attr="2" id="rating4-star-2">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating4 btn btn-default btn-md" data-attr="3" id="rating4-star-3">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating4 btn btn-default btn-md" data-attr="4" id="rating4-star-4">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <button type="button" class="btnrating4 btn btn-default btn-md" data-attr="5" id="rating4-star-5">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </button>
                                    <input type="hidden" id="selected_rating4" name="selected_rating4" value="" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="sr-only">
                                    Your Suggestion</label>
                                <asp:TextBox ID="txtsuggestion" placeholder="Please write your Suggestion " Text=""
                                    class="form-control" MaxLength="500" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Label ID="lblinfo" runat="server"></asp:Label>
                        <br />
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="btn btn-primary btn-lg"
                            OnClick="btnsubmit_click" />
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
