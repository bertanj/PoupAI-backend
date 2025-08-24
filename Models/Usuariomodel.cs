using PoupAI.Models;

public class UsuarioModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    //Relaction
    public ICollection<ReceitaModel> Receitas { get; set; } = new List<ReceitaModel>();
    public ICollection<DespesaModel> Despesas { get; set; } = new List<DespesaModel>();
    public ICollection<MetaModel> Metas { get; set; } = new List<MetaModel>();
}