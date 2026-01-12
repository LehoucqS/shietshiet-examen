using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Data
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
