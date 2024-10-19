namespace ReservaRefeicao.Views
{
    [QueryProperty(nameof(NomeFuncionario), "nomeFuncionario")]
    public partial class CardapioView : ContentPage
    {
        private string _nomeFuncionario;

        public string NomeFuncionario
        {
            get => _nomeFuncionario;
            set
            {
                _nomeFuncionario = value;
                OnPropertyChanged();
            }
        }

        public CardapioView()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}