using MemoryManaging;
using MSniperService.Enums;
using System;
using System.Collections.Generic;

namespace MSniperService.Models
{
    public class Connection : IStoreValue
    {
        public string ConnectionId { get; private set; }
        public HubType Type { get; private set; }
        public Dictionary<string, object> Cache { get; private set; }

        public Connection(string conectionId, HubType htyp)
        {
            ConnectionId = conectionId;
            PrimaryKey = ConnectionId;
            Type = htyp;
        }

        public void Dispose()
        {
            PrimaryKey = null;
            ConnectionId = null;
            GC.SuppressFinalize(this);
        }

        ~Connection()
        {
            Dispose();
        }

        public string PrimaryKey { get; set; }
    }
}