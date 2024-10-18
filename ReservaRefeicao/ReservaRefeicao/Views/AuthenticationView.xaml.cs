using ReservaRefeicao.Model;
using ReservaRefeicao.ModelView;
using ReservaRefeicao.Services;

namespace ReservaRefeicao.Views;

public partial class AuthenticationView : ContentPage
{
    private readonly AuthenticationViewModel  _viewModel;

    public AuthenticationView(AuthenticationViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    // Reseta o timer sempre que houver um toque na tela
    protected override bool OnBackButtonPressed()
    {
        _sessaoUsuario.ResetarTimer();
        return base.OnBackButtonPressed();
    }

    private void OnSessaoEncerrada()
    {
        // Voltar para a tela de login quando a sessão for encerrada
        Device.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("//AuthenticationView");
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _sessaoUsuario.SessaoEncerrada -= OnSessaoEncerrada;
    }

    private async void OnCompleted(object sender, EventArgs e)
    {
        // Chama o comando de autenticação
        await _viewModel.Autenticar();
    }
    }

}