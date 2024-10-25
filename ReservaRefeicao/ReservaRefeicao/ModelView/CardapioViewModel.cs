using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private List<Refeicao> _cardapioDaSemana;

        public Refeicao CardapioSelecionado { get; set; }

        public ICommand DiaAnteriorCommand { get; }
        public ICommand DiaProximoCommand { get; }
        public ICommand ReservarCommand { get; }

        // ObservableCollection para o Binding no CollectionView
        public ObservableCollection<Refeicao> CardapiosDoDia { get; } = new ObservableCollection<Refeicao>();

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

        public string NomeFuncionario
        {
            get => _nomeFuncionario;
            set
            {
                _nomeFuncionario = value;
                OnPropertyChanged();
            }
        }

        public string DiaAtual
        {
            get => _diaAtual.ToString("D");
            private set
            {
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
            _sessaoUsuario.IniciarTimer();
        }

        private async void OnSessaoEncerrada()
        {
            this.Dispose();
            await Shell.Current.GoToAsync($"//AuthenticationView");
        }

        public void Dispose()
        {
            _sessaoUsuario.SessaoEncerrada -= OnSessaoEncerrada;
        }

        public async Task CarregarCardapioAsync()
        {
            _cardapioDaSemana = await _gestorCardapioService.ObterCardapioDaSemana();
            await AtualizarCardapios();
        }

        private async Task AtualizarCardapios()
        {
            // Limpa o ObservableCollection antes de adicionar os itens do dia
            CardapiosDoDia.Clear();

            var cardapiosDoDia = _cardapioDaSemana.FindAll(r => r.Data.Date == _diaAtual.Date);

            // Adiciona cada item individualmente para que o CollectionView seja notificado
            foreach (var refeicao in cardapiosDoDia)
            {
                CardapiosDoDia.Add(refeicao);
            }

            await Task.CompletedTask;
        }

        private async Task NavegarDiaAnterior()
        {
            _diaAtual = _diaAtual.AddDays(-1);
            await AtualizarCardapios();
        }

        private async Task NavegarDiaProximo()
        {
            _diaAtual = _diaAtual.AddDays(1);
            await AtualizarCardapios();
        }

        private void Reservar()
        {
            if (CardapioSelecionado != null)
            {
                // Lógica de reserva
            }
        }

        public void StartTimer()
        {
            // Inicia o timer de sessao de usuario quando a pagina aparecer
            _sessaoUsuario.IniciarTimer();
        }
    }
}
