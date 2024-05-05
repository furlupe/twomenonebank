using Bank.Common;
using Bank.Common.Extensions;
using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bank.Core.App.Controllers;

[Route("test")]
public class TestController(
    TestClient client,
    ICurrencyConverter converter,
    IOptions<FeaturesOptions> options
) : ControllerBase
{
    [HttpGet("error")]
    public async Task Error()
    {
        await client.MakeErrorRequest();
    }

    [HttpGet("convert")]
    public async Task<Money> Convert([FromQuery] Money value, [FromQuery] Currency target)
    {
        return await converter.Convert(value, target);
    }

    [HttpGet("request")]
    public async Task<object?> TestRequest()
    {
        return await client.MakeRequest();
    }

    [HttpGet("features")]
    public FeaturesOptions Features()
    {
        return options.Value;
    }
}

public class TestClient(HttpClient httpClient)
{
    public async Task<object?> MakeErrorRequest()
    {
        return await httpClient.GetAsJson<object>("http://localhost:30000/test/error");
    }

    public async Task<object?> MakeRequest()
    {
        return await httpClient.GetAsJson<object>("http://localhost:30000/test/rates/USD");
    }
}
