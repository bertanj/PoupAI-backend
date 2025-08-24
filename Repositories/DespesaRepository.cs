using PoupAI.Data;
using PoupAI.Models;
using Microsoft.EntityFrameworkCore;

namespace PoupAI.Repositories;

public class DespesaRepository : IRepository<DespesaModel>
{
    private readonly PoupAIContext _context;
    public DespesaRepository(PoupAIContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<DespesaModel>> GetAll(){
        return await _context.Despesas.Include(d => d.Categoria).ToListAsync();
    }
    public async Task<DespesaModel?> GetById(int id) { 
        return await _context.Despesas.Include(d => d.Categoria).FirstOrDefaultAsync(d => d.Id == id);
    }
    public async Task<IEnumerable<DespesaModel>> GetByDateAsync(DateTime data)
    {
        return await _context.Despesas
            .Where(d => d.Data.Date == data.Date)
            .Include(d => d.Categoria)
            .ToListAsync();
    }
    public async Task AddValue(DespesaModel entity){
        _context.Despesas.Add(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateValue(DespesaModel entity){
        _context.Despesas.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteValue(int id){
        var despesa = await _context.Despesas.FindAsync(id);
        if(despesa != null){
            _context.Despesas.Remove(despesa);
            await _context.SaveChangesAsync();
        }
    }

}