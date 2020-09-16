using ImportadorContasAPagar.FileProcessor;
using ImportadorContasAPagar.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ImportadorContasAPagar
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly FileImport _fileImport;
        private readonly ContasAPagarRepository contasAPagarRepository;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, FileImport fileImport,
            ContasAPagarRepository contasAPagarRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _fileImport = fileImport;
            this.contasAPagarRepository = contasAPagarRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                while(_fileImport.ArquivosParaProcessar() > 0)
                {
                    try
                    {
                        var contas = _fileImport.LerArquivo();

                        foreach (var item in contas)
                        {
                            contasAPagarRepository.SalvarConta(item);
                            _logger.LogInformation(JsonSerializer.Serialize(item));
                        }

                        _fileImport.FinalizarArquivo();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"Erro durante o processamento: {e.Message}");
                        break;
                    }
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(Convert.ToInt32(_configuration["ServiceConfigurations:Intervalo"]), stoppingToken);
            }
        }
    }
}
