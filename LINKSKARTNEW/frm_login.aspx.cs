using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ASPSnippets.FaceBookAPI;
using System.Web.Script.Serialization;
using ASPSnippets.TwitterAPI;
using LINKSKARTNEW.Code;
using ASPSnippets.GoogleAPI;

using System.Configuration;
using System.Net.Mail;


namespace LINKSKART272015
{
    public partial class frm_login : System.Web.UI.Page
    {
        protected void Login(object sender, EventArgs e)
        {
            FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);
        }
        protected void Page_Load(object sender, EventArgs e)
        
        {
            try
            {
                if(Request.QueryString["redirectid"]==null)
                { 

                if(Session["USERINFO"]==null)
                {
                    

                    FaceBookConnect.API_Key = "794873443927888";
                    FaceBookConnect.API_Secret = "9acae58fe0463444952937a30c580db7";

                    //FaceBookConnect.API_Key = "794907040591195";
                    //FaceBookConnect.API_Secret = "c04f2ce2aa23fdae3ad06fce3ab78cb0";

                TwitterConnect.API_Key = "xZEV2y8Tlvu4RbZWuVtsFhGJO";
                TwitterConnect.API_Secret = "kokijgTSJnKmOLtEMlvZJtZMl0Dd0vFxqmK4kmIf1oVxFWgF9r";

                GoogleConnect.ClientId = "7682337252-coj546o017md56kuf69temoqh4hl3qt7.apps.googleusercontent.com";
                GoogleConnect.ClientSecret = "KMxXZCqWqk1YUtciM4g3IeuV";
                
                GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];

                if (!IsPostBack)
                {
                    Session["USERINFO"] = null;
                   
                    
                    //clearuserpass();
                    chk_remember.Checked = true;

                  


                    if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                    {
                        txt_loginemail.Value = Request.Cookies["UserName"].Value;
                       // txt_loginpass.Value = Request.Cookies["Password"].Value;
                        txt_loginpass.Attributes["value"] = Request.Cookies["Password"].Value;
                       
                          
                    }
                    if (Request.Cookies["LBRUserName"] != null && Request.Cookies["LBRPassword"] != null)
                    {
                        txt_loginemail.Value = Request.Cookies["LBRUserName"].Value;
                        // txt_loginpass.Value = Request.Cookies["Password"].Value;
                        txt_loginpass.Attributes["value"] = Request.Cookies["LBRPassword"].Value;
                        txt_loginpass.Text = Request.Cookies["LBRPassword"].Value;
                        autologin();
                       // Response.Cookies["UserName"].Value = Request.Cookies["LBRUserName"].Value;
                       // Response.Cookies["Password"].Value = Request.Cookies["LBRPassword"].Value;
                    }
                    //if (Response.Cookies["LBRADMINLOGIN"].Values["UNAME"] != null)
                    //{
                    //    txt_loginemail.Value = Response.Cookies["LBRADMINLOGIN"].Values["UNAME"];
                    //}
                    //if (Response.Cookies["LBRADMINLOGIN"].Values["UPASS"] != null)
                    //{
                    //    txt_loginpass.Value = Response.Cookies["LBRADMINLOGIN"].Values["UPASS"];
                    //}
                    string code = "";
                    if (Request.QueryString["oauth_token"] != null && Request.QueryString["oauth_verifier"] != null)
                    {
                        string oauth_token = Request.QueryString["oauth_token"];
                        string oauth_verifier = Request.QueryString["oauth_verifier"];

                        OAuthHelper oauthhelper = new OAuthHelper();
                        oauthhelper.GetUserTwAccessToken(oauth_token, oauth_verifier);

                        if (string.IsNullOrEmpty(oauthhelper.oauth_error))
                        {
                            Session["twtoken"] = oauthhelper.oauth_access_token;
                            Session["twsecret"] = oauthhelper.oauth_access_token_secret;
                            Session["twuserid"] = oauthhelper.user_id;
                            Session["twname"] = oauthhelper.screen_name;

                            LBR_SIGNUP obj = new LBR_SIGNUP();
                            //string[] split = facebook.Split(' ');
                            obj.LBR_EMAILID = oauthhelper.screen_name;
                            obj.LBR_MODIFIEDBY = 1;
                            obj.LBR_MODIFIEDTYPE = 1;
                            obj.LBR_CREATEDBY = 1;
                            obj.LBR_LOGINTYPE = 3;

                            obj.LBR_FIRSTNAME = oauthhelper.screen_name;

                            // obj.LBR_IMAGEURL = image;
                            obj.LBR_LOGINID = oauthhelper.user_id;
                            DataTable dt = BLL.checkemail(obj);
                            if (dt.Rows.Count > 0)
                            {
                                Session["USERINFO"] = dt;
                               Response.Redirect("Linkskart.aspx");
                            }
                            else
                            {
                                dt = BLL.ExecuteQuery("exec Usp_lbr_signup @OPERATION = 'signupsocial',@LBR_EMAILID='" + obj.LBR_EMAILID + "',@LBR_LOGINTYPE='" + obj.LBR_LOGINTYPE + "',@LBR_FIRSTNAME='" + obj.LBR_FIRSTNAME + "',@LBR_LASTNAME='" + obj.LBR_LASTNAME + "',@LBR_IMAGEURL='" + obj.LBR_IMAGEURL + "',@LBR_LOGINID='" + obj.LBR_LOGINID + "',@LBR_MODIFIEDBY='" + obj.LBR_MODIFIEDBY + "',@LBR_MODIFIEDTYPE='" + obj.LBR_MODIFIEDTYPE + "',@LBR_CREATEDBY='" + obj.LBR_CREATEDBY + "'");
                                if (dt.Rows.Count > 0)
                                {
                                    obj.LBR_ID = Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                                    obj.LBR_CREATEDBY = Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                                    obj.LBR_MODIFIEDBY = Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                                    bool status = BLL.setdefaulttabs(obj);
                                    Session["USERINFO"] = dt;
                                    Response.Redirect("Linkskart.aspx");
                                }
                                else
                                {
                                    BLL.ShowMessage(this, "Please try again after sometime");
                                }
                            }


                        }
                        else
                            Response.Write(oauthhelper.oauth_error);
                    }
                    if (Session["logintype"]!=null)
                    { 
                    if(Int32.Parse(Session["logintype"].ToString())==4)
                    {
                    if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                   
                    {
                         code = Request.QueryString["code"];
                        string json = GoogleConnect.Fetch("me", code);
                      
                        GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);
                        LBR_SIGNUP obj = new LBR_SIGNUP();
                        obj.LBR_EMAILID = profile.Emails.Find(email => email.Type == "account").Value;
                        obj.LBR_MODIFIEDBY = 1;
                        obj.LBR_MODIFIEDTYPE = 1;
                        obj.LBR_CREATEDBY = 1;
                        obj.LBR_LOGINTYPE = 4;
                        obj.USERS_STATUS = true;
                        obj.LBR_IMAGEURL = profile.Image.Url;
                        obj.LBR_FIRSTNAME = profile.DisplayName;
                        obj.LBR_LOGINID = profile.Id;
                        DataTable dt = BLL.checkemail(obj);
                        if (dt.Rows.Count > 0)
                        {
                            Session["USERINFO"] = dt;
                            Response.Redirect("Linkskart.aspx");
                        }
                        else
                        {
                            dt = BLL.ExecuteQuery("exec Usp_lbr_signup @OPERATION = 'signupsocial',@LBR_EMAILID='" + obj.LBR_EMAILID + "',@LBR_LOGINTYPE='" + obj.LBR_LOGINTYPE + "',@LBR_FIRSTNAME='" + obj.LBR_FIRSTNAME + "',@LBR_LASTNAME='" + obj.LBR_LASTNAME + "',@LBR_IMAGEURL='" + obj.LBR_IMAGEURL + "',@LBR_LOGINID='" + obj.LBR_LOGINID + "',@LBR_MODIFIEDBY='" + obj.LBR_MODIFIEDBY + "',@LBR_MODIFIEDTYPE='" + obj.LBR_MODIFIEDTYPE + "',@LBR_CREATEDBY='" + obj.LBR_CREATEDBY + "'");
                            if (dt.Rows.Count > 0)
                            {
                                obj.LBR_ID = Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                                obj.LBR_CREATEDBY = Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                                obj.LBR_MODIFIEDBY=Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                                bool status = BLL.setdefaulttabs(obj);
                                Session["USERINFO"] = dt;
                                Response.Redirect("Linkskart.aspx");
                            }
                            else
                            {
                                BLL.ShowMessage(this, "Please try again after sometime");
                            }
                        }
                        //lblId.Text = profile.Id;
                        //lblName.Text = profile.DisplayName;
                        //lblEmail.Text = profile.Emails.Find(email => email.Type == "account").Value;
                        //lblGender.Text = profile.Gender;
                        //lblType.Text = profile.ObjectType;
                        //ProfileImage.ImageUrl = profile.Image.Url;
                        //pnlProfile.Visible = true;
                        //btnLogin.Enabled = false;
                    }
                    if (Request.QueryString["error"] == "access_denied")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
                    }

                }
                   
                    if (Int32.Parse(Session["logintype"].ToString()) == 2)
                    {
                     code = Request.QueryString["code"];
                    if (!string.IsNullOrEmpty(code))
                    {
                        string data = FaceBookConnect.Fetch(code, "me");
                        FaceBookUser faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
                        faceBookUser.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);
                       string facebookid = faceBookUser.Id;
                       string username = faceBookUser.UserName;
                       string facebook = faceBookUser.Name;
                      string image = faceBookUser.PictureUrl;
                       string email = faceBookUser.Email;
                       LBR_SIGNUP obj = new LBR_SIGNUP();
                       string[] split = facebook.Split(' ');
                       obj.LBR_EMAILID = email;
                       obj.LBR_MODIFIEDBY = 1;
                       obj.LBR_MODIFIEDTYPE = 1;
                       obj.LBR_CREATEDBY = 1;
                       obj.LBR_LOGINTYPE = 2;
                        if(split.Length==2)
                        { 
                       obj.LBR_FIRSTNAME = split[0];
                       obj.LBR_LASTNAME = split[1];
                        }
                        else
                        {
                            obj.LBR_FIRSTNAME = facebook;
                        }
                        obj.LBR_IMAGEURL = image;
                        obj.LBR_LOGINID = facebookid;
                       DataTable dt = BLL.checkemail(obj);
                       if(dt.Rows.Count>0)
                       {
                           Session["USERINFO"] = dt;
                           Response.Redirect("Linkskart.aspx");
                       }
                        else
                       {
                           dt = BLL.ExecuteQuery("exec Usp_lbr_signup @OPERATION = 'signupsocial',@LBR_EMAILID='" + obj.LBR_EMAILID + "',@LBR_LOGINTYPE='" + obj.LBR_LOGINTYPE + "',@LBR_FIRSTNAME='" + obj.LBR_FIRSTNAME + "',@LBR_LASTNAME='" + obj.LBR_LASTNAME + "',@LBR_IMAGEURL='" + obj.LBR_IMAGEURL + "',@LBR_LOGINID='" + obj.LBR_LOGINID + "',@LBR_MODIFIEDBY='" + obj.LBR_MODIFIEDBY + "',@LBR_MODIFIEDTYPE='" + obj.LBR_MODIFIEDTYPE + "',@LBR_CREATEDBY='"+ obj.LBR_CREATEDBY +"'");
                      if(dt.Rows.Count>0)
                      {
                          obj.LBR_ID = Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                          obj.LBR_CREATEDBY = Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                          obj.LBR_MODIFIEDBY = Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                          bool status = BLL.setdefaulttabs(obj);
                          Session["USERINFO"] = dt;
                          Response.Redirect("Linkskart.aspx");
                      }
                           else
                      {
                          BLL.ShowMessage(this, "Please try again after sometime");
                      }
                       }


                    }
                    else
                    {

                    }
                    }
                }
                }
                }
                else
                {
                    Response.Redirect("Linkskart.aspx",false);
                }
            }
            else
            {
                DataTable dt_redirecturl = BLL.ExecuteQuery("select * from lbr_links where link_id="+ Request.QueryString["redirectid"].ToString() +"");        
           if(dt_redirecturl.Rows.Count>0)
           {
               Response.Redirect(""+ dt_redirecturl.Rows[0]["link_path"].ToString() +"");
           }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void clearuserpass()
        {
            txt_loginpass.Text = "";
            txt_loginemail.Value = "";
            txt_password.Value = "";
            txt_lastname.Value = "";
            txt_firstname.Value = "";
            txt_email.Value = "";
            txt_confirmpassword.Value = "";
            txt_phone.Value = "";
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_password.Value.Trim() == txt_confirmpassword.Value.Trim())
                {
                    bool status = false;
                    LBR_SIGNUP obj = new LBR_SIGNUP();
                    obj.LBR_FIRSTNAME = BLL.ReplaceQuote(txt_firstname.Value.Trim());
                    obj.LBR_LASTNAME = BLL.ReplaceQuote(txt_lastname.Value.Trim());
                    obj.LBR_EMAILID = txt_email.Value.Trim();
                    obj.LBR_PASSWORD = txt_password.Value.Trim();
                    obj.LBR_PHONENUMBER = txt_phone.Value.Trim();
                    obj.LBR_LOGINTYPE = 1;
                    DataTable dt_checkmail = BLL.checkemail(obj);
                    if (dt_checkmail.Rows.Count == 0)
                    {
                        
                        DataTable dt= BLL.createuser(obj);

                       
                        if (dt.Rows.Count>0)
                        {
                            obj.LBR_ID = Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                            obj.LBR_CREATEDBY = Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                            obj.LBR_MODIFIEDBY = Int32.Parse(dt.Rows[0]["LBR_ID"].ToString());
                            status = BLL.setdefaulttabs(obj);
                            clearuserpass();
                            BLL.ShowMessage(this, "Signup sucessfully completed,please login with your credentials");


                            MailMessage mailmessage = new MailMessage();
                            mailmessage.IsBodyHtml = true;
                            SmtpClient client = new SmtpClient("linkskart.com");

                            client.Credentials = new System.Net.NetworkCredential("info@linkskart.com", ".santhu143");
                            mailmessage.From = new System.Net.Mail.MailAddress("info@linkskart.com");

                            // mailmessage.From = new MailAddress("santhosh@pragatipadh.com");
                            mailmessage.To.Add(obj.LBR_EMAILID);
                            // mailmessage.CC.Add(emailid);
                            mailmessage.Subject = "Signup Process sucessfully Completed with LINKSKART";

                            mailmessage.Body = " <p> Dear " + obj.LBR_FIRSTNAME + " " + obj.LBR_LASTNAME + ",<br />Thank you so much for showing interest in LINKSKART</p><table align=\"center\"><tr><td>Username/EmailID</td><td>:</td><td>" + obj.LBR_EMAILID + "</td></tr><tr><td>Password</td><td>:</td><td>" + obj.LBR_PASSWORD + "</td></tr></table><p>Please <a href=\"www.linkskart.com\">Click Here</a> to visit LINKSKART></p>";
                            client.EnableSsl = false;

                            //MailMessage eMail = new MailMessage();
                            //eMail.BodyFormat = MailFormat.Html;
                            //eMail.From = "info@linkskart.com";
                            //eMail.To = obj.LBR_EMAILID;
                            //// eMail.Cc = "sales@globaltradersjaipur.com";
                            //// eMail.Bcc = "info@designtheweb.in";
                            //eMail.Subject = "Signup Process sucessfully Completed with LINKSKART";
                            //eMail.Body = " <p> Dear " + obj.LBR_FIRSTNAME + " " + obj.LBR_LASTNAME + ",<br />Thank you so much for showing interest in LINKSKART</p><table align=\"center\"><tr><td>Username/EmailID</td><td>:</td><td>" + obj.LBR_EMAILID + "</td></tr><tr><td>Password</td><td>:</td><td>" + obj.LBR_PASSWORD + "</td></tr></table><p>Please <a href=\"www.linkskart.com\">Click Here</a> to visit LINKSKART></p>";


                            //eMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                            //eMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "25");
                            //eMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "info@linkskart.com");
                            //eMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", ".santhu143");
                            //SmtpMail.SmtpServer = "linkskart.com";

                            try
                            {

                                client.Send(mailmessage);
                            }
                            catch (Exception ae)
                            {
                                
                            }

                            //MailMessage mailmessage = new MailMessage();
                            //mailmessage.IsBodyHtml = true;
                            //SmtpClient client = new SmtpClient(ConfigurationSettings.AppSettings["signupMsgclient"].ToString());

                            //client.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["signupMsgUser"].ToString(), ConfigurationSettings.AppSettings["signMsgPassword"].ToString());
                            //mailmessage.From = new System.Net.Mail.MailAddress(ConfigurationSettings.AppSettings["signupMsgFrom"].ToString());

                            //// mailmessage.From = new MailAddress("santhosh@pragatipadh.com");
                            //mailmessage.To.Add(ConfigurationSettings.AppSettings["signupMsgTo"].ToString());
                            //// mailmessage.CC.Add(emailid);
                            //mailmessage.Subject = "Signup process sucessfully completed with LINKSKART";

                            //mailmessage.Body = " <p> Dear "+ obj.LBR_FIRSTNAME +" "+ obj.LBR_LASTNAME+",<br />Thank you so much for showing interest in LINKSKART</p><table align=\"center\"><tr><td>Username/EmailID</td><td>:</td><td>"+ obj.LBR_EMAILID +"</td></tr><tr><td>Password</td><td>:</td><td>"+ obj.LBR_PASSWORD +"</td></tr></table><p>Please <a href=\"www.linkskart.com\">Click Here</a> to visit LINKSKART></p>";
                            //client.EnableSsl = false;
                            //client.Port = 25;
                            //client.Send(mailmessage);
                        }
                        else
                        {
                            BLL.ShowMessage(this, "please try again after sometime");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "$('#myModal').modal('show'); ", true);
                        }
                    }

                    else
                    {
                        BLL.ShowMessage(this, "EmailID already exists");
                        txt_email.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "$('#myModal').modal('show'); ", true);
                    }
                }
                else
                {
                    BLL.ShowMessage(this, "Password/confirm password doesnot match");
                    txt_confirmpassword.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "$('#myModal').modal('show');", true);
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_submitnew_Click(object sender, EventArgs e)
        
        {
            try
            {
                LBR_SIGNUP obj = new LBR_SIGNUP();
                obj.LBR_PASSWORD = BLL.Encrypt(BLL.ReplaceQuote(txt_loginpass.Text.Trim()));
                obj.LBR_EMAILID = BLL.ReplaceQuote(txt_loginemail.Value.Trim());
                obj.LBR_LOGINTYPE = 1;
                DataTable dt = BLL.checklogin(obj);
                if (dt.Rows.Count > 0)
                {

                    if (chk_remember.Checked)
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["UserName"].Value = txt_loginemail.Value.Trim();
                        Response.Cookies["Password"].Value = txt_loginpass.Text.Trim();
                    }
                    else
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                    }
                   

                    Response.Cookies["LBRUserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["LBRPassword"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["LBRUserName"].Value = txt_loginemail.Value.Trim();
                    Response.Cookies["LBRPassword"].Value = txt_loginpass.Text.Trim();
                    //if (chk_remember.Checked)
                    //{
                    //    if (Request.Browser.Cookies)
                    //    {
                    //        if (Request.Cookies["LBRADMINLOGIN"] == null)
                    //        {
                    //            HttpCookie cookie = new HttpCookie("LBRADMINLOGIN");
                    //            Response.Cookies["LBRADMINLOGIN"].Expires = DateTime.Now.AddDays(30);

                    //            Response.Cookies["LBRADMINLOGIN"].Values["UNAME"] = txt_loginemail.Value;
                    //            Response.Cookies["LBRADMINLOGIN"].Values["UPASS"] = txt_loginpass.Value.ToString().Trim();

                    //        }
                    //        else
                    //        {
                    //            HttpCookie cookie = new HttpCookie("LBRADMINLOGIN");

                    //            Response.Cookies["LBRADMINLOGIN"].Values["UNAME"] = txt_loginemail.Value;
                    //            Response.Cookies["LBRADMINLOGIN"].Values["UPASS"] = txt_loginpass.Value.ToString().Trim();

                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (Request.Browser.Cookies)
                    //    {
                    //        if (Request.Cookies["LBRADMINLOGIN"] != null)
                    //        {
                    //            HttpCookie myCookie = new HttpCookie("LBRADMINLOGIN");
                    //            myCookie.Expires = DateTime.Now.AddDays(-500);
                    //            Response.Cookies.Add(myCookie);
                    //        }
                    //    }
                    //}
                    Session["USERINFO"] = dt;
                   Response.Redirect("Linkskart.aspx",false);
                }
                else
                {
                    BLL.ShowMessage(this, "EmailID/Password Incorrect");
                    txt_loginpass.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void autologin()
        {
            try
            {
                LBR_SIGNUP obj = new LBR_SIGNUP();
                obj.LBR_PASSWORD = BLL.Encrypt(BLL.ReplaceQuote(txt_loginpass.Text.Trim()));
                obj.LBR_EMAILID = BLL.ReplaceQuote(txt_loginemail.Value.Trim());
                obj.LBR_LOGINTYPE = 1;
                DataTable dt = BLL.checklogin(obj);
                if (dt.Rows.Count > 0)
                {

                    if (chk_remember.Checked)
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                        Response.Cookies["UserName"].Value = txt_loginemail.Value.Trim();
                        Response.Cookies["Password"].Value = txt_loginpass.Text.Trim();
                    }
                    else
                    {
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                    }
                   

                    Response.Cookies["LBRUserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["LBRPassword"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["LBRUserName"].Value = txt_loginemail.Value.Trim();
                    Response.Cookies["LBRPassword"].Value = txt_loginpass.Text.Trim();
                    //if (chk_remember.Checked)
                    //{
                    //    if (Request.Browser.Cookies)
                    //    {
                    //        if (Request.Cookies["LBRADMINLOGIN"] == null)
                    //        {
                    //            HttpCookie cookie = new HttpCookie("LBRADMINLOGIN");
                    //            Response.Cookies["LBRADMINLOGIN"].Expires = DateTime.Now.AddDays(30);

                    //            Response.Cookies["LBRADMINLOGIN"].Values["UNAME"] = txt_loginemail.Value;
                    //            Response.Cookies["LBRADMINLOGIN"].Values["UPASS"] = txt_loginpass.Value.ToString().Trim();

                    //        }
                    //        else
                    //        {
                    //            HttpCookie cookie = new HttpCookie("LBRADMINLOGIN");

                    //            Response.Cookies["LBRADMINLOGIN"].Values["UNAME"] = txt_loginemail.Value;
                    //            Response.Cookies["LBRADMINLOGIN"].Values["UPASS"] = txt_loginpass.Value.ToString().Trim();

                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (Request.Browser.Cookies)
                    //    {
                    //        if (Request.Cookies["LBRADMINLOGIN"] != null)
                    //        {
                    //            HttpCookie myCookie = new HttpCookie("LBRADMINLOGIN");
                    //            myCookie.Expires = DateTime.Now.AddDays(-500);
                    //            Response.Cookies.Add(myCookie);
                    //        }
                    //    }
                    //}
                    Session["USERINFO"] = dt;
                    Response.Redirect("Linkskart.aspx", false);
                }
                else
                {
                    //Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    //Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                    //Response.Cookies["LBRUserName"].Expires = DateTime.Now.AddDays(-1);
                    //Response.Cookies["LBRPassword"].Expires = DateTime.Now.AddDays(-1);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void btn_forgotpass_Click(object sender, EventArgs e)
        {
            try
            {
                LBR_SIGNUP obj = new LBR_SIGNUP();
                obj.LBR_LOGINTYPE = 1;
                obj.LBR_EMAILID = txt_forgotemail.Value.Trim();
                DataTable dt_checkmail = BLL.checkemail(obj);
                if (dt_checkmail.Rows.Count > 0)
                {
                   // should i give you as sample code?
                    //is any thing wrong i wrote this codewas used in mny sites by me it worked well if you have any other code provide me

                    //for psl team
                    // make changes in this and try

                      MailMessage mailmessage = new MailMessage();
                        mailmessage.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient("linkskart.com");

                        client.Credentials = new System.Net.NetworkCredential("info@linkskart.com", ".santhu143");
                        mailmessage.From = new System.Net.Mail.MailAddress("info@linkskart.com");

                        // mailmessage.From = new MailAddress("santhosh@pragatipadh.com");
                        mailmessage.To.Add(obj.LBR_EMAILID);
                        // mailmessage.CC.Add(emailid);
                        mailmessage.Subject = "Password request";

                        mailmessage.Body = "<p> Dear " + dt_checkmail.Rows[0]["LBR_FIRSTNAME"].ToString() + " " + dt_checkmail.Rows[0]["LBR_LASTNAME"].ToString() + ",<br /> <br />You password is " + BLL.Decrypt(dt_checkmail.Rows[0]["LBR_PASSWORD"].ToString()) + " please <a href=\"http://www.linkskart.com\">Click Here</a> to visit LINKSKART.</p></div>";
                        client.EnableSsl = false;
                      

                    //MailMessage eMail = new MailMessage();
                    //eMail.BodyFormat = MailFormat.Html;
                    //eMail.From = "info@linkskart.com";
                    //eMail.To = obj.LBR_EMAILID;
                    //// eMail.Cc = "sales@globaltradersjaipur.com";
                    //// eMail.Bcc = "info@designtheweb.in";

                    //eMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "linkskart.com");
                    //eMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                    //eMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "25");
                    //eMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "info@linkskart.com");
                    //eMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", ".santhu143");
                    //eMail.Body = "<p> Dear " + dt_checkmail.Rows[0]["LBR_FIRSTNAME"].ToString() + " " + dt_checkmail.Rows[0]["LBR_LASTNAME"].ToString() + ",<br /> <br />You password is " + BLL.Decrypt(dt_checkmail.Rows[0]["LBR_PASSWORD"].ToString()) + " please <a href=\"http://www.linkskart.com\">Click Here</a> to visit LINKSKART.</p></div>";
                    //eMail.Subject = "Password request";

                   
                   
                    try
                    {
                        client.Send(mailmessage);
                        //SmtpMail.Send(eMail);
                    }
                    catch (Exception ae)
                    {
                        // Label1.Text = ae.Message;
                    }
                    //ok i will try this if i dont get i will message you again ok no problem, if chat is not available you can drop an email at support@hosting.co.in with code file attached.ok t

                    //MailMessage mailmessage = new MailMessage();
                    //mailmessage.IsBodyHtml = true;
                    //SmtpClient client = new SmtpClient(ConfigurationSettings.AppSettings["forgotMsgclient"].ToString());

                    //client.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["forgotMsgUser"].ToString(), ConfigurationSettings.AppSettings["forgotMsgPassword"].ToString());
                    //mailmessage.From = new System.Net.Mail.MailAddress(ConfigurationSettings.AppSettings["forgotMsgFrom"].ToString());

                    //// mailmessage.From = new MailAddress("santhosh@pragatipadh.com");
                    //mailmessage.To.Add(obj.LBR_EMAILID);
                    //// mailmessage.CC.Add(emailid);
                    //mailmessage.Subject = "Request for password";
                    
                    //mailmessage.Body = "<p> Dear "+ dt_checkmail.Rows[0]["LBR_FIRSTNAME"].ToString() + " "+ dt_checkmail.Rows[0]["LBR_LASTNAME"].ToString() +",<br /> <br />You password is "+ BLL.Decrypt(dt_checkmail.Rows[0]["LBR_PASSWORD"].ToString()) +" please <a href=\"http://www.linkskart.com\">Click Here</a> to visit LINKSKART.</p></div>";
                    //client.EnableSsl = false;
                    //client.Port = 25;
                    //client.Send(mailmessage);
                   
                    BLL.ShowMessage(this, "Password has been sent to your registered email id");
                    txt_forgotemail.Value = "";
                }
                else
                {
                    BLL.ShowMessage(this, "This is not registered Email id");
                    txt_forgotemail.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "$('#myModal2').modal('show'); ", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnk_fb_Click(object sender, EventArgs e)
        {
            try
            {
                Session["USERINFO"] = null;
                Session.Clear();
                
                Session["logintype"] = 2;
                FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);
               
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnk_twitter_Click(object sender, EventArgs e)
        {
            try
            {
                Session["USERINFO"] = null;
                Session.Clear();
                
                TwitterConnect twitter = new TwitterConnect();
                twitter.Authorize(Request.Url.AbsoluteUri.Split('?')[0]);
                //OAuthHelper oauthhelper = new OAuthHelper();
                //string requestToken = oauthhelper.GetRequestToken();

                //if (string.IsNullOrEmpty(oauthhelper.oauth_error))
                //    Response.Redirect(oauthhelper.GetAuthorizeUrl(requestToken));
                //else
                //    Response.Write(oauthhelper.oauth_error);
            }
            catch(Exception ex)
            {

            }
        }

        protected void lnk_google_Click(object sender, EventArgs e)
        {
            try
            {
                Session["USERINFO"] = null;
                Session.Clear();
                
                Session["logintype"] = 4;
                GoogleConnect.Authorize("profile", "email");
               
            }
            catch(Exception ex)
            {

            }
        }
    }
}