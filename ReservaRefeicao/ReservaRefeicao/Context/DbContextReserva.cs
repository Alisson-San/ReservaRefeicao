using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReservaRefeicao.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRefeicao.Context
{
    public class DbContextReserva:DbContext
    {
        private byte[] cookie;

        public DbContextReserva()
        {
            Database.GetDbConnection().Open();
            cookie = SetAppRole("DesenvolvimentoAppRole", "Botcha123");
        }
        public bool Checkconnection()
        {
            try
            {
                return Database.CanConnect();
            }
            catch
            {
                return false;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string stringConexao = "SistemaTramontina";
#if (DEBUG)
            stringConexao += "-DEBUG";
#endif

            string connectionString = Configuracao.ObterInstancia().ObterConnectionString(stringConexao).ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        byte[] SetAppRole(string approle, string password)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "sp_setapprole";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@rolename", approle));
            cmd.Parameters.Add(new SqlParameter("@password", password));
            cmd.Parameters.Add(new SqlParameter("@fCreateCookie", 1));

            var pCookieId = new SqlParameter("@cookie", System.Data.SqlDbType.VarBinary);
            pCookieId.Size = 8000;
            pCookieId.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(pCookieId);

            cmd.ExecuteNonQuery();


            return (byte[])pCookieId.Value;
        }
    }
}
