namespace PoupAI.Models;
using PoupAI.Models;

public class MetaModel { 
    public int Id { get; set; }
    public string descricao { get; set; } = string.Empty;
    public decimal valorAlvo { get; set; }
    public decimal valorAtual { get; set; }
    public DateTime dataAlvo { get; set; }
    public bool atingida { get; set; }

    //Relaction
    public int UsuarioId { get; set; }  
    public UsuarioModel Usuario { get; set; }
}