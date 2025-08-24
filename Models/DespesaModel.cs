namespace PoupAI.Models;
using PoupAI.Models;

public class DespesaModel
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }

    //Relaction 
    public int UsuarioId { get; set; }
    public UsuarioModel Usuario { get; set; }

    public int? CategoriaId  { get; set; }
    public CategoriaModel Categoria { get; set; }
}