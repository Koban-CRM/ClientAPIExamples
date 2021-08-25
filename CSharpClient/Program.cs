using CSharpClient.Service;
using NoCRM.Api.Client;
using NoCRM.Api.ClientStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string _apikey = "YOUR_KOBAN_API_KEY";
            string _apiuri = "YOUR_KOBAN_SERVER";
            string _username = "YOUR_KOBAN_USER_KEY";
            ncToken tk = new ncToken() { ApiKey = _apikey, ApiUri = _apiuri, Username = _username };
            ThirdService tsv = new ThirdService();

            // Retrieve Accounts created or updated since 6 months
            List<ncThird> thirds = tsv.GetUpdatedAccounts(tk, DateTime.Now.AddMonths(-6));

            // Post 2 new accounts to Koban
            ncThird t1 = new ncThird() { Extcode = "NEWACCOUNT1", Label = "New Account 1", EMail = "contact@newaccount1.fr" };
            ncThird t2 = new ncThird() { Extcode = "NEWACCOUNT2", Label = "New Account 2", EMail = "contact@newaccount2.fr" };

            List<ncThird> newthirds = new List<ncThird>();
            newthirds.Add(t1);
            newthirds.Add(t2);

            List<string> res = tsv.Add(tk, newthirds);
            t1.Guid = res[0];
            t2.Guid = res[1];
        }
    }
}
