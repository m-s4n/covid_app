using Covid.API.Entities;
using Covid.API.Enums;
using Covid.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Covid.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CovidController: ControllerBase
{
    private readonly CovidBilgiService _covidBilgiService;

    public CovidController(CovidBilgiService covidBilgiService) => _covidBilgiService = covidBilgiService;

    [HttpPost]
    public async Task<IActionResult> SaveCovid(CovidBilgi covidBilgi)
    {
        await _covidBilgiService.SaveCovid(covidBilgi);
        // eklendiktn sonra tüm covid bilgilerini client'lara gönderilir
        //List<CovidBilgi> bilgi = _covidBilgiService.GetList().ToList();

        return Ok(await _covidBilgiService.GetCovidChartList());
    }

    [HttpGet]
    public void InitializeCovid()
    {
        Random random = new();
        Enumerable.Range(1,10).ToList().ForEach(x => 
        {
            foreach(Sehir sehir in Enum.GetValues(typeof(Sehir)))
            {
                CovidBilgi covid = new()
                {
                    Sehir =sehir,
                    Sayi = random.Next(100, 1000), // vaka sayısı
                    Tarih = DateTime.Now.AddDays(x)
                };

                _covidBilgiService.SaveCovid(covid).Wait();
                System.Threading.Thread.Sleep(1000);
            }
        });
                
    }
}