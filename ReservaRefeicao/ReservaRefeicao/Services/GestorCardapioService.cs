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

        public List<Refeicao> ObterCardapioDoDia()
        {
#if (!DEBUG)
            return await _dbContext.refeicaos
                .Where(r => r.Data == DateTime.Today)
                .ToListAsync();
#else
            var dataTeste = DateTime.Parse("2024-10-05");
            _dbContext.Checkconnection();
            var Refeicoes = _dbContext.refeicaos
            .Where(r => r.Data == dataTeste)
            .ToList();
            if(Refeicoes.Count == 0)
                throw new Exception("Nenhuma refeição encontrada para o dia de hoje");
            return Refeicoes;
#endif
        }
    }
}
