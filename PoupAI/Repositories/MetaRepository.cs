using Dapper;
using Api.Comum;
using System.Collections.Generic;
using PoupAI.Persistencia;
using Npgsql;
using System.Threading.Tasks;

namespace PoupAI.Repositories;
public class MetaRepository
{
    private readonly string _connectionString;
    public MetaRepository(string connectionString) => _connectionString = connectionString;

    public async Task<IEnumerable<Meta>> GetAll()
    {
        using var conn = new NpgsqlConnection(_connectionString);
        return await conn.QueryAsync<Meta>("SELECT * FROM Meta");
    }

    public async Task AddValue(Meta meta)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        var sql = @"INSERT INTO Meta (Descricao, ValorAlvo, DataFinal, UsuarioId) 
                    VALUES (@Descricao, @ValorAlvo, @DataFinal, @UsuarioId)";
        await conn.ExecuteAsync(sql, meta);
    }
}
