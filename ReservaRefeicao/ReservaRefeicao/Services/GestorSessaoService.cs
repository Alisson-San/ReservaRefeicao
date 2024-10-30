using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ReservaRefeicao.Model;
using ReservaRefeicao.Utils;

namespace ReservaRefeicao.Services
{
    public class GestorSessaoService
    {
        private readonly DbContextServices _dbContext;

        private DateHelper _dateHelper;

        public GestorSessaoService(DbContextServices dbContext)
        {
            _dbContext = dbContext;
            _dateHelper = new DateHelper();
        }

        public async Task<Funcionario> ObterFuncionarioPorCodigo(int codigo)
        {
            return await _dbContext.Funcionarios.FirstOrDefaultAsync(f => f.Repreg == codigo);
        }
    }
}