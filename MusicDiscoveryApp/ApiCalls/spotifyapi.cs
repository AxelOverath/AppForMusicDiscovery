/*using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
/*
using Newtonsoft.Json;

class Program
{
    static async Task Main()
    {
        keys keys = new keys();
        string clientId = keys.getclientId();
        string clientSecret = keys.getclientSecret();
        string accessToken = await GetAccessToken(clientId, clientSecret);

        if (!string.IsNullOrEmpty(accessToken))
        {

            List<string> artistIds = new List<string> { };


            artistIds.Add("26VFTg2z8YR0cCuwLzESi2");
            //artistIds.Add("3fMbdgg4jU18AjLCKBhRSm");
            // Construct the URL with the list of artistIds
            string relatedArtistsUrl = $"https://api.spotify.com/v1/artists/{string.Join(",", artistIds)}/related-artists";
            // Example 1: Get related artists for a specific artist
            await MakeSpotifyApiRequestAndPrint("Related Artists:", relatedArtistsUrl, accessToken);



            //string artistId = "0OdUWJ0sBjDrqHygGUXeCF";
            //string recommendationsUrl = $"https://api.spotify.com/v1/recommendations?seed_artists={artistId}";
            //await MakeSpotifyApiRequestAndPrint("Recommendations:", recommendationsUrl, accessToken);


            // Example 2: Get audio analysis for a specific track
            //string trackId = "0OdUWJ0sBjDrqHygGUXeCF";
            //string trackAnalysisUrl = $"https://api.spotify.com/v1/audio-analysis/{trackId}";
            //await MakeSpotifyApiRequestAndPrint("Track Analysis:", trackAnalysisUrl, accessToken);


        }
        else
        {
            Console.WriteLine("Failed to obtain access token.");
        }
    }

    static async Task<string> GetAccessToken(string clientId, string clientSecret)
    {
        string tokenUrl = "https://accounts.spotify.com/api/token";
        string base64Auth = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {base64Auth}");

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
            });

            HttpResponseMessage response = await client.PostAsync(tokenUrl, content);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(responseData);

                if (tokenResponse != null)
                {
                    return tokenResponse.access_token;
                }
                else
                {
                    Console.WriteLine("Failed to deserialize access token response.");
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

    class AccessTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }


    static async Task MakeSpotifyApiRequestAndPrint(string title, string apiUrl, string accessToken)
    {
        Console.WriteLine($"{title} Requesting from: {apiUrl}");

        string response = await MakeSpotifyApiRequest(apiUrl, accessToken);

        if (!string.IsNullOrEmpty(response))
        {
            Console.WriteLine($"{title} Response:");
            Console.WriteLine(response);
        }
        else
        {
            Console.WriteLine($"Failed to get {title.ToLower()}.");
        }
    }

    static async Task<string> MakeSpotifyApiRequest(string apiUrl, string accessToken)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
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
}*/