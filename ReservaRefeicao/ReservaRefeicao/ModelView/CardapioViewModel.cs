using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using ReservaRefeicao.Model;
using ReservaRefeicao.ModelView;
using ReservaRefeicao.Services;

namespace ReservaRefeicao.ViewModels
{
    public class CardapioViewModel : ViewModelBase, IQueryAttributable
    {
        private readonly GestorCardapioService _gestorCardapioService;

        private readonly Sessao _sessaoUsuario;
        private string _nomeFuncionario;
        private DateTime _diaAtual;
        private List<Refeicao> _cardapioDoDia;
        private List<Refeicao> _cardapioDaSemana;


        public Refeicao CardapioSelecionado { get; set; }

        public ICommand DiaAnteriorCommand { get; }
        public ICommand DiaProximoCommand { get; }
        public ICommand ReservarCommand { get; }

        public List<Refeicao> CardapioDoDia
        {
            get => _cardapioDoDia;
            set
            {
                _cardapioDoDia = value;
                OnPropertyChanged();
            }
        }


        public CardapioViewModel(Sessao sessaoUsuario, GestorCardapioService gestorCardapioService)
        {
            _sessaoUsuario = sessaoUsuario;
            _gestorCardapioService = gestorCardapioService;
            _sessaoUsuario.SessaoEncerrada += OnSessaoEncerrada;
            DefineActualTiming();
            DiaAnteriorCommand = new Command(async () => await NavegarDiaAnterior());
            DiaProximoCommand = new Command(async () => await NavegarDiaProximo());
            ReservarCommand = new Command(Reservar);
            CarregarCardapioAsync();
        }

        private void DefineActualTiming()
        {
            // Define o dia atual para conforme a data atual
            // Se o dia atual ja tiver passado as 8:30 da manhã o dia atual é um dia seguinte
            // Se o funcionario é Turno N então o dia atual é um dia a frente
            _diaAtual = DateTime.Now;
            if (_sessaoUsuario.FuncionarioAtual.Turno == "N")
            {
                _diaAtual = _diaAtual.AddDays(1);
            }
            else if (_diaAtual.Hour >= 8 && _diaAtual.Minute >= 30)
            {
                _diaAtual = _diaAtual.AddDays(1);
            }
        }

        // Propriedade que será vinculada à View para exibir o nome do funcionário
        public string NomeFuncionario
        {
            get => _nomeFuncionario;
            set
            {
                _nomeFuncionario = value;
                OnPropertyChanged(); // Notifica a View de que a propriedade mudou
            }
        }

        public string DiaAtual
        {
            get => _diaAtual.ToString("D");
            set
            {
                _nomeFuncionario = value;
                OnPropertyChanged(); // Notifica a View de que a propriedade mudou
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("nomeFuncionario", out var nome))
            {
                NomeFuncionario = Uri.UnescapeDataString(nome.ToString());
            }
        }

        public void OnAppearing()
        {
            // Carrega o cardápio quando a página aparecer
            //    CarregarCardapioAsync();
        }

        public void StartTimer()
        {
            // Inicia o timer de sessao de usuario quando a pagina aparecer
            _sessaoUsuario.IniciarTimer();
        }

        private async void OnSessaoEncerrada()
        {
            // Navega para a tela de autenticação
            this.Dispose();
            await Shell.Current.GoToAsync($"//AuthenticationView");
        }

        public void Dispose()
        {
            _sessaoUsuario.SessaoEncerrada -= OnSessaoEncerrada;
        }


        // Simulação do carregamento de um cardápio
        public async Task CarregarCardapioAsync()
        {
            _cardapioDaSemana = await _gestorCardapioService.ObterCardapioDaSemana();
            await AtualizarCardapios();
        }

        private Task AtualizarCardapios()
        {
            CardapioDoDia = _cardapioDaSemana.FindAll(r => r.Data.Date == _diaAtual.Date);
            return Task.CompletedTask;
        }

        private async Task NavegarDiaAnterior()
        {
            _diaAtual = _diaAtual.AddDays(-1);
            await AtualizarCardapios();
        }

        private async Task NavegarDiaProximo()
        {
            _diaAtual.AddDays(-1);
            await AtualizarCardapios();
        }

        private void Reservar()
        {
            if (CardapioSelecionado != null)
            {
                // Lógica de reserva
            }
        }
    }
}
