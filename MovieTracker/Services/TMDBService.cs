using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using MovieTracker.Models;

namespace MovieTracker.Services
{
    public class TMDBService
    {
        private readonly HttpClient _httpClient;
        private const string BASE_URL = "https://api.themoviedb.org/3/";
        private const string API_KEY = "2b00fc34c31c44759fb69b7f8c64b75e"; 

        public TMDBService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BASE_URL);
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //the holy grail
        // this method will call the TMDB API to get the popular movies and return a MovieResponse
        public async Task<MovieResponse?> GetPopularMovies()

        {
            var response = await _httpClient.GetAsync($"movie/popular?api_key={API_KEY}");
            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine("TMDB JSON: " + json); 

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<MovieResponse>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }
    }
}