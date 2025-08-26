using Dapper;
using Npgsql;
using PoupAI.Persistencia;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoupAI.Repositories;
public class DashboardRepository
{
    private readonly string _connectionString;
    public DashboardRepository(string connectionString) => _connectionString = connectionString;

    public async Task<dynamic> GetResumoMensal(int ano, int mes)
    {
        using var conn = new NpgsqlConnection(_connectionString);

        var receitas = await conn.ExecuteScalarAsync<decimal>(
            "SELECT ISNULL(SUM(Valor),0) FROM Receita WHERE YEAR(Data)=@Ano AND MONTH(Data)=@Mes",
            new { Ano = ano, Mes = mes });

        var despesas = await conn.ExecuteScalarAsync<decimal>(
            "SELECT ISNULL(SUM(Valor),0) FROM Despesa WHERE YEAR(Data)=@Ano AND MONTH(Data)=@Mes",
            new { Ano = ano, Mes = mes });

        var metas = await conn.ExecuteScalarAsync<decimal>(
            "SELECT ISNULL(SUM(ValorAlvo),0) FROM Meta WHERE YEAR(DataFinal)=@Ano AND MONTH(DataFinal)=@Mes",
            new { Ano = ano, Mes = mes });

        return new
        {
            Receitas = receitas,
            Despesas = despesas,
            Economia = receitas - despesas,
            Investimento = metas
        };
    }

    public async Task<IEnumerable<dynamic>> GetGastosPorCategoria(int ano, int mes)
    {
        using var conn = new NpgsqlConnection(_connectionString);

        var sql = @"
            SELECT c.Nome AS Categoria, SUM(d.Valor) AS Total
            FROM Despesa d
            INNER JOIN Categoria c ON d.CategoriaId = c.Id
            WHERE YEAR(d.Data)=@Ano AND MONTH(d.Data)=@Mes
            GROUP BY c.Nome";

        return await conn.QueryAsync(sql, new { Ano = ano, Mes = mes });
    }
}
