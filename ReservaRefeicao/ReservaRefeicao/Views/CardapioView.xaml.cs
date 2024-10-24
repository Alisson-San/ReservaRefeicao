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

            // Chama a fun��o da ViewModel quando a p�gina aparecer
            if (BindingContext is CardapioViewModel viewModel)
            {
                viewModel.StartTimer(); 
            }
        }

    }
}