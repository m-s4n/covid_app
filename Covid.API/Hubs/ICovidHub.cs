using Covid.API.DTOs;

namespace Covid.API.Hubs;
public interface ICovidHub
{
    Task ReceiveCovidBilgi(List<CovidChartDTO> covidBilgi);
}