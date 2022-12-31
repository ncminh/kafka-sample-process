using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample3.Models
{
    /// <summary>
    /// Records from database that have clientid.
    /// </summary>
    public class ClientModel
    {
        readonly string _id;
        public string Id => _id;

        readonly string _ts;
        public string TimeStamp => _ts;

        public ClientModel(int id)
        {
            _id = id.ToString("00#");
            _ts = DateTime.Now.AddSeconds(id).ToString("yyyyMMdd_HHmmss");
        }
    }
}
