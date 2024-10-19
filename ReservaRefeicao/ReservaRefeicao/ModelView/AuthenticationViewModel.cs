using ReservaRefeicao.Model;
using ReservaRefeicao.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReservaRefeicao.ModelView
{
    public class AuthenticationViewModel : ViewModelBase
    {
        private int _codigoFuncionario;
        public int CodigoFuncionario
        {
            get => _codigoFuncionario;
            set => SetProperty(ref _codigoFuncionario, value);
        }

        // Propriedade que controla se a autenticação está em andamento
        private bool _isAuthenticating;
        public bool IsAuthenticating
        {
            get => _isAuthenticating;
            set => SetProperty(ref _isAuthenticating, value);
        }

        private readonly GestorSessaoService _gestorDeSessao;
        private readonly Sessao _sessaoUsuario;

        public ICommand AutenticarCommand { get; }

        public AuthenticationViewModel(GestorSessaoService gestorDeReserva, Sessao sessaoUsuario)
        {
            _gestorDeSessao = gestorDeReserva;
            _sessaoUsuario = sessaoUsuario;

            AutenticarCommand = new Command(async () => await Autenticar());
        }

        public async Task Autenticar()
        {
            // Lógica de autenticação, usando _gestorDeReserva
            var funcionario = await _gestorDeSessao.ObterFuncionarioPorCodigo(CodigoFuncionario);

            if (funcionario != null)
            {
                _sessaoUsuario.IniciarSessao(funcionario);
                await Shell.Current.GoToAsync($"//CardapioView?nomeFuncionario={funcionario.Nome}");
            }
            else
            {
                // Exibir erro
            }
        }
    }
}
