## NU2Rest 1.0b

### A service for developers that need to do asynchronous requests to Web APIs.

---


## Focus on business code, not infra code

This service is very useful for developers that don't want to waste time on HTTP requests code. It's simple to use and can save many development hours. See the example:

    Developer: I need to call the bank service to get user balance. But I don't have any idea how it works.


### C# Code
```

//Create a request instance
IRestRequest request = restService.CreateRequest("http://mybank.com/account/{id}/balance");

//Add params for request
request.Params.Add("id", "XXXXXX");

//Do request and save response
RestResponse<BalanceModel> response = await request.Read<BalanceModel>();

//Do some work with balance
Console.WriteLine($"My balance is: {response.Data.Balance}");

//Done!

```

## Get Started - C# Web Api

1. Download the package on Nuget.
2. On ConfigureServices put this code:

```

services.AddRestService(RestScheme.HTTPS);

```

3. On controller constructor:

```

public MyController(IRestService restService) {}

```

4. On an action make requests using the service to create it.
    


