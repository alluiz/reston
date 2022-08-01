using RestOn.Auth;
using RestOn.Service;

var restService = new RestService();

//Only for tests with https://openidconnect.net/
var credentials = new OAuth2Credentials(
    "kbyuFDidLLm280LIwVFiazOqjO3ty8KH",
    "60Op4HFM0I8ajz0WdiStAbziZ-VFQttXuxixHHs2R7r7-CW8GR79l-mmLqMhc-Sa");

var endpoints = new OAuth2Endpoints(
    "https://samples.auth0.com/oauth/token",
    "https://samples.auth0.com/oauth/token");

var idpService = new IdentityProviderService(endpoints, credentials, restService);

var token =  await idpService.GetToken(new OpenIdConnectCredentials(
    "aORBmYHW77lk_Ao-wdAsCoguoPlfqInw8CQSwcYsSJU_H", 
    "https://openidconnect.net/callback"));

Console.WriteLine(token.Access_Token);