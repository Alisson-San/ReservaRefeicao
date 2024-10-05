using ReservaRefeicao.Controllers;

namespace ReservaRefeicao.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
        BindingContext = new TestController(false);

        InitializeComponent();
	}

    async void OnButtonClick(object sender, EventArgs eventArgs)
    {
        await Shell.Current.GoToAsync("Session");
    }
}