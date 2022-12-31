using Sample3.Interfaces;
using Sample3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample3
{
    internal class HttpClientFaker : IHttpClientFaker
    {
        public IEnumerable<ClientModel> GetClients(int numOfClients)
        {
            var clients = new List<ClientModel>();

            for(var i = 1; i <= numOfClients; i++)  clients.Add(new ClientModel(i));

            return clients;
        }
    }
}
