using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImportadorContasAPagar.Repository
{
    public class MongoConnection : IConnection
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        //public static MongoClient client;
        public MongoClient client;
        public IMongoDatabase db;

        public MongoConnection(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            client = new MongoClient(_configuration["ServiceConfigurations:ConnectionString"]);
            db = client.GetDatabase("DBContasAPagar");
        }

        public MongoClient ObterClient()
        {
            return client;
        }
        public IMongoDatabase ObterBanco()
        {
            return db;
        }
    }
}
