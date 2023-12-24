using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Win32.SafeHandles;
using MusicDiscoveryApp.ApiCalls;
using Newtonsoft.Json;
using MusicDiscoveryApp.Models;
using static System.Net.WebRequestMethods;

class Apicalls
{
   //static async Task Main()
   //{

   //    if (!string.IsNullOrEmpty(accessToken))
   //    {

   //        List<string> artistIds = new List<string> { };


   //        artistIds.Add("26VFTg2z8YR0cCuwLzESi2");
   //        //artistIds.Add("3fMbdgg4jU18AjLCKBhRSm");
   //        // Construct the URL with the list of artistIds
   //        string relatedArtistsUrl = $"https://api.spotify.com/v1/artists/{string.Join(",", artistIds)}/related-artists";
   //        // Example 1: Get related artists for a specific artist
   //        await MakeSpotifyApiRequestAndPrint("Related Artists:", relatedArtistsUrl, accessToken);



   //        //string artistId = "0OdUWJ0sBjDrqHygGUXeCF";
   //        //string recommendationsUrl = $"https://api.spotify.com/v1/recommendations?seed_artists={artistId}";
   //        //await MakeSpotifyApiRequestAndPrint("Recommendations:", recommendationsUrl, accessToken);


   //        // Example 2: Get audio analysis for a specific track
   //        //string trackId = "0OdUWJ0sBjDrqHygGUXeCF";
   //        //string trackAnalysisUrl = $"https://api.spotify.com/v1/audio-analysis/{trackId}";
   //        //await MakeSpotifyApiRequestAndPrint("Track Analysis:", trackAnalysisUrl, accessToken);


   //    }
   //    else
   //    {
   //        Console.WriteLine("Failed to obtain access token.");
   //    }
   //}

   public static async Task<object> GetTrack(string trackId) {

        AuthResult keys = new AuthResult();

        string accessToken = keys.AccessToken;
        string apiUrl = $"https://api.spotify.com/v1/tracks/{trackId}";
        var response = await MakeSpotifyApiRequest(apiUrl, accessToken);



        return response;
       

    }


    public static async Task<object> GetRecomandationRandom()
    {
        string randomgenreUrl = "https://binaryjazz.us/wp-json/genrenator/v1/genre/";
        var randomgenre = await MakeRandomApiRequest(randomgenreUrl);

        AuthResult keys = new AuthResult();

        string accessToken = keys.AccessToken;
        string apiUrl = $"https://api.spotify.com/v1/recommendations?limit=1&market=BE&seed_genres={randomgenre}&min_duration_ms=60000";
        var response = await MakeSpotifyApiRequest(apiUrl, accessToken);

        return response;
        }


    //static async Task MakeSpotifyApiRequestAndPrint(string title, string apiUrl, string accessToken)
    //{
    //    Console.WriteLine($"{title} Requesting from: {apiUrl}");

    //    string response = await MakeSpotifyApiRequest(apiUrl, accessToken);

    //    if (!string.IsNullOrEmpty(response))
    //    {
    //        Console.WriteLine($"{title} Response:");
    //        Console.WriteLine(response);
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Failed to get {title.ToLower()}.");
    //    }
    //}

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

    static async Task<string> MakeRandomApiRequest(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
     
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

}
