using System.Text.Json;
using Bank.Exceptions.WebApiException;
using Microsoft.AspNetCore.Mvc;

namespace Bank.TransactionsGateway.App.Controllers;

[Route("test")]
[ApiController]
public class TestController() : ControllerBase
{
    [HttpGet("error")]
    public async Task Error()
    {
        throw new FailedRequestException(
            new NotFoundException("He gave us up.").Details,
            new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                ReasonPhrase = "NotFound",
                RequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://www.youtube.com/watch?v=dQw4w9WgXcQ")
                }
            }
        );
    }

    [HttpGet("rates/USD")]
    public async Task<object> Rates()
    {
        return JsonSerializer.Deserialize<object>(_rates);
    }

    private const string _rates =
        "{\r\n \"result\":\"success\",\r\n \"documentation\":\"https://www.exchangerate-api.com/docs\",\r\n \"terms_of_use\":\"https://www.exchangerate-api.com/terms\",\r\n \"time_last_update_unix\":1711065601,\r\n \"time_last_update_utc\":\"Fri, 22 Mar 2024 00:00:01 +0000\",\r\n \"time_next_update_unix\":1711152001,\r\n \"time_next_update_utc\":\"Sat, 23 Mar 2024 00:00:01 +0000\",\r\n \"base_code\":\"USD\",\r\n \"conversion_rates\":{\r\n  \"USD\":1,\r\n  \"AED\":3.6725,\r\n  \"AFN\":71.1233,\r\n  \"ALL\":94.6338,\r\n  \"AMD\":399.4717,\r\n  \"ANG\":1.7900,\r\n  \"AOA\":844.3200,\r\n  \"ARS\":854.2500,\r\n  \"AUD\":1.5201,\r\n  \"AWG\":1.7900,\r\n  \"AZN\":1.7004,\r\n  \"BAM\":1.7984,\r\n  \"BBD\":2.0000,\r\n  \"BDT\":109.7303,\r\n  \"BGN\":1.7978,\r\n  \"BHD\":0.3760,\r\n  \"BIF\":2857.4152,\r\n  \"BMD\":1.0000,\r\n  \"BND\":1.3422,\r\n  \"BOB\":6.9263,\r\n  \"BRL\":4.9684,\r\n  \"BSD\":1.0000,\r\n  \"BTN\":83.2020,\r\n  \"BWP\":13.6457,\r\n  \"BYN\":3.2579,\r\n  \"BZD\":2.0000,\r\n  \"CAD\":1.3519,\r\n  \"CDF\":2737.2899,\r\n  \"CHF\":0.8966,\r\n  \"CLP\":963.0934,\r\n  \"CNY\":7.2090,\r\n  \"COP\":3878.2224,\r\n  \"CRC\":502.8705,\r\n  \"CUP\":24.0000,\r\n  \"CVE\":101.3875,\r\n  \"CZK\":23.2315,\r\n  \"DJF\":177.7210,\r\n  \"DKK\":6.8624,\r\n  \"DOP\":58.9664,\r\n  \"DZD\":134.2929,\r\n  \"EGP\":46.6926,\r\n  \"ERN\":15.0000,\r\n  \"ETB\":56.7216,\r\n  \"EUR\":0.9195,\r\n  \"FJD\":2.2692,\r\n  \"FKP\":0.7884,\r\n  \"FOK\":6.8624,\r\n  \"GBP\":0.7884,\r\n  \"GEL\":2.7077,\r\n  \"GGP\":0.7884,\r\n  \"GHS\":12.9980,\r\n  \"GIP\":0.7884,\r\n  \"GMD\":66.3526,\r\n  \"GNF\":8566.8170,\r\n  \"GTQ\":7.7976,\r\n  \"GYD\":209.3042,\r\n  \"HKD\":7.8215,\r\n  \"HNL\":24.6919,\r\n  \"HRK\":6.9279,\r\n  \"HTG\":133.8442,\r\n  \"HUF\":362.2583,\r\n  \"IDR\":15686.8588,\r\n  \"ILS\":3.6106,\r\n  \"IMP\":0.7884,\r\n  \"INR\":83.2058,\r\n  \"IQD\":1310.5047,\r\n  \"IRR\":41956.1832,\r\n  \"ISK\":135.9959,\r\n  \"JEP\":0.7884,\r\n  \"JMD\":153.9169,\r\n  \"JOD\":0.7090,\r\n  \"JPY\":151.4988,\r\n  \"KES\":132.4400,\r\n  \"KGS\":89.5183,\r\n  \"KHR\":4041.8327,\r\n  \"KID\":1.5201,\r\n  \"KMF\":452.3593,\r\n  \"KRW\":1331.1638,\r\n  \"KWD\":0.3074,\r\n  \"KYD\":0.8333,\r\n  \"KZT\":450.8770,\r\n  \"LAK\":20799.8601,\r\n  \"LBP\":89500.0000,\r\n  \"LKR\":303.8320,\r\n  \"LRD\":193.5696,\r\n  \"LSL\":18.8123,\r\n  \"LYD\":4.8231,\r\n  \"MAD\":10.0385,\r\n  \"MDL\":17.7221,\r\n  \"MGA\":4458.5618,\r\n  \"MKD\":56.6231,\r\n  \"MMK\":2101.6081,\r\n  \"MNT\":3418.6724,\r\n  \"MOP\":8.0563,\r\n  \"MRU\":39.9741,\r\n  \"MUR\":46.0881,\r\n  \"MVR\":15.4303,\r\n  \"MWK\":1690.2567,\r\n  \"MXN\":16.7421,\r\n  \"MYR\":4.7139,\r\n  \"MZN\":63.8301,\r\n  \"NAD\":18.8123,\r\n  \"NGN\":1447.5321,\r\n  \"NIO\":36.6904,\r\n  \"NOK\":10.6533,\r\n  \"NPR\":133.1232,\r\n  \"NZD\":1.6523,\r\n  \"OMR\":0.3845,\r\n  \"PAB\":1.0000,\r\n  \"PEN\":3.6965,\r\n  \"PGK\":3.7687,\r\n  \"PHP\":56.0254,\r\n  \"PKR\":278.6493,\r\n  \"PLN\":3.9580,\r\n  \"PYG\":7306.1710,\r\n  \"QAR\":3.6400,\r\n  \"RON\":4.5571,\r\n  \"RSD\":107.4291,\r\n  \"RUB\":91.9094,\r\n  \"RWF\":1294.0553,\r\n  \"SAR\":3.7500,\r\n  \"SBD\":8.3265,\r\n  \"SCR\":13.4453,\r\n  \"SDG\":510.8776,\r\n  \"SEK\":10.4562,\r\n  \"SGD\":1.3422,\r\n  \"SHP\":0.7884,\r\n  \"SLE\":22.6030,\r\n  \"SLL\":22602.9839,\r\n  \"SOS\":571.3661,\r\n  \"SRD\":35.2463,\r\n  \"SSP\":1587.1290,\r\n  \"STN\":22.5275,\r\n  \"SYP\":12924.8884,\r\n  \"SZL\":18.8123,\r\n  \"THB\":36.2209,\r\n  \"TJS\":10.9480,\r\n  \"TMT\":3.4979,\r\n  \"TND\":3.0953,\r\n  \"TOP\":2.3411,\r\n  \"TRY\":32.1079,\r\n  \"TTD\":6.7791,\r\n  \"TVD\":1.5201,\r\n  \"TWD\":31.8989,\r\n  \"TZS\":2539.1950,\r\n  \"UAH\":38.9518,\r\n  \"UGX\":3881.7701,\r\n  \"UYU\":38.3251,\r\n  \"UZS\":12520.6869,\r\n  \"VES\":36.3217,\r\n  \"VND\":24780.4553,\r\n  \"VUV\":119.6671,\r\n  \"WST\":2.7424,\r\n  \"XAF\":603.1458,\r\n  \"XCD\":2.7000,\r\n  \"XDR\":0.7506,\r\n  \"XOF\":603.1458,\r\n  \"XPF\":109.7246,\r\n  \"YER\":250.1655,\r\n  \"ZAR\":18.8127,\r\n  \"ZMW\":25.9798,\r\n  \"ZWL\":20076.6007\r\n }\r\n}";
}
