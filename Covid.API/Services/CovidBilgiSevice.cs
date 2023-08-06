using Covid.API.DataBase;
using Microsoft.AspNetCore.SignalR;
using Covid.API.Hubs;
using Covid.API.Entities;
using Covid.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Covid.API.Services;
public class CovidBilgiService
{
    private readonly AppDbContext _dbContext;
    private readonly IHubContext<CovidHub> _hubContext;

    public CovidBilgiService(
        AppDbContext dbContext,
        IHubContext<CovidHub> hubContext)
    => (_dbContext, _hubContext) = (dbContext, hubContext);


    public IQueryable<CovidBilgi> GetList()
    {
        return _dbContext.CovidBilgis.AsQueryable();
    }

    public async Task SaveCovid(CovidBilgi covidBilgi)

    {
        await _dbContext.CovidBilgis.AddAsync(covidBilgi);
        await _dbContext.SaveChangesAsync();
        // veri eklendiğinde tüm client'lar bilgilendirilir
        await _hubContext.Clients.All.SendAsync("ReceiveCovidBilgi", arg1: await GetCovidChartList());
    }

    public async Task<List<CovidChartDTO>> GetCovidChartList()
    {
        List<CovidChartDTO> covidChartDTOListe = new();
        using var command = _dbContext.Database.GetDbConnection().CreateCommand();

        command.CommandText = @"select *
                    from crosstab(
	                'select extract(day from tarih) as tarih, sehir, sum (sayi) as sayi 
	                from covid_bilgi 
	                group by extract(day from tarih), sehir
	                order by tarih',
	                'select distinct sehir from covid_bilgi'
                ) as ct (tarih int, istanbul int, ankara int, izmir int, antalya int, manisa int)";

        command.CommandType = System.Data.CommandType.Text;

        await _dbContext.Database.OpenConnectionAsync();

        using var reader = await command.ExecuteReaderAsync();

        while(await reader.ReadAsync())
        {
            CovidChartDTO covidChartDTO = new();
            covidChartDTO.Tarih = (int)reader.GetValue(0);
            // index 0 -> tarih

            Enumerable.Range(1, 5).ToList().ForEach(item => 
            {
                if(System.DBNull.Value.Equals(reader[item]))
                {
                    // index 1, 2, 3, 4, 5 -> sayısal veriler listeye eklenir
                    covidChartDTO.Sayilar.Add(0);
                }
                else
                {
                    covidChartDTO.Sayilar.Add(reader.GetInt32(item));
                }
            });

            covidChartDTOListe.Add(covidChartDTO);
        }

        await _dbContext.Database.CloseConnectionAsync();
        return covidChartDTOListe;

    }
}