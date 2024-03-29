﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP.Model
{
    public class UserLoginState
    {
        public UserLoginState()
        { }
        #region Model
        private string _userid;
        private string _ipnum;
        private string _lastlogintime;
        private string _loginplace;
        private string _loginnum;
        /// <summary>
        /// 
        /// </summary>
        public string userId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ipNum
        {
            set { _ipnum = value; }
            get { return _ipnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastLoginTime
        {
            set { _lastlogintime = value; }
            get { return _lastlogintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string loginPlace
        {
            set { _loginplace = value; }
            get { return _loginplace; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string loginNum
        {
            set { _loginnum = value; }
            get { return _loginnum; }
        }
        #endregion Model
    }
}