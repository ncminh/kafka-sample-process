using Sample3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample3.Interfaces
{
    public interface IHttpClientFaker
    {
        IEnumerable<ClientModel> GetClients(int numOfClients);
    }
}
