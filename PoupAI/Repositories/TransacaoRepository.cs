using Dapper;
using Npgsql;
using PoupAI.Persistencia;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoupAI.Repositories;
public class TransacaoRepository
{
    private readonly string _connectionString;
    public TransacaoRepository(string connectionString) => _connectionString = connectionString;

    public async Task<IEnumerable<dynamic>> GetUltimasTransacoes()
    {
        using var conn = new NpgsqlConnection(_connectionString);
        var sql = @"
            SELECT Id, Descricao, Valor, Data, 'Receita' AS Tipo
            FROM Receita
            UNION ALL
            SELECT d.Id, d.Descricao, d.Valor, d.Data, c.Nome AS Tipo
            FROM Despesa d
            INNER JOIN Categoria c ON d.CategoriaId = c.Id
            ORDER BY Data DESC
            OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY";

        return await conn.QueryAsync(sql);
    }
}
