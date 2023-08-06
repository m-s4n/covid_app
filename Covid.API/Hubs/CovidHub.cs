using Microsoft.AspNetCore.SignalR;
using Covid.API.Services;

namespace Covid.API.Hubs;
public class CovidHub:Hub<ICovidHub>
{
    private readonly CovidBilgiService _covidBilgiService;
    public CovidHub(CovidBilgiService covidBilgiService) => _covidBilgiService = covidBilgiService;
    // Client'lar bu metou tetiklediğinde tüm covid vaka bilgilerini alacaklar 
    public async Task GetCovidBilgi()
    {
        await Clients.All.ReceiveCovidBilgi(await _covidBilgiService.GetCovidChartList());
    }
}