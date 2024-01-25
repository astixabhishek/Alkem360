<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../Images/favicon.png" rel="shortcut icon" type="image/x-icon" />

    <title>Alkem_E360</title>
    <!-- Latest compiled and minified CSS -->
    <link href="Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Content/Site.css" rel="stylesheet" type="text/css" />

    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="Scripts/jquery.min-1.12.4.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("img.bg-img").hide();
            var $url = $("img.bg-img").attr("src");
            $('.full-background').css('backgroundImage', 'url(' + $url + ')');

            $(".loginfrm").css({ "margin": $(window).height() - ($(this).outerHeight() + 400) / 2 + 'px auto 0' });

            //$("p.text-center").css({ "margin-top": $(window).height() - ($(this).outerHeight() + 180) / 2 + 'px' });

            //$(window).resize(function () {
            //    if (window.innerWidth < 768) {
            //        $(".loginfrm").css({ "margin-top": "85px" });
            //        $("p.text-center").css({ "margin-top": "18px", "font-size": "30pt" });
            //    }
            //});
        });
    </script>
    <script type="text/javascript">

        function fnReset() {
            //alert("In reset")
            document.getElementById("txtLoginID").value = "";
            document.getElementById("txtPassword").value = "";
            document.getElementById("dvMessage").innerText = "";
        }

        function fnHideError() {
            document.getElementById("tblProgress").style.display = "none";
            //window.clearInterval(refShow);
        }

        function fnValidate() {
            document.getElementById("hdnResolution").value = screen.availWidth + "*" + screen.availHeight;
            if (document.getElementById("txtLoginID").value == "") {

                document.getElementById("dvMessage").innerText = "Login ID can't be left blank";
                document.getElementById("txtLoginID").focus();
                return false;
            }
            if (document.getElementById("txtPassword").value == "") {
                if (LngId == 1) {
                    document.getElementById("dvMessage").innerText = "Password can't be left blank";
                }
                else {
                    document.getElementById("dvMessage").innerText = "Password can't be left blank"
                }
                document.getElementById("txtPassword").focus();
                return false;
            }
        }

        function fnKeyPress() {
            document.getElementById("dvMessage").innerText = "";
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="full-background">
            <img src="Images/wall.jpg" class="bg-img" />
        </div>
        <%--<nav class="navbar navbar-fixed-top">
            <div class="pull-left">
                <asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/alkem-logo.png" title="logo" CssClass="logo" />
            </div>
            <nav class="pull-right">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/A_EY.png" title="logo" CssClass="ey-logo-sec" />
            </nav>
        </nav>--%>
        <div class="loginfrm cls-4">
            <div class="login-box"></div>
            <div class="login-box">
                <div class="login-logo">
                    <asp:Image ID="imgLogo1" runat="server" ImageUrl="~/Images/alkem-logo.png" title="logo" CssClass="" />
                </div>
                <div class="login-box-msg">
                    <h3 class="title">Login To</h3>
                </div>
                <div class="login-box-body clearfix">
                    <div class="input-group frm-group-txt">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fa fa-user"></i></span>
                        </div>
                        <asp:TextBox onkeypress="fnKeyPress();" ID="txtLoginID" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="input-group frm-group-txt">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fa fa-lock"></i></span>
                        </div>
                        <asp:TextBox onkeypress="fnKeyPress();" ID="txtPassword" runat="server" TextMode="Password" class="form-control" autocomplete="off"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnLogin" Text="Login" CssClass="btns btn-submit w-100" runat="server"></asp:Button>
                </div>
                <div class="login-box-footer clearfix">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/A_EY.png" title="logo" CssClass="" />
                </div>
                <div class="text-center">
                    <div id="dvMessage" runat="server" cssclass="label label-danger labeldanger"></div>
                </div>
            </div>
            <div class="login-box alt">
                <div class="toggle"></div>
            </div>
        </div>
        <%--<p class="text-center" style="font-size: 35pt; font-family: 'Open Sans',Arial,sans-serif;">
                        Multi Stakeholder<br>
                        Feedback
                    </p>--%>
        <input id="hdnResolution" type="hidden" runat="server" />
    </form>
</body>
</html>
