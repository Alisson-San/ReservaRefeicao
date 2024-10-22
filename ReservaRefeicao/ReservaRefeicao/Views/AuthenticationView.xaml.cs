using ReservaRefeicao.Model;
using ReservaRefeicao.ModelView;
using ReservaRefeicao.Services;

namespace ReservaRefeicao.Views;

public partial class AuthenticationView : ContentPage
{
    private readonly AuthenticationViewModel _viewModel;

    // Construtor que recebe depend�ncias injetadas
    public AuthenticationView(AuthenticationViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.Limpar(); // Limpa a sess�o do usu�rio
    }

    // Evento acionado quando o usu�rio pressiona Enter
    private async void OnCompleted(object sender, EventArgs e)
    {
        // Chama o comando de autentica��o
        await _viewModel.Autenticar();
    }
}