using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

[Table("rescad_hist_saude")]
public class HistoricoSaude : BaseModel
{
    [PrimaryKey("cod_hist_saude", true)]
    public int CodHistSaude { get; set; }

    [Column("condicoes_medicas")]
    public string CondicoesMedicas { get; set; } = string.Empty;

    [Column("medicamentos")]
    public string Medicamentos { get; set; } = string.Empty;

    [Column("alergias")]
    public string Alergias { get; set; } = string.Empty;

    [Column("restricoes_alimentares")]
    public string RestricoesAlimentares { get; set; } = string.Empty;

    [Column("data_registro")]
    public DateTime DataRegistro { get; set; }

    [Column("cod_residente")]
    public int CodResidente { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    // Referência ao residente
    [Reference(typeof(Residentes))]
    public Residentes? Residente { get; set; }
}