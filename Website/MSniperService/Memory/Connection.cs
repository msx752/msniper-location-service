using MemoryManaging;
using System;
using System.Collections.Generic;

namespace MSniperService.Models
{
    public class Connection : IStoreValue
    {
        public string ConnectionId { get; private set; }

        public Dictionary<string, object> Caches { get; private set; }

        public Connection(string conectionId)
        {
            ConnectionId = conectionId;
            Caches = new Dictionary<string, object>();
        }

        public void Dispose()
        {
            ConnectionId = "";
            GC.SuppressFinalize(this);
        }
    }
}