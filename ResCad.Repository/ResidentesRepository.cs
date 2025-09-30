using AutoMapper;
using ResCad.Data;
using ResCad.Data.Interfaces;
using ResCad.Dominio.Dtos;
using ResCad.Repository.Interfaces;
using Supabase;
using System;
using System.Text.Json;

namespace ResCad.Repository;

public class ResidentesRepository(
        IRepositorioMap<ResidentesDto, Residentes> resMap,
        Client supabase
    ) : IResidentesRepository
{
    private readonly IRepositorioMap<ResidentesDto, Residentes> _resMap = resMap;
    private readonly Client _supabase = supabase;

    // Metodo nao utilizado em producao, para testes locais
    //public Task<ResidentesDto> GetResidentesLocal()
    //{
    //    try
    //    {
    //        using var connection = new Connection().GetConnection();
    //        connection.Open();

    //        Console.WriteLine(connection.State);

    //    }
    //    catch (Exception ex) 
    //    { 
    //        Console.WriteLine(ex.Message);
    //    }
    //    return Task.FromResult(new ResidentesDto());
    //}


    public async Task<List<ResidentesDto>> ObtemResidentes()
    {
        try
        {
            var residentes = new List<ResidentesDto>();

            // Buscar todos os residentes
            var response = await _supabase
                .From<Residentes>()
                .Get();

            // Para cada residente, buscar seus relacionamentos
            foreach (var residente in response.Models)
            {
                var residenteCompleto = await BuscarResidenteComRelacionamentos(residente.CodResidente);
                residentes.Add(_resMap.ParaDominio(residenteCompleto));
            }

            return residentes;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Erro ao obter residentes: {ex.Message}", ex);
        }
    }


    public async Task<ResidentesDto?> ObtemResidentesPorId(int id)
    {
        try
        {
            var residenteCompleto = await BuscarResidenteComRelacionamentos(id);
            return residenteCompleto != null ? _resMap.ParaDominio(residenteCompleto) : null;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Erro ao obter residente {id}: {ex.Message}", ex);
        }
    }


    public async Task<ResidentesDto> CriaResidente(ResidentesDto residenteDto)
    {
        try
        {
            var residente = _resMap.ParaEntidade(residenteDto);
            residente.CreatedAt = DateTime.UtcNow;
            residente.UpdatedAt = DateTime.UtcNow;

            // Insert do residente
            var response = await _supabase
                .From<Residentes>()
                .Insert(residente);

            var residenteInserido = response.Models.FirstOrDefault();
            if (residenteInserido == null)
                throw new ApplicationException("Erro ao inserir residente");

            // Inserir familiares se houver
            if (residenteDto.Familiares?.Any() == true)
            {
                var familiares = residenteDto.Familiares.Select(f => new Familiares
                {
                    Nome = f.Nome,
                    Parentesco = f.Parentesco,
                    Telefone = f.Telefone,
                    Email = f.Email,
                    CodResidente = residenteInserido.CodResidente,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                await _supabase
                    .From<Familiares>()
                    .Insert(familiares);
            }

            // Inserir histórico de saúde se houver
            if (residenteDto.HistoricosSaude?.Any() == true)
            {
                var historicos = residenteDto.HistoricosSaude.Select(h => new HistoricoSaude
                {
                    CondicoesMedicas = h.CondicoesMedicas,
                    Medicamentos = h.Medicamentos,
                    Alergias = h.Alergias,
                    RestricoesAlimentares = h.RestricoesAlimentares,
                    DataRegistro = h.DataRegistro,
                    CodResidente = residenteInserido.CodResidente,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

                await _supabase
                    .From<HistoricoSaude>()
                    .Insert(historicos);
            }

            // Buscar o residente e seus relacionamentos separadamente
            var residenteCompleto = await BuscarResidenteComRelacionamentos(residenteInserido.CodResidente);

            return _resMap.ParaDominio(residenteCompleto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Erro ao criar residente: {ex.Message}", ex);
        }
    }


    private async Task<Residentes> BuscarResidenteComRelacionamentos(int codResidente)
    {
        // Buscar residente
        var residenteResponse = await _supabase
            .From<Residentes>()
            .Where(r => r.CodResidente == codResidente)
            .Get();

        var residente = residenteResponse.Models.FirstOrDefault();
        if (residente == null) return null;

        // Buscar familiares separadamente
        var familiaresResponse = await _supabase
            .From<Familiares>()
            .Where(f => f.CodResidente == codResidente)
            .Get();

        residente.Familiares = familiaresResponse.Models.ToList();

        // Buscar histórico de saúde separadamente
        var historicosResponse = await _supabase
            .From<HistoricoSaude>()
            .Where(h => h.CodResidente == codResidente)
            .Get();

        residente.HistoricosSaude = historicosResponse.Models.ToList();

        return residente;
    }


    public async Task<ResidentesDto> AtualizaResidente(ResidentesDto residenteDto)
    {
        try
        {
            var residente = _resMap.ParaEntidade(residenteDto);
            residente.CodResidente = residenteDto.Id; // Importante!
            residente.UpdatedAt = DateTime.UtcNow;

            // Update do residente
            var response = await _supabase
                .From<Residentes>()
                .Where(r => r.CodResidente == residenteDto.Id)
                .Update(residente);

            // Atualizar/Inserir familiares
            await AtualizarFamiliares(residenteDto);

            // Atualizar/Inserir histórico de saúde
            await AtualizarHistoricosSaude(residenteDto);

            // Buscar o residente atualizado com relacionamentos
            var residenteAtualizado = await BuscarResidenteComRelacionamentos(residenteDto.Id);

            return _resMap.ParaDominio(residenteAtualizado);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Erro ao atualizar residente: {ex.Message}", ex);
        }
    }

    private async Task AtualizarFamiliares(ResidentesDto residenteDto)
    {
        if (residenteDto.Familiares?.Any() != true) return;

        // Primeiro, deletar familiares existentes
        await _supabase
            .From<Familiares>()
            .Where(f => f.CodResidente == residenteDto.Id)
            .Delete();

        // Inserir os novos familiares
        var familiares = residenteDto.Familiares.Select(f => new Familiares
        {
            Nome = f.Nome,
            Parentesco = f.Parentesco,
            Telefone = f.Telefone,
            Email = f.Email,
            CodResidente = residenteDto.Id,
            CreatedAt = DateTime.UtcNow
        }).ToList();

        await _supabase
            .From<Familiares>()
            .Insert(familiares);
    }

    private async Task AtualizarHistoricosSaude(ResidentesDto residenteDto)
    {
        if (residenteDto.HistoricosSaude?.Any() != true) return;

        // Primeiro, deletar históricos existentes
        await _supabase
            .From<HistoricoSaude>()
            .Where(h => h.CodResidente == residenteDto.Id)
            .Delete();

        // Inserir os novos históricos
        var historicos = residenteDto.HistoricosSaude.Select(h => new HistoricoSaude
        {
            CondicoesMedicas = h.CondicoesMedicas,
            Medicamentos = h.Medicamentos,
            Alergias = h.Alergias,
            RestricoesAlimentares = h.RestricoesAlimentares,
            DataRegistro = h.DataRegistro,
            CodResidente = residenteDto.Id,
            CreatedAt = DateTime.UtcNow
        }).ToList();

        await _supabase
            .From<HistoricoSaude>()
            .Insert(historicos);
    }


    public async Task<bool> DeletaResidente(int id)
    {
        try
        {
            // Primeiro deletar os relacionamentos (familiares e histórico)
            await _supabase
                .From<Familiares>()
                .Where(f => f.CodResidente == id)
                .Delete();

            await _supabase
                .From<HistoricoSaude>()
                .Where(h => h.CodResidente == id)
                .Delete();

            // Depois deletar o residente
            await _supabase
                .From<Residentes>()
                .Where(r => r.CodResidente == id)
                .Delete();

            // Verificar se o residente ainda existe (se não existe, foi deletado com sucesso)
            var residenteVerificado = await _supabase
                .From<Residentes>()
                .Where(r => r.CodResidente == id)
                .Single();

            return residenteVerificado == null;
        }
        catch (Exception ex)
        {
            // Se der exception ao buscar o residente, significa que foi deletado
            if (ex.Message.Contains("not found") || ex.Message.Contains("null"))
                return true;

            throw new ApplicationException($"Erro ao deletar residente {id}: {ex.Message}", ex);
        }
    }
}
