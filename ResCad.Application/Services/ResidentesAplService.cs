using ResCad.Application.Interfaces;
using ResCad.Dominio.Dtos;
using ResCad.Repository.Interfaces;

namespace ResCad.Application.Services;

public class ResidentesAplService(
        IResidentesRepository residentesRepository
    ) : IResidentesAplService
{
    private readonly IResidentesRepository _residentesRepository = residentesRepository;


    public Task<ResidentesDto> GetAllResidentes()
    {
        return _residentesRepository.GetResidentesSB();
    }

}
