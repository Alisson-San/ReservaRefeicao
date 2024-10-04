namespace ReservaRefeicao.Views;

public partial class SessionPage : ContentPage
{
	public SessionPage()
	{
		InitializeComponent();
	}

    async void OnTapped(object sender, EventArgs eventArgs)
    {
        Grid g = (Grid)sender;

        await g.FadeTo(0, 200);
        await g.FadeTo(0.5, 100);
        await g.FadeTo(0, 100);
        await g.FadeTo(0.3, 100);
        await g.FadeTo(0, 100);

        await Task.Delay(1000);

        await g.FadeTo(1, 400);
    }

    void OnButtonClick(object sender, EventArgs eventArgs)
    {
    }
}