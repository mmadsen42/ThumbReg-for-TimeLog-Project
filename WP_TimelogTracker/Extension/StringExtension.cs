using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThumbReg.Extension
{
    public static class StringExtension
    {
        public static string MaxLength(this string s, int maxlength)
        {
            return s.Length > maxlength ? s.Substring(0, maxlength) : s;
        }
    }
}
