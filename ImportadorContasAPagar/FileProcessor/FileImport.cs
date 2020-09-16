using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace ImportadorContasAPagar.FileProcessor
{
    public class FileImport
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private string pathIn;
        private string pathOut;
        private bool arquivoAberto;
        public FileImport(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            pathIn = _configuration["Arquivos:A_Processar"];
            pathOut = _configuration["Arquivos:Processado"];
            arquivoAberto = false;
        }

        public int ArquivosParaProcessar()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(pathIn);

            return directoryInfo.GetFiles().Length;
        }
        public List<ContasAPagar> LerArquivo()
        {
            if (arquivoAberto) throw new Exception("Finalizar arquivo antes da leitura do próximo arquivo");

            DirectoryInfo directoryInfo = new DirectoryInfo(pathIn);
            var arquivo = directoryInfo.GetFiles()[0].Name;

            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');
            CsvContasAPagarMapping csvMapper = new CsvContasAPagarMapping();
            CsvParser<ContasAPagar> csvParser = new CsvParser<ContasAPagar>(csvParserOptions, csvMapper);
            var result = csvParser
                         .ReadFromFile(pathIn + arquivo, Encoding.ASCII)
                         .ToList();

            arquivoAberto = true;

            return result.Select(x => x.Result).ToList();
        }
        public void FinalizarArquivo()
        {
            if (!arquivoAberto) throw new Exception("Nenhum arquivo pendente de finalização");

            DirectoryInfo directoryInfo = new DirectoryInfo(pathIn);
            var arquivo = directoryInfo.GetFiles()[0].Name;

            File.Move(pathIn + arquivo, pathOut + arquivo);

            arquivoAberto = false;
        }
    }
}
