using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace tourism_club.Functions
{
    public class Parse
    {
        public static void ParseFrame(string param, string value)
        {
            string reg = "param=\\\"(value)\\\"";
            Regex regex = new Regex(reg);

        }
    }
}
