using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorApp1;

public class MycustomeAuthenticationStateProvider : AuthenticationStateProvider
{
    ILocalStorageService localStoage;

    public MycustomeAuthenticationStateProvider(ILocalStorageService localStoage)
    {
        this.localStoage = localStoage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string tokenJWT = await localStoage.GetItemAsStringAsync("token");
        if(string.IsNullOrWhiteSpace(tokenJWT))
        {
            return new AuthenticationState(new ClaimsPrincipal());
        }
        var jwt = new JwtSecurityTokenHandler();
        var jwtReader = jwt.ReadJwtToken(tokenJWT);
        return new AuthenticationState(new ClaimsPrincipal(new[]
        {
            new ClaimsIdentity(jwtReader.Claims,"Bearer")
        }));
    }
}
