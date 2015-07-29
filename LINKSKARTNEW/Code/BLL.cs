using System.Collections.Generic;
using System.Web.Security;
using Microsoft.Win32;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using LINKSKARTNEW.Code;
namespace LINKSKARTNEW.Code
{
    public class BLL
    {
        #region Messages

        public const string msg_Saved = "Information saved successfully";
        public const string msg_Updated = "Information updated successfully";
        public const string msg_UnSaved = "Information not saved";
        public const string msg_NotUpdated = "Information not Updated";
        public const string msg_ImageNotUploaded = "Please Upload Image";
        public const string msg_ImageRestrict = "Only .jpeg and .png files are allowed!";

        public string MyProperty { get; set; }

        #endregion

        #region crypto  Methods to Encrypt and Decrypt

        static string myKey = "3d5900ae-111a-45be-96b3-d9e4606ca793";
        static TripleDESCryptoServiceProvider cryptDES3 = new TripleDESCryptoServiceProvider();
        static MD5CryptoServiceProvider cryptMD5Hash = new MD5CryptoServiceProvider();

        internal static string Decrypt(string myString)
        {
            cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey));
            cryptDES3.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = cryptDES3.CreateDecryptor();
            byte[] buff = Convert.FromBase64String(myString);
            return ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
        }

        internal static string Encrypt(string myString)
        {
            cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(myKey));
            cryptDES3.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = cryptDES3.CreateEncryptor();
            object MyASCIIEncoding = new ASCIIEncoding();
            byte[] buff = ASCIIEncoding.ASCII.GetBytes(myString);
            return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
        }

        #endregion

        internal static DataTable ExecuteQuery(string Query)
        {
            return Dal.ExecuteQuery(Query);
        }


        internal static bool ExecuteNonQuery(string Query)
        {
            return Dal.ExecuteNonQuery(Query);
        }
        internal static void ShowMessage(Control ctrl, string Msg)
        {
            ScriptManager.RegisterStartupScript(ctrl, ctrl.GetType(), Guid.NewGuid().ToString(), "alert('" + Msg + "');", true);
        }

        internal static string ReplaceQuote(string str)
        {
            return str.Replace("'", "''");
        }

        public static DataTable createuser(LBR_SIGNUP obj)
        {

            DataTable status = BLL.ExecuteQuery("EXEC USP_LBR_SIGNUP @OPERATION='signup',@LBR_FIRSTNAME='" + obj.LBR_FIRSTNAME + "',@LBR_LASTNAME='" + obj.LBR_LASTNAME + "',@LBR_EMAILID='" + obj.LBR_EMAILID + "',@LBR_PASSWORD='" + BLL.Encrypt(obj.LBR_PASSWORD) + "',@LBR_PHONENUMBER='" + obj.LBR_PHONENUMBER + "',@LBR_ADDRESS='" + obj.LBR_ADDRESS + "',@LBR_CREATEDBY=1,@LBR_MODIFIEDBY=1,@LBR_MODIFIEDTYPE=1,@LBR_LOGINTYPE='" + obj.LBR_LOGINTYPE + "'");
            return status;
        }
        public static DataTable checkemail(LBR_SIGNUP obj)
        {

            DataTable dt = BLL.ExecuteQuery("EXEC USP_LBR_SIGNUP @OPERATION='checkemailid',@LBR_EMAILID='" + obj.LBR_EMAILID + "',@LBR_LOGINTYPE="+ obj.LBR_LOGINTYPE +"");
            return dt;
        }
        public static DataTable checklogin(LBR_SIGNUP obj)
        {
            DataTable dt_user = new DataTable();
            dt_user = BLL.ExecuteQuery("exec USP_LBR_SIGNUP @OPERATION='checklogin',@LBR_EMAILID='" + obj.LBR_EMAILID + "',@LBR_PASSWORD='" + obj.LBR_PASSWORD + "',@LBR_LOGINTYPE='" + obj.LBR_LOGINTYPE + "'");
            return dt_user;
        }
        public static bool setdefaulttabs(LBR_SIGNUP obj)
        {
            bool status = BLL.ExecuteNonQuery("exec USP_LBR_SIGNUP @OPERATION='INSERTDEFAULTTABS',@LBR_CREATEDBY=" + obj.LBR_CREATEDBY + ",@LBR_MODIFIEDBY=" + obj.LBR_MODIFIEDBY + ",@LBR_ID="+ obj.LBR_ID +"");
            return status;
        }
        public static DataTable get_tabs(LBR_SIGNUP obj)
        {
            DataTable dt = BLL.ExecuteQuery("exec USP_LBR_SIGNUP @OPERATION='LOADTABS', @LBR_ID='" + obj.LBR_ID + "'");
       
            return dt;
        }
        public static DataTable set_link(LBR_LINKS obj)
        {
            DataTable dt = BLL.ExecuteQuery("exec USP_LBR_SIGNUP @OPERATION='INSERTLINK',@LINK_PATH='" + obj.LINK_PATH + "',@LINK_TITLE='" + obj.LINK_TITLE + "',@LINK_DESCRIPTION='" + obj.LINK_DESCRIPTION + "',@LINK_TYPE='" + obj.LINK_TYPE + "',@LINK_STATUS='" + obj.LINK_STATUS + "',@LINK_CREATEDBY='" + obj.LINK_CREATEDDBY + "',@LINK_MODIFIEDBY='" + obj.LINK_MODIFIEDBY + "',@LINK_IMAGEPATH='" + obj.LINK_IMAGEPATH + "',@LINK_TABID='" + obj.LINK_TABID + "',@LINK_SIGNUPID='"+ obj.LINK_SIGNUPID +"'");
            return dt;
        }
        public static DataTable get_links(LBR_LINKS obj)
        {
            DataTable dt = BLL.ExecuteQuery("exec USP_LBR_SIGNUP @OPERATION='GETLINKS',@LINK_TABID='" + obj.LINK_TABID + "',@LINK_SIGNUPID='"+ obj.LINK_SIGNUPID +"'");
            return dt;
        }

        public static bool updatedata(string htmlcontent,string id)
        {
            bool status = BLL.ExecuteNonQuery("update lbr_links set link_data='" + BLL.ReplaceQuote(htmlcontent) + "' where link_id='" + id + "'");
            return status;
        }
        public static string loadword(string path)
        {
            string text = "";
            try
            {
                
                object filenamenew = path;
                Microsoft.Office.Interop.Word.Application AC = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
                object readOnly = false;
                object isVisible = true;
                object missing = System.Reflection.Missing.Value;
                try
                {
                    doc = AC.Documents.Open(ref filenamenew, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible);
                    text = doc.Content.Text;
                    AC.Documents.Close();
                }
                catch (Exception ex)
                {
                    text = "";
                }
            }
            catch(Exception ex)
            {
                text="";
            }
            return text;
        }

        public static DataTable loadsearchresults(string searchkey,string signup)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = BLL.ExecuteQuery("exec USP_LBR_SIGNUP @operation='search',@searchkey='" + searchkey + "',@LINK_SIGNUPID=" + signup + "");

            
            }
            catch(Exception ex)
            {

            }
            return dt;
        }

       
    }
}