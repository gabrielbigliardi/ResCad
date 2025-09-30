using AutoMapper;
using ResCad.Data.Interfaces;
using ResCad.Dominio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResCad.Data.Mapping;

public class ResidentesMap : IRepositorioMap<ResidentesDto, Residentes>
{
    public ResidentesDto ParaDominio(Residentes ep)
    {
        return new ResidentesDto
        {
            Id = ep.CodResidente,
            NomeCompleto = ep.NomeCompleto,
            NumeroDocumento = ep.NumeroDocumento,
            DataNascimento = ep.DataNascimento,
            DataEntrada = ep.DataEntrada,
            Genero = ep.Genero,
            EstadoCivil = ep.EstadoCivil,
            Ativo = ep.Ativo,
            DataSaida = ep.DataSaida,
            TelefoneContato = ep.TelefoneContato,
            // Mapear familiares
            Familiares = ep.Familiares?.Select(f => new FamiliaresDto
            {
                Id = f.CodFamiliar,
                Nome = f.Nome,
                Parentesco = f.Parentesco,
                Telefone = f.Telefone,
                Email = f.Email,
                CodResidente = f.CodResidente
            }).ToList(),
            // Mapear histórico de saúde
            HistoricosSaude = ep.HistoricosSaude?.Select(h => new HistoricoSaudeDto
            {
                Id = h.CodHistSaude,
                CondicoesMedicas = h.CondicoesMedicas,
                Medicamentos = h.Medicamentos,
                Alergias = h.Alergias,
                RestricoesAlimentares = h.RestricoesAlimentares,
                DataRegistro = h.DataRegistro,
                CodResidente = h.CodResidente
            }).ToList()
        };
    }

    public Residentes ParaEntidade(ResidentesDto dto)
    {
        return new Residentes
        {
            NomeCompleto = dto.NomeCompleto,
            NumeroDocumento = dto.NumeroDocumento,
            DataNascimento = dto.DataNascimento,
            DataEntrada = dto.DataEntrada,
            Genero = dto.Genero,
            EstadoCivil = dto.EstadoCivil,
            Ativo = dto.Ativo,
            DataSaida = dto.DataSaida,
            TelefoneContato = dto.TelefoneContato
            // Não mapear Familiares e HistoricosSaude aqui para evitar loops
        };
    }
}