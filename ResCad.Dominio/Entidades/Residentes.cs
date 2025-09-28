using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

[Table("rescad_residentes")]
public class Residentes : BaseModel
{
    [PrimaryKey("cod_residente", true)]
    public int CodResidente { get; set; }

    [Column("nome_completo")]
    public string NomeCompleto { get; set; } = string.Empty;

    [Column("numero_documento")]
    public string NumeroDocumento { get; set; } = string.Empty;

    [Column("data_nascimento")]
    public DateTime DataNascimento { get; set; }

    [Column("data_entrada")]
    public DateTime DataEntrada { get; set; }

    [Column("genero")]
    public string Genero { get; set; } = string.Empty;

    [Column("estado_civil")]
    public string EstadoCivil { get; set; } = string.Empty;

    [Column("ativo")]
    public bool Ativo { get; set; } = true;

    [Column("data_saida")]
    public DateTime? DataSaida { get; set; }

    [Column("telefone_contato")]
    public string? TelefoneContato { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    // Para relacionamentos, usamos Reference do Supabase
    [Reference(typeof(Familiares))]
    public List<Familiares> Familiares { get; set; } = new List<Familiares>();

    [Reference(typeof(HistoricoSaude))]
    public List<HistoricoSaude> HistoricosSaude { get; set; } = new List<HistoricoSaude>();
}