using System;
using System.Collections.Generic;
using System.Text;
using TinyCsvParser.Mapping;

namespace ImportadorContasAPagar
{
    public class CsvContasAPagarMapping : CsvMapping<ContasAPagar>
    {
        public CsvContasAPagarMapping()
       : base()
        {
            MapProperty(0, x => x.Data);
            MapProperty(1, x => x.Competencia);
            MapProperty(2, x => x.Parcela);
            MapProperty(3, x => x.Categoria);
            MapProperty(4, x => x.Descricao);
            MapProperty(5, x => x.Valor);
        }
    }
}
