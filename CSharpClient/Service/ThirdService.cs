using NoCRM.Api.Client;
using NoCRM.Api.ClientStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpClient.Service
{
    public class ThirdService
    {
        /// <summary>
        /// Get Updated accounts since a date.
        /// </summary>
        /// <param name="tk">Koban API Token</param>
        /// <param name="since">Reference Date</param>
        /// <param name="key">Koban Key (Default extcode)</param>
        /// <returns></returns>
        public List<ncThird> GetUpdatedAccounts(ncToken tk, DateTime since, string key = "Extcode")
        {
            List<ncThird> res = new List<ncThird>();
            int startindex = 0;
            int maxlength = 0;
            int paginationlength = 20;

            ThirdRequest tr = new ThirdRequest(tk);
            ncResultList<ncThird> kbnresult = tr.ListUpdated(since, "", paginationlength, startindex);
            maxlength = kbnresult.Length;
            res.AddRange(kbnresult.List);
            startindex = startindex + paginationlength;
            while(startindex < maxlength)
            {
                kbnresult = tr.ListUpdated(since, "", paginationlength, startindex);
                res.AddRange(kbnresult.List);
                startindex = startindex + paginationlength;
            }

            return res;
        }

        /// <summary>
        /// Post several thirds to Koban
        /// </summary>
        /// <param name="tk">Koban Token</param>
        /// <param name="newthirds">Thirds to post</param>
        /// <param name="key">Koban Key (Default extcode)</param>
        public List<string> Add(ncToken tk, List<ncThird> newthirds, string key = "Extcode")
        {
            ThirdRequest tr = new ThirdRequest(tk);
            ncResult<List<string>> res = tr.BulkImport(newthirds, key);
            if (res.Errors != null && res.Errors.Count > 0)
                throw new Exception("Koban Post Error");
            else
            {
                return res.Result;
            }
        }
    }
}
