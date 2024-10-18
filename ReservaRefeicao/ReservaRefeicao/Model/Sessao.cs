using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ReservaRefeicao.Model
{
    public class Sessao
    {
        public Funcionario FuncionarioAtual { get; private set; }
        private Timer _timer;
        private const int TempoLimiteInatividade = 60000; // 60 segundos

        public event Action SessaoEncerrada;

        public Sessao()
        {
            _timer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
        }


        // Método chamado quando o tempo de inatividade for atingido
        private void TimerCallback(object state)
        {
            EncerrarSessao();
        }

        public bool IniciarSessao(Funcionario funcionario)
        {
            if (funcionario != null)
            {
                FuncionarioAtual = funcionario;
                ResetarTimer();
                return true;
            }
            return false;
        }

        public void EncerrarSessao()
        {
            FuncionarioAtual = null;
            _timer.Change(Timeout.Infinite, Timeout.Infinite); // Para o timer
            SessaoEncerrada?.Invoke(); // Disparar evento para retornar à tela inicial
        }

        public void ResetarTimer()
        {
            // Reinicia o timer sempre que houver uma ação do usuário
            _timer.Change(TempoLimiteInatividade, Timeout.Infinite);
        }
    }
}
