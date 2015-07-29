<%@ WebHandler Language="C#" Class="FileUploadHandler" %>
 
using System;
using System.Web;
using System.Data;
using LINKSKARTNEW.Code;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.ComponentModel;
using System.IO;
using System.Text;


 
public class FileUploadHandler : IHttpHandler {
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
    
   
    public void ProcessRequest(HttpContext context)
    {
        try
    {
        if (context.Request.Files.Count > 0)
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                string userid = context.Request.QueryString["user"].ToString();
                string tabid = context.Request.QueryString["tabid"].ToString();
                string fsmallpath = userid + DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "");
               
                string fname = context.Server.MapPath("/uploads/" + fsmallpath + file.FileName);
                
                file.SaveAs(fname);
                string id = "";
                string htmlcontent = "";
                bool status = false;
                if (IsImageFile(file.FileName))
                {
                    LBR_LINKS obj = new LBR_LINKS();
                    obj.LINK_CREATEDDBY = Int32.Parse(userid);
                    obj.LINK_DESCRIPTION = file.FileName;
                    obj.LINK_IMAGEPATH = "../uploads/" + fsmallpath  + file.FileName + "";
                    obj.LINK_MODIFIEDBY = Int32.Parse(userid);
                    obj.LINK_PATH = "../uploads/" + fsmallpath  + file.FileName + "";
                    obj.LINK_SIGNUPID = Int32.Parse(userid);
                    obj.LINK_STATUS = 1;
                    obj.LINK_TABID = Int32.Parse(tabid);
                    obj.LINK_TITLE = file.FileName;
                    obj.LINK_TYPE = 2;
                    DataTable dt = BLL.set_link(obj);
                   
                
                    //else
                    //{

                    //}
                }
                else if (IstextFile(file.FileName))
                {
                    string text = "";
                    try
                    {
                        text = System.IO.File.ReadAllText(context.Server.MapPath("~/uploads/" + fsmallpath + file.FileName));
                    }
                    catch (Exception ex)
                    {
                        text = file.FileName;
                    }
                    
                    LBR_LINKS obj = new LBR_LINKS();
                    obj.LINK_CREATEDDBY = Int32.Parse(userid);
                    obj.LINK_DESCRIPTION = HttpUtility.HtmlEncode(text);
                    obj.LINK_IMAGEPATH =  "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_MODIFIEDBY = Int32.Parse(userid);
                    obj.LINK_PATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_SIGNUPID = Int32.Parse(userid);
                    obj.LINK_STATUS = 1;
                    obj.LINK_TABID = Int32.Parse(tabid);
                    obj.LINK_TITLE = file.FileName;
                    string title = "";
                   
                    if(text.Length>=250)
                    {
                        title = HttpUtility.HtmlEncode(text.Substring(0, 250));
                    }
                    else
                    {
                        title = HttpUtility.HtmlEncode(text);
                    }
                    obj.LINK_TYPE = 3;
                    DataTable dt = BLL.set_link(obj);

                  
                }
                else if (IspdfFile(file.FileName))
                {
                   // System.Text.StringBuilder text = new System.Text.StringBuilder();
                   
                

                    LBR_LINKS obj = new LBR_LINKS();
                    obj.LINK_CREATEDDBY = Int32.Parse(userid);
                    obj.LINK_DESCRIPTION = file.FileName;
                    obj.LINK_IMAGEPATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_MODIFIEDBY = Int32.Parse(userid);
                    obj.LINK_PATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_SIGNUPID = Int32.Parse(userid);
                    obj.LINK_STATUS = 1;
                    obj.LINK_TABID = Int32.Parse(tabid);
                    obj.LINK_TITLE = file.FileName;
                   

                 
                    obj.LINK_TYPE = 4;
                    DataTable dt = BLL.set_link(obj);

                 
                }
                else if (IswordFile(file.FileName) )
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";
                    
                    string path = context.Server.MapPath("~/uploads/" + fsmallpath + file.FileName);
                   // text = BLL.loadword(path);
                    if(text=="")
                    {
                        text = file.FileName;
                    }
                    LBR_LINKS obj = new LBR_LINKS();
                    obj.LINK_CREATEDDBY = Int32.Parse(userid);
                    obj.LINK_DESCRIPTION = HttpUtility.HtmlEncode(text);
                    obj.LINK_IMAGEPATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_MODIFIEDBY = Int32.Parse(userid);
                    obj.LINK_PATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_SIGNUPID = Int32.Parse(userid);
                    obj.LINK_STATUS = 1;
                    obj.LINK_TABID = Int32.Parse(tabid);
                    obj.LINK_TITLE = file.FileName;
                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = HttpUtility.HtmlEncode(text.Substring(0, 250));
                    }
                    else
                    {
                        title = HttpUtility.HtmlEncode(text);
                    }
                    obj.LINK_TYPE = 5;
                    DataTable dt = BLL.set_link(obj);

             
                }
                else if (IsexcelFile(file.FileName))
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = context.Server.MapPath("~/uploads/" + fsmallpath + file.FileName);
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = file.FileName;
                    }
                    LBR_LINKS obj = new LBR_LINKS();
                    obj.LINK_CREATEDDBY = Int32.Parse(userid);
                    obj.LINK_DESCRIPTION = HttpUtility.HtmlEncode(text);
                    obj.LINK_IMAGEPATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_MODIFIEDBY = Int32.Parse(userid);
                    obj.LINK_PATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_SIGNUPID = Int32.Parse(userid);
                    obj.LINK_STATUS = 1;
                    obj.LINK_TABID = Int32.Parse(tabid);
                    obj.LINK_TITLE = file.FileName;
                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = HttpUtility.HtmlEncode(text.Substring(0, 250));
                    }
                    else
                    {
                        title = HttpUtility.HtmlEncode(text);
                    }
                    obj.LINK_TYPE = 6;
                    DataTable dt = BLL.set_link(obj);

              
                }
                else if (IssongFile(file.FileName))
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = context.Server.MapPath("~/uploads/" + fsmallpath + file.FileName);
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = file.FileName;
                    }
                    LBR_LINKS obj = new LBR_LINKS();
                    obj.LINK_CREATEDDBY = Int32.Parse(userid);
                    obj.LINK_DESCRIPTION = HttpUtility.HtmlEncode(text);
                    obj.LINK_IMAGEPATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_MODIFIEDBY = Int32.Parse(userid);
                    obj.LINK_PATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_SIGNUPID = Int32.Parse(userid);
                    obj.LINK_STATUS = 1;
                    obj.LINK_TABID = Int32.Parse(tabid);
                    obj.LINK_TITLE = file.FileName;
                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = HttpUtility.HtmlEncode(text.Substring(0, 250));
                    }
                    else
                    {
                        title = HttpUtility.HtmlEncode(text);
                    }
                    obj.LINK_TYPE = 7;
                    DataTable dt = BLL.set_link(obj);

                   
                }
                else if (IsvideoFile(file.FileName))
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = context.Server.MapPath("~/uploads/" + fsmallpath + file.FileName);
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = file.FileName;
                    }
                    LBR_LINKS obj = new LBR_LINKS();
                    obj.LINK_CREATEDDBY = Int32.Parse(userid);
                    obj.LINK_DESCRIPTION = HttpUtility.HtmlEncode(text);
                    obj.LINK_IMAGEPATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_MODIFIEDBY = Int32.Parse(userid);
                    obj.LINK_PATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_SIGNUPID = Int32.Parse(userid);
                    obj.LINK_STATUS = 1;
                    obj.LINK_TABID = Int32.Parse(tabid);
                    obj.LINK_TITLE = file.FileName;
                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = HttpUtility.HtmlEncode(text.Substring(0, 250));
                    }
                    else
                    {
                        title = HttpUtility.HtmlEncode(text);
                    }
                    obj.LINK_TYPE = 8;
                    DataTable dt = BLL.set_link(obj);

                }
                else  
                {
                    // System.Text.StringBuilder text = new System.Text.StringBuilder();

                    string text = "";

                    string path = context.Server.MapPath("~/uploads/" + fsmallpath + file.FileName);
                    // text = BLL.loadword(path);
                    if (text == "")
                    {
                        text = file.FileName;
                    }
                    LBR_LINKS obj = new LBR_LINKS();
                    obj.LINK_CREATEDDBY = Int32.Parse(userid);
                    obj.LINK_DESCRIPTION = HttpUtility.HtmlEncode(text);
                    obj.LINK_IMAGEPATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_MODIFIEDBY = Int32.Parse(userid);
                    obj.LINK_PATH = "../uploads/" + fsmallpath + "" + file.FileName + "";
                    obj.LINK_SIGNUPID = Int32.Parse(userid);
                    obj.LINK_STATUS = 1;
                    obj.LINK_TABID = Int32.Parse(tabid);
                    obj.LINK_TITLE = file.FileName;
                    string title = "";

                    if (text.Length >= 250)
                    {
                        title = HttpUtility.HtmlEncode(text.Substring(0, 250));
                    }
                    else
                    {
                        title = HttpUtility.HtmlEncode(text);
                    }
                    obj.LINK_TYPE = 9;
                    DataTable dt = BLL.set_link(obj);

             
                }
            }
        }
        
        context.Response.ContentType = "text/plain";
        context.Response.Write("File(s) Uploaded Successfully!");
    }
        catch(Exception ex)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Write("error"); 
    }
 
    }
 
    public bool IsReusable {
        get {
            return false;
        }
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
}