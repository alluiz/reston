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

var idpService = new IdentityService(endpoints, credentials, restService);

var token =  await idpService.ExchangeForToken(new OpenIdConnectCredentials(
    "vmWOSzBXFBNfDXg0TSjMc1JdnBptnAm2lfDkdT6UGeICz", 
    "https://openidconnect.net/callback"));

Console.WriteLine(token.ToString());