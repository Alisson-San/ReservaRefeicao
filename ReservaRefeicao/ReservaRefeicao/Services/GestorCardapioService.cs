using Microsoft.EntityFrameworkCore;
using ReservaRefeicao.Model;
using ReservaRefeicao.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRefeicao.Services
{
    public class GestorCardapioService
    {
        private readonly DbContextServices _dbContext;
        private DateHelper _dateHelper;

        public GestorCardapioService(DbContextServices dbContext)
        {
            _dbContext = dbContext;
            _dateHelper = new DateHelper();

        }

        public async Task<List<Refeicao>> ObterCardapioDaSemana()
        {
            DateTime InitWeek = _dateHelper.GetFirstDayOfWeek(DateTime.Now);
            DateTime EndWeek = _dateHelper.GetLastDayOfWeek(DateTime.Now);

            return await _dbContext.refeicaos
                .Where(r => r.Data >= InitWeek && r.Data <= EndWeek)
                .ToListAsync();
        }

        public async Task<List<Refeicao>> ObterCardapioDoDia()
        {
            _dbContext.Checkconnection();
            return await _dbContext.refeicaos
                .Where(r => r.Data == DateTime.Today)
                .ToListAsync();
        }
    }
}
