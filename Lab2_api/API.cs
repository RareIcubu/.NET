using System.Runtime.CompilerServices;
using System.Text.Json;

[assembly: InternalsVisibleTo("MovieGUI")]

namespace Lab2_api;

internal class API
{
    public HttpClient client;

    public async Task<Movie> GetData(string movieTitle)
    {
        client = new HttpClient();
        string call = $"http://www.omdbapi.com/?apikey=b59a7244&t={movieTitle}";
        string response = await client.GetStringAsync(call);
      
        Movie movieObj = JsonSerializer.Deserialize<Movie>(response);
      
        return movieObj;
    }
}