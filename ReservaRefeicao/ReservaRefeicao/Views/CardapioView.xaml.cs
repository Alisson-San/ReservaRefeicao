using ReservaRefeicao.ViewModels;

namespace ReservaRefeicao.Views
{
    public partial class CardapioView : ContentPage
    {
        public CardapioView(CardapioViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.AnimarTransicaoEvent += async (paraDireita) => await AnimarTransicao(paraDireita);
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

        private async Task AnimarTransicao(bool paraEsquerda)
        {
            // Define a dire��o do deslocamento
            double startTranslation = paraEsquerda ? 1000 : -1000;
            double endTranslation = 0;

            // Realiza o deslocamento inicial para fora da tela
            CollectionView collectionView = CardapioCollectionView;
            await collectionView.TranslateTo(startTranslation, 0, 0);

            // Anima o deslocamento para o centro da tela
            await collectionView.TranslateTo(endTranslation, 0, 300, Easing.CubicInOut);
        }

    }
}