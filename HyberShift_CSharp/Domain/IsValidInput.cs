using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HyberShift_CSharp
{
    public static class IsValidInput
    {
        // Kiểm tra định dạng Email
        public static bool isValidEmail(string value)
        {
            if (value.Trim().Length == 0)
                return false;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(value))
                return true;
            else
                return false;
        }

        // Kiểm tra định dạng Phone
        public static bool IsValidPhone(string value)
        {
            string strRegex = @"^-*[0-9,\.?\-?\(?\)?\ ]+$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(value))
                return true;
            else
                return false;
        }

        // Kiểm tra Name
        public static bool IsValidFullName(string value)
        {
            if (value.Trim().Length < 3)
                return false;
            else
                return true;
        }

        // Kiểm tra Password
        public static bool IsValidPassword(string value)
        {
            if (value.Trim().Length < 6)
                return false;
            else
                return true;
        }
    }
}
