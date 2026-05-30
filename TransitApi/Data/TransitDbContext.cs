using Microsoft.EntityFrameworkCore;
using TransitApi.Models;
using BCrypt.Net;

namespace TransitApi.Data;

public class TransitDbContext : DbContext
{
    public TransitDbContext(DbContextOptions<TransitDbContext> options) : base(options) { }

    public DbSet<TransitRoute> Routes { get; set; }
    public DbSet<Stop> Stops { get; set; }
    public DbSet<RouteStop> RouteStops { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Schedule>()
            .Property(s => s.DepartureTime)
            .HasConversion(
                v => v.ToString("HH:mm"),
                v => TimeOnly.ParseExact(v, "HH:mm")
            );

        // Database is populated from JSON by DataImporter

        // ===== УВЕДОМЛЕНИЯ (БРЕСТ) =====
        var baseDate = new DateTime(2024, 5, 1, 12, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<Notification>().HasData(
            new Notification
            {
                Id = 1, Type = NotificationType.Disruption,
                Title = "Перекрытие пр. Машерова",
                Message = "В связи с проведением городского мероприятия пр. Машерова перекрыт от ул. Советской до ул. Московской. Маршруты 1, 3, 5, 8 следуют по объездным маршрутам через ул. Пушкинскую. Ожидаются задержки 15–20 мин.",
                AffectedRoutes = "1,3,5,8",
                CreatedAt = baseDate.AddMinutes(-5)
            },
            new Notification
            {
                Id = 2, Type = NotificationType.Delay,
                Title = "Задержка маршрута №10 «БЭТЗ»",
                Message = "Автобус маршрута №10 задерживается приблизительно на 10–15 минут из-за плотного трафика на ул. Московской в районе завода БЭТЗ.",
                AffectedRoutes = "10",
                CreatedAt = baseDate.AddMinutes(-20)
            },
            new Notification
            {
                Id = 3, Type = NotificationType.Info,
                Title = "Перенос остановки «ЦУМ»",
                Message = "В связи с ремонтными работами на ул. Ленина временная остановка «ЦУМ» перенесена на 80 метров в сторону площади Ленина. Изменение касается маршрутов 5, 15, 49.",
                AffectedRoutes = "5,15,49",
                CreatedAt = baseDate.AddHours(-3)
            },
            new Notification
            {
                Id = 4, Type = NotificationType.Maintenance,
                Title = "Плановый ремонт дороги на ул. Советской",
                Message = "С 3 по 7 июня на ул. Советской ведутся дорожные работы. Маршруты 3 и 6, следующие через Брест-Восточный, будут использовать объездной путь через ул. Московскую. Рекомендуем выходить из дома на 10 минут раньше.",
                AffectedRoutes = "3,6",
                CreatedAt = baseDate.AddDays(-1)
            }
        );

        // ===== ПОЛЬЗОВАТЕЛИ =====
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "testuser", Email = "user@brest-transit.by", PasswordHash = "$2a$11$fyHxJtrVmySwWBdzgeXmVuKyFDRxpnmD3Iy9LipJdsqDVL0aqwuzm", IsAdmin = false },
            new User { Id = 2, Username = "admin",    Email = "admin@brest-transit.by", PasswordHash = "$2a$11$t7batDXMOA8Y5X76aq0wguUN/v64mtxhbXaUSU0Mt8a/gjojYdcv2", IsAdmin = true }
        );

    }
}
