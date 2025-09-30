using ResCad.Application.Interfaces;
using ResCad.Dominio.Dtos;
using ResCad.Repository.Interfaces;

namespace ResCad.Application.Services;

public class ResidentesAplService(
        IResidentesRepository residentesRepository
    ) : IResidentesAplService
{
    private readonly IResidentesRepository _residentesRepository = residentesRepository;


    public Task<List<ResidentesDto>> ObtemTodosResidentes()
    {
        return _residentesRepository.ObtemResidentes();
    }

    public Task<ResidentesDto?> ObtemUmResidente(int id)
    {
        return _residentesRepository.ObtemResidentesPorId(id);
    }

    public Task<ResidentesDto> CriaResidente(ResidentesDto residente)
    {
        return _residentesRepository.CriaResidente(residente);
    }

    public Task<ResidentesDto> AtualizaResidente(ResidentesDto residente)
    {
        return _residentesRepository.AtualizaResidente(residente);
    }

    public Task<bool> DeletaResidente(int id)
    {
        return _residentesRepository.DeletaResidente(id);
    }

}
