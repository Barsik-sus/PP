using System;
using System.Text.RegularExpressions;
namespace RefactorMethodLib
{
    public class RefactorMethod
    {
        public string DelParam(string str, string method, string parametr)
        {
            string pattern = "\b(qwe)";
            string replacement = "qwe";
            string rez = Regex.Replace(str, pattern, replacement);
            return "";
        }

        public string Rename(string str, string method, string new_name)
        {   
            string pattern = @"([^/""]|^)\b("+method+@")[ (]+";
            //string replacement = "Tested(";
            string rez = Regex.Replace(str, pattern, new_name+"(");

            return rez; 
        }
    }
}