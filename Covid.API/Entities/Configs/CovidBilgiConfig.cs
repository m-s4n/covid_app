using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Covid.API.Entities.Configs;
public class CovidBilgiConfig : IEntityTypeConfiguration<CovidBilgi>
{
    public void Configure(EntityTypeBuilder<CovidBilgi> builder)
    {
        builder.ToTable("covid_bilgi").HasKey(x => x.Id);
        builder
        .Property(x => x.Id)
        .HasColumnType("integer")
        .HasColumnName("id");
        
        builder
        .Property(x => x.Sayi)
        .HasColumnType("integer")
        .HasColumnName("sayi");

        builder
        .Property(x => x.Sehir)
        .HasColumnType("integer")
        .HasColumnName("sehir");

        builder
        .Property(x => x.Tarih)
        .HasColumnType("timestamp")
        .HasColumnName("tarih");
    }
}