using Microsoft.EntityFrameworkCore;
using ReservaRefeicao.Model;
using ReservaRefeicao.Services;
using ReservaRefeicao.Views;
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
        private int? _codigoFuncionario;
        public int? CodigoFuncionario
        {
            get => _codigoFuncionario;
            set => SetProperty(ref _codigoFuncionario, value);
        }

        public string CurrentDate { get; set; } = DateTime.Now.ToString("D");

        public List<Refeicao> CardapioDoDia
        {
            get => _cardapioDoDia;
            set
            {
                _cardapioDoDia = value;
                OnPropertyChanged();
            }
        }
        // Propriedade que controla se a autenticação está em andamento
        private bool _isAuthenticating;
        public bool IsAuthenticating
        {
            get => _isAuthenticating;
            set => SetProperty(ref _isAuthenticating, value);
        }

        private readonly GestorSessaoService _gestorDeSessao;
        private readonly GestorCardapioService _gestorCardapio;
        private readonly Sessao _sessaoUsuario;
        private List<Refeicao> _cardapioDoDia;

        public ICommand AutenticarCommand { get; }

        public AuthenticationViewModel(GestorSessaoService gestorDeReserva, GestorCardapioService gestoCardapioService,Sessao sessaoUsuario)
        {
            _gestorDeSessao = gestorDeReserva;
            _sessaoUsuario = sessaoUsuario;
            _gestorCardapio = gestoCardapioService;
            _codigoFuncionario = null;
            CarregarCardapioDoDia();

            AutenticarCommand = new Command(async () => await Autenticar());
        }

        private void CarregarCardapioDoDia()
        {
            CardapioDoDia = _gestorCardapio.ObterCardapioDoDia();
            Console.WriteLine(CardapioDoDia.Count);
        }

        public async Task Autenticar()
        {
            // Lógica de autenticação, usando _gestorDeReserva
            
            if (CodigoFuncionario != null)
            {
                int Repreg = (int)CodigoFuncionario;
                var funcionario = await _gestorDeSessao.ObterFuncionarioPorCodigo(Repreg);

                if (funcionario != null)
                {
                    _sessaoUsuario.IniciarSessao(funcionario);
                    if (Shell.Current != null)
                    {
                        try
                        {
                            await Shell.Current.GoToAsync($"{nameof(CardapioView)}?nomeFuncionario={Uri.EscapeDataString(funcionario.Nome)}");
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                    }
                }
                else
                {
                    // Exibir erro
                }
            }
        }

        public void Limpar()
        {
            CodigoFuncionario = null;
            _sessaoUsuario.EncerrarSessao();
        }
    }
}
