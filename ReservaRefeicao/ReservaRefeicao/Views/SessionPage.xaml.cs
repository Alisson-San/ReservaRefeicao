using ReservaRefeicao.Controllers;
using ReservaRefeicao.Model;

namespace ReservaRefeicao.Views;

public partial class SessionPage : ContentPage
{
	public SessionPage()
	{
        BindingContext = new TestController(true);

        InitializeComponent();
	}

    void OnButtonClick(object sender, EventArgs eventArgs)
    {
    }
}