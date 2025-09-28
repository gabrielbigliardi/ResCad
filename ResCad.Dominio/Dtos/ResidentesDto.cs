using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResCad.Dominio.Dtos
{
    public class ResidentesDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string EstadoCivil { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime? DataSaida { get; set; }
        public string? TelefoneContato { get; set; }
    }
}
