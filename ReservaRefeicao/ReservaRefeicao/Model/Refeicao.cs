using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRefeicao.Model
{
    public class Refeicao
    {
        public string CodRefeicao { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }

    }
}
