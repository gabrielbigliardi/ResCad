using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResCad.Web.Dtos;

public class HistoricoSaudeDto
{
    public int Id { get; set; }
    public string CondicoesMedicas { get; set; } = string.Empty;
    public string Medicamentos { get; set; } = string.Empty;
    public string Alergias { get; set; } = string.Empty;
    public string RestricoesAlimentares { get; set; } = string.Empty;
    public DateTime? DataRegistro { get; set; }
    public int CodResidente { get; set; }
}
