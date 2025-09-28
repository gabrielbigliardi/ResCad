using ResCad.Dominio.Dtos;

namespace ResCad.Repository.Interfaces;

public interface IResidentesRepository
{
    public Task<ResidentesDto> GetResidentes();
    public Task<ResidentesDto> GetResidentesSB();
}
