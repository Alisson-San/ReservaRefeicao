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
        codigoFuncionarioEntry.Focus();
        _viewModel.Limpar(); // Limpa a sess�o do usu�rio

    }

    private async void OnItemAppearing(object sender, EventArgs e)
    {
        if (sender is Frame frame)
        {
            // Define a anima��o, por exemplo, opacidade e escala
            frame.Opacity = 0;
            frame.Scale = 0.8;

            // Executa as anima��es
            await frame.FadeTo(1, 500);  // Anima��o de Fade
            await frame.ScaleTo(1, 500); // Anima��o de Zoom/Scale
        }
    }

    // Evento acionado quando o usu�rio pressiona Enter
    private async void OnCompleted(object sender, EventArgs e)
    {
        // Chama o comando de autentica��o
        await _viewModel.Autenticar();
    }
}