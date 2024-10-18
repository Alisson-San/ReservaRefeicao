using System.Configuration;

namespace ReservaRefeicao.Utils
{
    public class Configuracao
    {
        private static Configuracao _instancia = null;

        public string ObterConfiguracao(string nomeConfiguracao)
        {
            if (ConfigurationManager.AppSettings[nomeConfiguracao] == null)
                throw new Exception("Você deve inserir a appSetting \"" + nomeConfiguracao + "\" no .config !");
            return ConfigurationManager.AppSettings[nomeConfiguracao];
        }

        public ConnectionStringSettings ObterConnectionString(string nomeConnectionString)
        {
            if (ConfigurationManager.ConnectionStrings[nomeConnectionString].ConnectionString == null)
                throw new Exception("Você deve inserir a connectionString \"" + nomeConnectionString + "\" no .config !");
            return ConfigurationManager.ConnectionStrings[nomeConnectionString];
        }

        public static Configuracao ObterInstancia()
        {
            if (_instancia == null)
                _instancia = new Configuracao();
            return _instancia;
        }

    }
}
