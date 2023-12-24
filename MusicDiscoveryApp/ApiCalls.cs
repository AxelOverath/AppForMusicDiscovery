using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyAPI.Web;
namespace MusicDiscoveryApp
{
    internal class ApiCalls
    {
        internal class ApiResponse
        {
            public List<Seed> Seeds { get; set; }
            public List<Track> Tracks { get; set; }
        }

        internal class Seed
        {
            public int AfterFilteringSize { get; set; }
            public int AfterRelinkingSize { get; set; }
            public string Href { get; set; }
            public string Id { get; set; }
            public int InitialPoolSize { get; set; }
            public string Type { get; set; }
        }

        internal class Track
        {
            public Album Album { get; set; }
            public List<Artist> Artists { get; set; }
            public int DiscNumber { get; set; }
            public int DurationMs { get; set; }
            public bool Explicit { get; set; }
            public ExternalIds ExternalIds { get; set; }
            public ExternalUrls ExternalUrls { get; set; }
            public string Href { get; set; }
            public string Id { get; set; }
            public bool IsPlayable { get; set; }
            public string Name { get; set; }
            public int Popularity { get; set; }
            [JsonProperty("preview_url")]
            public string PreviewUrl { get; set; }
            public int TrackNumber { get; set; }
            public string Type { get; set; }
            public string Uri { get; set; }
            public bool IsLocal { get; set; }
            // Additional property to indicate if a preview is available
            public bool HasPreview => !string.IsNullOrEmpty(PreviewUrl);
        }

        internal class Album
        {
            public string AlbumType { get; set; }
            public int TotalTracks { get; set; }
            public ExternalUrls ExternalUrls { get; set; }
            public string Href { get; set; }
            public string Id { get; set; }
            public List<Image> Images { get; set; }
            public string Name { get; set; }
            public string ReleaseDate { get; set; }
            public string ReleaseDatePrecision { get; set; }
            public string Type { get; set; }
            public string Uri { get; set; }
            public List<Artist> Artists { get; set; }
            public bool IsPlayable { get; set; }
        }

        internal class Artist
        {
            public ExternalUrls ExternalUrls { get; set; }
            public string Href { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Uri { get; set; }
        }

        internal class ExternalIds
        {
            public string Isrc { get; set; }
        }

        internal class ExternalUrls
        {
            public string Spotify { get; set; }
        }

        internal class Image
        {
            public string Url { get; set; }
            public int Height { get; set; }
            public int Width { get; set; }
        }

        static async Task Main()
        {

            string clientId = Constants.SpotifyClientId;
            string clientSecret = Constants.SpotifyClientsecret;
            string accessToken = UserStorage.accessToken;

            if (!string.IsNullOrEmpty(accessToken))
            {
                List<string> artistIds = new List<string> { "26VFTg2z8YR0cCuwLzESi2" };

                string relatedArtistsUrl = $"https://api.spotify.com/v1/artists/{string.Join(",", artistIds)}/related-artists";
                ApiResponse relatedArtistsResponse = await MakeSpotifyApiRequest(relatedArtistsUrl, accessToken);

                // Do something with the response object, e.g., return it from the method or pass it to another function.
            }
            else
            {
                Console.WriteLine("Failed to obtain access token.");
            }
        }

        public static async Task<ApiCalls.ApiResponse> GetRandomSong()
        {
            // Get a random genre
            string randomGenre = await GetRandomGenre();

            if (randomGenre == null)
            {
                Console.WriteLine("Failed to obtain a random genre.");
                return null;
            }

            // Replace with the appropriate API endpoint for getting a random song
            string apiUrl = $"https://api.spotify.com/v1/recommendations?limit=1&market=BE&seed_genres={randomGenre}";

            // Make the API call to get a random song
            ApiCalls.ApiResponse response = await MakeSpotifyApiRequest(apiUrl, UserStorage.accessToken);

            // Return the API response
            return response;
        }



        static async Task<ApiResponse> MakeSpotifyApiRequest(string apiUrl, string accessToken)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    // Use Newtonsoft.Json.JsonConvert to deserialize the JSON string
                    ApiResponse apiResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse>(jsonContent);

                    return apiResponse;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Response: {errorResponse}");

                    return new ApiResponse
                    {
                        Seeds = new List<Seed>(),
                        Tracks = new List<Track>()
                    };
                }
            }
        }

        public static async Task<string> GetRandomGenre()
        {
            string genreApiUrl = "https://api.spotify.com/v1/recommendations/available-genre-seeds";

            using (HttpClient client = new HttpClient())
            {
                // Set up the Spotify API authorization header
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {UserStorage.accessToken}"); // Replace YOUR_ACCESS_TOKEN with your Spotify access token

                HttpResponseMessage response = await client.GetAsync(genreApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string genreJson = await response.Content.ReadAsStringAsync();

                    // Use Newtonsoft.Json.JsonConvert to deserialize the JSON string
                    var genreResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<GenreResponse>(genreJson);

                    if (genreResponse != null && genreResponse.Genres != null && genreResponse.Genres.Count > 0)
                    {
                        // Pick a random genre from the list
                        Random random = new Random();
                        int randomIndex = random.Next(genreResponse.Genres.Count);
                        string randomGenre = genreResponse.Genres[randomIndex];

                        return randomGenre;
                    }
                    else
                    {
                        Console.WriteLine("Error: Unable to parse genre response.");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Response: {errorResponse}");

                    return null;
                }
            }
        }

        // Define a class to represent the structure of the Spotify API response
        public class GenreResponse
        {
            public List<string> Genres { get; set; }
        }


    }
}