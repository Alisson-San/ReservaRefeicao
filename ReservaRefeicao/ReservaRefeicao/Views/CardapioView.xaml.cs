using ReservaRefeicao.ViewModels;

namespace ReservaRefeicao.Views
{
    public partial class CardapioView : ContentPage
    {
        public CardapioView(CardapioViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        // Manipulador do evento Appearing
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Chama a função da ViewModel quando a página aparecer
            if (BindingContext is CardapioViewModel viewModel)
            {
                viewModel.StartTimer(); 
            }
        }

    }
}