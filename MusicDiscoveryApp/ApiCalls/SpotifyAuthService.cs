
/*using Amazon.SecurityToken.Model;

using MongoDB.Bson.Serialization.Serializers;
//using MusicDiscoveryApp.ApiCalls;
//using Security;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using Swan;
using System;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
/*
public class SpotifyAuthService
{
    private static EmbedIOAuthServer _server;

    private static keys sleutel = new keys();

    private static string clientid = sleutel.getclientId();
    private static string clientsecret = sleutel.getclientSecret();

    public static async Task Main()
    {


        // Make sure "http://localhost:5543/callback" is in your spotify application as redirect uri!
        _server = new EmbedIOAuthServer(new Uri("http://localhost:5543/callback"), 5543);
        await _server.Start();

        _server.AuthorizationCodeReceived += OnAuthorizationCodeReceived;
        _server.ErrorReceived += OnErrorReceived;

        var request = new LoginRequest(_server.BaseUri, clientid, LoginRequest.ResponseType.Code)
        {
            Scope = new List<string> { Scopes.UserReadEmail }
        };
        BrowserUtil.Open(request.ToUri());

       
    }

    public static async Task OnAuthorizationCodeReceived(object sender, AuthorizationCodeResponse response)
    {
        await _server.Stop();

        var config = SpotifyClientConfig.CreateDefault();
        var tokenResponse = await new OAuthClient(config).RequestToken(
          new AuthorizationCodeTokenRequest(
            clientid, clientsecret, response.Code, new Uri("http://localhost:5543/callback")
          )
        );

        var spotify = new SpotifyClient(tokenResponse.AccessToken);
        // do calls with Spotify and save token?
        
    }
    

    private static async Task OnErrorReceived(object sender, string error, string state)
    {
        Console.WriteLine($"Aborting authorization, error received: {error}");
        await _server.Stop();
    }


}*/

