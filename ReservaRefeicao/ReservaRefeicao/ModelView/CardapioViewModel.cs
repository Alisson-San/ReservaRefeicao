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

        public event Action<bool> AnimarTransicaoEvent;

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

        // ObservableCollection para o Binding no CollectionView
        public ObservableCollection<RefeicaoViewModel> CardapiosDoDia { get; } = new ObservableCollection<RefeicaoViewModel>();
        public ObservableCollection<Refeicao> CardapiosSelecionados { get; } = new ObservableCollection<Refeicao>();


        public CardapioViewModel(Sessao sessaoUsuario, GestorCardapioService gestorCardapioService)
        {
            _sessaoUsuario = sessaoUsuario;
            _gestorCardapioService = gestorCardapioService;
            _sessaoUsuario.SessaoEncerrada += OnSessaoEncerrada;
            DefineActualTiming();
            DiaAnteriorCommand = new Command(async () => await NavegarDiaAnterior());
            DiaProximoCommand = new Command(async () => await NavegarDiaProximo());
            ReservarCommand = new Command<ObservableCollection<Refeicao>>(Reservar);
            AtualizarRefeicoesFuncionario();
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

        private async Task OnSessaoEncerradaAsync()
        {
            Dispose();
            await Shell.Current.GoToAsync("//AuthenticationView");
        }

        private void OnSessaoEncerrada()
        {
            _ = OnSessaoEncerradaAsync();
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
                var refeicaoViewModel = new RefeicaoViewModel { Refeicao = refeicao };
                refeicaoViewModel.CorExibicao = _sessaoUsuario.ReservasSemana.Any(r => r.CodRefeicao == refeicao.CodRefeicao)
                    ? refeicao.Nome.Contains("CAFÉ") ? Colors.Orange : Colors.Green
                    : Colors.Transparent;

                CardapiosDoDia.Add(refeicaoViewModel);
            }
            OnPropertyChanged(nameof(CardapiosDoDia));
        }

        private async Task AtualizarRefeicoesFuncionario()
        {
            // Limpa o ObservableCollection antes de adicionar os itens do dia
            CardapiosSelecionados.Clear();

            var cardapiosSelecionados = _sessaoUsuario.ReservasSemana.Select(r => r.Refeicao);

            // Adiciona cada item individualmente para que o CollectionView seja notificado
            foreach (var refeicao in cardapiosSelecionados)
            {
                CardapiosSelecionados.Add(refeicao);
            }
            OnPropertyChanged();
            await Task.CompletedTask;
        }

        private async Task NavegarDiaAnterior()
        {
            _diaAtual = _diaAtual.AddDays(-1);
            OnPropertyChanged(nameof(DiaAtual));
            await AtualizarCardapios();
            AnimarTransicaoEvent?.Invoke(false);

        }

        private async Task NavegarDiaProximo()
        {
            _diaAtual = _diaAtual.AddDays(1);
            OnPropertyChanged(nameof(DiaAtual));
            await AtualizarCardapios();
            AnimarTransicaoEvent?.Invoke(true);
        }

        private void Reservar(ObservableCollection<Refeicao> selectedItems)
        {
            if (selectedItems.Count == 0 || selectedItems == null)
            {
                // Lógica de reserva
                return;
            }
        }

        public void StartTimer()
        {
            // Inicia o timer de sessao de usuario quando a pagina aparecer
            _sessaoUsuario.IniciarTimer();
        }

        private async void OnReservar(Refeicao refeicaoSelecionada)
        {
            var reservaExistente = _sessaoUsuario.ReservasSemana.FirstOrDefault(r => r.CodRefeicao == refeicaoSelecionada.CodRefeicao);

            if (reservaExistente != null)
            {
                // Atualiza a reserva existente, se necessário
                reservaExistente.DataReserva = DateTime.Now;
                await _gestorCardapioService.AtualizarReserva(reservaExistente);
            }
            else
            {
                // Cria nova reserva
                var novaReserva =  new Reserva
                {
                    Repreg = _sessaoUsuario.FuncionarioAtual.Repreg,
                    CodRefeicao = refeicaoSelecionada.CodRefeicao,
                    DataReserva = DateTime.Now
                };


                await _gestorCardapioService.AdicionarReserva(novaReserva);
            }

            // Atualiza a UI
            await AtualizarCardapios();
        }

    }
}
