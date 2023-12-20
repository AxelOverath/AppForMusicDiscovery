﻿using System;

namespace MusicDiscoveryApp.Models;

public record AuthResult
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}