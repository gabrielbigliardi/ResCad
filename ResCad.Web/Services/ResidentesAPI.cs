using ResCad.Web.Dtos;
using System.Net.Http.Json;

namespace ResCad.Web.Services;

public class ResidentesAPI
{
    private readonly HttpClient _httpClient;

    public ResidentesAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResidentesDto>> GetResidentesAsync()
    {
        try
        {
            // Tente a rota original
            return await _httpClient.GetFromJsonAsync<List<ResidentesDto>>("residentes")
                   ?? new List<ResidentesDto>();
        }
        catch
        {
            return new List<ResidentesDto>();
        }
    }

    public async Task<bool> CreateResidenteAsync(ResidentesDto residente)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("residentes", residente);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateResidenteAsync(ResidentesDto residente)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"residentes/{residente.Id}", residente);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }


    public async Task<bool> DeleteResidenteAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"residentes/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}