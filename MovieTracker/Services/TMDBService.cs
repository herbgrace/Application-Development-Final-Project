using MovieTracker.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MovieTracker.Services;

public class TmdbService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "2b00fc34c31c44759fb69b7f8c64b75e"; 
    private const string BaseUrl = "https://api.themoviedb.org/3";

    public TmdbService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MovieResponse> GetMovies(string genreIds = null, string sortBy = "popularity.desc", int page = 1)
    {

        var url = $"{BaseUrl}/discover/movie?api_key={ApiKey}&sort_by={sortBy}&page={page}";

        if (!string.IsNullOrEmpty(genreIds))
        {
            url += $"&with_genres={genreIds}";
        }

        return await _httpClient.GetFromJsonAsync<MovieResponse>(url);
    }

    public async Task<MovieResponse> SearchMovies(string query, int page = 1)
    {

        var url = $"{BaseUrl}/search/movie?api_key={ApiKey}&query={Uri.EscapeDataString(query)}&page={page}";

        return await _httpClient.GetFromJsonAsync<MovieResponse>(url);
    }

    public async Task<Movie> GetMovieDetails(int movieId)
    {
        var url = $"{BaseUrl}/movie/{movieId}?api_key={ApiKey}";
        return await _httpClient.GetFromJsonAsync<Movie>(url);
    }

}