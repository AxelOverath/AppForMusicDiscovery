using System;
//using Android.App.AppSearch;
//using static Android.Provider.MediaStore.Audio;
namespace MusicDiscoveryApp.Services;

public interface ISpotifyService
{
    Task<bool> Initialize(string authCode);
}

