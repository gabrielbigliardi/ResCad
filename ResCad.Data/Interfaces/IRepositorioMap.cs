namespace ResCad.Data.Interfaces;

public interface IRepositorioMap<TResposta, TEntidade>
{
    TResposta ParaDominio(TEntidade entPersistencia);
    TEntidade ParaEntidade(TResposta entResposta);
}
