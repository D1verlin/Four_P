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
    public DbSet<FavoriteStop> FavoriteStops { get; set; }
    public DbSet<FavoriteRoute> FavoriteRoutes { get; set; }
    public DbSet<FavoritePlace> FavoritePlaces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Schedule>()
            .Property(s => s.DepartureTime)
            .HasConversion(
                v => v.ToString("HH:mm"),
                v => TimeOnly.ParseExact(v, "HH:mm")
            );

        
        modelBuilder.Entity<Stop>().HasData(
            new Stop { Id = 1, Name = "Центральный вокзал", Address = "пл. Восстания, 1", Direction = "В центр и на юг" },
            new Stop { Id = 2, Name = "Площадь Восстания", Address = "пл. Восстания", Direction = "Все направления" },
            new Stop { Id = 3, Name = "Невский проспект", Address = "Невский пр., 28", Direction = "На восток/запад" },
            new Stop { Id = 4, Name = "Сенная площадь", Address = "Сенная пл., 1", Direction = "На юг" },
            new Stop { Id = 5, Name = "Парк Культуры", Address = "ул. Садовая, 50", Direction = "На север" },
            new Stop { Id = 6, Name = "Гостиный Двор", Address = "Невский пр., 35", Direction = "Центр" },
            new Stop { Id = 7, Name = "Торговый Центр «Галерея»", Address = "Лиговский пр., 30а", Direction = "На восток" },
            new Stop { Id = 8, Name = "Аэропорт Пулково", Address = "Пулковское ш., 41Л1", Direction = "Конечная" },
            new Stop { Id = 9, Name = "Северный вокзал", Address = "пр. Просвещения, 1", Direction = "На север" },
            new Stop { Id = 10, Name = "Южный парк", Address = "ул. Витебская, 15", Direction = "На юг" },
            new Stop { Id = 11, Name = "Университет", Address = "Университетская наб., 7", Direction = "На запад" },
            new Stop { Id = 12, Name = "Площадь Победы", Address = "пл. Победы, 1", Direction = "На юг" },
            new Stop { Id = 13, Name = "Петроградская", Address = "Большой пр. П.С., 1", Direction = "На север" },
            new Stop { Id = 14, Name = "Васильевский остров", Address = "В.О., 1-я линия, 10", Direction = "На запад" },
            new Stop { Id = 15, Name = "Конная улица", Address = "Конная ул., 5", Direction = "Восток" },
            new Stop { Id = 16, Name = "Тульская улица", Address = "Тульская ул., 3", Direction = "Запад" }
        );

        
        modelBuilder.Entity<TransitRoute>().HasData(
            new TransitRoute { Id = 1, Number = "15", Name = "Центр — Аэропорт", Type = TransportType.Bus, StartStop = "Центральный вокзал", EndStop = "Аэропорт Пулково", FrequencyMinutes = 10 },
            new TransitRoute { Id = 2, Number = "42", Name = "Северный вокзал — Южный парк", Type = TransportType.Bus, StartStop = "Северный вокзал", EndStop = "Южный парк", FrequencyMinutes = 15 },
            new TransitRoute { Id = 3, Number = "3", Name = "Гостиный Двор — Сенная", Type = TransportType.Tram, StartStop = "Гостиный Двор", EndStop = "Сенная площадь", FrequencyMinutes = 8 },
            new TransitRoute { Id = 4, Number = "7", Name = "Центр — Петроградская", Type = TransportType.Trolleybus, StartStop = "Невский проспект", EndStop = "Петроградская", FrequencyMinutes = 12 },
            new TransitRoute { Id = 5, Number = "112т", Name = "Центр — Галерея", Type = TransportType.Minibus, StartStop = "Центральный вокзал", EndStop = "Торговый Центр «Галерея»", FrequencyMinutes = 7 },
            new TransitRoute { Id = 6, Number = "8", Name = "Университет — Площадь Победы", Type = TransportType.Bus, StartStop = "Университет", EndStop = "Площадь Победы", FrequencyMinutes = 8 },
            new TransitRoute { Id = 7, Number = "5", Name = "Конная улица — Тульская улица", Type = TransportType.Trolleybus, StartStop = "Конная улица", EndStop = "Тульская улица", FrequencyMinutes = 18 }
        );

        
        modelBuilder.Entity<RouteStop>().HasData(
            new RouteStop { Id = 1,  RouteId = 1, StopId = 1,  Order = 1, OffsetMinutes = 0 },
            new RouteStop { Id = 2,  RouteId = 1, StopId = 2,  Order = 2, OffsetMinutes = 5 },
            new RouteStop { Id = 3,  RouteId = 1, StopId = 3,  Order = 3, OffsetMinutes = 10 },
            new RouteStop { Id = 4,  RouteId = 1, StopId = 7,  Order = 4, OffsetMinutes = 18 },
            new RouteStop { Id = 5,  RouteId = 1, StopId = 4,  Order = 5, OffsetMinutes = 24 },
            new RouteStop { Id = 6,  RouteId = 1, StopId = 12, Order = 6, OffsetMinutes = 33 },
            new RouteStop { Id = 7,  RouteId = 1, StopId = 8,  Order = 7, OffsetMinutes = 45 },
            
            new RouteStop { Id = 8,  RouteId = 2, StopId = 9,  Order = 1, OffsetMinutes = 0 },
            new RouteStop { Id = 9,  RouteId = 2, StopId = 13, Order = 2, OffsetMinutes = 8 },
            new RouteStop { Id = 10, RouteId = 2, StopId = 2,  Order = 3, OffsetMinutes = 15 },
            new RouteStop { Id = 11, RouteId = 2, StopId = 5,  Order = 4, OffsetMinutes = 22 },
            new RouteStop { Id = 12, RouteId = 2, StopId = 10, Order = 5, OffsetMinutes = 30 },
            
            new RouteStop { Id = 13, RouteId = 3, StopId = 6,  Order = 1, OffsetMinutes = 0 },
            new RouteStop { Id = 14, RouteId = 3, StopId = 3,  Order = 2, OffsetMinutes = 5 },
            new RouteStop { Id = 15, RouteId = 3, StopId = 4,  Order = 3, OffsetMinutes = 12 },
            
            new RouteStop { Id = 16, RouteId = 4, StopId = 3,  Order = 1, OffsetMinutes = 0 },
            new RouteStop { Id = 17, RouteId = 4, StopId = 2,  Order = 2, OffsetMinutes = 6 },
            new RouteStop { Id = 18, RouteId = 4, StopId = 13, Order = 3, OffsetMinutes = 14 },
            
            new RouteStop { Id = 19, RouteId = 5, StopId = 1,  Order = 1, OffsetMinutes = 0 },
            new RouteStop { Id = 20, RouteId = 5, StopId = 2,  Order = 2, OffsetMinutes = 4 },
            new RouteStop { Id = 21, RouteId = 5, StopId = 7,  Order = 3, OffsetMinutes = 10 },
            
            new RouteStop { Id = 22, RouteId = 6, StopId = 11, Order = 1, OffsetMinutes = 0 },
            new RouteStop { Id = 23, RouteId = 6, StopId = 14, Order = 2, OffsetMinutes = 7 },
            new RouteStop { Id = 24, RouteId = 6, StopId = 3,  Order = 3, OffsetMinutes = 15 },
            new RouteStop { Id = 25, RouteId = 6, StopId = 12, Order = 4, OffsetMinutes = 25 },
            
            new RouteStop { Id = 26, RouteId = 7, StopId = 15, Order = 1, OffsetMinutes = 0 },
            new RouteStop { Id = 27, RouteId = 7, StopId = 3,  Order = 2, OffsetMinutes = 8 },
            new RouteStop { Id = 28, RouteId = 7, StopId = 16, Order = 3, OffsetMinutes = 16 }
        );

        
        var scheduleId = 1;
        var scheduleData = new List<object>();
        foreach (var routeId in new[] { 1, 2, 3, 4, 5, 6, 7 })
        {
            int freq = routeId switch { 1 => 10, 2 => 15, 3 => 8, 4 => 12, 5 => 7, 6 => 8, 7 => 18, _ => 15 };
            int startHour = 6, endHour = 22;
            var current = new TimeOnly(startHour, 0);
            var endTime = new TimeOnly(endHour, 0);
            while (current <= endTime)
            {
                scheduleData.Add(new Schedule { Id = scheduleId++, RouteId = routeId, DepartureTime = current });
                current = current.AddMinutes(freq);
            }
        }
        modelBuilder.Entity<Schedule>().HasData(scheduleData.Cast<Schedule>().ToArray());

        
        var baseDate = new DateTime(2024, 5, 1, 12, 0, 0, DateTimeKind.Utc);
        modelBuilder.Entity<Notification>().HasData(
            new Notification
            {
                Id = 1, Type = NotificationType.Disruption,
                Title = "Движение парализовано",
                Message = "Авария на центральном проспекте блокирует движение маршрутов 42, 15 и 8. Ожидаются значительные задержки. Рекомендуется использовать объездные пути.",
                AffectedRoutes = "42,15,8",
                CreatedAt = baseDate.AddMinutes(-5)
            },
            new Notification
            {
                Id = 2, Type = NotificationType.Delay,
                Title = "Отклонение от расписания",
                Message = "Троллейбус маршрута 7 задерживается примерно на 15-20 минут из-за плотного трафика на Южной магистрали.",
                AffectedRoutes = "7",
                CreatedAt = baseDate.AddMinutes(-15)
            },
            new Notification
            {
                Id = 3, Type = NotificationType.Info,
                Title = "Изменение остановки",
                Message = "В связи с ремонтными работами, остановка «Парк Культуры» временно перенесена на 100 метров вперёд по ходу движения.",
                AffectedRoutes = "42",
                CreatedAt = baseDate.AddHours(-2)
            },
            new Notification
            {
                Id = 4, Type = NotificationType.Maintenance,
                Title = "Ремонт путей на Северной линии",
                Message = "В выходные дни движение трамваев по Северной линии будет ограничено. Предоставляются компенсационные автобусные маршруты (КМ). Пожалуйста, планируйте поездки заранее.",
                AffectedRoutes = "3",
                CreatedAt = baseDate.AddDays(-1)
            }
        );

        
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "testuser", Email = "user@urbantransit.local", PasswordHash = "$2a$11$fyHxJtrVmySwWBdzgeXmVuKyFDRxpnmD3Iy9LipJdsqDVL0aqwuzm", IsAdmin = false },
            new User { Id = 2, Username = "admin", Email = "admin@urbantransit.local", PasswordHash = "$2a$11$t7batDXMOA8Y5X76aq0wguUN/v64mtxhbXaUSU0Mt8a/gjojYdcv2", IsAdmin = true }
        );

        modelBuilder.Entity<FavoriteStop>().HasData(
            new FavoriteStop { Id = 1, UserId = 1, StopId = 2 }, 
            new FavoriteStop { Id = 2, UserId = 1, StopId = 4 }  
        );

        modelBuilder.Entity<FavoriteRoute>().HasData(
            new FavoriteRoute { Id = 1, UserId = 1, TransitRouteId = 1 }, 
            new FavoriteRoute { Id = 2, UserId = 1, TransitRouteId = 7 }  
        );

        modelBuilder.Entity<FavoritePlace>().HasData(
            new FavoritePlace { Id = 1, UserId = 1, Name = "Дом", Address = "Невский проспект, 28", Icon = "home" },
            new FavoritePlace { Id = 2, UserId = 1, Name = "Работа", Address = "Лиговский пр., 30а", Icon = "work" }
        );
    }
}
