using Bank.Common.Extensions;
using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.App.Controllers;

[Route("test")]
public class TestController(TestClient service, ICurrencyConverter converter) : ControllerBase
{
    [HttpGet("error")]
    public async Task Error()
    {
        await service.MakeErrorRequest();
    }

    [HttpGet("convert")]
    public async Task<Money> Convert([FromQuery] Money value, [FromQuery] Currency target)
    {
        return await converter.Convert(value, target);
    }
}

public class TestClient(HttpClient httpClient)
{
    public async Task<object> MakeErrorRequest()
    {
        return await httpClient.GetAsJson<object>("https://localhost:7200/test/error");
    }
}
