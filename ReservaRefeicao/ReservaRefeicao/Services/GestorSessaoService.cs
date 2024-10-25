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

        public async Task<List<Reserva>> ObterReservasSemanalFuncionario(int codigo)
        {
            var InitWeek = _dateHelper.GetFirstDayOfWeek(DateTime.Now);
            var EndWeek = _dateHelper.GetLastDayOfWeek(DateTime.Now);

            return await _dbContext.reservas
                .Where(r => r.Repreg == codigo
                    && r.DataReserva >= InitWeek
                    && r.DataReserva <= EndWeek)
                .ToListAsync();
        }
    }
}