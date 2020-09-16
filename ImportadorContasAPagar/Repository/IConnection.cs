using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImportadorContasAPagar.Repository
{
    public interface IConnection
    {
        public MongoClient ObterClient();
        public IMongoDatabase ObterBanco();
    }
}
