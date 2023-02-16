using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingLibrary
{
	public class PasswordWorking
	{
        public static string CreatePassword(int length)
        {
            const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();

            while (0 < length--)
            {
                res.Append(Valid[rnd.Next(Valid.Length)]);
            }

            return res.ToString();
        }
    }
}
