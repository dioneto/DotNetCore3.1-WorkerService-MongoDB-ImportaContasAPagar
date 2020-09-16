using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImportadorContasAPagar.Repository
{
    public class ContasAPagarRepository
    {
        private readonly ILogger<Worker> _logger;
        public IMongoDatabase db;
        private IMongoCollection<ContasAPagar> contas;

        public ContasAPagarRepository(ILogger<Worker> logger, IConnection connection)
        {
            _logger = logger;

            db = connection.ObterBanco();
            contas = db.GetCollection<ContasAPagar>("Contas");
        }
        public void SalvarConta(ContasAPagar conta)
        {
            contas.InsertOneAsync(conta);
        }
    }
}
