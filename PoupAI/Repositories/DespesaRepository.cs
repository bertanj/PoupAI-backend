using Dapper;
using Api.Comum;
using Npgsql;
using PoupAI.Persistencia;

namespace PoupAI.Repositories;
public class DespesaRepository
{
    private readonly string _connectionString;
    public DespesaRepository(string connectionString) => _connectionString = connectionString;

    public async Task<IEnumerable<Despesa>> GetAll()
    {
        using var conn = new NpgsqlConnection(_connectionString);
        return await conn.QueryAsync<Despesa>(@"
            SELECT d.Id, d.Descricao, d.Valor, d.Data, d.UsuarioId, d.CategoriaId, c.Nome AS CategoriaNome
            FROM Despesa d
            INNER JOIN Categoria c ON d.CategoriaId = c.Id");
    }

    public async Task<Despesa?> GetById(int id)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        return await conn.QueryFirstOrDefaultAsync<Despesa>(
            @"SELECT * FROM Despesa WHERE Id = @Id", new { Id = id });
    }

    public async Task AddValue(Despesa despesa)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        var sql = @"INSERT INTO Despesa (Descricao, Valor, Data, UsuarioId, CategoriaId) 
                    VALUES (@Descricao, @Valor, @Data, @UsuarioId, @CategoriaId)";
        await conn.ExecuteAsync(sql, despesa);
    }

    public async Task UpdateValue(Despesa despesa)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        var sql = @"UPDATE Despesa SET Descricao=@Descricao, Valor=@Valor, Data=@Data, CategoriaId=@CategoriaId 
                    WHERE Id=@Id";
        await conn.ExecuteAsync(sql, despesa);
    }

    public async Task DeleteValue(int id)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        await conn.ExecuteAsync("DELETE FROM Despesa WHERE Id = @Id", new { Id = id });
    }
}
