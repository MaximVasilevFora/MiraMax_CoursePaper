using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WorkingLibrary
{
	public class MailWorking
	{
		public static bool CheckMail(string value)
		{
            if (!string.IsNullOrEmpty(value?.Trim()))
            {
                const string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                var email = value.Trim().ToLowerInvariant();

                if (Regex.Match(email, pattern).Success)
                {
                    return true;
                }
            }

            return false;
        }
	}
}
