using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResCad.Dominio.Dtos;

public class FamiliaresDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Parentesco { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public int CodResidente { get; set; }
}
