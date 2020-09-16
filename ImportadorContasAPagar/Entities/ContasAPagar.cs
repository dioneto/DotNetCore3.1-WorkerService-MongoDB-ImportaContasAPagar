using System;
using System.Collections.Generic;
using System.Text;

namespace ImportadorContasAPagar
{
    public class ContasAPagar
    {
        public DateTime Data { get; set; }
        public string Competencia { get; set; }
        public string Parcela { get; set; }
        public string Categoria { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
