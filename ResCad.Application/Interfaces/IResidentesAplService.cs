using ResCad.Dominio.Dtos;

namespace ResCad.Application.Interfaces;

public interface IResidentesAplService
{
    public Task<List<ResidentesDto>> ObtemTodosResidentes();
    public Task<ResidentesDto?> ObtemUmResidente(int id);
    public Task<ResidentesDto> CriaResidente(ResidentesDto residente);
    public Task<ResidentesDto> AtualizaResidente(ResidentesDto residentes);
    public Task<bool> DeletaResidente(int id);

}
