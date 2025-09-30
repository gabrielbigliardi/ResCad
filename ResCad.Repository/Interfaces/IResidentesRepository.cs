using ResCad.Dominio.Dtos;

namespace ResCad.Repository.Interfaces;

public interface IResidentesRepository
{
    //public Task<ResidentesDto> GetResidentesLocal();
    public Task<List<ResidentesDto>> ObtemResidentes();
    public Task<ResidentesDto?> ObtemResidentesPorId(int id);
    public Task<ResidentesDto> CriaResidente(ResidentesDto residente);
    public Task<ResidentesDto> AtualizaResidente(ResidentesDto residente);
    public Task<bool> DeletaResidente(int id);
}
