

namespace Covid.API.DTOs;
public class CovidChartDTO
{

    public CovidChartDTO()
    {
        Sayilar = new();
    }
    public int Tarih {get;set;}
    public List<int> Sayilar {get;set;}

}