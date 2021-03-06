﻿using System;
using System.Threading.Tasks;
using AeroGear.Mobile.Auth.Authenticator;
using OpenId.AppAuth;
using Org.Json;

namespace Auth.Platform.Authenticator.extensions
{
    internal static class TokenLifecycleManagerExtensions
    {
        public async static Task<TokenResponse> RefreshTokenAsync(this TokenLifecycleManager tlcm, TokenRequest tokenRequest) {
            try
            {
                var json = await tlcm.RefreshAsync(tokenRequest.Configuration.TokenEndpoint.ToString(), tokenRequest.ClientId, tokenRequest.RefreshToken).ConfigureAwait(false);
                return new TokenResponse.Builder(tokenRequest).FromResponseJson(new JSONObject(json.ToString())).Build();
            }
            catch (AuthzException ae)
            {
                throw ae;
            }
            catch (Exception je)
            {
                throw AuthzException.fromTemplate(
                    AuthzException.GeneralErrors.JSON_DESERIALIZATION_ERROR, je);
            }
        }
    }
}
