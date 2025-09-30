using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

[Table("rescad_familiares")]
public class Familiares : BaseModel
{
    [PrimaryKey("cod_familiar", false)]
    public int CodFamiliar { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = string.Empty;

    [Column("parentesco")]
    public string Parentesco { get; set; } = string.Empty;

    [Column("telefone")]
    public string? Telefone { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("cod_residente")]
    public int CodResidente { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    // Referência ao residente
    [Reference(typeof(Residentes))]
    public Residentes? Residente { get; set; }
}