# Pan(パン)
A simple and lightweight utility but also provide full customization for flexible usage.

| Utility | Status                                                                                                                                                                                             |
| ------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Pan.Web | [![Build Status](https://dev.azure.com/rairkao/rairkao/_apis/build/status/rai-kao.Pan?branchName=master)](https://dev.azure.com/rairkao/rairkao/_build/latest?definitionId=1&branchName=master)    |

## Pan.Web
Pan.Web provides HTTP methods and auto request/response conversions.

### Getting Started

Startup.cs
```c#
using Pan.Web

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.UserPanWeb();
    }
}
```

Example.cs
```c#
using Pan.Web

public class Example
{
    private readonly IRestClient _restClient;

    public Example(IRestClient restClient)
    {
        _restClient = restClient;
        _restClient.Host("http://your-host");
    }
    
    public async void DoSomething()
    {
        var response = await _restClient.Get<ResponseDto>("/path-to-action");
    } 
}
```

