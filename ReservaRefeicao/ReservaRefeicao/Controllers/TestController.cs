using ReservaRefeicao.Model;

namespace ReservaRefeicao.Controllers
{
    internal class TestController
    {
        public TestController(bool temFuncionario) {
            if (temFuncionario)
            {
                this.Funcionario = new Funcionario
                {
                    Repreg = 0,
                    Nome = "",
                    Turno = "",
                    CodSecao = 0,
                    Secao = new Secao(),
                };
            }
        }

        public Funcionario? Funcionario { get; }

        public bool TemFuncionario => Funcionario != null;

        public List<Refeicao> TestList
        {
            get
            {
                return new List<Refeicao>
                    {
                        new Refeicao{ CodRefeicao = 1, Data = DateTime.Now, Tipo = "Refeição 1", Cardapio = "Cardapio 1" },
                        new Refeicao{ CodRefeicao = 2, Data = DateTime.Now, Tipo = "Refeição 2", Cardapio = "Cardapio 2" },
                        new Refeicao{ CodRefeicao = 3, Data = DateTime.Now, Tipo = "Refeição 3", Cardapio = "Cardapio 3" },
                    };
            }

        }

}
}
