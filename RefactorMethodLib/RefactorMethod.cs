using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
namespace RefactorMethodLib
{
    public class RefactorMethod
    {
        public string DelParam(string str, string method, string parametr)
        {
            if (str.Contains(method))
            {
                string res = "", tmp = "";
                int start_index = 0, end_index = str.IndexOf(method, 0);
                string before = str.Substring(start_index, end_index);
                start_index = end_index + method.Length + 1;
                res += before + str.Substring(end_index, str.IndexOf('(', end_index) + 1 - end_index);
                end_index = str.IndexOf(")", end_index);
                tmp = str.Substring(start_index, end_index - start_index);

                if (tmp.Contains(","))
                {
                    string res_params = "", last_params = tmp;
                    int tmp_index_2 = tmp.IndexOf(',', 0);
                    for (int i = 0; i <= tmp.Count(c => c == ','); i++)
                    {

                        string current_param = last_params.Substring(0, tmp_index_2);
                        if (last_params.Length != tmp_index_2)
                        {
                            last_params = last_params.Substring(tmp_index_2);
                        }
                        if (!current_param.Contains(parametr))
                        {
                            if (res_params == "" && current_param[0] == ',') { res_params += current_param.Substring(1); }
                            else { res_params += current_param; }
                        }
                        tmp_index_2 = last_params.IndexOf(',', 1);
                        if (tmp_index_2 == -1) { tmp_index_2 = last_params.Length; }
                    }
                    res += res_params;
                }
                else
                {
                    if (!(tmp.Contains(parametr)))
                    {
                        res += tmp;
                    }
                }
                res += str.Substring(end_index);
                return res;
            }
            return str;
        }

        public string Rename(string str, string method, string new_name)
        {
            string pattern = @"([^/""]|^)\b("+method+@")([ (]+)";
            string rez = Regex.Replace(str, pattern, "$1"+new_name+"$3");

            return rez; 
        }
    }
}