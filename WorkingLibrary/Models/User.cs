using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingLibrary.Models
{
	public class User
	{
		public int Id;
		public string SurName;
		public string Name;
		public string Patronymic;
		public string Login;
		public string Password;
		public string Phone;
		public string Mail;
		public byte[] Image;
		public int Role;
		public bool Ban;
		public string Description;

		public string GetName
		{
			get
			{
				return Name;
			}
		}

		public string GetLogin
		{
			get
			{
				return Login;
			}
		}

		public string GetMail
		{
			get
			{
				return Mail;
			}
		}

		public string GetRole
		{
			get
			{
				return Convert.ToString(Role);
			}
		}

		public string GetBan
		{
			get
			{
				return Ban.ToString();
			}
		}
	}
}
