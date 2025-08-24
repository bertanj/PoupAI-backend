using PoupAI.Data;
using PoupAI.Models;
using Microsoft.EntityFrameworkCore;

namespace PoupAI.Repositories;

public class ReceitaRepository : IRepository<ReceitaModel>
{
    private readonly PoupAIContext _context;
    public ReceitaRepository(PoupAIContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<ReceitaModel>> GetAll(){
        return await _context.Receitas.ToListAsync();
    }
    public async Task<ReceitaModel?> GetById(int id) { 
        return await _context.Receitas.FindAsync(id);
    }
    public async Task<IEnumerable<ReceitaModel>> GetByDateAsync(DateTime data){
        return await _context.Receitas
             .Where(d => d.Data.Date == data.Date)
             .ToListAsync();
    }
    public async Task AddValue(ReceitaModel entity){
        _context.Receitas.Add(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateValue(ReceitaModel entity){
        _context.Receitas.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteValue(int id){
        var receita = await _context.Receitas.FindAsync(id);
        if(receita != null){
            _context.Receitas.Remove(receita);
            await _context.SaveChangesAsync();
        }
    }
    