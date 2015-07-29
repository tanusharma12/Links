using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LINKSKARTNEW.Code;

namespace LINKSKARTNEW.Code
{
    public enum operation
    {
        Select,
        Insert,
        Update,
        Check,
        Delete,
        Empty,
        SelectAll,
        select,
        get_structure,
        Commit,
        checkemail,
        Start1,
        SelectUserID,
        GET_ALLPOSTS,
        SELECTUSERID,
        SELECTUSEREMAIL
    }

    public class SMVTS_MAIN
    {
        private int _CREATEDBY;

        public int CREATEDBY
        {
            get
            {
                return this._CREATEDBY;
            }
            set
            {
                this._CREATEDBY = value;
            }
        }

        private DateTime _CREATEDDATE;

        public DateTime CREATEDDATE
        {
            get
            {
                return this._CREATEDDATE;
            }
            set
            {
                this._CREATEDDATE = value;
            }
        }

        private int _LASTMDFBY;

        public int LASTMDFBY
        {
            get
            {
                return this._LASTMDFBY;
            }
            set
            {
                this._LASTMDFBY = value;
            }
        }

        private DateTime _LASTMDFDATE;

        public DateTime LASTMDFDATE
        {
            get
            {
                return (this._LASTMDFDATE);
            }
            set
            {
                this._LASTMDFDATE = value;
            }
        }

        private bool? _USERS_STATUS;

        public bool? USERS_STATUS
        {
            get
            {
                return (this._USERS_STATUS);
            }
            set
            {
                this._USERS_STATUS = value;
            }
        }

        private operation _OPERATION;

        public operation OPERATION
        {
            get
            {
                return (this._OPERATION);
            }
            set
            {
                this._OPERATION = value;
            }
        }

        private DateTime? _STARTDATE;

        public DateTime? STARTDATE
        {
            get
            {
                return (this._STARTDATE);
            }
            set
            {
                this._STARTDATE = value;
            }
        }

        private DateTime? _ENDDATE;

        public DateTime? ENDDATE
        {
            get
            {
                return (this._ENDDATE);
            }
            set
            {
                this._ENDDATE = value;
            }
        }
        //private int _USER_ID;
        //public int USER_ID
        //{
        //    get { return (this._USER_ID); }
        //    set { this._USER_ID = value; }
        //}
    }
    public class FaceBookUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PictureUrl { get; set; }
        public string Email { get; set; }
    }
    public class GoogleProfile
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public Image Image { get; set; }
        public List<Email> Emails { get; set; }
        public string Gender { get; set; }
        public string ObjectType { get; set; }
    }

    public class Email
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class Image
    {
        public string Url { get; set; }
    }
    public class LBR_SIGNUP : SMVTS_MAIN
    {
        public int LBR_ID { get; set; }

        public string LBR_FIRSTNAME { get; set; }
        public string LBR_LASTNAME { get; set; }
        public string LBR_EMAILID { get; set; }
        public string LBR_PASSWORD { get; set; }
        public string LBR_PHONENUMBER { get; set; }
        public string LBR_ADDRESS { get; set; }
        public int LBR_CREATEDBY { get; set; }
        public DateTime? LBR_CREATEDDATE { get; set; }
        public int LBR_MODIFIEDBY { get; set; }
        public DateTime? LBR_MODIFIEDDATE { get; set; }
        public int LBR_MODIFIEDTYPE { get; set; }
        public int LBR_LOGINTYPE { get; set; }
        public string LBR_IMAGEURL { get; set; }
        public string LBR_LOGINID { get; set; }
    }
    public class LBR_LINKS : SMVTS_MAIN
    {
        public int LINK_ID { get; set; }
        public string LINK_PATH { get; set; }
        public string LINK_TITLE { get; set; }
        public string LINK_DESCRIPTION { get; set; }
        public int LINK_TYPE { get; set; }
        public string LINK_DATA { get; set; }
        public int LINK_STATUS { get; set; }
        public DateTime? LINK_CREATEDDATE { get; set; }
        public int LINK_MODIFIEDBY { get; set; }
        public int LINK_CREATEDDBY { get; set; }
        public DateTime? LINK_MODIFIEDDATE { get; set; }
        public string LINK_IMAGEPATH { get; set; }
        public int LINK_SIGNUPID { get; set; }
        public int LINK_TABID { get; set; }
    }
}