using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResCad.Web.Dtos
{
    public class ResidentesDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public DateTime? DataEntrada { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string EstadoCivil { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime? DataSaida { get; set; }
        public string? TelefoneContato { get; set; }

        public List<FamiliaresDto>? Familiares { get; set; } = new List<FamiliaresDto>();
        public List<HistoricoSaudeDto>? HistoricosSaude { get; set; } = new List<HistoricoSaudeDto>();
    }
}
