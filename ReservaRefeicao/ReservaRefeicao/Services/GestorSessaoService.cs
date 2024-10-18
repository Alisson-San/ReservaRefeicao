using Microsoft.EntityFrameworkCore;
using ReservaRefeicao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRefeicao.Services
{
    public class GestorSessaoService
    {
        private readonly DbContextServices _dbContext;

        public GestorSessaoService(DbContextServices dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Funcionario> ObterFuncionarioPorCodigo(int codigo)
        {
            return await _dbContext.Funcionarios.FirstOrDefaultAsync(f => f.Repreg == codigo);
        }
    }
}
