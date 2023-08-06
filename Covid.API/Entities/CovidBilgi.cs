using Covid.API.Enums;

namespace Covid.API.Entities;
public class CovidBilgi
{
    public int Id {get;set;}
    public Sehir Sehir {get;set;}
    public int Sayi {get;set;}
    public DateTime Tarih {get;set;}
}