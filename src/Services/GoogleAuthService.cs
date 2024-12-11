﻿using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using MarketplaceBackend.DTOs;
using MarketplaceBackend.Interfaces;
using MarketplaceBackend.Models;
using MarketplaceBackend.Repositories;

namespace MarketplaceBackend.Services;

public class GoogleAuthService(IConfiguration configuration, IUserRepository _userRepository) : IGoogleAuthService
{
    private readonly string _clientId = configuration["GoogleAuthCredentials:ClientId"]!;
    private readonly string _clientSecret = configuration["GoogleAuthCredentials:ClientSecret"]!;
    private readonly string _redirectUri = configuration["GoogleAuthCredentials:RedirectUri"]!;

    public Uri GetGoogleAuthUrl()
    {
        var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret
            },
            Scopes = new[] { "email", "profile" }
        });

        return flow.CreateAuthorizationCodeRequest(_redirectUri).Build();
    }

    public async Task<GoogleAuthResultDto?> HandleGoogleCallbackAsync(string code)
    {
        try
        {
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = _clientId,
                    ClientSecret = _clientSecret
                }
            });

            var tokenResponse = await flow.ExchangeCodeForTokenAsync("me", code, _redirectUri, CancellationToken.None);

            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenResponse.IdToken);

            return new GoogleAuthResultDto
            {
                AuthToken = payload.Email,
                Email = payload.Email,
                Username = payload.Email,
                AvatarUri = payload.Picture
            };
        }
        catch
        {
            return null;
        }
    }
}
