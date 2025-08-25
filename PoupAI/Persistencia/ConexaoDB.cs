using Dapper.Contrib.Extensions;
using Npgsql;

namespace PoupAI.Persistencia
{
    public class ConexaoDB
    {
        private static readonly string stringConexao;

        static ConexaoDB()
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            stringConexao = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("String de conexão 'DefaultConnection' não encontrada.");

            SqlMapperExtensions.TableNameMapper = (type) =>
            {
                return type.Name;
            };
        }

        // Fechar conexão após uso
        public static NpgsqlConnection ObterConexao()
        {
            var conexao = new NpgsqlConnection(stringConexao);
            conexao.Open();
            return conexao;
        }
    }
}