using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingLibrary.Models
{
	public class ActionHistory
	{
		public int Id { get; set; }
		public DateTime Time { get; set; }
		public string EventName { get; set; }
		public int IdUser { get; set; }

		public string GetTime
		{
			get
			{
				return Time.ToString("HH:mm"); 
			}
		}

		public string GetDate
		{
			get
			{
				return Time.ToString("dd.MM.yyyy");
			}
		}

		public string GetLogin
		{
			get
			{
				return CurrentUser.Login;
			}
		}
	}
}
