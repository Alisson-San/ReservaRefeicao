using ReservaRefeicao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRefeicao.Controllers
{
    public class Session
    {
        public Funcionario funcionario { get; private set; }

        public Session(Funcionario funcionario)
        {
            this.funcionario = funcionario;
        }


    }
}
