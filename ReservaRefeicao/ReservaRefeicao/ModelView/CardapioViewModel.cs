using System;
using System.Collections.Generic;
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
        private readonly Sessao _sessaoUsuario;
        private string _nomeFuncionario;

        public CardapioViewModel(Sessao sessaoUsuario)
        {
            _sessaoUsuario = sessaoUsuario;
            _sessaoUsuario.SessaoEncerrada += OnSessaoEncerrada;
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
            // Coloque aqui a lógica para carregar o cardápio, por exemplo, do banco de dados ou API
            await Task.Delay(500); // Simulando um atraso para o carregamento dos dados

        }
    }
}
