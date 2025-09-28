using ResCad.Dominio.Dtos;

namespace ResCad.Application.Interfaces;

public interface IResidentesAplService
{
    public Task<ResidentesDto> GetAllResidentes();
}
