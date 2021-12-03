using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using tourism_club.Models;

namespace tourism_club.Functions
{
    public class Parse
    {
        public static string ParseFrame(string param, string text)
        {
            string reg =$@"{param}=\""(\s*\S*)\""";
            Regex regex = new Regex(reg, RegexOptions.IgnoreCase);

            MatchCollection matches = regex.Matches(text);
            string t = null;
            foreach(Match match in matches)
            {
                if (match.Groups[1].Value != null)
                {
                    return match.Groups[1].Value;
                }
            }

            return " ";
        }
        public static Frame returnFrame(string text)
        {
            Frame frame = new Frame();
            frame.source = ParseFrame("src", text);
            frame.width = ParseFrame("width", text);
            frame.height = ParseFrame("height", text);
            frame.style = ParseFrame("style", text);
            frame.screen = ParseFrame("allowfullscreen", text);
            frame.loading = ParseFrame("loading", text);
            return frame;
        }

    }
}
