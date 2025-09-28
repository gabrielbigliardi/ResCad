using ResCad.Data;
using ResCad.Dominio.Dtos;
using ResCad.Repository.Interfaces;
using Supabase;

namespace ResCad.Repository;

public class ResidentesRepository : IResidentesRepository
{
    

    public Task<ResidentesDto> GetResidentes()
    {
        try
        {
            using var connection = new Connection().GetConnection();
            connection.Open();

            Console.WriteLine(connection.State);

        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.Message);
        }
        return Task.FromResult(new ResidentesDto());
    }


    public async Task<ResidentesDto> GetResidentesSB()
    {
        //var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
        var url = "https://fkpjmlgwsovbyygdqmys.supabase.co";
        //var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");
        var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImZrcGptbGd3c292Ynl5Z2RxbXlzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTg4MzIyMDgsImV4cCI6MjA3NDQwODIwOH0.65hZS6ud8t8uskyeOQ59viAfi7KsDClxaAZ9i10JJqg";

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = true,
            Schema = "public" // Especifica o schema explicitamente
        };

        var supabase = new Client(url, key, options);

        // Inicializa a conexão
        await supabase.InitializeAsync();

        // Faz a consulta
        var response = await supabase
            .From<Residentes>()
            .Get();

        return new ResidentesDto();
        //return await supabase.InitializeAsync();
    }
}
