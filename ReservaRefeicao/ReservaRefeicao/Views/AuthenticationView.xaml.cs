using ReservaRefeicao.Model;
using ReservaRefeicao.ModelView;
using ReservaRefeicao.Services;

namespace ReservaRefeicao.Views;

public partial class AuthenticationView : ContentPage
{
    private readonly AuthenticationViewModel _viewModel;

    // Construtor que recebe dependências injetadas
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
        _viewModel.Limpar(); // Limpa a sessão do usuário

    }

    private async void OnItemAppearing(object sender, EventArgs e)
    {
        if (sender is Frame frame)
        {
            // Define a animação, por exemplo, opacidade e escala
            frame.Opacity = 0;
            frame.Scale = 0.8;

            // Executa as animações
            await frame.FadeTo(1, 500);  // Animação de Fade
            await frame.ScaleTo(1, 500); // Animação de Zoom/Scale
        }
    }

    // Evento acionado quando o usuário pressiona Enter
    private async void OnCompleted(object sender, EventArgs e)
    {
        // Chama o comando de autenticação
        await _viewModel.Autenticar();
    }
}