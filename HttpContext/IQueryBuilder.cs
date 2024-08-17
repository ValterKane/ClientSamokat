using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSamokat.HttpContext
{
    public interface IQueryBuilder
    {
        public string GetQueryString(KeyValuePair<string, string>[] QueryParams, string NameOfConnection);

        public string GetRESTString(string param, string NameOfConnection);

    }
}
