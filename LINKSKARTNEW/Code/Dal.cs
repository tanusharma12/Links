using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LINKSKARTNEW.Code
{
    public class Dal
    {
        static string strConn = BLL.Decrypt(ConfigurationManager.ConnectionStrings["connection"].ToString());
        //static string strConn_PROD = BLL.Decrypt(ConfigurationManager.ConnectionStrings["connection_PROD"].ToString());
        //static string strConn_TEST = BLL.Decrypt(ConfigurationManager.ConnectionStrings["connection_TEST"].ToString());
        //static string strConn_DEVDB = BLL.Decrypt(ConfigurationManager.ConnectionStrings["connection_DEVDB"].ToString());
        //static string strConn_LIVETEST = BLL.Decrypt(ConfigurationManager.ConnectionStrings["connection_LIVETEST"].ToString());

        //static string strConn;


        public static DataTable ExecuteQuery(string Query)
        {
            // SMVTS_TRACE_ERROR_LOG _obj = new SMVTS_TRACE_ERROR_LOG();
            try
            {

                //_obj.STRSQL = Convert.ToString(Query).Replace("'","''");
                //_obj.STARTDATE = System.DateTime.Now;
                //return SqlHelper.ExecuteDataset(strConn, CommandType.Text, Query).Tables[0];
                DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.Text, Query).Tables[0];
                //_obj.ENDDATE = System.DateTime.Now;
                //_obj.USERID = "0";
                //_obj.OPERATION = operation.Insert;
                //BLL.set_TraceLog_ErrorLog(_obj);


                return dt;
            }
            catch (Exception)
            {
                //_obj.ERR_DESC = Convert.ToString(Query).Replace("'", "''");
                //_obj.STARTDATE = System.DateTime.Now;
                //_obj.ERR_DESC = ex.Message;
                //  DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.Text, Query).Tables[0];
                //_obj.ENDDATE = System.DateTime.Now;
                //_obj.USERID = "0";
                //_obj.OPERATION = operation.Update;
                //BLL.set_TraceLog_ErrorLog(_obj);
                return null;
            }
        }

        public static bool ExecuteNonQuery(string Query)
        {
            // SMVTS_TRACE_ERROR_LOG _obj1 = new SMVTS_TRACE_ERROR_LOG();
            try
            {

                //    _obj1.STRSQL = Convert.ToString(Query).Replace("'", "''");
                //   _obj1.STARTDATE = System.DateTime.Now;
                //return SqlHelper.ExecuteDataset(strConn, CommandType.Text, Query).Tables[0];
                bool strAns = Convert.ToBoolean(SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, Query));
                //  _obj1.ENDDATE = System.DateTime.Now;
                //   _obj1.USERID = "0";
                //   _obj1.OPERATION = operation.Insert;
                //   BLL.set_TraceLog_ErrorLog(_obj1);


                return strAns;
            }
            catch (Exception)
            {
                //    _obj1.STRSQL = Convert.ToString(Query).Replace("'", "''");
                //   _obj1.ERR_DESC = ex.Message;
                //   _obj1.STARTDATE = System.DateTime.Now;
                //  DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.Text, Query).Tables[0];
                //    _obj1.ENDDATE = System.DateTime.Now;
                //    _obj1.USERID = "0";
                //    _obj1.OPERATION = operation.Update;
                //   BLL.set_TraceLog_ErrorLog(_obj1);
                return false;
            }

        }

    }



}