using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReservaRefeicao.Model;
using ReservaRefeicao.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaRefeicao.Services
{
    public class DbContextServices : DbContext
    {
        private byte[] cookie;

        public DbContextServices(DbContextOptions<DbContextServices> options): base (options)
        {
            Database.GetDbConnection().Open();
            cookie = SetAppRole("RefeicaoAppRole", "Botcha123");
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

        byte[] SetAppRole(string approle, string password)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "sp_setapprole";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@rolename", approle));
            cmd.Parameters.Add(new SqlParameter("@password", password));
            cmd.Parameters.Add(new SqlParameter("@fCreateCookie", 1));

            var pCookieId = new SqlParameter("@cookie", SqlDbType.VarBinary);
            pCookieId.Size = 8000;
            pCookieId.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(pCookieId);

            cmd.ExecuteNonQuery();


            return (byte[])pCookieId.Value;
        }

        void UnSetAppRole(byte[] cookie)
        {
            if (Database.GetDbConnection().State == ConnectionState.Open)
            {
                var pCookieId = new SqlParameter("@cookie", SqlDbType.VarBinary)
                {
                    Size = 8000,
                    Value = cookie
                };

                using (IDbCommand cmd = Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "sp_unsetapprole";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(pCookieId);

                    cmd.ExecuteNonQuery();
                    cookie = null;
                }
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

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Secao> secaos { get; set; }
        public DbSet<Predio> predios { get; set; }
        public DbSet<Refeicao> refeicaos { get; set; }
    }
}
