using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using HtmlAgilityPack;
using System.IO;
using System.Drawing;
using System.Data;

using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Net.Mail;
using ASPSnippets.TwitterAPI;
using System.Web.Hosting;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using LINKSKARTNEW.Code;

namespace LINKSKARTNEW
{

    public partial class Linkskart : System.Web.UI.Page
    {
        static string[] mediaExtensions = {
    ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF"
};
        static string[] textExtensions = {
    ".TXT", ".MD", ".CSV", ".NFO", ".PHP", ".INI", ".ASPX", ".HTML", ".CS"
};
        static string[] pdfExtensions = {
    ".PDF"
};
        static string[] wordExtensions = {
    ".DOCX", ".DOTM", ".DOT", ".DOC", ".DOCM",".DOTX"
};
        static string[] excelExtensions = {
    ".XPS", ".XML", ".XLW", ".XLTX", ".XLTM",".XLT",".XLSX", ".XLSM", ".XLSB", ".XLS", ".XLAM",".XLA"
};
        static string[] songExtensions = {
     ".MP3", ".WAV"
};
        static string[] videoExtensions = {
     ".MP4", ".OGG",".WebM"
};
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERINFO"] != null)
                {



                    if (!IsPostBack)
                    {
                        // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "$('#loading').hide();", true);


                        DataTable dt_user = (DataTable)Session["USERINFO"];



                        if (dt_user.Rows.Count > 0)
                        {
                            hid_userid.Value = dt_user.Rows[0]["lbr_id"].ToString();
                           // lbl_name.InnerHtml = "" + dt_user.Rows[0]["LBR_FIRSTNAME"].ToString() + " ";
                            //if (dt_user.Rows[0]["LBR_LOGINTYPE"].ToString() != "1")
                            //{
                            //  //  LinkButtonchange.Visible = false;
                            //}
                            //else
                            //{
                            //   // LinkButtonchange.Visible = true;
                            //}
                            //if (dt_user.Rows[0]["LBR_IMAGEURL"].ToString() != "")
                            //{
                            //    //img_user.Src = dt_user.Rows[0]["LBR_IMAGEURL"].ToString();
                            //}
                            //else
                            //{
                            //  //  img_user.Attributes.Add("display", "none");
                            //}
                            string name = dt_user.Rows[0]["LBR_FIRSTNAME"].ToString();

                            if (dt_user.Rows[0]["LBR_LASTNAME"].ToString() != null && dt_user.Rows[0]["LBR_LASTNAME"].ToString()!="")
                            {
                                name = name + " " + dt_user.Rows[0]["LBR_LASTNAME"].ToString();
                            }

                            //tabs loading
                            //imp
                            //  loadadmintabs();
                            loadtabs(dt_user.Rows[0]["lbr_id"].ToString(), dt_user.Rows[0]["LBR_IMAGEURL"].ToString(),name);
                        }
                        else
                        {
                            Response.Redirect("frm_login.aspx", false);
                        }
                    }
                }

                else
                {
                    Response.Redirect("frm_login.aspx", false);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void loadadmintabs()
        {
            LBR_SIGNUP obj = new LBR_SIGNUP();
            obj.LBR_ID = 22;
            DataTable dt_tabs = BLL.get_tabs(obj);
            string innerhtml = "";
            string tabcontent = "";
            string remainingtabs = "";
            string remainintabscontent = "";
            //int k = 0;
            if (dt_tabs.Rows.Count > 0)
            {
                for (int i = 0; i < dt_tabs.Rows.Count; i++)
                {
                    if (i <= 2)
                    {
                        if (i == 0)
                        {
                            innerhtml = "<li class=\"admintabs\" class=\"active\"><a class=\"admintabsa\" id=\"aadmin_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttabadmin('22_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";

                            LBR_LINKS objlinks = new LBR_LINKS();
                            objlinks.LINK_TABID = Int32.Parse(dt_tabs.Rows[i]["TAB_ID"].ToString());
                            objlinks.LINK_SIGNUPID = 22;
                            string content = loadhtml(objlinks);
                            //if (content != string.Empty)
                            //{
                            tabcontent = "<div class=\"tab-pane fade active in\" id=\"tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + content + "</div>";
                            //}
                            //else
                            //{
                            //    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</div>";
                            //}

                            hid_tabadmin.Value = "22" + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();

                        }

                        else
                        {
                            innerhtml = innerhtml + "<li class=\"admintabs\"><a class=\"admintabsa\" id=\"aadmin_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttabadmin('22_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                            tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
                        }
                    }
                    else
                    {
                        remainingtabs = remainingtabs + "<li class=\"\"><a id=\"aadmin_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttabadmin('22_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                        remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
                        if (hid_remainingtabsadmin.Value != "")
                        {
                            hid_remainingtabsadmin.Value = hid_remainingtabsadmin.Value + "," + dt_tabs.Rows[i]["TAB_ID"].ToString();
                        }
                        else
                        {
                            hid_remainingtabsadmin.Value = dt_tabs.Rows[i]["TAB_ID"].ToString();
                        }
                    }

                }
                remainingtabs = "<li class=\"dropdown\" style=\"width:10px !important;\"  title=\"Click Here more Tabs\"><a href=\"#\" data-toggle=\"dropdown\" class=\"alltabs\"><i class=\"fa fa-ellipsis-v\"></i></a><ul id='div_remainingtabsadmin' class=\"dropdown-menu dropdown__menu1\" role=\"menu\">" + remainingtabs + "</ul></li>";

                //u_admintabs.InnerHtml = innerhtml + remainingtabs;
                //div_admintabs.InnerHtml = tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>";
            }
        }
        public void loadtabs(string lbrid,string imageurl,string name)
        {
            LBR_SIGNUP obj = new LBR_SIGNUP();
            obj.LBR_ID = Int32.Parse(lbrid);
            //  DataSet dt_set = BLL.get_tabs(obj);
            DataTable dt_atab = BLL.get_tabs(obj);
            DataTable dt_tabs = BLL.ExecuteQuery("exec USP_LBR_SIGNUP @OPERATION='LOADANONYMOUSTAB', @LBR_ID='" + obj.LBR_ID + "'");
            dt_tabs.Merge(dt_atab);
            string innerhtml = "";
            string tabcontent = "";
            string remainingtabs = "";
            string remainintabscontent = "";
            int sharedcount = 0;
            int anonyshared = 0;
            //int k = 0;
            if (dt_tabs.Rows.Count > 0)
            {
                if(imageurl!=null && imageurl!="")
                { 
                innerhtml = " <li class='nav-header'><div class='dropdown profile-element'><span><img  id='img_user' alt='profile pic' class='img-circle' src='"+ imageurl +"' /></span><a href='#'><span class='clear'><span class='block m-t-xs'><strong  id='lbl_name' class='font-bold'>"+ name +"</strong></span></span></a></div></li><li class='nav-header' style='padding-top: 0px !important;padding-bottom: 0px;padding-left: 0px;'><div class='dropdown profile-element' style='padding:13px 25px'><a data-toggle='modal' data-target='#mynewtab'><span class='clear'>Create a new tab</span></a></div></li>";
                }
                else
                {
                    innerhtml = " <li class='nav-header'><div class='dropdown profile-element'><span><img  id='img_user' alt='profile pic' class='img-circle' src='Images/Dummy.jpg' /></span><a href='#'><span class='clear'><span class='block m-t-xs'><strong  id='lbl_name' class='font-bold'>"+ name +"</strong></span></span></a></div></li><li class='nav-header' style='padding-top: 0px !important;padding-bottom: 0px;padding-left: 0px;'><div class='dropdown profile-element' style='padding:13px 25px'><a data-toggle='modal' data-target='#mynewtab'><span class='clear'>Create a new tab</span></a></div></li>";
                }
                    for (int i = 0; i < dt_tabs.Rows.Count; i++)
                {
                    sharedcount = 0;
                    anonyshared = 0;
                    sharedcount = Int32.Parse(dt_tabs.Rows[i]["sharedcount"].ToString());



                    if (sharedcount == 0)
                    {
                        if(i==0)
                        {
                            innerhtml = innerhtml + "<li class='pull-left leftwidth active'><a title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\" id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href='#' onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" ><span class='nav-label block'>" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</span></a><ul></ul></li><li class='pull-left '> <a class='rightpadding'><span class='block m-t-xs'><b class='caret'></b></span><ul class='dropdown-menu animated fadeInRight m-t-xs'><li><a onclick=\"Sharetotaltableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Share</a></li><li><a  onclick=\"opentabnameleft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Rename</a></li><li><a onclick=\"deletetableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Delete</a></li></ul></a></li>";
                        }
                        else
                        { 
                        // innerhtml = "<li class=\"active\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                        innerhtml = innerhtml + "<li class='pull-left leftwidth'><a title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\" id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href='#' onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" ><span class='nav-label block'>" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</span></a><ul></ul></li><li class='pull-left '> <a class='rightpadding'><span class='block m-t-xs'><b class='caret'></b></span><ul class='dropdown-menu animated fadeInRight m-t-xs'><li><a onclick=\"Sharetotaltableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Share</a></li><li><a  onclick=\"opentabnameleft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Rename</a></li><li><a onclick=\"deletetableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Delete</a></li></ul></a></li>";
                        }
                        }
                    else
                    {
                        if(i==0)
                        {
                            innerhtml = innerhtml + "<li class='pull-left leftwidth active'><a title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\" id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href='#'  onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" ><span class='nav-label block'>" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "<span class='label label-warning pull-right' id=\"lbn_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\">" + sharedcount + "</span></span></a><ul></ul></li><li class='pull-left '> <a class='rightpadding'><span class='block m-t-xs'><b class='caret'></b></span><ul class='dropdown-menu animated fadeInRight m-t-xs'><li><a onclick=\"Sharetotaltableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Share</a></li><li><a onclick=\"opentabnameleft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Rename</a></li><li><a onclick=\"deletetableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Delete</a></li></ul></a></li>";
                        }
                        else
                        {
                            innerhtml = innerhtml + "<li class='pull-left leftwidth'><a title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\" id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href='#'  onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" ><span class='nav-label block'>" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "<span class='label label-warning pull-right' id=\"lbn_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\">" + sharedcount + "</span></span></a><ul></ul></li><li class='pull-left '> <a class='rightpadding'><span class='block m-t-xs'><b class='caret'></b></span><ul class='dropdown-menu animated fadeInRight m-t-xs'><li><a onclick=\"Sharetotaltableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Share</a></li><li><a onclick=\"opentabnameleft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Rename</a></li><li><a onclick=\"deletetableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Delete</a></li></ul></a></li>";
                        }
                        
                    }
                   
                    if(i==0)
                    { 
                    LBR_LINKS objlinks = new LBR_LINKS();
                    objlinks.LINK_TABID = Int32.Parse(dt_tabs.Rows[i]["TAB_ID"].ToString());
                    objlinks.LINK_SIGNUPID = Int32.Parse(lbrid);
                    DataTable dt_links = BLL.get_links(objlinks);
                    string content = System.Text.RegularExpressions.Regex.Replace(Loadlinks(dt_links), @"\s+", " ");
                    //if (content != string.Empty)
                    //{
                    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + content + "</div>";
                    //}
                    //else
                    //{
                    //    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</div>";
                    //}

                    hid_tab.Value = lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
                    div_tabcontent.InnerHtml = tabcontent;
                    }

                }
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "loadmenus("++")", true);
                div_menus.InnerHtml = innerhtml;

            }







        }
        public static string loadtabsstatic(string lbrid,string tabid)
        {
            LBR_SIGNUP obj = new LBR_SIGNUP();
            obj.LBR_ID = Int32.Parse(lbrid);
            //  DataSet dt_set = BLL.get_tabs(obj);
            DataTable dt_user = BLL.ExecuteQuery("select lbr_imageurl,lbr_firstname,lbr_lastname from lbr_signup where lbr_id="+ lbrid +"");
            string imageurl = dt_user.Rows[0]["LBR_imageurl"].ToString();
              string name = dt_user.Rows[0]["LBR_FIRSTNAME"].ToString();

                            if (dt_user.Rows[0]["LBR_LASTNAME"].ToString() != null && dt_user.Rows[0]["LBR_LASTNAME"].ToString()!="")
                            {
                                name = name + " " + dt_user.Rows[0]["LBR_LASTNAME"].ToString();
                            }
            DataTable dt_atab = BLL.get_tabs(obj);
            DataTable dt_tabs = BLL.ExecuteQuery("exec USP_LBR_SIGNUP @OPERATION='LOADANONYMOUSTAB', @LBR_ID='" + obj.LBR_ID + "'");
            dt_tabs.Merge(dt_atab);
            string innerhtml = "";
            string tabcontent = "";
            string remainingtabs = "";
            string remainintabscontent = "";
            int sharedcount = 0;
            int anonyshared = 0;
            //int k = 0;
            if (dt_tabs.Rows.Count > 0)
            {
                if (imageurl != null && imageurl != "")
                {
                    innerhtml = " <li class='nav-header'><div class='dropdown profile-element'><span><img  id='img_user' alt='profile pic' class='img-circle' src='" + imageurl + "' /></span><a href='#'><span class='clear'><span class='block m-t-xs'><strong  id='lbl_name' class='font-bold'>" + name + "</strong></span></span></a></div></li><li class='nav-header' style='padding-top: 0px !important;padding-bottom: 0px;padding-left: 0px;'><div class='dropdown profile-element' style='padding:13px 25px'><a data-toggle='modal' data-target='#mynewtab'><span class='clear'>Create a new tab</span></a></div></li>";
                }
                else
                {
                    innerhtml = " <li class='nav-header'><div class='dropdown profile-element'><span><img  id='img_user' alt='profile pic' class='img-circle' src='Images/Dummy.jpg' /></span><a href='#'><span class='clear'><span class='block m-t-xs'><strong  id='lbl_name' class='font-bold'>" + name + "</strong></span></span></a></div></li><li class='nav-header' style='padding-top: 0px !important;padding-bottom: 0px;padding-left: 0px;'><div class='dropdown profile-element' style='padding:13px 25px'><a data-toggle='modal' data-target='#mynewtab'><span class='clear'>Create a new tab</span></a></div></li>";
                }
                for (int i = 0; i < dt_tabs.Rows.Count; i++)
                {
                    sharedcount = 0;
                    anonyshared = 0;
                    sharedcount = Int32.Parse(dt_tabs.Rows[i]["sharedcount"].ToString());



                    if (sharedcount == 0)
                    {
                        if (dt_tabs.Rows[i]["TAB_ID"].ToString() == tabid)
                        {
                            innerhtml = innerhtml + "<li class='pull-left leftwidth active'><a title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\" id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href='#' onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" ><span class='nav-label block'>" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</span></a><ul></ul></li><li class='pull-left '> <a class='rightpadding'><span class='block m-t-xs'><b class='caret'></b></span><ul class='dropdown-menu animated fadeInRight m-t-xs'><li><a onclick=\"Sharetotaltableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Share</a></li><li><a  onclick=\"opentabnameleft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Rename</a></li><li><a onclick=\"deletetableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Delete</a></li></ul></a></li>";
                        }
                        else
                        {
                            // innerhtml = "<li class=\"active\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                            innerhtml = innerhtml + "<li class='pull-left leftwidth'><a title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\" id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href='#' onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" ><span class='nav-label block'>" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</span></a><ul></ul></li><li class='pull-left '> <a class='rightpadding'><span class='block m-t-xs'><b class='caret'></b></span><ul class='dropdown-menu animated fadeInRight m-t-xs'><li><a onclick=\"Sharetotaltableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Share</a></li><li><a  onclick=\"opentabnameleft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Rename</a></li><li><a onclick=\"deletetableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Delete</a></li></ul></a></li>";
                        }
                    }
                    else
                    {
                        if (dt_tabs.Rows[i]["TAB_ID"].ToString() == tabid)
                        {
                            innerhtml = innerhtml + "<li class='pull-left leftwidth active'><a title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\" id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href='#'  onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" ><span class='nav-label block'>" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "<span class='label label-warning pull-right' id=\"lbn_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\">" + sharedcount + "</span></span></a><ul></ul></li><li class='pull-left '> <a class='rightpadding'><span class='block m-t-xs'><b class='caret'></b></span><ul class='dropdown-menu animated fadeInRight m-t-xs'><li><a onclick=\"Sharetotaltableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Share</a></li><li><a onclick=\"opentabnameleft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Rename</a></li><li><a onclick=\"deletetableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Delete</a></li></ul></a></li>";
                        }
                        else
                        {
                            innerhtml = innerhtml + "<li class='pull-left leftwidth'><a title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\" id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href='#'  onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" ><span class='nav-label block'>" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "<span class='label label-warning pull-right' id=\"lbn_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\">" + sharedcount + "</span></span></a><ul></ul></li><li class='pull-left '> <a class='rightpadding'><span class='block m-t-xs'><b class='caret'></b></span><ul class='dropdown-menu animated fadeInRight m-t-xs'><li><a onclick=\"Sharetotaltableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Share</a></li><li><a onclick=\"opentabnameleft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\">Rename</a></li><li><a onclick=\"deletetableft('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "');\" >Delete</a></li></ul></a></li>";
                        }

                    }

                 

                }
               

            }


            return innerhtml;




        }
     
       
        protected void lnk_logout_Click(object sender, EventArgs e)
        {
            try
            {


                Session["USERINFO"] = null;
                Session.Clear();
                Session.Abandon();

                if (Response.Cookies["LBRUserName"] != null && Response.Cookies["LBRPassword"] != null)
                {
                    Response.Cookies["LBRUserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["LBRPassword"].Expires = DateTime.Now.AddDays(-1);
                }
                Response.Redirect("frm_login.aspx", false);
                //Server.TransferRequest("frm_login.aspx", false);
            }
            catch (Exception ex)
            {
                // Server.TransferRequest("frm_login.aspx",false);
                Response.Redirect("frm_login.aspx", false);
            }
        }

        public static LinkDetails getlinkdetailsnew(LinkDetails linkDetails)
        {
            try
            {
                //bool sta = false;
                //http://htmlagilitypack.codeplex.com/
                string url = linkDetails.Url;
                linkDetails.Url = url.Replace(" ", "").Trim();
                //     linkDetails = GetHeadersnew(linkDetails.Url, linkDetails);
                // if (linkDetails.MimeType!="Don't Download")
                //  {
                //if (linkDetails.MimeType.ToLower().Contains("text/html"))
                //{
                linkDetails.LinkType = 1;
                HtmlDocument htmlDocument = new HtmlDocument();
                System.Net.WebClient webClient = new System.Net.WebClient();
                // ServicePointManager.DefaultConnectionLimit = 20000;
                webClient.Proxy = null;
                WebRequest.DefaultWebProxy = null;
                webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:30.0) Gecko/20100101 Firefox/30.0");


                string download = webClient.DownloadString(linkDetails.Url);



                htmlDocument.LoadHtml(download);
                HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//html//head");
                if (htmlNode == null)
                {
                    htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//head");
                }
                if (htmlNode == null)
                {
                    htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//html");
                }


                if (htmlNode != null)
                {
                    linkDetails.status = true;
                    // sta = true;
                    linkDetails = GetStandardInfonew(linkDetails, htmlNode);

                    linkDetails = GetOpenGraphInfonew(linkDetails, htmlNode);
                    if (linkDetails.Image != null)
                    {
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("" + linkDetails.Image.Url + "");
                        request.Proxy = null;
                        request.Method = "HEAD";
                        bool exists;
                        try
                        {
                            request.GetResponse();
                            exists = true;
                        }
                        catch
                        {
                            exists = false;
                        }
                        if (exists == false)
                        {
                            linkDetails.Image = null;
                        }
                    }

                    if (linkDetails.Image == null)
                    {
                        linkDetails = GuessImagenew(htmlDocument, linkDetails);
                    }
                }

                else
                {
                    linkDetails = GetHeadersnew(linkDetails.Url, linkDetails);
                    if (linkDetails.MimeType.ToLower().Contains("image/"))
                    {
                        linkDetails.LinkType = 2;
                        linkDetails.Image = new ImageLink(linkDetails.Url);
                    }
                    else
                    {
                        linkDetails.status = false;
                        // sta = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //     linkDetails = GetHeadersnew(linkDetails.Url, linkDetails);
                //sta = false;
            }
            //if (linkDetails.MimeType.ToLower().Contains("image/"))
            //{
            //    linkDetails.LinkType = 2;
            //    linkDetails.Image = new ImageLink(linkDetails.Url);
            //}
            //  }
            //  }
            return linkDetails;
        }
        public LinkDetails getlinkdetails(LinkDetails linkDetails)
        {

            //http://htmlagilitypack.codeplex.com/
            string url = linkDetails.Url;
            linkDetails.Url = url;
            linkDetails = GetHeaders(linkDetails.Url, linkDetails);

            if (linkDetails.MimeType.ToLower().Contains("text/html"))
            {
                linkDetails.LinkType = 1;
                HtmlDocument htmlDocument = new HtmlDocument();
                System.Net.WebClient webClient = new System.Net.WebClient();
                string download = webClient.DownloadString(linkDetails.Url);

                htmlDocument.LoadHtml(download);
                HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("html/head");

                linkDetails = GetStandardInfo(linkDetails, htmlNode);

                linkDetails = GetOpenGraphInfo(linkDetails, htmlNode);

                if (linkDetails.Image == null)
                {
                    linkDetails = GuessImage(htmlDocument, linkDetails);
                }
            }

            if (linkDetails.MimeType.ToLower().Contains("image/"))
            {
                linkDetails.LinkType = 2;
                linkDetails.Image = new ImageLink(linkDetails.Url);
            }
            return linkDetails;
        }
        private void GetMetaTagValues(string url)
        {
            //Get Meta Tags
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);

            var metaTags = document.DocumentNode.SelectNodes("//meta");

            if (metaTags != null)
            {
                foreach (var tag in metaTags)
                {
                    if (tag.Attributes["name"] != null && tag.Attributes["content"] != null && tag.Attributes["name"].Value == "description")
                    {
                        string title = tag.Attributes["content"].Value;
                    }
                }
            }
        }
        private static string FullyQualifiedImage(string imageUrl, string siteUrl)
        {
            if (imageUrl.Contains("http:") || imageUrl.Contains("https:"))
            {
                return imageUrl;
            }

            if (imageUrl.IndexOf("//") == 0)
            {
                return "http:" + imageUrl;
            }
            try
            {
                string baseurl = siteUrl.Replace("http://", string.Empty).Replace("https://", string.Empty);
                baseurl = baseurl.Split('/')[0];
                return string.Format("http://{0}{1}", baseurl, imageUrl);

            }
            catch { }

            return imageUrl;

        }
        public class ImageLink
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public string Url { get; set; }

            public ImageLink()
            {
            }

            public ImageLink(string url, string siteUrl)
            {
                SetImageLink(FullyQualifiedImage(url, siteUrl));
            }

            public ImageLink(string url)
            {
                SetImageLink(url);
            }

            private void SetImageLink(string url)
            {
                this.Url = url;
                try
                {
                    System.Net.WebClient webClient = new System.Net.WebClient();
                    byte[] imageData = webClient.DownloadData(url);
                    MemoryStream stream = new MemoryStream(imageData);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                    stream.Close();
                    this.Width = img.Width;
                    this.Height = img.Height;
                }
                catch
                {

                }
            }

        }
        public class LinkDetails
        {
            public LinkDetails()
            {
                Images = new List<ImageLink>();
            }
            public string Title { get; set; }
            public string Url { get; set; }
            public string Type { get; set; }
            public ImageLink Image { get; set; }
            public List<ImageLink> Images { get; set; }
            public string SiteName { get; set; }
            public string Description { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string FaxNumber { get; set; }
            public Int64 ContentLength { get; set; }
            public string MimeType { get; set; }
            public int LinkType { get; set; }
            public bool status { get; set; }
            // 0=bad, 1=html, 2=image

        }
        public LinkDetails GetHeaders(string link, LinkDetails linkDetails)
        {
            try
            {

                System.Net.WebClient wc = new System.Net.WebClient();
                wc.OpenRead(link);
                linkDetails.ContentLength = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
                linkDetails.MimeType = wc.ResponseHeaders["Content-Type"];
            }
            catch
            {
                linkDetails.MimeType = "Don't Download";
            }
            return linkDetails;
        }
        public static LinkDetails GetHeadersnew(string link, LinkDetails linkDetails)
        {
            try
            {

                System.Net.WebClient wc = new System.Net.WebClient();
                wc.Headers.Add("user-agent", "Only a test!");

                wc.OpenRead(link);
                linkDetails.ContentLength = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
                linkDetails.MimeType = wc.ResponseHeaders["Content-Type"];
            }
            catch
            {
                linkDetails.MimeType = "Don't Download";
            }
            return linkDetails;
        }
        //score the image based on matches in comparing alt to title and h1 tag
        private int ScoreImage(string text, string compare)
        {
            text = text.Replace("\r\n", string.Empty).Replace("\t", string.Empty);
            compare = compare.Replace("\r\n", string.Empty).Replace("\t", string.Empty);
            int score = 0;
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(compare))
            {
                string[] c = compare.Split(' ');

                foreach (string test in c)
                {
                    if (text.Contains(test)) { score++; }
                }
            }
            return score;
        }
        private static int ScoreImagenew(string text, string compare)
        {
            text = text.Replace("\r\n", string.Empty).Replace("\t", string.Empty);
            compare = compare.Replace("\r\n", string.Empty).Replace("\t", string.Empty);
            int score = 0;
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(compare))
            {
                string[] c = compare.Split(' ');

                foreach (string test in c)
                {
                    if (text.Contains(test)) { score++; }
                }
            }
            return score;
        }
        private LinkDetails GuessImage(HtmlDocument htmlDocument, LinkDetails linkDetails)
        {
            LinkDetails detail = linkDetails;
            HtmlNodeCollection imageNodes = htmlDocument.DocumentNode.SelectNodes("//img");
            string h1 = string.Empty;
            HtmlNode h1Node = htmlDocument.DocumentNode.SelectSingleNode("//h1");
            if (h1Node != null)
            {
                h1 = h1Node.InnerText;
            }
            int bestScore = -1;
            if (imageNodes != null)
            {
                foreach (HtmlNode imageNode in imageNodes)
                {
                    if (imageNode != null && imageNode.Attributes["src"] != null && imageNode.Attributes["alt"] != null)
                    {
                        ImageLink imageLink = new ImageLink(imageNode.Attributes["src"].Value, detail.Url);
                        if (!(imageLink.Width > 0 && imageLink.Width < 50)) //if we don't have a width go with it but if we know width is less than 50 don't use it
                        {
                            int myScore = ScoreImage(imageNode.Attributes["alt"].Value, linkDetails.Title);
                            myScore += ScoreImage(imageNode.Attributes["alt"].Value, h1);

                            if (myScore > bestScore)
                            {
                                detail.Image = imageLink;
                                bestScore = myScore;
                            }

                            if (!detail.Images.Contains(imageLink)) { detail.Images.Add(imageLink); }
                        }
                    }
                }
            }

            return detail;
        }
        private static LinkDetails GuessImagenew(HtmlDocument htmlDocument, LinkDetails linkDetails)
        {
            LinkDetails detail = linkDetails;
            HtmlNodeCollection imageNodes = htmlDocument.DocumentNode.SelectNodes("//img");
            string h1 = string.Empty;
            HtmlNode h1Node = htmlDocument.DocumentNode.SelectSingleNode("//h1");
            if (h1Node != null)
            {
                h1 = h1Node.InnerText;
            }
            int bestScore = -1;
            if (imageNodes != null)
            {
                foreach (HtmlNode imageNode in imageNodes)
                {
                    //if (imageNode.Attributes["alt"]==null)
                    //{
                    //    imageNode.Attributes["alt"].Value = Path.GetFileName(@"" + imageNode.Attributes["src"].Value + "");
                    //}

                    if (imageNode != null && imageNode.Attributes["data-old-hires"] != null)
                    {
                        if (imageNode.Attributes["alt"] != null && imageNode.Attributes["alt"].Value != "")
                        {
                            Uri absolute = new Uri(linkDetails.Url);
                            Uri result = new Uri(absolute, imageNode.Attributes["data-old-hires"].Value);
                            ImageLink imageLink = new ImageLink(result.AbsoluteUri, detail.Url);
                            //ImageLink imageLink = new ImageLink(imageNode.Attributes["data-old-hires"].Value, detail.Url);
                            if (!(imageLink.Width > 0 && imageLink.Width < 50)) //if we don't have a width go with it but if we know width is less than 50 don't use it
                            {
                                int myScore = ScoreImagenew(imageNode.Attributes["alt"].Value, linkDetails.Title);
                                myScore += ScoreImagenew(imageNode.Attributes["alt"].Value, h1);

                                if (myScore > bestScore)
                                {
                                    detail.Image = imageLink;
                                    bestScore = myScore;
                                }

                                if (!detail.Images.Contains(imageLink)) { detail.Images.Add(imageLink); }
                            }
                        }
                    }
                    if (detail.Images.Count == 0 || detail.Images.Count == null)
                    {
                        if (imageNode != null && imageNode.Attributes["src"] != null)
                        {
                            if (imageNode.Attributes["alt"] != null && imageNode.Attributes["alt"].Value != "")
                            {
                                Uri absolute = new Uri(linkDetails.Url);
                                Uri result = new Uri(absolute, imageNode.Attributes["src"].Value);
                                ImageLink imageLink = new ImageLink(result.AbsoluteUri, detail.Url);
                                //ImageLink imageLink = new ImageLink(imageNode.Attributes["src"].Value, detail.Url);
                                if (!(imageLink.Width > 0 && imageLink.Width < 50)) //if we don't have a width go with it but if we know width is less than 50 don't use it
                                {
                                    int myScore = ScoreImagenew(imageNode.Attributes["alt"].Value, linkDetails.Title);
                                    myScore += ScoreImagenew(imageNode.Attributes["alt"].Value, h1);

                                    if (myScore > bestScore)
                                    {
                                        detail.Image = imageLink;
                                        bestScore = myScore;
                                    }

                                    if (!detail.Images.Contains(imageLink)) { detail.Images.Add(imageLink); }
                                }
                            }

                        }
                    }
                }
                if (detail.Images.Count == 0 || detail.Images.Count == null)
                {
                    foreach (HtmlNode imageNode in imageNodes)
                    {
                        string value = "";
                        if (imageNode.Attributes["src"] != null)
                        {
                            value = Path.GetFileName(@"" + imageNode.Attributes["src"].Value + "");
                        }
                        else if (imageNode.Attributes["ng-src"] != null)
                        {
                            value = Path.GetFileName(@"" + imageNode.Attributes["ng-src"].Value + "");
                        }
                        if (value != "")
                        {
                            Uri absolute = new Uri(linkDetails.Url);
                            if (imageNode.Attributes["src"] != null)
                            {
                                Uri result = new Uri(absolute, imageNode.Attributes["src"].Value);
                                ImageLink imageLink = new ImageLink(result.AbsoluteUri, detail.Url);

                                if (!(imageLink.Width > 0 && imageLink.Width < 50)) //if we don't have a width go with it but if we know width is less than 50 don't use it
                                {
                                    int myScore = ScoreImagenew(value, linkDetails.Title);
                                    myScore += ScoreImagenew(value, h1);

                                    if (myScore > bestScore)
                                    {
                                        detail.Image = imageLink;
                                        bestScore = myScore;
                                    }
                                    // Uri ur = new System.Uri(linkDetails.Url,  ("~/mypage.aspx")).AbsoluteUri;
                                    if (!detail.Images.Contains(imageLink)) { detail.Images.Add(imageLink); }
                                }
                            }
                            else if (imageNode.Attributes["ng-src"] != null)
                            {
                                Uri result = new Uri(absolute, imageNode.Attributes["ng-src"].Value);
                                ImageLink imageLink = new ImageLink(result.AbsoluteUri, detail.Url);

                                if (!(imageLink.Width > 0 && imageLink.Width < 50)) //if we don't have a width go with it but if we know width is less than 50 don't use it
                                {
                                    int myScore = ScoreImagenew(value, linkDetails.Title);
                                    myScore += ScoreImagenew(value, h1);

                                    if (myScore > bestScore)
                                    {
                                        detail.Image = imageLink;
                                        bestScore = myScore;
                                    }
                                    // Uri ur = new System.Uri(linkDetails.Url,  ("~/mypage.aspx")).AbsoluteUri;
                                    if (!detail.Images.Contains(imageLink)) { detail.Images.Add(imageLink); }
                                }
                            }

                        }

                    }
                }
            }

            return detail;
        }
        private LinkDetails GetOpenGraphInfo(LinkDetails linkDetails, HtmlNode head)
        {
            foreach (HtmlNode headNode in head.ChildNodes)
            {
                switch (headNode.Name.ToLower())
                {
                    case "link": break;

                    case "meta":
                        if (headNode.Attributes["property"] != null && headNode.Attributes["content"] != null)
                        {
                            switch (headNode.Attributes["property"].Value.ToLower())
                            {
                                case "og:title":
                                    linkDetails.Title = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                                case "og:type":
                                    linkDetails.Type = headNode.Attributes["content"].Value;
                                    break;
                                case "og:url":
                                    linkDetails.Url = headNode.Attributes["content"].Value;
                                    break;
                                case "og:image":
                                    linkDetails.Image = new ImageLink(headNode.Attributes["content"].Value, linkDetails.Url);
                                    break;
                                case "og:site_name":
                                    linkDetails.SiteName = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                                case "og:description":
                                    linkDetails.Description = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                                case "og:email":
                                    linkDetails.Email = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                                case "og:phone_number":
                                    linkDetails.PhoneNumber = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                                case "og:fax_number":
                                    linkDetails.FaxNumber = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;

                            }
                        }
                        break;
                }

            }
            return linkDetails;
        }
        private static LinkDetails GetOpenGraphInfonew(LinkDetails linkDetails, HtmlNode head)
        {
            foreach (HtmlNode headNode in head.ChildNodes)
            {
                switch (headNode.Name.ToLower())
                {
                    case "link": break;

                    case "meta":
                        if (headNode.Attributes["property"] != null && headNode.Attributes["content"] != null)
                        {
                            switch (headNode.Attributes["property"].Value.ToLower())
                            {
                                case "og:title":
                                    linkDetails.Title = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                                case "og:type":
                                    linkDetails.Type = headNode.Attributes["content"].Value;
                                    break;
                                case "og:url":
                                    linkDetails.Url = headNode.Attributes["content"].Value;
                                    break;
                                case "og:image":
                                    linkDetails.Image = new ImageLink(headNode.Attributes["content"].Value, linkDetails.Url);
                                    break;
                                case "og:site_name":
                                    linkDetails.SiteName = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                                case "og:description":
                                    linkDetails.Description = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                                case "og:email":
                                    linkDetails.Email = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                                case "og:phone_number":
                                    linkDetails.PhoneNumber = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                                case "og:fax_number":
                                    linkDetails.FaxNumber = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;

                            }
                        }
                        break;
                }
                if (linkDetails.Image != null && linkDetails.Title != null && linkDetails.Description != null)
                {
                    break;
                }

            }
            return linkDetails;
        }
        //get info off of basic meta tags
        private LinkDetails GetStandardInfo(LinkDetails linkDetails, HtmlNode head)
        {
            foreach (HtmlNode headNode in head.ChildNodes)
            {
                switch (headNode.Name.ToLower())
                {
                    case "link": break;
                    case "title":
                        linkDetails.Title = HttpUtility.HtmlDecode(headNode.InnerText);
                        break;
                    case "meta":
                        if (headNode.Attributes["name"] != null && headNode.Attributes["content"] != null)
                        {
                            switch (headNode.Attributes["name"].Value.ToLower())
                            {
                                case "description":
                                    linkDetails.Description = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                            }
                        }
                        break;
                }

            }
            // look for apple touch icon in header
            HtmlNode imageNode = head.SelectSingleNode("link[@rel='apple-touch-icon']");
            if (imageNode != null)
            {
                if (imageNode.Attributes["href"] != null) { linkDetails.Image = new ImageLink(imageNode.Attributes["href"].Value, linkDetails.Url); }
                if (imageNode.Attributes["src"] != null) { linkDetails.Image = new ImageLink(imageNode.Attributes["src"].Value, linkDetails.Url); }
            }
            //look for link image in header
            imageNode = head.SelectSingleNode("link[@rel='image_src']");
            if (imageNode != null)
            {
                if (imageNode.Attributes["href"] != null) { linkDetails.Image = new ImageLink(imageNode.Attributes["href"].Value, linkDetails.Url); }
                if (imageNode.Attributes["src"] != null) { linkDetails.Image = new ImageLink(imageNode.Attributes["src"].Value, linkDetails.Url); }
            }

            return linkDetails;
        }
        private static LinkDetails GetStandardInfonew(LinkDetails linkDetails, HtmlNode head)
        {
            foreach (HtmlNode headNode in head.ChildNodes)
            {
                switch (headNode.Name.ToLower())
                {
                    case "link": break;
                    case "title":
                        linkDetails.Title = HttpUtility.HtmlDecode(headNode.InnerText);
                        break;
                    case "meta":
                        if (headNode.Attributes["name"] != null && headNode.Attributes["content"] != null)
                        {
                            switch (headNode.Attributes["name"].Value.ToLower())
                            {
                                case "description":
                                    linkDetails.Description = HttpUtility.HtmlDecode(headNode.Attributes["content"].Value);
                                    break;
                            }
                        }
                        break;
                }
                if (linkDetails.Title != null && linkDetails.Description != null)
                {
                    break;
                }
            }
            // look for apple touch icon in header
            HtmlNode imageNode = head.SelectSingleNode("link[@rel='apple-touch-icon']");
            if (imageNode != null)
            {
                if (imageNode.Attributes["href"] != null) { linkDetails.Image = new ImageLink(imageNode.Attributes["href"].Value, linkDetails.Url); }
                if (imageNode.Attributes["src"] != null) { linkDetails.Image = new ImageLink(imageNode.Attributes["src"].Value, linkDetails.Url); }
            }
            //look for link image in header
            imageNode = head.SelectSingleNode("link[@rel='image_src']");
            if (imageNode != null)
            {
                if (imageNode.Attributes["href"] != null) { linkDetails.Image = new ImageLink(imageNode.Attributes["href"].Value, linkDetails.Url); }
                if (imageNode.Attributes["src"] != null) { linkDetails.Image = new ImageLink(imageNode.Attributes["src"].Value, linkDetails.Url); }
            }

            return linkDetails;
        }
        public static bool isValidURL(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse;

            try
            {
                webRequest.Headers.Add("user-agent", "Only a test!");
                webResponse = webRequest.GetResponse();
            }
            catch //If exception thrown then couldn't get response from address
            {
                return false;
            }
            return true;
        }

        public static string loadhtml(LBR_LINKS obj)
        {
            string html = "";
            try
            {
                DataTable dt = BLL.get_links(obj);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["link_type"].ToString() == "1")
                    {
                        Match regexMatch = Regex.Match(dt.Rows[i]["link_path"].ToString(), @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)",
                           RegexOptions.IgnoreCase);
                        Match vimeoMatch = Regex.Match(dt.Rows[i]["link_path"].ToString(), @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)",
                            RegexOptions.IgnoreCase);


                        if (regexMatch.Success || vimeoMatch.Success)
                        {
                            string value = "";
                            if (regexMatch.Success)
                            {
                                value = regexMatch.Groups[1].Value;

                               // htmlcontent = htmlcontent + "!@#" + value + "!@#" + obj.LINK_TITLE.Replace("{", "").Replace("}", "") + "!@#" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "");
                                // htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a></span><iframe class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"http://www.youtube.com/embed/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";

                            }
                            else if (vimeoMatch.Success)
                            {
                                value = vimeoMatch.Groups[1].Value;
                                //htmlcontent = "2";
                               // htmlcontent = htmlcontent + "!@#" + value + "!@#" + obj.LINK_TITLE.Replace("{", "").Replace("}", "") + "!@#" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "");
                                //   htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a></span><iframe class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"//player.vimeo.com/video/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";

                            }
                            //htmlcontent = htmlcontent + "!@#" + obj.LINK_PATH + "!@#" + obj.LINK_IMAGEPATH;

                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return html;
        }
        //protected void btn_save_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txt_url.Text != string.Empty)
        //        {

        //            if (!txt_url.Text.Trim().Contains("http://") && !txt_url.Text.Trim().Contains("https://"))
        //            {
        //                txt_url.Text = "http://" + txt_url.Text;
        //            }

        //            LinkDetails link = new LinkDetails();
        //            link.Url = txt_url.Text;
        //            link = getlinkdetails(link);
        //            //  if (isValidURL(link.Url))
        //            {
        //                LBR_LINKS obj = new LBR_LINKS();
        //                if (link.Description != null)
        //                {
        //                    obj.LINK_DESCRIPTION = BLL.ReplaceQuote(link.Description.ToString().Replace("{", "").Replace("}", ""));
        //                }
        //                if (link.Image != null)
        //                {
        //                    obj.LINK_IMAGEPATH = link.Image.Url.ToString();
        //                }
        //                if (dt_user.Rows.Count > 0)
        //                {
        //                    obj.LINK_MODIFIEDBY = Int32.Parse(dt_user.Rows[0]["lbr_id"].ToString());
        //                    obj.CREATEDBY = Int32.Parse(dt_user.Rows[0]["lbr_id"].ToString());
        //                }
        //                obj.LINK_PATH = link.Url;
        //                obj.LINK_STATUS = 1;
        //                if (link.Title != null)
        //                {
        //                    obj.LINK_TITLE = BLL.ReplaceQuote(link.Title.ToString()).Replace("{", "").Replace("}", "");
        //                }
        //                obj.LINK_TYPE = 1;
        //                string[] values = hid_tab.Value.Split('_');

        //                if (values.Length == 2)
        //                {
        //                    obj.LINK_TABID = Int32.Parse(values[1]);
        //                    obj.LINK_SIGNUPID = Int32.Parse(values[0]);

        //                    DataTable dt = BLL.set_link(obj);

        //                    string id = dt.Rows[0]["link_id"].ToString();
        //                    if (dt.Rows.Count > 0)
        //                    {

        //                        string htmlcontent = "";

        //                        string url = "";
        //                        Match regexMatch = Regex.Match(obj.LINK_PATH, @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)",
        //                RegexOptions.IgnoreCase);
        //                        Match vimeoMatch = Regex.Match(obj.LINK_PATH, @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)",
        //                            RegexOptions.IgnoreCase);


        //                        if (regexMatch.Success || vimeoMatch.Success)
        //                        {
        //                            string value = "";
        //                            if (regexMatch.Success)
        //                            {
        //                                value = regexMatch.Groups[1].Value;
        //                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a></span><iframe width=\"190\" height=\"132\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"http://www.youtube.com/embed/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";

        //                            }
        //                            else if (vimeoMatch.Success)
        //                            {
        //                                value = vimeoMatch.Groups[1].Value;
        //                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a></span><iframe width=\"190\" height=\"132\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"//player.vimeo.com/video/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";

        //                            }


        //                        }



        //                        else
        //                        {
        //                            if (obj.LINK_IMAGEPATH != "" && obj.LINK_IMAGEPATH != null)
        //                            {

        //                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"background-image:url('" + obj.LINK_IMAGEPATH + "');cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a></span>" + dt.Rows[0]["link_title"].ToString().Replace("{", "").Replace("}", "") + "</div>";

        //                            }
        //                            else
        //                            {
        //                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a></span>" + dt.Rows[0]["link_title"].ToString().Replace("{", "").Replace("}", "") + "</div>";
        //                            }


        //                        }
        //                        if (htmlcontent != "")
        //                        {
        //                            bool status = BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
        //                            txt_url.Text = string.Empty;
        //                            hid_html.Value = htmlcontent;
        //                            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "saveurl();return false;", true);


        //                        }
        //                    }



        //                }
        //            }
        //            //else
        //            //{
        //            //    BLL.ShowMessage(this, "Please enter a valid URL");
        //            //    txt_url.Focus();
        //            //}
        //        }
        //        else
        //        {
        //            BLL.ShowMessage(this, "Please enter URL");
        //            txt_url.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        [WebMethod]
        public static string buttonsaveclick(List<string> arraylinks)
        {
            string htmlcontent = "";
            DataTable dt = new DataTable();
            LinkDetails link = new LinkDetails();
            LBR_LINKS obj = new LBR_LINKS();
            string id = "";
            try
            {
                string linknew = arraylinks[0];

                if (linknew != string.Empty)
                {

                    if (!linknew.Trim().Contains("http://") && !linknew.Trim().Contains("https://"))
                    {
                        linknew = "http://" + linknew;
                    }


                    link.Url = linknew;
                    link = getlinkdetailsnew(link);
                    Match regx = Regex.Match(link.Url, @"http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?",
                      RegexOptions.IgnoreCase);
                    //Regex regx = new Regex("http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?", RegexOptions.IgnoreCase);
                    //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("" + link.Url + "");
                    //request.Proxy = null;
                    //request.Method = "HEAD";
                    //bool exists;
                    //try
                    //{
                    //    request.GetResponse();
                    //    exists = true;
                    //}
                    //catch
                    //{
                    //    exists = false;
                    //}

                    if (link.status != false)
                    {

                        if (link.Description != null)
                        {
                            obj.LINK_DESCRIPTION = (link.Description.ToString().Replace("{", "").Replace("}", "")).Trim().Replace("'", "");
                        }
                        if (link.Image != null)
                        {
                            obj.LINK_IMAGEPATH = link.Image.Url.ToString();
                        }
                        // if (dt_user.Rows.Count > 0)
                        {
                            obj.LINK_MODIFIEDBY = Int32.Parse(arraylinks[1]);
                            obj.CREATEDBY = Int32.Parse(arraylinks[1]);
                        }
                        obj.LINK_PATH = link.Url;
                        obj.LINK_STATUS = 1;
                        if (link.Title != null)
                        {
                            obj.LINK_TITLE = (link.Title.ToString()).Replace("{", "").Replace("}", "").Trim().Replace("'", "");
                        }
                        obj.LINK_TYPE = 1;
                        //string[] values = hid_tab.Value.Split('_');

                        if (arraylinks.Count == 3)
                        {
                            obj.LINK_TABID = Int32.Parse(arraylinks[2]);
                            obj.LINK_SIGNUPID = Int32.Parse(arraylinks[1]);

                            if (link.Title == null || link.Title == "")
                            {
                                obj.LINK_TITLE = link.Url.Replace("{", "").Replace("}", "").Trim().Replace("'", "");
                            }
                            if (link.Url.Contains("fb.com") || link.Url.Contains("facebook.com"))
                            {
                                obj.LINK_TITLE = "Facebook<br/>" + link.Url.Replace("{", "").Replace("}", "") + "";
                                obj.LINK_IMAGEPATH = "../images/fb_icon_325x325.png";
                            }

                            //  dt = BLL.set_link(obj);

                            //  id = dt.Rows[0]["link_id"].ToString();
                            // if (dt.Rows.Count > 0)
                            {

                                obj.LINK_PATH = linknew;

                                string url = "";
                                Match regexMatch = Regex.Match(obj.LINK_PATH, @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)",
                        RegexOptions.IgnoreCase);
                                Match vimeoMatch = Regex.Match(obj.LINK_PATH, @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)",
                                    RegexOptions.IgnoreCase);


                                if (regexMatch.Success || vimeoMatch.Success)
                                {
                                    string value = "";
                                    if (regexMatch.Success)
                                    {
                                        value = regexMatch.Groups[1].Value;
                                        htmlcontent = "1";
                                        htmlcontent = htmlcontent + "!@#" + value + "!@#" + obj.LINK_TITLE.Replace("{", "").Replace("}", "") + "!@#" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "");
                                        // htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a></span><iframe class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"http://www.youtube.com/embed/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";

                                    }
                                    else if (vimeoMatch.Success)
                                    {
                                        value = vimeoMatch.Groups[1].Value;
                                        htmlcontent = "2";
                                        htmlcontent = htmlcontent + "!@#" + value + "!@#" + obj.LINK_TITLE.Replace("{", "").Replace("}", "") + "!@#" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "");
                                        //   htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a></span><iframe class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"//player.vimeo.com/video/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";

                                    }
                                    htmlcontent = htmlcontent + "!@#" + obj.LINK_PATH + "!@#" + obj.LINK_IMAGEPATH;

                                }



                                else
                                {
                                    htmlcontent = "3";
                                    if (obj.LINK_IMAGEPATH != "" && obj.LINK_IMAGEPATH != null)
                                    {

                                        if (obj.LINK_TITLE != null && obj.LINK_TITLE != "")
                                        {
                                            htmlcontent = htmlcontent + "!@#" + obj.LINK_IMAGEPATH + "!@#" + obj.LINK_TITLE.Replace("{", "").Replace("}", "");
                                            if (obj.LINK_DESCRIPTION != null && obj.LINK_DESCRIPTION != "")
                                            {
                                                htmlcontent = htmlcontent + "!@#" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "");
                                            }
                                            else
                                            {
                                                htmlcontent = htmlcontent + "!@#" + "0";
                                            }
                                        }
                                        else
                                        {
                                            htmlcontent = htmlcontent + "!@#" + obj.LINK_IMAGEPATH + "!@#" + "0";
                                            if (obj.LINK_DESCRIPTION != null && obj.LINK_DESCRIPTION != "")
                                            {
                                                htmlcontent = htmlcontent + "!@#" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "");
                                            }
                                            else
                                            {
                                                htmlcontent = htmlcontent + "!@#" + "0";
                                            }
                                        }
                                        // htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><div class=\"boximage\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" style=\"background-image:url('" + obj.LINK_IMAGEPATH + "');\"><span class=\"delete\"><a href=\"#\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a></span></div><div class=\"boxtext\" ><a href='#' onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\">" + dt.Rows[0]["link_title"].ToString() + "</a></div></div>";
                                        ////  htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a></span>" + dt.Rows[0]["link_title"].ToString() + "</div>";

                                    }
                                    else
                                    {

                                        if (obj.LINK_TITLE != null && obj.LINK_TITLE != "")
                                        {
                                            htmlcontent = htmlcontent + "!@#" + "0" + "!@#" + obj.LINK_TITLE.Replace("{", "").Replace("}", "");
                                            if (obj.LINK_DESCRIPTION != null && obj.LINK_DESCRIPTION != "")
                                            {
                                                htmlcontent = htmlcontent + "!@#" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "");
                                            }
                                            else
                                            {
                                                htmlcontent = htmlcontent + "!@#" + "0";
                                            }
                                        }
                                        else
                                        {
                                            htmlcontent = htmlcontent + "!@#" + "0" + "!@#" + "0";
                                            if (obj.LINK_DESCRIPTION != null && obj.LINK_DESCRIPTION != "")
                                            {
                                                htmlcontent = htmlcontent + "!@#" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "");
                                            }
                                            else
                                            {
                                                htmlcontent = htmlcontent + "!@#" + "0";
                                            }
                                        }
                                        // htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a></span>" + new Uri("" + link.Url + "").GetLeftPart(UriPartial.Path) + "<br/>" + dt.Rows[0]["link_title"].ToString() + "</div>";
                                    }
                                    Uri myUri = new Uri(obj.LINK_PATH);
                                    string host = myUri.Host;
                                    // HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                                    htmlcontent = htmlcontent + "!@#" + obj.LINK_PATH + "!@#" + host;

                                }
                                if (htmlcontent != "")
                                {

                                    //bool status = BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                                    //txt_url.Text = string.Empty;
                                    //hid_html.Value = htmlcontent;
                                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "saveurl();return false;", true);


                                }
                            }



                        }

                        //}
                    }
                    else
                    {
                        //BLL.ShowMessage(this, "Please enter a valid URL");
                        //txt_url.Focus();
                        htmlcontent = "invalid";
                    }
                }
                else
                {
                    //BLL.ShowMessage(this, "Please enter URL");
                    //txt_url.Focus();
                }
            }
            catch (Exception ex)
            {

            }
            return htmlcontent;
        }
        [WebMethod]
        public static string buttonsaveNewclick(List<string> array)
        {
            string htmlcontent = "";
            DataTable dt = new DataTable();
            LinkDetails link = new LinkDetails();
            LBR_LINKS obj = new LBR_LINKS();
            string id = "";
            try
            {

                if (array.Count == 4)
                {
                    string[] arraylinks = array[3].Split(new string[] { "!@#" }, StringSplitOptions.None);
                    if (arraylinks.Length == 6)
                    {
                        obj.LINK_CREATEDDBY = Int32.Parse(array[1]);
                        obj.LINK_MODIFIEDBY = Int32.Parse(array[1]);
                        obj.LINK_DESCRIPTION = arraylinks[3].Replace("{", "").Replace("}", "").Trim();
                        if (arraylinks[0] == "3")
                        {
                            obj.LINK_IMAGEPATH = arraylinks[1];
                        }
                        else
                        {
                            obj.LINK_IMAGEPATH = arraylinks[5];
                        }
                        obj.LINK_PATH = arraylinks[4];
                        obj.LINK_SIGNUPID = Int32.Parse(array[1]);
                        obj.LINK_STATUS = 1;
                        obj.LINK_TABID = Int32.Parse(array[2]);
                        obj.LINK_SIGNUPID = Int32.Parse(array[1]);
                        obj.LINK_TYPE = 1;
                        obj.LINK_TITLE = arraylinks[2].Replace("{", "").Replace("}", "").Trim();

                        dt = BLL.set_link(obj);

                        if (dt.Rows.Count > 0)
                        {


                         htmlcontent=LoadlinksStatic(dt);


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return htmlcontent;
        }

        [WebMethod]
        public static string delete(string id)
        {
            bool status = false;
            try
            {
                string result = Regex.Replace(id, @"[^\d]", "");
                status = BLL.ExecuteNonQuery("delete from lbr_links where link_id='" + result + "'");
            }
            catch (Exception ex)
            {

            }
            return status.ToString();
        }
        [WebMethod]
        public static string gettabcontent(string value)
        {
            string html = "";
            LBR_LINKS obj = new LBR_LINKS();
            DataTable dt_con = new DataTable();
            try
            {
                string[] arraylinks = value.Split('_');
                if (arraylinks.Length == 2)
                {
                    obj.LINK_TABID = Int32.Parse(arraylinks[1]);
                    obj.LINK_SIGNUPID = Int32.Parse(arraylinks[0]);
                    string newcontent = "";
           
                    {
                        DataTable dt_links = BLL.get_links(obj);
                        newcontent = System.Text.RegularExpressions.Regex.Replace(LoadlinksStatic(dt_links), @"\s+", " ");

                        //  newcontent = newcontent + "@!~&*" + "tab" + obj.LINK_TABID + "default" + "@!~&*" + "invalid" + "@!~&*" + "invalid";
                        // totalcontent = totalcontent + "@!~&*" + innerhtml + remainingtabs + "@!~&*" + tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>" + "@!~&*" + remtabs;
                        newcontent = newcontent + "@!~&*" + value;
                    }
                    if (newcontent != "" || newcontent != null)
                    {
                        html = newcontent;
                    }
                    else
                    {
                        html = "" + "@!~&*" + value;
                    }
                }
                bool st = BLL.ExecuteNonQuery("update lbr_links set link_sharedbystatus=1 where link_tabid=" + obj.LINK_TABID + "");
            }
            catch (Exception ex)
            {

            }
            return html;

        }
        [WebMethod]
        public static string gettabcontentadmin(string value)
        {
            string html = "";
            LBR_LINKS obj = new LBR_LINKS();
            DataTable dt_con = new DataTable();
            try
            {
                string[] arraylinks = value.Split('_');
                if (arraylinks.Length == 3)
                {
                    obj.LINK_TABID = Int32.Parse(arraylinks[1]);
                    obj.LINK_SIGNUPID = Int32.Parse(arraylinks[0]);
                    string newcontent = "";
                    //if (Array.IndexOf(arraylinks[2].Split(','), "" + obj.LINK_TABID + "") >= 0)
                    //{
                    //    //newcontent = activetabadmin(obj.LINK_TABID);


                    //}
                    //else
                    //{
                    newcontent = loadhtml(obj);
                    newcontent = newcontent + "@!~&*" + "tabadmin" + obj.LINK_TABID + "default" + "@!~&*" + "invalid" + "@!~&*" + "invalid";
                    // totalcontent = totalcontent + "@!~&*" + innerhtml + remainingtabs + "@!~&*" + tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>" + "@!~&*" + remtabs;

                    //}
                    if (newcontent != "" || newcontent != null)
                    {
                        html = newcontent;
                    }
                    else
                    {
                        html = "";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return html;

        }

        //public static string activetab(int tab_id)
        //{
        //    string totalcontent = "";
        //    try
        //    {

        //        LBR_SIGNUP obj = new LBR_SIGNUP();

        //        obj.LBR_ID = Int32.Parse(dt_user.Rows[0]["LBR_ID"].ToString());

        //        //     DataTable dt_check = BLL.ExecuteQuery("EXEC USP_LBR_SIGNUP  @OPERATION='Checktab',@LBR_ID='" + obj.LBR_ID + "',@tab_id='" + tab_id + "'");
        //        DataTable dt_tabs = new DataTable();
        //        //if (dt_check.Rows.Count == 0)
        //        //{
        //        dt_tabs = BLL.ExecuteQuery("EXEC USP_LBR_SIGNUP  @OPERATION='ACTIVETAB',@LBR_ID='" + obj.LBR_ID + "',@tab_id='" + tab_id + "'");
        //        DataTable dt_nexttabs = BLL.ExecuteQuery("EXEC USP_LBR_SIGNUP  @OPERATION='EXCEPTACTIVETAB',@LBR_ID='" + obj.LBR_ID + "',@tab_id='" + tab_id + "'");
        //        if (dt_nexttabs.Rows.Count > 0 && dt_tabs.Rows.Count > 0)
        //        {
        //            dt_tabs.Merge(dt_nexttabs);
        //        }
        //        //}
        //        //else
        //        //{
        //        //    dt_tabs = BLL.get_tabs(obj);
        //        //}

        //        string innerhtml = "";
        //        string tabcontent = "";
        //        string remainingtabs = "";
        //        string remainintabscontent = "";
        //        //int k = 0;
        //        string remtabs = "";
        //        if (dt_tabs.Rows.Count > 0)
        //        {
        //            if (dt_tabs.Rows.Count > 5)
        //            {
        //                for (int i = 0; i < dt_tabs.Rows.Count; i++)
        //                {
        //                    if (i <= 4)
        //                    {
        //                        if (i == 0)
        //                        {
        //                            innerhtml = "<li class=\"active\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";

        //                            LBR_LINKS objlinks = new LBR_LINKS();
        //                            objlinks.LINK_TABID = Int32.Parse(dt_tabs.Rows[i]["TAB_ID"].ToString());
        //                            objlinks.LINK_SIGNUPID = Int32.Parse(dt_user.Rows[0]["lbr_id"].ToString());
        //                            string content = loadhtml(objlinks);
        //                            //if (content != string.Empty)
        //                            //{
        //                            tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + content + "</div>";
        //                            //}
        //                            //else
        //                            //{
        //                            //    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</div>";
        //                            //}

        //                            //    hid_tab.Value = dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                            totalcontent = dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                        }

        //                        else
        //                        {
        //                            if ((i + 1) == dt_tabs.Rows.Count)
        //                            {
        //                                if (dt_atab.Rows.Count > 0)
        //                                {
        //                                    innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "\">" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "</a></li>";
        //                                    tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\"></div>";
        //                                }
        //                            }
        //                            else
        //                            {
        //                                innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
        //                                tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (i == 5)
        //                        {
        //                            if (dt_atab.Rows.Count > 0)
        //                            {
        //                                innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "\">" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "</a></li>";
        //                                tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\"></div>";
        //                            }

        //                        }
        //                        remainingtabs = remainingtabs + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
        //                        remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
        //                        if (remtabs != "")
        //                        {
        //                            remtabs = remtabs + "," + dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                        }
        //                        else
        //                        {
        //                            remtabs = dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                        }
        //                    }

        //                }
        //                remainingtabs = "<li class=\"dropdown\"  title=\"Click Here more Tabs\"><a href=\"#\" data-toggle=\"dropdown\" class=\"smalltab\"><i class=\"fa fa-chevron-down\"></i></a><ul id='div_remainingtabs' class=\"dropdown-menu dropdown__menu\" role=\"menu\">" + remainingtabs + "<li class=\"\"> <a href=\"#\" class=\"btn btn-primary\" data-toggle=\"modal\" data-target=\"#mynewtab\">Create new tab</a></li></ul></li>";
        //                remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tabnewdefault\">Newtab</div>";
        //                totalcontent = totalcontent + "@!~&*" + innerhtml + remainingtabs + "@!~&*" + tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>" + "@!~&*" + remtabs;

        //            }
        //            else
        //            {
        //                dt_tabs.Merge(dt_atab);
        //                for (int i = 0; i < dt_tabs.Rows.Count; i++)
        //                {
        //                    if (i <= 5)
        //                    {
        //                        if (i == 0)
        //                        {
        //                            innerhtml = "<li class=\"active\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";

        //                            LBR_LINKS objlinks = new LBR_LINKS();
        //                            objlinks.LINK_TABID = Int32.Parse(dt_tabs.Rows[i]["TAB_ID"].ToString());
        //                            objlinks.LINK_SIGNUPID = Int32.Parse(dt_user.Rows[0]["lbr_id"].ToString());
        //                            string content = loadhtml(objlinks);
        //                            //if (content != string.Empty)
        //                            //{
        //                            tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + content + "</div>";
        //                            //}
        //                            //else
        //                            //{
        //                            //    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</div>";
        //                            //}

        //                            //    hid_tab.Value = dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                            totalcontent = dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                        }

        //                        else
        //                        {

        //                            innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
        //                            tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";

        //                        }
        //                    }
        //                    else
        //                    {

        //                        remainingtabs = remainingtabs + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
        //                        remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
        //                        if (remtabs != "")
        //                        {
        //                            remtabs = remtabs + "," + dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                        }
        //                        else
        //                        {
        //                            remtabs = dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                        }
        //                    }

        //                }
        //                remainingtabs = "<li class=\"dropdown\"  title=\"Click Here more Tabs\"><a href=\"#\" data-toggle=\"dropdown\" class=\"smalltab\"><i class=\"fa fa-chevron-down\"></i></a><ul id='div_remainingtabs' class=\"dropdown-menu dropdown__menu\" role=\"menu\">" + remainingtabs + "<li class=\"\"> <a href=\"#\" class=\"btn btn-primary\" data-toggle=\"modal\" data-target=\"#mynewtab\">Create new tab</a></li></ul></li>";
        //                remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tabnewdefault\">Newtab</div>";
        //                totalcontent = totalcontent + "@!~&*" + innerhtml + remainingtabs + "@!~&*" + tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>" + "@!~&*" + remtabs;

        //            }
        //            //tabs.InnerHtml = innerhtml + remainingtabs;
        //            // div_tabcontent.InnerHtml = tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>";
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return totalcontent;
        //}
        //public static string activetabadmin(int tab_id)
        //{
        //    string totalcontent = "";
        //    try
        //    {

        //        LBR_SIGNUP obj = new LBR_SIGNUP();
        //        obj.LBR_ID = 22;

        //        //     DataTable dt_check = BLL.ExecuteQuery("EXEC USP_LBR_SIGNUP  @OPERATION='Checktab',@LBR_ID='" + obj.LBR_ID + "',@tab_id='" + tab_id + "'");
        //        DataTable dt_tabs = new DataTable();
        //        //if (dt_check.Rows.Count == 0)
        //        //{
        //        dt_tabs = BLL.ExecuteQuery("EXEC USP_LBR_SIGNUP  @OPERATION='ACTIVETAB',@LBR_ID='" + obj.LBR_ID + "',@tab_id='" + tab_id + "'");
        //        DataTable dt_nexttabs = BLL.ExecuteQuery("EXEC USP_LBR_SIGNUP  @OPERATION='EXCEPTACTIVETAB',@LBR_ID='" + obj.LBR_ID + "',@tab_id='" + tab_id + "'");
        //        if (dt_nexttabs.Rows.Count > 0 && dt_tabs.Rows.Count > 0)
        //        {
        //            dt_tabs.Merge(dt_nexttabs);
        //        }
        //        //}
        //        //else
        //        //{
        //        //    dt_tabs = BLL.get_tabs(obj);
        //        //}

        //        string innerhtml = "";
        //        string tabcontent = "";
        //        string remainingtabs = "";
        //        string remainintabscontent = "";
        //        //int k = 0;
        //        string remtabs = "";
        //        if (dt_tabs.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt_tabs.Rows.Count; i++)
        //            {
        //                if (i <= 2)
        //                {
        //                    if (i == 0)
        //                    {
        //                        innerhtml = "<li class=\"admintabs\" class=\"active\"><a class=\"admintabsa\" id=\"aadmin_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttabadmin('22_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";

        //                        LBR_LINKS objlinks = new LBR_LINKS();
        //                        objlinks.LINK_TABID = Int32.Parse(dt_tabs.Rows[i]["TAB_ID"].ToString());
        //                        objlinks.LINK_SIGNUPID = 22;
        //                        string content = loadhtml(objlinks);
        //                        //if (content != string.Empty)
        //                        //{
        //                        tabcontent = "<div class=\"tab-pane fade active in\" id=\"tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + content + "</div>";
        //                        //}
        //                        //else
        //                        //{
        //                        //    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</div>";
        //                        //}

        //                        //    hid_tab.Value = dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                        totalcontent = "22" + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                    }

        //                    else
        //                    {
        //                        innerhtml = innerhtml + "<li class=\"admintabs\"><a class=\"admintabsa\" id=\"aadmin_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttabadmin('22_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
        //                        tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
        //                    }
        //                }
        //                else
        //                {
        //                    remainingtabs = remainingtabs + "<li class=\"\"><a id=\"aadmin_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttabadmin('22_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
        //                    remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tabadmin" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";

        //                    if (remtabs != "")
        //                    {
        //                        remtabs = remtabs + "," + dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                    }
        //                    else
        //                    {
        //                        remtabs = dt_tabs.Rows[i]["TAB_ID"].ToString();
        //                    }
        //                }

        //            }
        //            remainingtabs = "<li class=\"dropdown\" style=\"width:10px !important;\"  title=\"Click Here more Tabs\"><a href=\"#\" data-toggle=\"dropdown\" class=\"alltabs\"><i class=\"fa fa-ellipsis-v\"></i></a><ul id='div_remainingtabsadmin' class=\"dropdown-menu dropdown__menu1\" role=\"menu\">" + remainingtabs + "</ul></li>";

        //            // remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tabnewdefault\">Newtab</div>";
        //            totalcontent = totalcontent + "@!~&*" + innerhtml + remainingtabs + "@!~&*" + tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>" + "@!~&*" + remtabs;

        //            //tabs.InnerHtml = innerhtml + remainingtabs;
        //            // div_tabcontent.InnerHtml = tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>";
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return totalcontent;
        //}
        public static bool Url(string p_strValue)
        {
            if (Uri.IsWellFormedUriString(p_strValue, UriKind.RelativeOrAbsolute))
            {
                Uri l_strUri = new Uri(p_strValue);
                return (l_strUri.Scheme == Uri.UriSchemeHttp || l_strUri.Scheme == Uri.UriSchemeHttps);
            }
            else
            {
                return false;
            }
        }
        public static string refreshloadtabs(string lbrid)
        {
            string totalcontent = "";
            try
            {

                LBR_SIGNUP obj = new LBR_SIGNUP();
                obj.LBR_ID = Int32.Parse(lbrid.ToString());
                DataTable dt_tabs = BLL.get_tabs(obj);
                DataTable dt_atab = BLL.ExecuteQuery("exec USP_LBR_SIGNUP @OPERATION='LOADANONYMOUSTAB', @LBR_ID='" + obj.LBR_ID + "'");
                string innerhtml = "";
                string tabcontent = "";
                string remainingtabs = "";
                string remainintabscontent = "";
                //int k = 0;
                string remtabs = "";
                if (dt_tabs.Rows.Count > 0)
                {
                    if (dt_tabs.Rows.Count > 5)
                    {
                        for (int i = 0; i < dt_tabs.Rows.Count; i++)
                        {
                            if (i <= 4)
                            {
                                if (i == 0)
                                {
                                    innerhtml = "<li class=\"active\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid.ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";

                                    LBR_LINKS objlinks = new LBR_LINKS();
                                    objlinks.LINK_TABID = Int32.Parse(dt_tabs.Rows[i]["TAB_ID"].ToString());
                                    objlinks.LINK_SIGNUPID = Int32.Parse(lbrid.ToString());
                                    // string content = loadhtml(objlinks);
                                    //if (content != string.Empty)
                                    //{
                                    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
                                    //}
                                    //else
                                    //{
                                    //    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</div>";
                                    //}

                                    //    hid_tab.Value = dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
                                    totalcontent = lbrid.ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
                                }

                                else
                                {
                                    if ((i + 1) == dt_tabs.Rows.Count)
                                    {
                                        if (dt_atab.Rows.Count > 0)
                                        {
                                            innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "\">" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "</a></li>";
                                            tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\"></div>";
                                        }
                                    }
                                    else
                                    {
                                        innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                                        tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
                                    }
                                }
                            }
                            else
                            {

                                if (i == 5)
                                {
                                    if (dt_atab.Rows.Count > 0)
                                    {
                                        innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "\">" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "</a></li>";
                                        tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\"></div>";
                                    }

                                }
                                remainingtabs = remainingtabs + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                                remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
                                if (remtabs != "")
                                {
                                    remtabs = remtabs + "," + dt_tabs.Rows[i]["TAB_ID"].ToString();
                                }
                                else
                                {
                                    remtabs = dt_tabs.Rows[i]["TAB_ID"].ToString();
                                }
                            }

                        }
                        remainingtabs = "<li class=\"dropdown\"  title=\"Click Here more Tabs\"><a href=\"#\" data-toggle=\"dropdown\" class=\"smalltab\"><i class=\"fa fa-chevron-down\"></i></a><ul id='div_remainingtabs' class=\"dropdown-menu dropdown__menu\" role=\"menu\">" + remainingtabs + "<li class=\"\"> <a href=\"#\" class=\"btn btn-primary\" data-toggle=\"modal\" data-target=\"#mynewtab\">Create new tab</a></li></ul></li>";
                        remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tabnewdefault\">Newtab</div>";
                        totalcontent = remtabs + "!@#" + totalcontent + "!@#" + innerhtml + remainingtabs + "!@#" + tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>";
                        //tabs.InnerHtml = innerhtml + remainingtabs;
                        // div_tabcontent.InnerHtml = tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>";
                    }
                    else
                    {
                        dt_tabs.Merge(dt_atab);
                        for (int i = 0; i < dt_tabs.Rows.Count; i++)
                        {
                            if (i <= 5)
                            {
                                if (i == 0)
                                {
                                    innerhtml = "<li class=\"active\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";

                                    LBR_LINKS objlinks = new LBR_LINKS();
                                    objlinks.LINK_TABID = Int32.Parse(dt_tabs.Rows[i]["TAB_ID"].ToString());
                                    objlinks.LINK_SIGNUPID = Int32.Parse(lbrid);
                                    // string content = loadhtml(objlinks);
                                    //if (content != string.Empty)
                                    //{
                                    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
                                    //}
                                    //else
                                    //{
                                    //    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</div>";
                                    //}

                                    //    hid_tab.Value = dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
                                    totalcontent = lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
                                }

                                else
                                {

                                    innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                                    tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";

                                }
                            }
                            else
                            {


                                remainingtabs = remainingtabs + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                                remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
                                if (remtabs != "")
                                {
                                    remtabs = remtabs + "," + dt_tabs.Rows[i]["TAB_ID"].ToString();
                                }
                                else
                                {
                                    remtabs = dt_tabs.Rows[i]["TAB_ID"].ToString();
                                }
                            }

                        }
                        remainingtabs = "<li class=\"dropdown\"  title=\"Click Here more Tabs\"><a href=\"#\" data-toggle=\"dropdown\" class=\"smalltab\"><i class=\"fa fa-chevron-down\"></i></a><ul id='div_remainingtabs' class=\"dropdown-menu dropdown__menu\" role=\"menu\">" + remainingtabs + "<li class=\"\"> <a href=\"#\" class=\"btn btn-primary\" data-toggle=\"modal\" data-target=\"#mynewtab\">Create new tab</a></li></ul></li>";
                        remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tabnewdefault\">Newtab</div>";
                        totalcontent = remtabs + "!@#" + totalcontent + "!@#" + innerhtml + remainingtabs + "!@#" + tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>";

                    }
                }


            }
            catch (Exception ex)
            {

            }
            return totalcontent;
        }
        public static string refreshloadtabsnew(string lbrid)
        {
            string totalcontent = "";
            try
            {

                LBR_SIGNUP obj = new LBR_SIGNUP();
                obj.LBR_ID = Int32.Parse(lbrid);
                DataTable dt_tabs = BLL.get_tabs(obj);
                DataTable dt_atab = BLL.ExecuteQuery("exec USP_LBR_SIGNUP @OPERATION='LOADANONYMOUSTAB', @LBR_ID='" + obj.LBR_ID + "'");
                string innerhtml = "";
                string tabcontent = "";
                string remainingtabs = "";
                string remainintabscontent = "";
                //int k = 0;

                if (dt_tabs.Rows.Count > 0)
                {
                    if (dt_tabs.Rows.Count > 5)
                    {
                        for (int i = 0; i < dt_tabs.Rows.Count; i++)
                        {
                            if (i <= 4)
                            {
                                if (i == 0)
                                {
                                    innerhtml = "<li class=\"active\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";

                                    LBR_LINKS objlinks = new LBR_LINKS();
                                    objlinks.LINK_TABID = Int32.Parse(dt_tabs.Rows[i]["TAB_ID"].ToString());
                                    objlinks.LINK_SIGNUPID = Int32.Parse(lbrid);
                                    // string content = loadhtml(objlinks);
                                    //if (content != string.Empty)
                                    //{
                                    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + loadhtml(objlinks) + "</div>";
                                    //}
                                    //else
                                    //{
                                    //    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</div>";
                                    //}

                                    //    hid_tab.Value = dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
                                    totalcontent = lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
                                }

                                else
                                {
                                    if ((i + 1) == dt_tabs.Rows.Count)
                                    {
                                        if (dt_atab.Rows.Count > 0)
                                        {
                                            innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "\">" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "</a></li>";
                                            tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\"></div>";
                                        }
                                    }
                                    else
                                    {
                                        innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                                        tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
                                    }
                                }
                            }
                            else
                            {
                                if (i == 5)
                                {
                                    if (dt_atab.Rows.Count > 0)
                                    {
                                        innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_atab.Rows[0]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "\">" + dt_atab.Rows[0]["TAB_NAME"].ToString() + "</a></li>";
                                        tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_atab.Rows[0]["TAB_ID"].ToString() + "default\"></div>";
                                    }

                                }
                                remainingtabs = remainingtabs + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                                remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
                            }

                        }
                        remainingtabs = "<li class=\"dropdown\"  title=\"Click Here more Tabs\"><a href=\"#\" data-toggle=\"dropdown\" class=\"smalltab\"><i class=\"fa fa-chevron-down\"></i></a><ul id='div_remainingtabs' class=\"dropdown-menu dropdown__menu\" role=\"menu\">" + remainingtabs + "<li class=\"\"> <a href=\"#\" class=\"btn btn-primary\" data-toggle=\"modal\" data-target=\"#mynewtab\">Create new tab</a></li></ul></li>";
                        remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tabnewdefault\">Newtab</div>";
                        totalcontent = totalcontent + "!@#" + innerhtml + remainingtabs + "!@#" + tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>";

                    }
                    else
                    {
                        dt_tabs.Merge(dt_atab);
                        for (int i = 0; i < dt_tabs.Rows.Count; i++)
                        {
                            if (i <= 5)
                            {
                                if (i == 0)
                                {
                                    innerhtml = "<li class=\"active\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";

                                    LBR_LINKS objlinks = new LBR_LINKS();
                                    objlinks.LINK_TABID = Int32.Parse(dt_tabs.Rows[i]["TAB_ID"].ToString());
                                    objlinks.LINK_SIGNUPID = Int32.Parse(lbrid);
                                    // string content = loadhtml(objlinks);
                                    //if (content != string.Empty)
                                    //{
                                    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + loadhtml(objlinks) + "</div>";
                                    //}
                                    //else
                                    //{
                                    //    tabcontent = "<div class=\"tab-pane fade active in\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</div>";
                                    //}

                                    //    hid_tab.Value = dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
                                    totalcontent = lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString();
                                }

                                else
                                {
                                    innerhtml = innerhtml + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\"  title=\"" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                                    tabcontent = tabcontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";

                                }
                            }
                            else
                            {

                                remainingtabs = remainingtabs + "<li class=\"\"><a id=\"a_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "\" href=\"#tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + lbrid + "_" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\">" + dt_tabs.Rows[i]["TAB_NAME"].ToString() + "</a></li>";
                                remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[i]["TAB_ID"].ToString() + "default\"></div>";
                            }

                        }
                        remainingtabs = "<li class=\"dropdown\"  title=\"Click Here more Tabs\"><a href=\"#\" data-toggle=\"dropdown\" class=\"smalltab\"><i class=\"fa fa-chevron-down\"></i></a><ul id='div_remainingtabs' class=\"dropdown-menu dropdown__menu\" role=\"menu\">" + remainingtabs + "<li class=\"\"> <a href=\"#\" class=\"btn btn-primary\" data-toggle=\"modal\" data-target=\"#mynewtab\">Create new tab</a></li></ul></li>";
                        remainintabscontent = remainintabscontent + "<div class=\"tab-pane fade\" id=\"tabnewdefault\">Newtab</div>";
                        totalcontent = totalcontent + "!@#" + innerhtml + remainingtabs + "!@#" + tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>";

                    }
                    //tabs.InnerHtml = innerhtml + remainingtabs;
                    // div_tabcontent.InnerHtml = tabcontent + remainintabscontent + "<div class=\"clearfix\"></div>";
                }


            }
            catch (Exception ex)
            {

            }
            return totalcontent;
        }


        [WebMethod]
        public static string buttonsavetab(string name)
        {
            string[] arraylinks = name.Split('@');
            string content = "";
            try
            {
                DataTable dt_tabs = new DataTable();
                // if (dt_user.Rows.Count > 0)
                {
                    DataTable dt_count = BLL.ExecuteQuery("select * from lbr_tabs where tab_name='" + BLL.ReplaceQuote(arraylinks[1].Replace("@", "")) + "' and tab_signup_id='" + arraylinks[0].ToString() + "' and tab_status=1");
                    if (dt_count.Rows.Count == 0)
                    {
                        dt_tabs = BLL.ExecuteQuery("EXEC USP_LBR_SIGNUP @OPERATION='SAVETAB',@tab_signup_id='" + arraylinks[0].ToString() + "',@tab_name='" + arraylinks[1] + "',@tab_status=1,@tab_createdby='" + arraylinks[0] + "',@tab_modifiedby='" + arraylinks[0] + "'");
                        if (dt_tabs.Rows.Count > 0)
                        {
                            content = loadtabsstatic(arraylinks[0].ToString(), dt_tabs.Rows[0]["TAB_ID"].ToString()) + "!@#" + arraylinks[0].ToString() + "_" + dt_tabs.Rows[0]["TAB_ID"].ToString() + "!@#" + arraylinks[0].ToString();
                            //content = "<li class=\"\"><a href=\"#tab" + dt_tabs.Rows[0]["TAB_ID"].ToString() + "default\" onclick=\"selecttab('" + dt_user.Rows[0]["lbr_id"].ToString() + "_" + dt_tabs.Rows[0]["TAB_ID"].ToString() + "')\" data-toggle=\"tab\">" + dt_tabs.Rows[0]["TAB_NAME"].ToString().Replace("@", "") + "</a></li>";
                            //content = content + "@" + "<div class=\"tab-pane fade\" id=\"tab" + dt_tabs.Rows[0]["TAB_ID"].ToString() + "default\"></div>";
                        }
                    }
                    else
                    {
                        content = "alreadyexists";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return content;

        }
        [WebMethod]
        public static string buttonchangename(string content)
        {
            bool status = false;
            try
            {
                string[] arraylinks = content.Split('@');
                if (arraylinks.Length == 2)
                {
                    status = BLL.ExecuteNonQuery("update lbr_tabs set tab_name='" + arraylinks[1] + "' where tab_id='" + arraylinks[0] + "'");
                }
            }
            catch (Exception ex)
            {

            }
            return status.ToString();
        }
        [WebMethod]
        public static string buttondeletetab(string content)
        {
            string[] arraylinks = content.Split('@');
            bool status = false;
            string stat = "";
            try
            {

                if (arraylinks.Length == 3)
                {
                    DataTable dt_tabs = BLL.ExecuteQuery("select * from lbr_tabs where tab_signup_id='" + arraylinks[2] + "'");
                    if (dt_tabs.Rows.Count > 0)
                    {
                        status = BLL.ExecuteNonQuery("update lbr_tabs set tab_status=0 where tab_id='" + arraylinks[0] + "'");
                        stat = "true";
                    }
                    else
                    {
                        stat = "invalid";
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return stat + "!@#" + refreshloadtabsnew(arraylinks[2]);
        }
        [WebMethod]
        public static List<string> GetCities(string cityName)
        {
            DataTable dt_emails = BLL.ExecuteQuery("select distinct *,lbr_firstname+' '+lbr_lastname+'-->'+lbr_emailid as total from lbr_signup(nolock) where lbr_logintype=1 and lbr_firstname LIKE '%" + cityName + "%' or lbr_lastname like'%" + cityName + "%' or lbr_emailid='%" + cityName + "%'");

            List<string> City = new List<string>();
            //string query = string.Format("SELECT DISTINCT City FROM Customers WHERE City LIKE '%{0}%'", cityName);
            //Note: you can configure Connection string in web.config also.

            for (int i = 0; i < dt_emails.Rows.Count; i++)
            {
                City.Add(dt_emails.Rows[i]["total"].ToString());
            }

            return City;
        }

        [WebMethod]
        public static string linkshare(List<string> array)
        {
            bool statusnew = false;
            string status = "";
            try
            {
                string[] emails = array[1].Split(',');

                for (int e = 0; e < emails.Length; e++)
                {
                    string email = emails[e].ToString();
                    if (email != "")
                    {
                        DataTable dt_user = BLL.ExecuteQuery("select * from lbr_signup where lbr_id=" + array[3] + "");
                        //  string[] splitString = email.Split(new string[] { "-->" }, StringSplitOptions.None);

                        DataTable dt_email = BLL.ExecuteQuery("select * from lbr_signup(nolock) inner join lbr_tabs(nolock) on lbr_id=tab_signup_id where lbr_emailid='" + email + "' and tab_name='Anonymous'");
                        if (dt_email.Rows.Count > 0)
                        {
                            DataTable dt_links = BLL.ExecuteQuery("select * from lbr_links(nolock) where link_id='" + array[0] + "'");
                            if (dt_links.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt_email.Rows.Count; i++)
                                {
                                    DataTable dt = BLL.ExecuteQuery("insert into lbr_links(LINK_TABID,LINK_SIGNUPID,LINK_PATH,LINK_TITLE,LINK_DESCRIPTION,LINK_TYPE,LINK_IMAGEPATH,LINK_DATA,LINK_STATUS,LINK_CREATEDBY,LINK_CREATEDDATE,LINK_MODIFIEDBY,LINK_MODIFIEDDATE,LINK_SHAREDBY,LINK_SHAREDBYSTATUS) select '" + dt_email.Rows[i]["tab_id"] + "','" + dt_email.Rows[i]["lbr_id"] + "','" + BLL.ReplaceQuote(dt_links.Rows[0]["link_path"].ToString()) + "','" + BLL.ReplaceQuote(dt_links.Rows[0]["LINK_TITLE"].ToString()) + "','" + BLL.ReplaceQuote(dt_links.Rows[0]["LINK_DESCRIPTION"].ToString()) + "','" + dt_links.Rows[0]["LINK_TYPE"] + "','" + BLL.ReplaceQuote(dt_links.Rows[0]["LINK_IMAGEPATH"].ToString()) + "','" + BLL.ReplaceQuote(dt_links.Rows[0]["LINK_DATA"].ToString()) + "','" + dt_links.Rows[0]["LINK_STATUS"] + "','" + array[2] + "',getdate(),'" + dt_email.Rows[i]["lbr_id"] + "',getdate()," + array[2] + ",0 select * from lbr_links where link_id=(select scope_identity())");
                                    if (dt.Rows.Count > 0)
                                    {
                                        statusnew = formatcopy(dt, dt_user.Rows[0]["lbr_emailid"].ToString(), array[3]);
                                    }
                                    else
                                    {
                                        statusnew = false;
                                    }
                                }
                                if (statusnew == true)
                                {
                                    status = "true";
                                }
                                else
                                {
                                    status = "false";
                                }
                            }
                            else
                            {
                                status = "false";
                            }
                        }
                        else
                        {
                            if (status == "")
                            {
                                status = status + "," + email + "";
                            }
                            else
                            {
                                status = status + "" + email + "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        [WebMethod]
        public static string linkshareemail(List<string> array)
        {
            string status = "";
            try
            {
                DataTable dt_user = BLL.ExecuteQuery("select * from lbr_signup where lbr_id=" + array[3] + "");
                string[] emails = array[1].Split(',');
                DataTable dt_links = BLL.ExecuteQuery("select * from lbr_links(nolock) where link_id='" + array[0] + "'");
                if (dt_links.Rows.Count > 0)
                {
                    string name = dt_user.Rows[0]["lbr_firstname"].ToString();
                    string html = "<div><p> Dear User,<br />" + name + " has shared you below Link/File from Linkskart </p><table align=\"center\">";

                    html = html + "<tr><td>" + dt_links.Rows[0]["link_path"] + "</td></tr>";

                    html = html + "</table><p>Please <a href=\"http://www.linkskart.com\">Click Here</a> to visit LINKSKART</p></div>";


                    for (int e = 0; e < emails.Length; e++)
                    {

                        MailMessage mailmessage = new MailMessage();
                        mailmessage.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient("linkskart.com");

                        client.Credentials = new System.Net.NetworkCredential("info@linkskart.com", ".santhu143");
                        mailmessage.From = new System.Net.Mail.MailAddress("info@linkskart.com");
                        // mailmessage.From = new MailAddress("santhosh@pragatipadh.com");
                        mailmessage.To.Add(emails[e]);
                        // mailmessage.CC.Add(emailid);
                        mailmessage.Subject = "Link/File from Linkskart-" + name + "";

                        mailmessage.Body = html;
                        client.EnableSsl = false;

                        try
                        {
                            client.Send(mailmessage);
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        [WebMethod]
        public static string tabshare(List<string> array)
        {
            bool statusnew = false;
            DataTable dt_user = BLL.ExecuteQuery("select * from lbr_signup where lbr_id=" + array[3] + "");
            string status = "";
            try
            {
                string[] emails = array[1].Split(',');

                for (int e = 0; e < emails.Length; e++)
                {

                    string email = emails[e].ToString();
                    if (email != "")
                    {
                        //  string[] splitString = email.Split(new string[] { "-->" }, StringSplitOptions.None);

                        DataTable dt_email = BLL.ExecuteQuery("select * from lbr_signup(nolock) where lbr_emailid='" + email + "'");
                        if (dt_email.Rows.Count > 0)
                        {
                            DataTable dt_tabs = BLL.ExecuteQuery("select * from lbr_tabs(nolock) where tab_id='" + array[0] + "'");
                            if (dt_tabs.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt_email.Rows.Count; i++)
                                {
                                    DataTable dt_itab = new DataTable();

                                    DataTable dt_checktab = BLL.ExecuteQuery("select * from lbr_tabs(nolock) where tab_name='" + dt_tabs.Rows[0]["tab_name"] + "' and tab_signup_id='" + dt_email.Rows[i]["lbr_id"].ToString() + "' and tab_status=1");

                                    string tabid = "";

                                    if (dt_checktab.Rows.Count == 0)
                                    {
                                        dt_itab = BLL.ExecuteQuery("insert into lbr_tabs(tab_signup_id,tab_name,tab_order,tab_status,tab_createdby,tab_createddate,tab_modifiedby,tab_modifieddate) select '" + dt_email.Rows[i]["lbr_id"] + "','" + BLL.ReplaceQuote(dt_tabs.Rows[0]["tab_name"].ToString()) + "','',1,'" + (array[2]) + "',getdate(),'" + dt_email.Rows[i]["lbr_id"] + "',getdate() select id=scope_identity()");
                                        tabid = dt_itab.Rows[0]["id"].ToString();
                                    }
                                    else
                                    {
                                        tabid = dt_checktab.Rows[0]["tab_id"].ToString();
                                    }



                                    if (tabid != "")
                                    {
                                        statusnew = true;
                                        DataTable dt_links = BLL.ExecuteQuery("select * from lbr_links where link_tabid='" + array[0] + "'");
                                        for (int k = 0; k < dt_links.Rows.Count; k++)
                                        {
                                            DataTable dt = BLL.ExecuteQuery("insert into lbr_links(LINK_TABID,LINK_SIGNUPID,LINK_PATH,LINK_TITLE,LINK_DESCRIPTION,LINK_TYPE,LINK_IMAGEPATH,LINK_STATUS,LINK_CREATEDBY,LINK_CREATEDDATE,LINK_MODIFIEDBY,LINK_MODIFIEDDATE,LINK_SHAREDBY,LINK_SHAREDBYSTATUS) select '" + tabid + "','" + dt_email.Rows[i]["lbr_id"] + "','" + BLL.ReplaceQuote(dt_links.Rows[k]["link_path"].ToString()) + "','" + BLL.ReplaceQuote(dt_links.Rows[k]["LINK_TITLE"].ToString()) + "','" + BLL.ReplaceQuote(dt_links.Rows[k]["LINK_DESCRIPTION"].ToString()) + "','" + dt_links.Rows[k]["LINK_TYPE"] + "','" + BLL.ReplaceQuote(dt_links.Rows[k]["LINK_IMAGEPATH"].ToString()) + "','" + dt_links.Rows[k]["LINK_STATUS"] + "','" + array[2] + "',getdate(),'" + dt_email.Rows[i]["lbr_id"] + "',getdate(),'" + array[2] + "',0 select * from lbr_links where link_id=(select scope_identity())");
                                            if (dt.Rows.Count > 0)
                                            {
                                                statusnew = formatcopy(dt, dt_user.Rows[0]["lbr_emailid"].ToString(), array[3]);
                                            }
                                            else
                                            {
                                                statusnew = false;
                                            }
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                                if (statusnew == true)
                                {
                                    status = "true";
                                }
                                else
                                {
                                    status = "false";
                                }
                            }
                            else
                            {
                                status = "false";
                            }
                        }
                        else
                        {
                            if (status == "")
                            {
                                status = status + "," + email + "";
                            }
                            else
                            {
                                status = status + "" + email + "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        [WebMethod]
        public static string tabshareemail(List<string> array)
        {


            DataTable dt_user = BLL.ExecuteQuery("select * from lbr_signup where lbr_id=" + array[3] + "");
            string status = "true";
            try
            {

                string[] emails = array[1].Split(',');
                DataTable dt_links = BLL.ExecuteQuery("select * from lbr_links where link_tabid=" + array[0] + " and link_status=1");
                if (dt_links.Rows.Count > 0)
                {
                    string name = dt_user.Rows[0]["lbr_firstname"].ToString();
                    string html = "<div><p> Dear User,<br />" + name + " has shared you below Links/Files from Linkskart </p><table align=\"center\">";
                    for (int i = 0; i < dt_links.Rows.Count; i++)
                    {
                        html = html + "<tr><td>" + i + 1 + "</td>&nbsp;&nbsp;<td>" + dt_links.Rows[i]["link_path"] + "</td></tr>";
                    }
                    html = html + "</table><p>Please <a href=\"http://www.linkskart.com\">Click Here</a> to visit LINKSKART</p></div>";


                    for (int e = 0; e < emails.Length; e++)
                    {

                        MailMessage mailmessage = new MailMessage();
                        mailmessage.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient("linkskart.com");

                        client.Credentials = new System.Net.NetworkCredential("info@linkskart.com", ".santhu143");
                        mailmessage.From = new System.Net.Mail.MailAddress("info@linkskart.com");
                        // mailmessage.From = new MailAddress("santhosh@pragatipadh.com");
                        mailmessage.To.Add(emails[e]);
                        // mailmessage.CC.Add(emailid);
                        mailmessage.Subject = "Links/Files from Linkskart-" + name + "";

                        mailmessage.Body = html;
                        client.EnableSsl = false;

                        try
                        {
                            client.Send(mailmessage);
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        [WebMethod]
        public static string passwordchange(List<string> array)
        {
            string stat = "";
            try
            {
                DataTable dt_checkpass = BLL.ExecuteQuery("select * from lbr_signup where lbr_password='" + BLL.Encrypt(BLL.ReplaceQuote(array[0])) + "' and lbr_id='" + array[2] + "'");
                if (dt_checkpass.Rows.Count > 0)
                {
                    bool status = BLL.ExecuteNonQuery("update lbr_signup set lbr_password='" + BLL.Encrypt(BLL.ReplaceQuote(array[1])) + "' where lbr_id='" + array[2] + "'");

                    if (status == true)
                    {
                        stat = "true";
                    }
                    else
                    {
                        stat = "false";
                    }

                }
                else
                {
                    stat = "wrongpass";
                }
            }
            catch (Exception ex)
            {

            }
            return stat;

        }

        protected void LinkButtonchange_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "$('#mymodalchangepass').modal('show'); ", true);
        }
        [WebMethod]
        public static string Gettabs(string value)
        {
            string[] arraylinks = value.Split('@');
            string id = arraylinks[0];

            string options = " <option value=\"0\" selected=\"selected\">Select Tab</option>";
            try
            {
                DataTable dt_tabs = BLL.ExecuteQuery("select * from lbr_tabs(nolock) where tab_id!='" + id + "' and tab_signup_id='" + arraylinks[1] + "' and tab_status=1");

                for (int i = 0; i < dt_tabs.Rows.Count; i++)
                {
                    options = options + " <option value=\"" + dt_tabs.Rows[i]["tab_id"].ToString() + "\">" + dt_tabs.Rows[i]["tab_name"].ToString() + "</option>";
                }

            }
            catch (Exception ex)
            {

            }
            return options;
        }
        [WebMethod]
        public static bool movelink(List<string> array)
        {

            bool status = false;
            try
            {
                DataTable dt = BLL.ExecuteQuery("select * from lbr_links where link_id='" + array[0] + "'");
                if (dt.Rows.Count > 0)
                {
                    // if (dt.Rows[0]["LINK_SIGNUPID"].ToString()!="22")
                    // { 
                    status = BLL.ExecuteNonQuery("update lbr_links set LINK_TABID='" + array[2] + "',link_modifieddate=getdate() where link_id='" + array[0] + "'");
                    // }
                }
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        [WebMethod]
        public static bool copylink(List<string> array)
        {
            bool status = false;

            try
            {
                DataTable dt_user = BLL.ExecuteQuery("select * from lbr_signup where lbr_id=" + array[3] + "");
                DataTable dt_new = BLL.ExecuteQuery("select * from lbr_links(nolock) where link_id='" + array[0] + "'");
                if (dt_new.Rows.Count > 0)
                {
                    DataTable dt = BLL.ExecuteQuery("insert into lbr_links(LINK_TABID,LINK_SIGNUPID,LINK_PATH,LINK_TITLE,LINK_DESCRIPTION,LINK_TYPE,LINK_IMAGEPATH,LINK_STATUS,LINK_CREATEDBY,LINK_CREATEDDATE,LINK_MODIFIEDBY,LINK_MODIFIEDDATE) select '" + array[2] + "','" + array[3] + "','" + BLL.ReplaceQuote(dt_new.Rows[0]["link_path"].ToString()) + "','" + BLL.ReplaceQuote(dt_new.Rows[0]["LINK_TITLE"].ToString()) + "','" + BLL.ReplaceQuote(dt_new.Rows[0]["LINK_DESCRIPTION"].ToString()) + "','" + dt_new.Rows[0]["LINK_TYPE"] + "','" + BLL.ReplaceQuote(dt_new.Rows[0]["LINK_IMAGEPATH"].ToString()) + "','" + dt_new.Rows[0]["LINK_STATUS"] + "','" + dt_new.Rows[0]["LINK_SIGNUPID"] + "',getdate(),'" + array[3] + "',getdate() select * from lbr_links where link_id =(select scope_identity()) ");

                    if (dt.Rows.Count > 0)
                    {

                        status = formatnewcopy(dt, dt_user.Rows[0]["lbr_emailid"].ToString(), array[3]);
                    }


                }
            }
            catch (Exception ex)
            {

            }
            return status;
        }
        public static bool formatnewcopy(DataTable dt, string emailnewid, string lbrid)
        {

            string email = emailnewid;
            bool status = false;
            try
            {

                string htmlcontent = "";
                LBR_LINKS obj = new LBR_LINKS();
                obj.LINK_CREATEDDBY = Int32.Parse(dt.Rows[0]["LINK_CREATEDBY"].ToString());
                obj.LINK_MODIFIEDBY = Int32.Parse(dt.Rows[0]["LINK_MODIFIEDBY"].ToString());
                obj.LINK_DESCRIPTION = dt.Rows[0]["LINK_DESCRIPTION"].ToString().Replace("{", "").Replace("}", "").Trim();
                obj.LINK_ID = Int32.Parse(dt.Rows[0]["LINK_ID"].ToString());
                obj.LINK_IMAGEPATH = dt.Rows[0]["LINK_IMAGEPATH"].ToString();

                obj.LINK_PATH = dt.Rows[0]["LINK_PATH"].ToString();

                obj.LINK_SIGNUPID = Int32.Parse(dt.Rows[0]["LINK_SIGNUPID"].ToString());
                obj.LINK_STATUS = 1;
                obj.LINK_TABID = Int32.Parse(dt.Rows[0]["LINK_TABID"].ToString());
                // obj.LINK_SIGNUPID = Int32.Parse(array[1]);
                obj.LINK_TYPE = Int32.Parse(dt.Rows[0]["LINK_TYPE"].ToString());
                obj.LINK_TITLE = dt.Rows[0]["LINK_TITLE"].ToString().Replace("{", "").Replace("}", "").Trim();

                string id = dt.Rows[0]["link_id"].ToString();

                if (obj.LINK_TYPE == 1)
                {

                    Match regexMatch = Regex.Match(dt.Rows[0]["LINK_PATH"].ToString(), @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)",
                 RegexOptions.IgnoreCase);
                    Match vimeoMatch = Regex.Match(dt.Rows[0]["LINK_PATH"].ToString(), @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)",
                        RegexOptions.IgnoreCase);
                    if (regexMatch.Success || vimeoMatch.Success)
                    {
                        if (regexMatch.Success)
                        {
                            obj.LINK_TYPE = 1;
                        }
                        else if (vimeoMatch.Success)
                        {
                            obj.LINK_TYPE = 2;
                        }
                    }
                    else
                    {
                        obj.LINK_TYPE = 3;
                    }
                    if (obj.LINK_TYPE.ToString() == "1" || obj.LINK_TYPE.ToString() == "2")
                    {
                        string value = "";
                        if (obj.LINK_TYPE.ToString() == "1")
                        {
                            value = regexMatch.Groups[1].Value;
                            //htmlcontent = "1";
                            if (lbrid != "22")
                            {
                                htmlcontent = "<div class=\"box\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','none')\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span><iframe class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"http://www.youtube.com/embed/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";
                            }
                            else
                            {
                                htmlcontent = "<div class=\"box\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','none')\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\" class=\"deleteadmin\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span><iframe class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"http://www.youtube.com/embed/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";
                            }
                        }
                        else if (obj.LINK_TYPE.ToString() == "2")
                        {
                            value = vimeoMatch.Groups[1].Value;
                            // htmlcontent = "2";
                            // htmlcontent = htmlcontent + "!@#" + value + "!@#" + obj.LINK_TITLE + "!@#" + obj.LINK_DESCRIPTION;
                            if (lbrid != "22")
                            {
                                htmlcontent = "<div class=\"box\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','none')\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a></span><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><iframe class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"//player.vimeo.com/video/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";
                            }
                            else
                            {
                                htmlcontent = "<div class=\"box\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','none')\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" class=\"deleteadmin\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a></span><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><iframe class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"//player.vimeo.com/video/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe></div>";
                            }
                        }


                    }
                    else if (obj.LINK_TYPE.ToString() == "3")
                    {
                        if (obj.LINK_IMAGEPATH != null && obj.LINK_IMAGEPATH != "0")
                        {
                            if (lbrid != "22")
                            {
                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><div class=\"boximage\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" style=\"background-image:url('" + obj.LINK_IMAGEPATH + "');\"><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + string.Format(obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "")) + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span></div><div class=\"boxtext\" ><a href='#' onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\">" + string.Format(dt.Rows[0]["link_title"].ToString().Replace("'", "")) + "</a></div></div>";
                            }
                            else
                            {
                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><div class=\"boximage\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" style=\"background-image:url('" + obj.LINK_IMAGEPATH + "');\"><span class=\"delete\"><a href=\"#\" class=\"deleteadmin\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + string.Format(obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "")) + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span></div><div class=\"boxtext\" ><a href='#' onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\">" + string.Format(dt.Rows[0]["link_title"].ToString().Replace("'", "")) + "</a></div></div>";
                            }
                        }
                        else
                        {
                            if (lbrid != "22")
                            {
                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span>" + dt.Rows[0]["link_title"].ToString().Replace("{", "").Replace("}", "") + "<br/>" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "") + "" + "</div>";
                            }
                            else
                            {
                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" class=\"deleteadmin\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span>" + dt.Rows[0]["link_title"].ToString().Replace("{", "").Replace("}", "") + "<br/>" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "") + "" + "</div>";
                            }
                        }

                    }

                }

                else
                {

                    htmlcontent = htmlformcopy(obj, emailnewid, lbrid);

                }
                //if(htmlcontent!="")
                //{
                status = BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                //}




            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public static bool formatcopy(DataTable dt, string emailnewid, string lbrid)
        {
            string email = emailnewid;
            bool status = false;
            try
            {

                string htmlcontent = "";
                LBR_LINKS obj = new LBR_LINKS();
                obj.LINK_CREATEDDBY = Int32.Parse(dt.Rows[0]["LINK_CREATEDBY"].ToString());
                obj.LINK_MODIFIEDBY = Int32.Parse(dt.Rows[0]["LINK_MODIFIEDBY"].ToString());
                obj.LINK_DESCRIPTION = dt.Rows[0]["LINK_DESCRIPTION"].ToString().Replace("{", "").Replace("}", "").Trim();

                obj.LINK_IMAGEPATH = dt.Rows[0]["LINK_IMAGEPATH"].ToString();

                obj.LINK_PATH = dt.Rows[0]["LINK_PATH"].ToString();

                obj.LINK_SIGNUPID = Int32.Parse(dt.Rows[0]["LINK_SIGNUPID"].ToString());
                obj.LINK_STATUS = 1;
                obj.LINK_TABID = Int32.Parse(dt.Rows[0]["LINK_TABID"].ToString());
                // obj.LINK_SIGNUPID = Int32.Parse(array[1]);
                obj.LINK_TYPE = Int32.Parse(dt.Rows[0]["LINK_TYPE"].ToString());
                obj.LINK_TITLE = dt.Rows[0]["LINK_TITLE"].ToString().Replace("{", "").Replace("}", "").Trim();

                string id = dt.Rows[0]["link_id"].ToString();

                if (obj.LINK_TYPE == 1)
                {

                    Match regexMatch = Regex.Match(dt.Rows[0]["LINK_PATH"].ToString(), @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)",
                 RegexOptions.IgnoreCase);
                    Match vimeoMatch = Regex.Match(dt.Rows[0]["LINK_PATH"].ToString(), @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)",
                        RegexOptions.IgnoreCase);
                    if (regexMatch.Success || vimeoMatch.Success)
                    {
                        if (regexMatch.Success)
                        {
                            obj.LINK_TYPE = 1;
                        }
                        else if (vimeoMatch.Success)
                        {
                            obj.LINK_TYPE = 2;
                        }
                    }
                    else
                    {
                        obj.LINK_TYPE = 3;
                    }
                    if (obj.LINK_TYPE.ToString() == "1" || obj.LINK_TYPE.ToString() == "2")
                    {
                        string value = "";
                        if (obj.LINK_TYPE.ToString() == "1")
                        {
                            value = regexMatch.Groups[1].Value;
                            //htmlcontent = "1";
                            if (lbrid != "22")
                            {
                                htmlcontent = "<div class=\"box\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','none')\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span><iframe class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"http://www.youtube.com/embed/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe><div class='sharedby'>Sent by " + email + "</div></div>";
                            }
                            else
                            {
                                htmlcontent = "<div class=\"box\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','none')\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\" class=\"deleteadmin\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span><iframe class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"http://www.youtube.com/embed/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe><div class='sharedby'>Sent by Linkskart</div></div>";
                            }
                        }
                        else if (obj.LINK_TYPE.ToString() == "2")
                        {
                            value = vimeoMatch.Groups[1].Value;
                            // htmlcontent = "2";
                            // htmlcontent = htmlcontent + "!@#" + value + "!@#" + obj.LINK_TITLE + "!@#" + obj.LINK_DESCRIPTION;
                            if (lbrid != "22")
                            {
                                htmlcontent = "<div class=\"box\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','none')\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a></span><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><iframe class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"//player.vimeo.com/video/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe><div class='sharedby'>Sent by " + email + "</div></div>";
                            }
                            else
                            {
                                htmlcontent = "<div class=\"box\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','none')\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" class=\"deleteadmin\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a></span><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><iframe class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"//player.vimeo.com/video/" + value + "\" frameborder=\"0\" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe><div class='sharedby'>Sent by Linkskart</div></div>";
                            }
                        }


                    }
                    else if (obj.LINK_TYPE.ToString() == "3")
                    {
                        if (obj.LINK_IMAGEPATH != null && obj.LINK_IMAGEPATH != "0")
                        {
                            if (lbrid != "22")
                            {
                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><div class=\"boximage\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" style=\"background-image:url('" + obj.LINK_IMAGEPATH + "');\"><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + string.Format(obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "")) + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span></div><div class=\"boxtext\" ><a href='#' onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\"><div class='sharedbytext'>Sent by " + email + "</div>" + string.Format(dt.Rows[0]["link_title"].ToString().Replace("'", "")) + "</a></div></div>";
                            }
                            else
                            {
                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><div class=\"boximage\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" style=\"background-image:url('" + obj.LINK_IMAGEPATH + "');\"><span class=\"delete\"><a href=\"#\" class=\"deleteadmin\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + string.Format(obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "")) + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span></div><div class=\"boxtext\" ><a href='#' onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\"><div class='sharedbytext'>Sent by Linkskart</div>" + string.Format(dt.Rows[0]["link_title"].ToString().Replace("'", "")) + "</a></div></div>";
                            }
                        }
                        else
                        {
                            if (lbrid != "22")
                            {
                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span><div class='sharedbytext'>Sent by " + email + "</div>" + dt.Rows[0]["link_title"].ToString().Replace("{", "").Replace("}", "") + "<br/>" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "") + "" + "</div>";
                            }
                            else
                            {
                                htmlcontent = "<div class=\"box\" id=\"divbox_" + id + "\" style=\"cursor:pointer\" onclick=\"OpenInNewTab('" + obj.LINK_PATH + "','divbox_" + id + "')\" ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" class=\"deleteadmin\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a href=\"#\"  title=\"Move or Copy\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a><a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a></span><div class='sharedbytext'>Sent by Linkskart</div>" + dt.Rows[0]["link_title"].ToString().Replace("{", "").Replace("}", "") + "<br/>" + obj.LINK_DESCRIPTION.Replace("{", "").Replace("}", "") + "" + "</div>";
                            }
                        }

                    }

                }

                else
                {
                    obj.LINK_ID = Int32.Parse(id.ToString());
                    htmlcontent = htmlco(obj, emailnewid, lbrid);
                }
                //if(htmlcontent!="")
                //{
                status = BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                //}




            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public static string htmlco(LBR_LINKS obj, string emailnewid, string lbrid)
        {
            string htmlcontent = "";
            string userid = obj.LINK_SIGNUPID.ToString();
            string id = obj.LINK_ID.ToString();
            string email = emailnewid;
            try
            {
                if (IsImageFile(obj.LINK_TITLE))
                {



                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><img onclick=\"onclickimage('" + obj.LINK_PATH + "','none')\" class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"" + obj.LINK_PATH + "\"/><div class='sharedby'>Sent by " + email + "</div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"adminbox\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a class='deleteadmin' href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><img onclick=\"onclickimage('" + obj.LINK_PATH + "','none')\" class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"" + obj.LINK_PATH + "\"/><div class='sharedby'>Sent by " + email + "</div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                    //else
                    //{

                    //}
                }
                else if (IstextFile(obj.LINK_TITLE))
                {
                    string text = "";
                    //try
                    //{
                    //    text = System.IO.File.ReadAllText(obj.LINK_PATH);
                    //}
                    //catch (Exception ex)
                    //{
                    text = obj.LINK_DESCRIPTION;
                    //}


                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = (text.Substring(0, 250));
                    }
                    else
                    {
                        title = (text);
                    }
                    obj.LINK_TYPE = 3;
                    DataTable dt = BLL.set_link(obj);


                    {
                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'>" + title + "<span class='wrap-indicator' title='" + obj.LINK_TITLE + "' onclick=\"$('#modalopen_" + id + "').modal('show')\">[…]</span></div><div class='sharedby'>Sent by " + email + "</div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\"><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'>" + title + "<span class='wrap-indicator' title='" + obj.LINK_TITLE + "' onclick=\"$('#modalopen_" + id + "').modal('show')\">[…]</span></div><div class='sharedby'>Sent by Linkskart</div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100%; height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
                else if (IspdfFile(obj.LINK_TITLE))
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";
                    //try
                    //{
                    //    //text = System.IO.File.ReadAllText(obj.LINK_PATH);
                    //    using (PdfReader reader = new PdfReader(obj.LINK_PATH))
                    //    {
                    //        if (reader.NumberOfPages >= 1)
                    //        {
                    //            for (int k = 1; k <= reader.NumberOfPages; k++)
                    //            {
                    //                text = text + PdfTextExtractor.GetTextFromPage(reader, k);
                    //            }
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    text = obj.LINK_DESCRIPTION;
                    //}


                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = (text.Substring(0, 250));
                    }
                    else
                    {
                        title = (text);
                    }
                    obj.LINK_TYPE = 3;



                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("openkartshare(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'>" + title + "<span class='wrap-indicator' title='" + obj.LINK_TITLE + "' onclick=\"$('#modalopen_" + id + "').modal('show')\">[…]</span></div><div class='sharedby'>Sent by " + email + "</div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("openkartshare(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class=\"adminShareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\"><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'>" + title + "<span class='wrap-indicator' title='" + obj.LINK_TITLE + "' onclick=\"$('#modalopen_" + id + "').modal('show')\">[…]</span></div><div class='sharedby'>Sent by Linkskart</div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100%; height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
                else if (IswordFile(obj.LINK_TITLE) || IsexcelFile(obj.LINK_TITLE))
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = obj.LINK_PATH;
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = obj.LINK_TITLE;
                    }

                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = (text.Substring(0, 250));
                    }
                    else
                    {
                        title = (text);
                    }
                    obj.LINK_TYPE = 3;



                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><span class='wrap-indicator' title='" + obj.LINK_TITLE + "' download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\">[…]</span></div><div class='sharedby'>Sent by " + email + "</div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\"><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><span class='wrap-indicator' title='" + obj.LINK_TITLE + "' download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\">[…]</span></div><div class='sharedby'>Sent by Linkskart</div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100%; height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
                else if (IssongFile(obj.LINK_TITLE))
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = obj.LINK_PATH;
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = obj.LINK_TITLE;
                    }
                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = (text.Substring(0, 250));
                    }
                    else
                    {
                        title = (text);
                    }
                    obj.LINK_TYPE = 3;


                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div  class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><audio controls><source src='" + obj.LINK_PATH + "' type='audio/ogg'><source src='" + obj.LINK_PATH + "' type='audio/mpeg'<source src='" + obj.LINK_PATH + "' type='audio/wav'>Your browser does not support the audio element.</audio></div><div class='sharedby'>Sent by " + email + "</div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div  class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><audio controls><source src='" + obj.LINK_PATH + "' type='audio/ogg'><source src='" + obj.LINK_PATH + "' type='audio/mpeg'><source src='" + obj.LINK_PATH + "' type='audio/wav'>Your browser does not support the audio element.</audio></div><div class='sharedby'>Sent by Linkskart</div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
                else if (IsvideoFile(obj.LINK_TITLE))
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = obj.LINK_PATH;
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = obj.LINK_TITLE;
                    }

                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = (text.Substring(0, 250));
                    }
                    else
                    {
                        title = (text);
                    }
                    obj.LINK_TYPE = 3;



                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><video class='Shareboxvideo' style=\"height:auto;max-height: 140px;max-width:236px;\" controls><source src='" + obj.LINK_PATH + "' type='video/mp4'><source src='" + obj.LINK_PATH + "' type='video/ogg'><source src='" + obj.LINK_PATH + "' type='video/WebM'>Your browser does not support the video tag.</video><h4 class='h4class'>" + obj.LINK_TITLE + "</h4><div class='sharedby'>Sent by " + email + "</div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><video class='Shareboxvideo' style=\"height:auto;max-height: 140px;max-width:236px;\" controls><source src='" + obj.LINK_PATH + "' type='video/mp4'><source src='" + obj.LINK_PATH + "' type='video/ogg'><source src='" + obj.LINK_PATH + "' type='video/WebM'>Your browser does not support the video tag.</video><h4 class='h4class'>" + obj.LINK_TITLE + "</h4><div class='sharedby'>Sent by Linkskart</div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
                else
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = obj.LINK_PATH;
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = obj.LINK_TITLE;
                    }

                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = (text.Substring(0, 250));
                    }
                    else
                    {
                        title = (text);
                    }
                    obj.LINK_TYPE = 3;



                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" class=\"Shareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><span class='wrap-indicator' title='" + obj.LINK_TITLE + "' download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\">[…]</span></div><div class='sharedby'>Sent by " + email + "</div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" class=\"adminShareboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\"><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><span class='wrap-indicator' title='" + obj.LINK_TITLE + "' download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\">[…]</span></div><div class='sharedby'>Sent by Linkskart</div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100%; height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return htmlcontent;
        }

        public static string htmlformcopy(LBR_LINKS obj, string emailnewid, string lbrid)
        {
            string htmlcontent = "";
            string userid = obj.LINK_SIGNUPID.ToString();
            string id = obj.LINK_ID.ToString();
            string email = emailnewid;
            try
            {
                if (IsImageFile(obj.LINK_TITLE))
                {



                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><img onclick=\"onclickimage('" + obj.LINK_PATH + "','none')\" class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"" + obj.LINK_PATH + "\"/></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"adminbox\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a class='deleteadmin' href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><img onclick=\"onclickimage('" + obj.LINK_PATH + "','none')\" class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"" + obj.LINK_PATH + "\"/></div>";
                            //htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><img onclick=\"onclickimage('" + obj.LINK_PATH + "','none')\" class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" src=\"" + obj.LINK_PATH + "\"/></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                    //else
                    //{

                    //}
                }
                else if (IstextFile(obj.LINK_TITLE))
                {
                    string text = "";
                    //try
                    //{
                    //    text = System.IO.File.ReadAllText(obj.LINK_PATH);
                    //}
                    //catch (Exception ex)
                    //{
                    text = obj.LINK_DESCRIPTION;
                    //}


                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = (text.Substring(0, 250));
                    }
                    else
                    {
                        title = (text);
                    }
                    obj.LINK_TYPE = 3;
                    DataTable dt = BLL.set_link(obj);


                    {
                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'>" + title + "<span class='wrap-indicator' title='" + obj.LINK_TITLE + "' onclick=\"$('#modalopen_" + id + "').modal('show')\">[…]</span></div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"adminbox\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a class='deleteadmin' href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\"><h4 class='adminh4class'>" + obj.LINK_TITLE + "</h4>" + title + "<span class='wrap-indicator' title='" + obj.LINK_TITLE + "' onclick=\"$('#modalopen_" + id + "').modal('show')\">[…]</span></div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='adminh4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100%; height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
                else if (IspdfFile(obj.LINK_TITLE))
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";
                    //try
                    //{
                    //    //text = System.IO.File.ReadAllText(obj.LINK_PATH);
                    //    using (PdfReader reader = new PdfReader(obj.LINK_PATH))
                    //    {
                    //        if (reader.NumberOfPages >= 1)
                    //        {
                    //            for (int k = 1; k <= reader.NumberOfPages; k++)
                    //            {
                    //                text = text + PdfTextExtractor.GetTextFromPage(reader, k);
                    //            }
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    text = obj.LINK_DESCRIPTION;
                    //}


                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = (text.Substring(0, 250));
                    }
                    else
                    {
                        title = (text);
                    }
                    obj.LINK_TYPE = 3;



                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'>" + title + "<span class='wrap-indicator' title='" + obj.LINK_TITLE + "' onclick=\"$('#modalopen_" + id + "').modal('show')\">[…]</span></div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"adminbox\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\"><h4 class='adminh4class'>" + obj.LINK_TITLE + "</h4>" + title + "<span class='wrap-indicator' title='" + obj.LINK_TITLE + "' onclick=\"$('#modalopen_" + id + "').modal('show')\">[…]</span></div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='adminh4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100%; height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                            // htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\"><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'>" + title + "<span class='wrap-indicator' title='" + obj.LINK_TITLE + "' onclick=\"$('#modalopen_" + id + "').modal('show')\">[…]</span></div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100%; height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
                else if (IswordFile(obj.LINK_TITLE) || IsexcelFile(obj.LINK_TITLE))
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = obj.LINK_PATH;
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = obj.LINK_TITLE;
                    }

                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = (text.Substring(0, 250));
                    }
                    else
                    {
                        title = (text);
                    }
                    obj.LINK_TYPE = 3;



                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><span class='wrap-indicator' title='" + obj.LINK_TITLE + "' download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\">[…]</span></div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"adminbox\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\" class='deleteadmin'  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\"><h4 class='adminh4class'>" + obj.LINK_TITLE + "</h4><span class='wrap-indicator' title='" + obj.LINK_TITLE + "' download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\">[…]</span></div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='adminh4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100%; height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                            // htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\"><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><span class='wrap-indicator' title='" + obj.LINK_TITLE + "' download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\">[…]</span></div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100%; height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
                else if (IssongFile(obj.LINK_TITLE))
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = obj.LINK_PATH;
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = obj.LINK_TITLE;
                    }
                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = (text.Substring(0, 250));
                    }
                    else
                    {
                        title = (text);
                    }
                    obj.LINK_TYPE = 3;


                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\"  target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div  class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><audio controls><source src='" + obj.LINK_PATH + "' type='audio/ogg'><source src='" + obj.LINK_PATH + "' type='audio/mpeg'<source src='" + obj.LINK_PATH + "' type='audio/wav'>Your browser does not support the audio element.</audio></div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"adminbox\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  class='deleteadmin' title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\"  target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div  class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='adminh4class'>" + obj.LINK_TITLE + "</h4><audio controls><source src='" + obj.LINK_PATH + "' type='audio/ogg'><source src='" + obj.LINK_PATH + "' type='audio/mpeg'><source src='" + obj.LINK_PATH + "' type='audio/wav'>Your browser does not support the audio element.</audio></div></div>";
                            //  htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div  class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><audio controls><source src='" + obj.LINK_PATH + "' type='audio/ogg'><source src='" + obj.LINK_PATH + "' type='audio/mpeg'><source src='" + obj.LINK_PATH + "' type='audio/wav'>Your browser does not support the audio element.</audio></div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
                else if (IsvideoFile(obj.LINK_TITLE))
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = obj.LINK_PATH;
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = obj.LINK_TITLE;
                    }

                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = (text.Substring(0, 250));
                    }
                    else
                    {
                        title = (text);
                    }
                    obj.LINK_TYPE = 3;



                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><video class='boxvideo' style=\"height:auto;max-height: 140px;max-width:236px;\" controls><source src='" + obj.LINK_PATH + "' type='video/mp4'><source src='" + obj.LINK_PATH + "' type='video/ogg'><source src='" + obj.LINK_PATH + "' type='video/WebM'>Your browser does not support the video tag.</video><h4 class='h4class'>" + obj.LINK_TITLE + "</h4></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"adminbox\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\" class='deleteadmin'  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><video class='adminboxvideo' style=\"height:auto;max-height: 140px;max-width:236px;\" controls><source src='" + obj.LINK_PATH + "' type='video/mp4'><source src='" + obj.LINK_PATH + "' type='video/ogg'><source src='" + obj.LINK_PATH + "' type='video/WebM'>Your browser does not support the video tag.</video><h4 class='adminh4class'>" + obj.LINK_TITLE + "</h4></div>";
                            // htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><video class='adminboxvideo' style=\"height:auto;max-height: 140px;max-width:236px;\" controls><source src='" + obj.LINK_PATH + "' type='video/mp4'><source src='" + obj.LINK_PATH + "' type='video/ogg'><source src='" + obj.LINK_PATH + "' type='video/WebM'>Your browser does not support the video tag.</video><h4 class='h4class'>" + obj.LINK_TITLE + "</h4></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
                else
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = obj.LINK_PATH;
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = obj.LINK_TITLE;
                    }

                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = HttpUtility.HtmlEncode(text.Substring(0, 250));
                    }
                    else
                    {
                        title = HttpUtility.HtmlEncode(text);
                    }
                    obj.LINK_TYPE = 3;



                    {

                        //  BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
                        if (userid != "22")
                        {
                            htmlcontent = "<div class=\"box\"   id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"delete\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" class=\"boxvideo\" style=\"margin-left:-6px;margin-top:-6px;\" ><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><span class='wrap-indicator' title='" + obj.LINK_TITLE + "' download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\">[…]</span></div><div class='sharedby'>Sent by " + email + "</div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        else
                        {
                            htmlcontent = "<div class=\"box\"  id=\"divbox_" + id + "\" style=\"cursor:pointer\"  ><span class=\"deleteadmin\"><a href=\"#\"  title=\"Delete\" onclick=\"deletebox('divbox_" + id + "')\"><i class=\"fa fa-trash\"></i></a><a  title=\"Move or Copy\" href=\"#\" onclick=\"movelinks(" + id + ")\"><i class=\"fa fa-edit\"></i></a><a  title=\"Share With Linkskart\" onclick=\"openkartshare(" + id + ")\" href=\"#\"><i class=\"fa fa-share\"></i></a> <a  title=\"Share With Facebook\" onclick='" + String.Format("share(\"" + obj.LINK_TITLE.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + obj.LINK_DESCRIPTION.Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + obj.LINK_IMAGEPATH.Replace("'", "") + "\"); return false;") + "' href=\"#\"><i class=\"fa fa-facebook\"></i></a><a  title=\"Share With Twitter\" onclick='opentwitter(" + id + ")' href=\"#\"><i class=\"fa fa-twitter\"></i></a><a  title=\"Download\" download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" ><i class=\"fa fa-download\"></i></a></span><div download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\" class=\"adminboxvideo\" style=\"margin-left:-6px;margin-top:-6px;\"><h4 class='h4class'>" + obj.LINK_TITLE + "</h4 class='h4class'><span class='wrap-indicator' title='" + obj.LINK_TITLE + "' download=\"" + obj.LINK_TITLE + "\" target='_blank' href=\"" + obj.LINK_PATH + "\">[…]</span></div><div class='sharedby'>Sent by Linkskart</div></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + obj.LINK_TITLE + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100%; height: 500.25px;' readonly=''>" + obj.LINK_DESCRIPTION + "</textarea></div></div></div></div>";
                        }
                        bool stat = BLL.updatedata(htmlcontent, id);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return htmlcontent;
        }

        public string Loadlinks(DataTable dt)
        {
            string html = "";
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["link_id"].ToString();
                    if (dt.Rows[i]["link_type"].ToString() == "1")
                    {
                        Match regexMatch = Regex.Match(dt.Rows[i]["link_path"].ToString(), @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)",
                           RegexOptions.IgnoreCase);
                        Match vimeoMatch = Regex.Match(dt.Rows[i]["link_path"].ToString(), @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)",
                            RegexOptions.IgnoreCase);


                        if (regexMatch.Success || vimeoMatch.Success)
                        {
                            string value = "";
                            if (regexMatch.Success)
                            {
                                value = regexMatch.Groups[1].Value;
                                html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div class='icon'><i class='fa fa-youtube-play'></i></div></a><div class='file-name'><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\">" + dt.Rows[i]["link_title"].ToString() + "<br/>" + dt.Rows[i]["LINK_DESCRIPTION"].ToString() + "</a></div></a></div></div>";
                            }
                            else if (vimeoMatch.Success)
                            {
                                value = regexMatch.Groups[1].Value;
                                html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div class='icon'><i class='fa fa-youtube-play'></i></div></a><div class='file-name'><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\">" + dt.Rows[i]["link_title"].ToString() + "<br/>" + dt.Rows[i]["LINK_DESCRIPTION"].ToString() + "</a></div></a></div></div>";
                            }
                        

                        }
                        else
                        {

                            if (dt.Rows[i]["LINK_IMAGEPATH"].ToString() != "")
                            {
                                html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div class='image'><img alt='image' class='img-responsive' src='" + dt.Rows[i]["LINK_IMAGEPATH"].ToString() + "'></div></a><div class='file-name'><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\">" + dt.Rows[i]["link_title"].ToString() + "<br/>" + dt.Rows[i]["LINK_DESCRIPTION"].ToString() + "</a></div></a></div></div>";
                            }
                            else
                            {
                                html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div class='icon'><i class='fa fa-link'></i>></div></a><div class='file-name'><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\">" + dt.Rows[i]["link_title"].ToString() + "<br/>" + dt.Rows[i]["LINK_DESCRIPTION"].ToString() + "</a></div></a></div></div>";
                            }
                        }
                    }
                    else
                    {
                        if(dt.Rows[i]["link_type"].ToString()=="2")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div class='image onlyimage'><img onclick=\"onclickimage('" + dt.Rows[i]["link_path"].ToString() + "','none')\" alt='image' class='img-responsive onlyimage' src='" + dt.Rows[i]["LINK_IMAGEPATH"].ToString() + "'/></div></a><div class='file-name'><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\">" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div></div>";

                        }
                        else if(dt.Rows[i]["link_type"].ToString()=="3")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a ><div onclick=\"$('#modalopen_" + id + "').modal('show')\"  class='icon'><i class='fa fa-file-text'></i></div></a><div  class='file-name'><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else if(dt.Rows[i]["link_type"].ToString()=="4")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div  class='icon'><i class='fa fa-file-pdf-o'></i></div></a><div  class='file-name'><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else if (dt.Rows[i]["link_type"].ToString() == "5")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a ><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class='icon'><i class='fa fa-file-word-o'></i></div></a><div class='file-name'><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">" + dt.Rows[i]["link_title"].ToString() +  "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else if (dt.Rows[i]["link_type"].ToString() == "6")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a ><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class='icon'><i class='fa fa-bar-chart-o'></i></div></a><div  class='file-name'><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_description"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else if (dt.Rows[i]["link_type"].ToString() == "7")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\"><div  class='icon'><i class='fa fa-music'></i></div></a><div download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\" class='file-name'><a >" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else if (dt.Rows[i]["link_type"].ToString() == "8")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a ><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class='icon'><i class='fa fa-vimeo-square'></i></div></a><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class='file-name'><a >" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\"><div  class='icon'><i class='fa fa-file'></i></div></a><div  class='file-name'><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return html;
        }
        public static string LoadlinksStatic(DataTable dt)
        {
            string html = "";
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["link_id"].ToString();
                    if (dt.Rows[i]["link_type"].ToString() == "1")
                    {
                        Match regexMatch = Regex.Match(dt.Rows[i]["link_path"].ToString(), @"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)",
                           RegexOptions.IgnoreCase);
                        Match vimeoMatch = Regex.Match(dt.Rows[i]["link_path"].ToString(), @"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)",
                            RegexOptions.IgnoreCase);


                        if (regexMatch.Success || vimeoMatch.Success)
                        {
                            string value = "";
                            if (regexMatch.Success)
                            {
                                value = regexMatch.Groups[1].Value;
                                html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div class='icon'><i class='fa fa-youtube-play'></i></div></a><div class='file-name'><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\">" + dt.Rows[i]["link_title"].ToString() + "<br/>" + dt.Rows[i]["LINK_DESCRIPTION"].ToString() + "</a></div></a></div></div>";
                            }
                            else if (vimeoMatch.Success)
                            {
                                value = regexMatch.Groups[1].Value;
                                html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div class='icon'><i class='fa fa-youtube-play'></i></div></a><div class='file-name'><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\">" + dt.Rows[i]["link_title"].ToString() + "<br/>" + dt.Rows[i]["LINK_DESCRIPTION"].ToString() + "</a></div></a></div></div>";
                            }
                       

                        }
                        else
                        {
                          
                            if (dt.Rows[i]["LINK_IMAGEPATH"].ToString() != "")
                            {
                                html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div class='image'><img alt='image' class='img-responsive' src='" + dt.Rows[i]["LINK_IMAGEPATH"].ToString() + "'></div></a><div class='file-name'><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\">" + dt.Rows[i]["link_title"].ToString() + "<br/>" + dt.Rows[i]["LINK_DESCRIPTION"].ToString() + "</a></div></a></div></div>";
                            }
                            else
                            {
                                html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div class='icon'><i class='fa fa-link'></i>></div></a><div class='file-name'><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\">" + dt.Rows[i]["link_title"].ToString() + "<br/>" + dt.Rows[i]["LINK_DESCRIPTION"].ToString() + "</a></div></a></div></div>";
                            }
                        }
                    }
                    else
                    {
                        if (dt.Rows[i]["link_type"].ToString() == "2")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div class='image onlyimage'><img onclick=\"onclickimage('" + dt.Rows[i]["link_path"].ToString() + "','none')\" alt='image' class='img-responsive onlyimage' src='" + dt.Rows[i]["LINK_IMAGEPATH"].ToString() + "'/></div></a><div class='file-name'><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\">" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div></div>";

                        }
                        else if (dt.Rows[i]["link_type"].ToString() == "3")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a ><div onclick=\"$('#modalopen_" + id + "').modal('show')\"  class='icon'><i class='fa fa-file-text'></i></div></a><div  class='file-name'><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else if (dt.Rows[i]["link_type"].ToString() == "4")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a onclick=\"OpenInNewTab('" + dt.Rows[i]["LINK_PATH"].ToString() + "','none')\"><div  class='icon'><i class='fa fa-file-pdf-o'></i></div></a><div  class='file-name'><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else if (dt.Rows[i]["link_type"].ToString() == "5")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a ><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class='icon'><i class='fa fa-file-word-o'></i></div></a><div class='file-name'><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else if (dt.Rows[i]["link_type"].ToString() == "6")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a ><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class='icon'><i class='fa fa-bar-chart-o'></i></div></a><div  class='file-name'><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">" + dt.Rows[i]["link_title"].ToString() +  "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else if (dt.Rows[i]["link_type"].ToString() == "7")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\"><div  class='icon'><i class='fa fa-music'></i></div></a><div download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\" class='file-name'><a >" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else if (dt.Rows[i]["link_type"].ToString() == "8")
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a ><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class='icon'><i class='fa fa-vimeo-square'></i></div></a><div onclick=\"$('#modalopen_" + id + "').modal('show')\" class='file-name'><a >" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                        else
                        {
                            html = html + "<div class='file-box' id=\"divbox_" + id + "\"><div class='file'><div class='container'><div class='row'></div></div><a href='#'><span class='corner'></span><div class='dropdown pull-right linkdrop' style='z-index:1000'><a id='dLabel' role='button' data-toggle='dropdown' class='btn btn-primary' data-target='#' href='/page.html'><span class='caret'></span></a><ul class='dropdown-menu multi-level' role='menu' aria-labelledby='dropdownMenu'><li><a onclick=\"deletebox('divbox_" + id + "')\" >Delete</a></li><li><a onclick=\"movelinks(" + id + ")\">Move or Copy</a></li><li><a onclick=\"openkartshare(" + id + ")\">Share With Linkskart</a></li><li><a onclick='" + String.Format("share(\"" + dt.Rows[i]["LINK_TITLE"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"http://linkskart.com?redirectid=" + id + "\",\"\",\"" + dt.Rows[i]["LINK_DESCRIPTION"].ToString().Replace("'", "").Replace("{", "").Replace("}", "") + "\",\"" + dt.Rows[i]["LINK_IMAGEPATH"].ToString().Replace("'", "") + "\"); return false;") + "'>Share With Facebook</a></li><li><a onclick='opentwitter(" + id + ")'>Share With Twitter</a></li><li><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">Download</a></li></ul></div><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\"><div  class='icon'><i class='fa fa-file'></i></div></a><div  class='file-name'><a download=\"" + dt.Rows[i]["link_title"].ToString() + "\" target='_blank' href=\"" + dt.Rows[i]["link_path"].ToString() + "\">" + dt.Rows[i]["link_title"].ToString() + "</a></div></a></div><div id='modalopen_" + id + "' class='modal fade' aria-hidden='true' style='display: none;'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button><h4 class='h4class' class='modal-title'>Detailed Preview <small>" + dt.Rows[i]["link_title"].ToString() + "</small></h4 class='h4class'></div><div class='modal-body'><textarea class='form-control' style='font-family:Monaco,Consolas,monospace;width:100% ;height: 500.25px;' readonly=''>" + dt.Rows[i]["link_description"].ToString() + "</textarea></div></div></div></div></div>";
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return html;
        }
        [WebMethod]
        public static void loadword(string path)
        {
            try
            {
                object filenamenew = HostingEnvironment.MapPath(path.Replace("../", "~/"));
                Microsoft.Office.Interop.Word.Application AC = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
                object readOnly = false;
                object isVisible = true;
                object missing = System.Reflection.Missing.Value;

                doc = AC.Documents.Open(ref filenamenew, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible);
                // text = doc.Content.Text;
                //AC.Documents.Close();

            }
            catch (Exception ex)
            {

            }
        }
        [WebMethod]
        public static void logo()
        {
            try
            {



                HttpContext.Current.Session["USERINFO"] = null;
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();

                if (HttpContext.Current.Response.Cookies["LBRUserName"] != null && HttpContext.Current.Response.Cookies["LBRPassword"] != null)
                {
                    HttpContext.Current.Response.Cookies["LBRUserName"].Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies["LBRPassword"].Expires = DateTime.Now.AddDays(-1);
                }
                
                //Server.TransferRequest("frm_login.aspx", false);
            }
            catch (Exception ex)
            {
                // Server.TransferRequest("frm_login.aspx",false);
               
            }
        }
        [WebMethod]
        public static string searchfiles(string value)
        {
            string[] arraylinks = value.Split('@');
            string key = arraylinks[0];
            string results = "";
            string id = arraylinks[1];

            try
            {
                DataTable dt_result = BLL.loadsearchresults(key, id);
                if (dt_result.Rows.Count > 0)
                {
                    results = LoadlinksStatic(dt_result);

                }
            }
            catch (Exception ex)
            {

            }
            return results;
        }

        public static bool IsImageFile(string path)
        {
            return -1 != Array.IndexOf(mediaExtensions, System.IO.Path.GetExtension(path).ToUpperInvariant());
        }
        public static bool IstextFile(string path)
        {
            return -1 != Array.IndexOf(textExtensions, System.IO.Path.GetExtension(path).ToUpperInvariant());
        }
        public static bool IspdfFile(string path)
        {
            return -1 != Array.IndexOf(pdfExtensions, System.IO.Path.GetExtension(path).ToUpperInvariant());
        }
        public static bool IswordFile(string path)
        {
            return -1 != Array.IndexOf(wordExtensions, System.IO.Path.GetExtension(path).ToUpperInvariant());
        }
        public static bool IsexcelFile(string path)
        {
            return -1 != Array.IndexOf(excelExtensions, System.IO.Path.GetExtension(path).ToUpperInvariant());
        }
        public static bool IssongFile(string path)
        {
            return -1 != Array.IndexOf(songExtensions, System.IO.Path.GetExtension(path).ToUpperInvariant());
        }
        public static bool IsvideoFile(string path)
        {
            return -1 != Array.IndexOf(videoExtensions, System.IO.Path.GetExtension(path).ToUpperInvariant());
        }

       
        //[WebMethod(EnableSession = true)]
        //public static void  sharetwitter()
        //{
        //    TwitterConnect.API_Key = "BO3uWIXV5nh7ZGnhUeo9Ktp8T";
        //    TwitterConnect.API_Secret = "gnWXB4aDfFJE87cbkUeJTTFkkdYwEd6g56DLEfqU6EbLicy92e";

        //   HttpContext.Current.Session["twitterID"] = e.CommandArgument;
        //    if (!TwitterConnect.IsAuthorized)
        //    {
        //        try
        //        {

        //            TwitterConnect twitter = new TwitterConnect();
        //            twitter.Authorize(Request.Url.AbsoluteUri.Split('?')[0]);

        //        }
        //        catch (Exception ex)
        //        {

        //            //TwitterConnect.API_Key = "BO3uWIXV5nh7ZGnhUeo9Ktp8T";
        //            //TwitterConnect.API_Secret = "gnWXB4aDfFJE87cbkUeJTTFkkdYwEd6g56DLEfqU6EbLicy92e";
        //            //TwitterConnect twitter = new TwitterConnect();
        //            //twitter.Tweet("hi pragatibharath");
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {

        //            if ( Session["twitterID"] != null)
        //            {

        //                TwitterConnect twitter = new TwitterConnect();

        //                string id = Session["twitterID"].ToString();

        //                DataTable dt = BLL.ExecuteQuery("select * from pb_blogs where  ID=" + id + "");

        //                string desc = dt.Rows[0]["Blog_Desc"].ToString();

        //                if (desc.Length > 80)
        //                {
        //                    desc = desc.Substring(0, 95);
        //                }

        //                twitter.Tweet(desc.Replace("'", "") + "  Readmore..." + "http://www.pragatibharath.com/Election/Blogs.aspx");

        //                BLL.ShowMessage(this, "Your Tweet posted");
        //                //twitter.Tweet();
        //            }
        //            Session["twiterID"] = null;
        //        }
        //        catch (Exception ex)
        //        {
        //            BLL.ShowMessage(this, "Please try again after some time!!!");
        //        }
        //    }
        //}
    }
}