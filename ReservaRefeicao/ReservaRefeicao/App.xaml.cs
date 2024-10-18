using ReservaRefeicao.Model;
using ReservaRefeicao.Views;

namespace ReservaRefeicao
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var _sessaoUsuario = new Sessao();
            MainPage = new NavigationPage(new AuthenticationView(_sessaoUsuario));
        }
    }
}
