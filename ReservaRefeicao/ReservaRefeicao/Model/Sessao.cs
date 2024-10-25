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
        public List<Reserva> ReservasSemana { get; private set; }
        private Timer _timer;
        private const int TempoLimiteInatividade = 50000; // 60 segundos
        private bool _sessaoCarregada;

        public event Action SessaoEncerrada;

        public Sessao()
        {
            _timer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
        }


        // Método chamado quando o tempo de inatividade for atingido
        private void TimerCallback(object state)
        {
            if (_sessaoCarregada) 
                EncerrarSessao();
        }

        public bool IniciarSessao(Funcionario funcionario, List<Reserva> reservasSemana)
        {
            if (funcionario != null)
            {
                FuncionarioAtual = funcionario;
                ReservasSemana = reservasSemana;
                ResetarTimer();
                _sessaoCarregada = false;
                return true;
            }
            return false;
        }

        public void IniciarTimer()
        {
            // Inicia o timer
            _sessaoCarregada = true;
            _timer.Change(TempoLimiteInatividade, Timeout.Infinite);
        }

        public async Task EncerrarSessao()
        {
            FuncionarioAtual = null;
            _sessaoCarregada = false;
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
