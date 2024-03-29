﻿using System;
namespace CP.Model
{
	/// <summary>
	/// SignUpInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SignUpInfo
	{
		public SignUpInfo()
		{}
		#region Model
		private string _newsubmitid;
		private string _newusername;
		private string _newusersex;
		private string _newschooldepartment;
		private string _newjoincp;
		private string _newsubmittime;
		private string _newphonenumber;
		private string _newphotoaddress;
		private string _newmajor;
		private string _newgrade;
		private string _newclasswide;
		private string _newremark;
		private string _newstatus;
		/// <summary>
		/// 
		/// </summary>
		public string newSubmitId
		{
			set{ _newsubmitid=value;}
			get{return _newsubmitid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newUserName
		{
			set{ _newusername=value;}
			get{return _newusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newUserSex
		{
			set{ _newusersex=value;}
			get{return _newusersex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newSchoolDepartment
		{
			set{ _newschooldepartment=value;}
			get{return _newschooldepartment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newJoinCp
		{
			set{ _newjoincp=value;}
			get{return _newjoincp;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newSubmitTime
		{
			set{ _newsubmittime=value;}
			get{return _newsubmittime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newPhoneNumber
		{
			set{ _newphonenumber=value;}
			get{return _newphonenumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newPhotoAddress
		{
			set{ _newphotoaddress=value;}
			get{return _newphotoaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newMajor
		{
			set{ _newmajor=value;}
			get{return _newmajor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newGrade
		{
			set{ _newgrade=value;}
			get{return _newgrade;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newClassWide
		{
			set{ _newclasswide=value;}
			get{return _newclasswide;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newRemark
		{
			set{ _newremark=value;}
			get{return _newremark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string newStatus
		{
			set{ _newstatus=value;}
			get{return _newstatus;}
		}
		#endregion Model

	}
}

