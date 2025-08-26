using Dapper;
using PoupAI.Persistencia;
using System.Threading.Tasks;

public class SaldoService
{
    public async Task<decimal> GetSaldoUsuarioAsync(int usuarioId)
    {
        using var conexao = ConexaoDB.ObterConexao();

        var sql = @"
            SELECT 
                COALESCE((SELECT SUM(Valor) FROM Receita WHERE UsuarioId = @UsuarioId),0) -
                COALESCE((SELECT SUM(Valor) FROM Despesa WHERE UsuarioId = @UsuarioId),0)
            AS Saldo";

        return await conexao.ExecuteScalarAsync<decimal>(sql, new { UsuarioId = usuarioId });
    }
}
