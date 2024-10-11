using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReservaRefeicao.Model
{
    public class Reserva
    {

        public string CodReserva { get; set; }
        public Funcionario Repreg { get; set; }
        public Refeicao CodRefeicao { get; set; }
        public DateTime DataReserva { get; set; }
    }
}