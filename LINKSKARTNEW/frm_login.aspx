<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frm_login.aspx.cs" Inherits="LINKSKART272015.frm_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>LINKSKART</title>

    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" type="text/css">
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <style>
        .main-text {
            position: absolute;
            top: 50px;
            width: 96.66666666666666%;
        }

        .item img {
            width: 100%;  
        }

        @media (min-width: 768px) {
            .omb_row-sm-offset-3 div:first-child[class*="col-"] {
                margin-left: 25%;
            }
        }

        .omb_login .omb_authTitle {
            text-align: center;
            line-height: 300%;
        }

        .omb_login .omb_socialButtons a {
            color: white;
            // In yourUse @body-bg opacity: 0.9;
        }

            .omb_login .omb_socialButtons a:hover {
                color: white;
                opacity: 1;
            }

        .omb_login .omb_socialButtons .omb_btn-facebook {
            background: #3b5998;
        }

        .omb_login .omb_socialButtons .omb_btn-twitter {
            background: #00aced;
        }

        .omb_login .omb_socialButtons .omb_btn-google {
            background: #c32f10;
        }

        .omb_login .omb_loginOr {
            position: relative;
            font-size: 1.5em;
            color: #aaa;
            margin-top: 1em;
            margin-bottom: 1em;
            padding-top: 0.5em;
            padding-bottom: 0.5em;
        }

            .omb_login .omb_loginOr .omb_hrOr {
                background-color: #cdcdcd;
                height: 1px;
                margin-top: 0px !important;
                margin-bottom: 0px !important;
            }

            .omb_login .omb_loginOr .omb_spanOr {
                display: block;
                position: absolute;
                left: 50%;
                top: -0.6em;
                margin-left: -1.5em;
                background-color: white;
                width: 3em;
                text-align: center;
            }

        .omb_login .omb_loginForm .input-group.i {
            width: 2em;
        }

        .omb_login .omb_loginForm .help-block {
            color: red;
        }

        @media (min-width: 768px) {
            .omb_login .omb_forgotPwd {
                text-align: right;
                margin-top: 10px;
            }
        }
       
    </style>
     <script>
         function onlyAlphabets(e, t) {
             try {
                 if (window.event) {
                     var charCode = window.event.keyCode;
                 }
                 else if (e) {
                     var charCode = e.which;
                 }
                 else { return true; }
                 if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                     return true;
                 else
                     return false;
             }
             catch (err) {
                 alert(err.Description);
             }
         }
         function validate(evt) {
             var theEvent = evt || window.event;
             var key = theEvent.keyCode || theEvent.which;
             key = String.fromCharCode(key);
             var regex = /[0-9]|\./;
             if (!regex.test(key)) {
                 theEvent.returnValue = false;
                 if (theEvent.preventDefault) theEvent.preventDefault();
             }
         }
         function clearsignup()
         {
             document.getElementById('txt_firstname').value = "";
             document.getElementById('txt_lastname').value = "";
             document.getElementById('txt_email').value = "";
             document.getElementById('txt_password').value = "";
             document.getElementById('txt_confirmpassword').value = "";
             document.getElementById('txt_phone').value = "";
         }
         function clearforgotemail()
         {
             document.getElementById('txt_forgotemail').value = "";
         }
    </script>
    <style>
                #loading {
  width: 100%;
  height: 100%;
  top: 0px;
  left: 0px;
  position: fixed;
  display: block;
  opacity: 0.7;
  background-color: #fff;
  z-index: 99;
  text-align: center;
}

#loading-image {
  position: absolute;
  top: 50%;
  left: 40%;
  z-index: 100;
}
    </style>

</head>
<body id="logiin" style="position: fixed; width: 100%">
    <form id="form1" runat="server">
          <div id="loading" style="display:none">
  <img id="loading-image" src="images/ajax-loader.gif" alt="Loading..." />
</div>
        <asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <asp:HiddenField runat="server" ID="hid_logintype" />
        <div class="container-fluid">
            <div class="row">
                <div class="">
                    <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                        <ol class="carousel-indicators" style="display: none;">
                            <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                            <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                        </ol>
                        <div class="carousel-inner">
                            <div class="item active">
                                <img src="images/ban1.png" alt="First slide" width="100%">
                            </div>
                          <%--  <div class="item">
                                <img src="images/ban1.png" alt="Second slide">
                            </div>
                            <div class="item">
                                <img src="images/ban2.jpg" alt="Third slide">
                            </div>--%>
                        </div>
                        <span style="display: none;">
                            <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                                <span class="glyphicon glyphicon-chevron-left"></span>
                            </a>
                            <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
                                <span class="glyphicon glyphicon-chevron-right"></span>
                            </a>
                        </span>
                    </div>

                    <div class="main-text">
                        <div class="col-md-12">
                            <div class="col-md-8">
                                <h1  class="text-center" style="color: #fff; font-size: 40px;display:none">Welcome to LinksKart</h1>
                            </div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" DefaultButton="btn_submitnew">
                                    <div class="col-md-4 pull-right">
                                <div class="well login-box">

                                    <legend>Login</legend>
                                    <div style="display:inline-flex" class="form-group">
                                        <input autocomplete="on"  runat="server"  id="txt_loginemail" placeholder="E-mail or Username" type="text" class="form-control" />
                                    
                               &nbsp;&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*" Style="visibility:visible" SetFocusOnError="true" ControlToValidate="txt_loginemail" ValidationGroup="LoginControls" ErrorMessage="Please enter EmailID" />
                                 &nbsp;&nbsp;   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Text="*" ControlToValidate="txt_loginemail" runat="server" Style="visibility:visible" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ErrorMessage="Please enter valid EmailID" SetFocusOnError="true" ValidationGroup="LoginControls" />
                                         </div>
                                    <div style="display:inline-flex" class="form-group">
                                       <%-- <input id="txt_loginpass" runat="server"    placeholder="Password" type="password" class="form-control" />--%>
                                        <asp:TextBox ID ="txt_loginpass" placeholder="Password" runat ="server" TextMode ="Password" class="form-control"  ></asp:TextBox>
                                   
                               &nbsp;&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="*" Style="visibility:visible" SetFocusOnError="true" ControlToValidate="txt_loginpass" ValidationGroup="LoginControls" ErrorMessage="Please enter Password" />
                                          </div>

                                    <div class="form-group  text-center">
                                        <asp:Button runat="server" ID="btn_submitnew" ValidationGroup="LoginControls" CssClass="btn btn-success btn-block btn-login-submit" OnClick="btn_submitnew_Click" Text="Login" />
                                       <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ValidationGroup="LoginControls" ShowSummary="false" DisplayMode="BulletList" />                
                                    </div>
                                     <div class="form-group text-center">
                                     <div class="checkbox">
  <label ><input type="checkbox" runat="server" id="chk_remember" value=""> <a style="font-size:14px;" >Remember me</a> </label>
  <label ><a href="#" style="font-size:14px;" onclick="clearforgotemail();" data-toggle="modal" data-target="#myModal2">Forgot Password ?</a> </label>
  
</div>
                                        
                                    </div>
                                  
                                    <div class="omb_login form-group">
                                        <label>Login With</label>
                                        <div class="omb_socialButtons">
                                            <div class="col-xs-4">
                                                <%--<asp:Button ID="btnLogin" runat="server" Text="Login with FaceBook" OnClick="Login" />--%>
                                                <asp:LinkButton runat="server" ID="lnk_fb" OnClick="lnk_fb_Click" CssClass="btn btn-sm btn-block omb_btn-facebook "><i class="fa fa-facebook"></i>
                                                    <span>Facebook</span></asp:LinkButton>
                                              <%--  <a href="#" class="btn btn-sm btn-block omb_btn-facebook">
                                                    <i class="fa fa-facebook"></i>
                                                    <span>Facebook</span>
                                                </a>--%>
                                            </div>
                                            <div class="col-xs-4">
                                                 <asp:LinkButton runat="server" ID="lnk_twitter" OnClick="lnk_twitter_Click" CssClass="btn btn-sm btn-block omb_btn-twitter"><i class="fa fa-twitter"></i>
                                                    <span>Twitter</span></asp:LinkButton>
                                              <%--  <a href="#" class="btn btn-sm btn-block omb_btn-twitter">
                                                    <i class="fa fa-twitter"></i>
                                                    <span>Twitter</span>
                                                </a>--%>
                                            </div>
                                            <div class="col-xs-4">
                                                  <asp:LinkButton runat="server" ID="lnk_google" OnClick="lnk_google_Click" CssClass="btn btn-sm btn-block omb_btn-google"><i class="fa fa-google-plus"></i>
                                                    <span>Google+</span></asp:LinkButton>
                                                <%--<a href="#" class="btn btn-sm btn-block omb_btn-google">
                                                    <i class="fa fa-google-plus"></i>
                                                    <span>Google+</span>
                                                </a>--%>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="form-group text-center">
                                        <a href="#" class="btn btn-warning btn-block" onclick="clearsignup();" data-toggle="modal" data-target="#myModal">Create an account ?</a>
                                    </div>

                                </div>
                                <!--
                            
-->
                            </div>
                                        </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="push">
        </div>

        <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
        <script src="js/jquery.min.js" type="text/javascript"></script>
        <!-- Include all compiled plugins (below), or include individual files as needed -->
        <script src="js/bootstrap.min.js" type="text/javascript"></script>

        <!--forgot password Modal Popup starts -->
        <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:Panel runat="server" DefaultButton="btn_forgotpass">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">×</span>
                            <span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title text-center" id="myModalLabel">Forgot Password</h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-10 col-md-offset-1" style="width:290px">
                            <div class="col-md-4" style="margin-top:6px;">
                                <label>Email Id</label>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group" style="display:inline-flex">
                                    <input type="text" runat="server" id="txt_forgotemail" name="email" placeholder="Enter Email id" class="form-control" />
                                       &nbsp;&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="*" Style="visibility:visible" SetFocusOnError="true" ControlToValidate="txt_forgotemail" ValidationGroup="ForgotControls" ErrorMessage="Please enter EmailID" />
                                 &nbsp;&nbsp;   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Text="*" ControlToValidate="txt_forgotemail" runat="server" Style="visibility:visible" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ErrorMessage="Please enter valid EmailID" SetFocusOnError="true" ValidationGroup="ForgotControls" />
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ValidationGroup="ForgotControls" runat="server" Text="Reset Password" CssClass="btn btn-success btn-sm" ID="btn_forgotpass" OnClick="btn_forgotpass_Click" />
                       <%-- <a type="button" class="btn btn-success btn-sm" data-dismiss="modal">Reset Password</a>--%>
                        <input type="button" runat="server"  class="btn btn-danger btn-sm" data-dismiss="modal" value="Close">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ValidationGroup="ForgotControls" ShowSummary="false" DisplayMode="BulletList" />                
                    </div>
                </div>
                    </asp:Panel>
            </div>
        </div>
        <!--forgot password Modal Popup starts -->

        <!--forgot password Modal Popup starts -->
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                  <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:Panel runat="server" DefaultButton="btn_submit">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">×</span>
                            <span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title text-center" id="myModalLabel">Signup</h4>
                    </div>
                    <div class="modal-body">
                        <div class="well col-md-8 col-md-offset-2">

                            <div style="display:inline-flex"  class="form-group">
                                <input runat="server" id="txt_firstname" onkeypress="return onlyAlphabets(event,this);" placeholder="First Name" type="text" class="form-control" />
                               &nbsp;&nbsp;
                                    <asp:RequiredFieldValidator ID="revFirstName" runat="server" Text="*" Style="visibility:visible" SetFocusOnError="true" ControlToValidate="txt_firstname" ValidationGroup="SignUPControls" ErrorMessage="Please enter First Name" />
                                &nbsp;&nbsp;<asp:RegularExpressionValidator ID="rgevFirstName" Text="*" ControlToValidate="txt_firstname" runat="server" ValidationExpression="^[a-zA-Z ]*$" Style="visibility:visible"
                                        ErrorMessage="Only alphabets are allowed for First Name(Use atleast 3 alphabets)" SetFocusOnError="true" ValidationGroup="SignUPControls" ToolTip="Please enter valid First Name" />
                                   
                              
                            </div>
                            
                            <div class="form-group">
                                <input runat="server" id="txt_lastname" onkeypress="return onlyAlphabets(event,this);" placeholder="Last Name" type="text" class="form-control" />
                            </div>
                            <div style="display:inline-flex" class="form-group">
                                <input placeholder="Phone number" runat="server" onkeypress='validate(event)' id="txt_phone" type="text" name="email" class="form-control" />
                            &nbsp;&nbsp;<asp:RequiredFieldValidator ID="revPhoneNo" runat="server" SetFocusOnError="true" Style="visibility:visible" Text="*" ControlToValidate="txt_phone" ValidationGroup="SignUPControls" ErrorMessage="Please enter Phone Number" />
         &nbsp;&nbsp; <asp:RegularExpressionValidator ControlToValidate="txt_phone" ID="revrtxtNumber" Text="*" style="visibility:visible" ForeColor="Red" 
                                                ValidationExpression="^[0-9]{10,12}$" runat="server" ErrorMessage="Please enter Phone Number between 10 to 12" ValidationGroup="SignUPControls"></asp:RegularExpressionValidator>
                            </div>
                            <div style="display:inline-flex" class="form-group">
                                <input placeholder="Email" runat="server" id="txt_email" type="text" name="email" class="form-control" />
                               &nbsp;&nbsp; <asp:RequiredFieldValidator ID="revEmailID" runat="server" Text="*" Style="visibility:visible" SetFocusOnError="true" ControlToValidate="txt_email" ValidationGroup="SignUPControls" ErrorMessage="Please enter EmailID" />
                                 &nbsp;&nbsp;   <asp:RegularExpressionValidator ID="revEmail" Text="*" ControlToValidate="txt_email" runat="server" Style="visibility:visible" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ErrorMessage="Please enter valid EmailID" SetFocusOnError="true" ValidationGroup="SignUPControls" />
                            </div>

                            <div style="display:inline-flex" class="form-group">
                                <input id="txt_password" runat="server" placeholder="Password" type="password" class="form-control" />
                               &nbsp;&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" Style="visibility:visible" SetFocusOnError="true" ControlToValidate="txt_password" ValidationGroup="SignUPControls" ErrorMessage="Please enter Password" />
                            </div>
                            <div style="display:inline-flex" class="form-group">
                                <input id="txt_confirmpassword" runat="server" placeholder="Confirm Password" type="password" class="form-control" />
                              &nbsp;&nbsp; <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*" Style="visibility:visible" SetFocusOnError="true" ControlToValidate="txt_confirmpassword" ValidationGroup="SignUPControls" ErrorMessage="Please enter confirm Password" />
                                 </div>
                            <div class="text-center">
                                <asp:Button runat="server" ValidationGroup="SignUPControls" ID="btn_submit" Text="Signup" OnClick="btn_submit_Click" CssClass="btn btn-success btn-block btn-login-submit" />
                                <%-- <input type="submit" class="btn btn-success btn-block btn-login-submit" value="Signup" />--%>
                            </div>
                            <asp:ValidationSummary ID="vsSignUp" runat="server" ShowMessageBox="true" ValidationGroup="SignUPControls" ShowSummary="false" DisplayMode="BulletList" />                
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="modal-footer">
                        <a type="button" onclick="clearsignup();" class="btn btn-success btn-sm">Clear</a>

                        <input type="button" class="btn btn-danger btn-sm" data-dismiss="modal" value="Close">
                    </div>
                </div>
                    </asp:Panel>
            </div>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      
        <!--forgot password Modal Popup starts -->
    </form>
</body>
</html>
