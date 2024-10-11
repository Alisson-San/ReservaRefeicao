using ReservaRefeicao.Views;

namespace ReservaRefeicao
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("Session", typeof(SessionPage));
        }
    }
}
