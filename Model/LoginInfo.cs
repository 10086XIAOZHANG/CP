﻿using System;
namespace CP.Model
{
	/// <summary>
	/// LoginInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public partial class LoginInfo
	{
		public LoginInfo()
		{}
		#region Model
		private string _userid;
		private string _phonenumber;
		private string _userpwd;
		/// <summary>
		/// 
		/// </summary>
		public string UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string phoneNumber
		{
			set{ _phonenumber=value;}
			get{return _phonenumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string userPwd
		{
			set{ _userpwd=value;}
			get{return _userpwd;}
		}
		#endregion Model

	}
}

