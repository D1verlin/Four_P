
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransitApi.Data;

#nullable disable

namespace TransitApi.Migrations
{
    [DbContext(typeof(TransitDbContext))]
    partial class TransitDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "10.0.7");

            modelBuilder.Entity("NotificationTransitRoute", b =>
                {
                    b.Property<int>("AffectedTransitRoutesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NotificationsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AffectedTransitRoutesId", "NotificationsId");

                    b.HasIndex("NotificationsId");

                    b.ToTable("NotificationTransitRoute");
                });

            modelBuilder.Entity("TransitApi.Models.FavoritePlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FavoritePlaces");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Невский проспект, 28",
                            Icon = "home",
                            Name = "Дом",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Address = "Лиговский пр., 30а",
                            Icon = "work",
                            Name = "Работа",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("TransitApi.Models.FavoriteRoute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("TransitRouteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TransitRouteId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteRoutes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TransitRouteId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            TransitRouteId = 7,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("TransitApi.Models.FavoriteStop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("StopId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StopId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteStops");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StopId = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            StopId = 4,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("TransitApi.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AffectedRoutes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Notifications");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AffectedRoutes = "42,15,8",
                            CreatedAt = new DateTime(2024, 5, 1, 11, 55, 0, 0, DateTimeKind.Utc),
                            Message = "Авария на центральном проспекте блокирует движение маршрутов 42, 15 и 8. Ожидаются значительные задержки. Рекомендуется использовать объездные пути.",
                            Title = "Движение парализовано",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            AffectedRoutes = "7",
                            CreatedAt = new DateTime(2024, 5, 1, 11, 45, 0, 0, DateTimeKind.Utc),
                            Message = "Троллейбус маршрута 7 задерживается примерно на 15-20 минут из-за плотного трафика на Южной магистрали.",
                            Title = "Отклонение от расписания",
                            Type = 1
                        },
                        new
                        {
                            Id = 3,
                            AffectedRoutes = "42",
                            CreatedAt = new DateTime(2024, 5, 1, 10, 0, 0, 0, DateTimeKind.Utc),
                            Message = "В связи с ремонтными работами, остановка «Парк Культуры» временно перенесена на 100 метров вперёд по ходу движения.",
                            Title = "Изменение остановки",
                            Type = 2
                        },
                        new
                        {
                            Id = 4,
                            AffectedRoutes = "3",
                            CreatedAt = new DateTime(2024, 4, 30, 12, 0, 0, 0, DateTimeKind.Utc),
                            Message = "В выходные дни движение трамваев по Северной линии будет ограничено. Предоставляются компенсационные автобусные маршруты (КМ). Пожалуйста, планируйте поездки заранее.",
                            Title = "Ремонт путей на Северной линии",
                            Type = 3
                        });
                });

            modelBuilder.Entity("TransitApi.Models.RouteStop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OffsetMinutes")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RouteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StopId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("StopId");

                    b.ToTable("RouteStops");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OffsetMinutes = 0,
                            Order = 1,
                            RouteId = 1,
                            StopId = 1
                        },
                        new
                        {
                            Id = 2,
                            OffsetMinutes = 5,
                            Order = 2,
                            RouteId = 1,
                            StopId = 2
                        },
                        new
                        {
                            Id = 3,
                            OffsetMinutes = 10,
                            Order = 3,
                            RouteId = 1,
                            StopId = 3
                        },
                        new
                        {
                            Id = 4,
                            OffsetMinutes = 18,
                            Order = 4,
                            RouteId = 1,
                            StopId = 7
                        },
                        new
                        {
                            Id = 5,
                            OffsetMinutes = 24,
                            Order = 5,
                            RouteId = 1,
                            StopId = 4
                        },
                        new
                        {
                            Id = 6,
                            OffsetMinutes = 33,
                            Order = 6,
                            RouteId = 1,
                            StopId = 12
                        },
                        new
                        {
                            Id = 7,
                            OffsetMinutes = 45,
                            Order = 7,
                            RouteId = 1,
                            StopId = 8
                        },
                        new
                        {
                            Id = 8,
                            OffsetMinutes = 0,
                            Order = 1,
                            RouteId = 2,
                            StopId = 9
                        },
                        new
                        {
                            Id = 9,
                            OffsetMinutes = 8,
                            Order = 2,
                            RouteId = 2,
                            StopId = 13
                        },
                        new
                        {
                            Id = 10,
                            OffsetMinutes = 15,
                            Order = 3,
                            RouteId = 2,
                            StopId = 2
                        },
                        new
                        {
                            Id = 11,
                            OffsetMinutes = 22,
                            Order = 4,
                            RouteId = 2,
                            StopId = 5
                        },
                        new
                        {
                            Id = 12,
                            OffsetMinutes = 30,
                            Order = 5,
                            RouteId = 2,
                            StopId = 10
                        },
                        new
                        {
                            Id = 13,
                            OffsetMinutes = 0,
                            Order = 1,
                            RouteId = 3,
                            StopId = 6
                        },
                        new
                        {
                            Id = 14,
                            OffsetMinutes = 5,
                            Order = 2,
                            RouteId = 3,
                            StopId = 3
                        },
                        new
                        {
                            Id = 15,
                            OffsetMinutes = 12,
                            Order = 3,
                            RouteId = 3,
                            StopId = 4
                        },
                        new
                        {
                            Id = 16,
                            OffsetMinutes = 0,
                            Order = 1,
                            RouteId = 4,
                            StopId = 3
                        },
                        new
                        {
                            Id = 17,
                            OffsetMinutes = 6,
                            Order = 2,
                            RouteId = 4,
                            StopId = 2
                        },
                        new
                        {
                            Id = 18,
                            OffsetMinutes = 14,
                            Order = 3,
                            RouteId = 4,
                            StopId = 13
                        },
                        new
                        {
                            Id = 19,
                            OffsetMinutes = 0,
                            Order = 1,
                            RouteId = 5,
                            StopId = 1
                        },
                        new
                        {
                            Id = 20,
                            OffsetMinutes = 4,
                            Order = 2,
                            RouteId = 5,
                            StopId = 2
                        },
                        new
                        {
                            Id = 21,
                            OffsetMinutes = 10,
                            Order = 3,
                            RouteId = 5,
                            StopId = 7
                        },
                        new
                        {
                            Id = 22,
                            OffsetMinutes = 0,
                            Order = 1,
                            RouteId = 6,
                            StopId = 11
                        },
                        new
                        {
                            Id = 23,
                            OffsetMinutes = 7,
                            Order = 2,
                            RouteId = 6,
                            StopId = 14
                        },
                        new
                        {
                            Id = 24,
                            OffsetMinutes = 15,
                            Order = 3,
                            RouteId = 6,
                            StopId = 3
                        },
                        new
                        {
                            Id = 25,
                            OffsetMinutes = 25,
                            Order = 4,
                            RouteId = 6,
                            StopId = 12
                        },
                        new
                        {
                            Id = 26,
                            OffsetMinutes = 0,
                            Order = 1,
                            RouteId = 7,
                            StopId = 15
                        },
                        new
                        {
                            Id = 27,
                            OffsetMinutes = 8,
                            Order = 2,
                            RouteId = 7,
                            StopId = 3
                        },
                        new
                        {
                            Id = 28,
                            OffsetMinutes = 16,
                            Order = 3,
                            RouteId = 7,
                            StopId = 16
                        });
                });

            modelBuilder.Entity("TransitApi.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DepartureTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RouteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Schedules");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartureTime = "06:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 2,
                            DepartureTime = "06:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 3,
                            DepartureTime = "06:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 4,
                            DepartureTime = "06:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 5,
                            DepartureTime = "06:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 6,
                            DepartureTime = "06:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 7,
                            DepartureTime = "07:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 8,
                            DepartureTime = "07:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 9,
                            DepartureTime = "07:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 10,
                            DepartureTime = "07:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 11,
                            DepartureTime = "07:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 12,
                            DepartureTime = "07:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 13,
                            DepartureTime = "08:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 14,
                            DepartureTime = "08:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 15,
                            DepartureTime = "08:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 16,
                            DepartureTime = "08:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 17,
                            DepartureTime = "08:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 18,
                            DepartureTime = "08:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 19,
                            DepartureTime = "09:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 20,
                            DepartureTime = "09:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 21,
                            DepartureTime = "09:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 22,
                            DepartureTime = "09:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 23,
                            DepartureTime = "09:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 24,
                            DepartureTime = "09:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 25,
                            DepartureTime = "10:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 26,
                            DepartureTime = "10:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 27,
                            DepartureTime = "10:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 28,
                            DepartureTime = "10:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 29,
                            DepartureTime = "10:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 30,
                            DepartureTime = "10:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 31,
                            DepartureTime = "11:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 32,
                            DepartureTime = "11:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 33,
                            DepartureTime = "11:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 34,
                            DepartureTime = "11:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 35,
                            DepartureTime = "11:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 36,
                            DepartureTime = "11:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 37,
                            DepartureTime = "12:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 38,
                            DepartureTime = "12:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 39,
                            DepartureTime = "12:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 40,
                            DepartureTime = "12:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 41,
                            DepartureTime = "12:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 42,
                            DepartureTime = "12:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 43,
                            DepartureTime = "13:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 44,
                            DepartureTime = "13:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 45,
                            DepartureTime = "13:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 46,
                            DepartureTime = "13:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 47,
                            DepartureTime = "13:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 48,
                            DepartureTime = "13:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 49,
                            DepartureTime = "14:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 50,
                            DepartureTime = "14:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 51,
                            DepartureTime = "14:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 52,
                            DepartureTime = "14:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 53,
                            DepartureTime = "14:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 54,
                            DepartureTime = "14:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 55,
                            DepartureTime = "15:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 56,
                            DepartureTime = "15:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 57,
                            DepartureTime = "15:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 58,
                            DepartureTime = "15:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 59,
                            DepartureTime = "15:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 60,
                            DepartureTime = "15:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 61,
                            DepartureTime = "16:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 62,
                            DepartureTime = "16:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 63,
                            DepartureTime = "16:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 64,
                            DepartureTime = "16:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 65,
                            DepartureTime = "16:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 66,
                            DepartureTime = "16:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 67,
                            DepartureTime = "17:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 68,
                            DepartureTime = "17:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 69,
                            DepartureTime = "17:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 70,
                            DepartureTime = "17:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 71,
                            DepartureTime = "17:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 72,
                            DepartureTime = "17:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 73,
                            DepartureTime = "18:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 74,
                            DepartureTime = "18:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 75,
                            DepartureTime = "18:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 76,
                            DepartureTime = "18:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 77,
                            DepartureTime = "18:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 78,
                            DepartureTime = "18:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 79,
                            DepartureTime = "19:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 80,
                            DepartureTime = "19:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 81,
                            DepartureTime = "19:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 82,
                            DepartureTime = "19:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 83,
                            DepartureTime = "19:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 84,
                            DepartureTime = "19:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 85,
                            DepartureTime = "20:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 86,
                            DepartureTime = "20:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 87,
                            DepartureTime = "20:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 88,
                            DepartureTime = "20:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 89,
                            DepartureTime = "20:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 90,
                            DepartureTime = "20:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 91,
                            DepartureTime = "21:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 92,
                            DepartureTime = "21:10",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 93,
                            DepartureTime = "21:20",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 94,
                            DepartureTime = "21:30",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 95,
                            DepartureTime = "21:40",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 96,
                            DepartureTime = "21:50",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 97,
                            DepartureTime = "22:00",
                            RouteId = 1
                        },
                        new
                        {
                            Id = 98,
                            DepartureTime = "06:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 99,
                            DepartureTime = "06:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 100,
                            DepartureTime = "06:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 101,
                            DepartureTime = "06:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 102,
                            DepartureTime = "07:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 103,
                            DepartureTime = "07:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 104,
                            DepartureTime = "07:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 105,
                            DepartureTime = "07:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 106,
                            DepartureTime = "08:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 107,
                            DepartureTime = "08:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 108,
                            DepartureTime = "08:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 109,
                            DepartureTime = "08:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 110,
                            DepartureTime = "09:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 111,
                            DepartureTime = "09:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 112,
                            DepartureTime = "09:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 113,
                            DepartureTime = "09:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 114,
                            DepartureTime = "10:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 115,
                            DepartureTime = "10:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 116,
                            DepartureTime = "10:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 117,
                            DepartureTime = "10:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 118,
                            DepartureTime = "11:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 119,
                            DepartureTime = "11:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 120,
                            DepartureTime = "11:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 121,
                            DepartureTime = "11:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 122,
                            DepartureTime = "12:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 123,
                            DepartureTime = "12:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 124,
                            DepartureTime = "12:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 125,
                            DepartureTime = "12:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 126,
                            DepartureTime = "13:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 127,
                            DepartureTime = "13:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 128,
                            DepartureTime = "13:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 129,
                            DepartureTime = "13:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 130,
                            DepartureTime = "14:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 131,
                            DepartureTime = "14:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 132,
                            DepartureTime = "14:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 133,
                            DepartureTime = "14:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 134,
                            DepartureTime = "15:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 135,
                            DepartureTime = "15:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 136,
                            DepartureTime = "15:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 137,
                            DepartureTime = "15:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 138,
                            DepartureTime = "16:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 139,
                            DepartureTime = "16:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 140,
                            DepartureTime = "16:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 141,
                            DepartureTime = "16:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 142,
                            DepartureTime = "17:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 143,
                            DepartureTime = "17:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 144,
                            DepartureTime = "17:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 145,
                            DepartureTime = "17:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 146,
                            DepartureTime = "18:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 147,
                            DepartureTime = "18:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 148,
                            DepartureTime = "18:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 149,
                            DepartureTime = "18:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 150,
                            DepartureTime = "19:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 151,
                            DepartureTime = "19:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 152,
                            DepartureTime = "19:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 153,
                            DepartureTime = "19:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 154,
                            DepartureTime = "20:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 155,
                            DepartureTime = "20:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 156,
                            DepartureTime = "20:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 157,
                            DepartureTime = "20:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 158,
                            DepartureTime = "21:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 159,
                            DepartureTime = "21:15",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 160,
                            DepartureTime = "21:30",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 161,
                            DepartureTime = "21:45",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 162,
                            DepartureTime = "22:00",
                            RouteId = 2
                        },
                        new
                        {
                            Id = 163,
                            DepartureTime = "06:00",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 164,
                            DepartureTime = "06:08",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 165,
                            DepartureTime = "06:16",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 166,
                            DepartureTime = "06:24",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 167,
                            DepartureTime = "06:32",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 168,
                            DepartureTime = "06:40",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 169,
                            DepartureTime = "06:48",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 170,
                            DepartureTime = "06:56",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 171,
                            DepartureTime = "07:04",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 172,
                            DepartureTime = "07:12",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 173,
                            DepartureTime = "07:20",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 174,
                            DepartureTime = "07:28",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 175,
                            DepartureTime = "07:36",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 176,
                            DepartureTime = "07:44",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 177,
                            DepartureTime = "07:52",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 178,
                            DepartureTime = "08:00",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 179,
                            DepartureTime = "08:08",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 180,
                            DepartureTime = "08:16",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 181,
                            DepartureTime = "08:24",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 182,
                            DepartureTime = "08:32",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 183,
                            DepartureTime = "08:40",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 184,
                            DepartureTime = "08:48",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 185,
                            DepartureTime = "08:56",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 186,
                            DepartureTime = "09:04",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 187,
                            DepartureTime = "09:12",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 188,
                            DepartureTime = "09:20",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 189,
                            DepartureTime = "09:28",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 190,
                            DepartureTime = "09:36",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 191,
                            DepartureTime = "09:44",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 192,
                            DepartureTime = "09:52",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 193,
                            DepartureTime = "10:00",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 194,
                            DepartureTime = "10:08",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 195,
                            DepartureTime = "10:16",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 196,
                            DepartureTime = "10:24",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 197,
                            DepartureTime = "10:32",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 198,
                            DepartureTime = "10:40",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 199,
                            DepartureTime = "10:48",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 200,
                            DepartureTime = "10:56",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 201,
                            DepartureTime = "11:04",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 202,
                            DepartureTime = "11:12",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 203,
                            DepartureTime = "11:20",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 204,
                            DepartureTime = "11:28",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 205,
                            DepartureTime = "11:36",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 206,
                            DepartureTime = "11:44",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 207,
                            DepartureTime = "11:52",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 208,
                            DepartureTime = "12:00",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 209,
                            DepartureTime = "12:08",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 210,
                            DepartureTime = "12:16",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 211,
                            DepartureTime = "12:24",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 212,
                            DepartureTime = "12:32",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 213,
                            DepartureTime = "12:40",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 214,
                            DepartureTime = "12:48",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 215,
                            DepartureTime = "12:56",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 216,
                            DepartureTime = "13:04",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 217,
                            DepartureTime = "13:12",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 218,
                            DepartureTime = "13:20",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 219,
                            DepartureTime = "13:28",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 220,
                            DepartureTime = "13:36",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 221,
                            DepartureTime = "13:44",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 222,
                            DepartureTime = "13:52",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 223,
                            DepartureTime = "14:00",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 224,
                            DepartureTime = "14:08",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 225,
                            DepartureTime = "14:16",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 226,
                            DepartureTime = "14:24",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 227,
                            DepartureTime = "14:32",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 228,
                            DepartureTime = "14:40",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 229,
                            DepartureTime = "14:48",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 230,
                            DepartureTime = "14:56",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 231,
                            DepartureTime = "15:04",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 232,
                            DepartureTime = "15:12",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 233,
                            DepartureTime = "15:20",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 234,
                            DepartureTime = "15:28",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 235,
                            DepartureTime = "15:36",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 236,
                            DepartureTime = "15:44",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 237,
                            DepartureTime = "15:52",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 238,
                            DepartureTime = "16:00",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 239,
                            DepartureTime = "16:08",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 240,
                            DepartureTime = "16:16",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 241,
                            DepartureTime = "16:24",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 242,
                            DepartureTime = "16:32",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 243,
                            DepartureTime = "16:40",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 244,
                            DepartureTime = "16:48",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 245,
                            DepartureTime = "16:56",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 246,
                            DepartureTime = "17:04",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 247,
                            DepartureTime = "17:12",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 248,
                            DepartureTime = "17:20",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 249,
                            DepartureTime = "17:28",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 250,
                            DepartureTime = "17:36",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 251,
                            DepartureTime = "17:44",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 252,
                            DepartureTime = "17:52",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 253,
                            DepartureTime = "18:00",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 254,
                            DepartureTime = "18:08",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 255,
                            DepartureTime = "18:16",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 256,
                            DepartureTime = "18:24",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 257,
                            DepartureTime = "18:32",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 258,
                            DepartureTime = "18:40",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 259,
                            DepartureTime = "18:48",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 260,
                            DepartureTime = "18:56",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 261,
                            DepartureTime = "19:04",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 262,
                            DepartureTime = "19:12",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 263,
                            DepartureTime = "19:20",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 264,
                            DepartureTime = "19:28",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 265,
                            DepartureTime = "19:36",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 266,
                            DepartureTime = "19:44",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 267,
                            DepartureTime = "19:52",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 268,
                            DepartureTime = "20:00",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 269,
                            DepartureTime = "20:08",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 270,
                            DepartureTime = "20:16",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 271,
                            DepartureTime = "20:24",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 272,
                            DepartureTime = "20:32",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 273,
                            DepartureTime = "20:40",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 274,
                            DepartureTime = "20:48",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 275,
                            DepartureTime = "20:56",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 276,
                            DepartureTime = "21:04",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 277,
                            DepartureTime = "21:12",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 278,
                            DepartureTime = "21:20",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 279,
                            DepartureTime = "21:28",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 280,
                            DepartureTime = "21:36",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 281,
                            DepartureTime = "21:44",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 282,
                            DepartureTime = "21:52",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 283,
                            DepartureTime = "22:00",
                            RouteId = 3
                        },
                        new
                        {
                            Id = 284,
                            DepartureTime = "06:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 285,
                            DepartureTime = "06:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 286,
                            DepartureTime = "06:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 287,
                            DepartureTime = "06:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 288,
                            DepartureTime = "06:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 289,
                            DepartureTime = "07:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 290,
                            DepartureTime = "07:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 291,
                            DepartureTime = "07:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 292,
                            DepartureTime = "07:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 293,
                            DepartureTime = "07:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 294,
                            DepartureTime = "08:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 295,
                            DepartureTime = "08:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 296,
                            DepartureTime = "08:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 297,
                            DepartureTime = "08:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 298,
                            DepartureTime = "08:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 299,
                            DepartureTime = "09:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 300,
                            DepartureTime = "09:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 301,
                            DepartureTime = "09:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 302,
                            DepartureTime = "09:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 303,
                            DepartureTime = "09:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 304,
                            DepartureTime = "10:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 305,
                            DepartureTime = "10:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 306,
                            DepartureTime = "10:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 307,
                            DepartureTime = "10:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 308,
                            DepartureTime = "10:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 309,
                            DepartureTime = "11:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 310,
                            DepartureTime = "11:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 311,
                            DepartureTime = "11:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 312,
                            DepartureTime = "11:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 313,
                            DepartureTime = "11:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 314,
                            DepartureTime = "12:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 315,
                            DepartureTime = "12:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 316,
                            DepartureTime = "12:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 317,
                            DepartureTime = "12:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 318,
                            DepartureTime = "12:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 319,
                            DepartureTime = "13:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 320,
                            DepartureTime = "13:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 321,
                            DepartureTime = "13:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 322,
                            DepartureTime = "13:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 323,
                            DepartureTime = "13:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 324,
                            DepartureTime = "14:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 325,
                            DepartureTime = "14:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 326,
                            DepartureTime = "14:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 327,
                            DepartureTime = "14:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 328,
                            DepartureTime = "14:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 329,
                            DepartureTime = "15:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 330,
                            DepartureTime = "15:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 331,
                            DepartureTime = "15:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 332,
                            DepartureTime = "15:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 333,
                            DepartureTime = "15:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 334,
                            DepartureTime = "16:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 335,
                            DepartureTime = "16:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 336,
                            DepartureTime = "16:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 337,
                            DepartureTime = "16:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 338,
                            DepartureTime = "16:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 339,
                            DepartureTime = "17:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 340,
                            DepartureTime = "17:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 341,
                            DepartureTime = "17:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 342,
                            DepartureTime = "17:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 343,
                            DepartureTime = "17:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 344,
                            DepartureTime = "18:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 345,
                            DepartureTime = "18:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 346,
                            DepartureTime = "18:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 347,
                            DepartureTime = "18:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 348,
                            DepartureTime = "18:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 349,
                            DepartureTime = "19:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 350,
                            DepartureTime = "19:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 351,
                            DepartureTime = "19:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 352,
                            DepartureTime = "19:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 353,
                            DepartureTime = "19:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 354,
                            DepartureTime = "20:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 355,
                            DepartureTime = "20:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 356,
                            DepartureTime = "20:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 357,
                            DepartureTime = "20:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 358,
                            DepartureTime = "20:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 359,
                            DepartureTime = "21:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 360,
                            DepartureTime = "21:12",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 361,
                            DepartureTime = "21:24",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 362,
                            DepartureTime = "21:36",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 363,
                            DepartureTime = "21:48",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 364,
                            DepartureTime = "22:00",
                            RouteId = 4
                        },
                        new
                        {
                            Id = 365,
                            DepartureTime = "06:00",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 366,
                            DepartureTime = "06:07",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 367,
                            DepartureTime = "06:14",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 368,
                            DepartureTime = "06:21",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 369,
                            DepartureTime = "06:28",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 370,
                            DepartureTime = "06:35",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 371,
                            DepartureTime = "06:42",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 372,
                            DepartureTime = "06:49",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 373,
                            DepartureTime = "06:56",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 374,
                            DepartureTime = "07:03",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 375,
                            DepartureTime = "07:10",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 376,
                            DepartureTime = "07:17",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 377,
                            DepartureTime = "07:24",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 378,
                            DepartureTime = "07:31",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 379,
                            DepartureTime = "07:38",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 380,
                            DepartureTime = "07:45",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 381,
                            DepartureTime = "07:52",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 382,
                            DepartureTime = "07:59",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 383,
                            DepartureTime = "08:06",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 384,
                            DepartureTime = "08:13",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 385,
                            DepartureTime = "08:20",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 386,
                            DepartureTime = "08:27",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 387,
                            DepartureTime = "08:34",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 388,
                            DepartureTime = "08:41",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 389,
                            DepartureTime = "08:48",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 390,
                            DepartureTime = "08:55",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 391,
                            DepartureTime = "09:02",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 392,
                            DepartureTime = "09:09",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 393,
                            DepartureTime = "09:16",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 394,
                            DepartureTime = "09:23",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 395,
                            DepartureTime = "09:30",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 396,
                            DepartureTime = "09:37",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 397,
                            DepartureTime = "09:44",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 398,
                            DepartureTime = "09:51",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 399,
                            DepartureTime = "09:58",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 400,
                            DepartureTime = "10:05",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 401,
                            DepartureTime = "10:12",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 402,
                            DepartureTime = "10:19",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 403,
                            DepartureTime = "10:26",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 404,
                            DepartureTime = "10:33",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 405,
                            DepartureTime = "10:40",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 406,
                            DepartureTime = "10:47",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 407,
                            DepartureTime = "10:54",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 408,
                            DepartureTime = "11:01",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 409,
                            DepartureTime = "11:08",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 410,
                            DepartureTime = "11:15",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 411,
                            DepartureTime = "11:22",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 412,
                            DepartureTime = "11:29",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 413,
                            DepartureTime = "11:36",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 414,
                            DepartureTime = "11:43",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 415,
                            DepartureTime = "11:50",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 416,
                            DepartureTime = "11:57",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 417,
                            DepartureTime = "12:04",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 418,
                            DepartureTime = "12:11",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 419,
                            DepartureTime = "12:18",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 420,
                            DepartureTime = "12:25",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 421,
                            DepartureTime = "12:32",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 422,
                            DepartureTime = "12:39",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 423,
                            DepartureTime = "12:46",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 424,
                            DepartureTime = "12:53",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 425,
                            DepartureTime = "13:00",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 426,
                            DepartureTime = "13:07",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 427,
                            DepartureTime = "13:14",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 428,
                            DepartureTime = "13:21",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 429,
                            DepartureTime = "13:28",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 430,
                            DepartureTime = "13:35",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 431,
                            DepartureTime = "13:42",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 432,
                            DepartureTime = "13:49",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 433,
                            DepartureTime = "13:56",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 434,
                            DepartureTime = "14:03",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 435,
                            DepartureTime = "14:10",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 436,
                            DepartureTime = "14:17",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 437,
                            DepartureTime = "14:24",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 438,
                            DepartureTime = "14:31",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 439,
                            DepartureTime = "14:38",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 440,
                            DepartureTime = "14:45",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 441,
                            DepartureTime = "14:52",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 442,
                            DepartureTime = "14:59",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 443,
                            DepartureTime = "15:06",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 444,
                            DepartureTime = "15:13",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 445,
                            DepartureTime = "15:20",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 446,
                            DepartureTime = "15:27",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 447,
                            DepartureTime = "15:34",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 448,
                            DepartureTime = "15:41",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 449,
                            DepartureTime = "15:48",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 450,
                            DepartureTime = "15:55",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 451,
                            DepartureTime = "16:02",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 452,
                            DepartureTime = "16:09",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 453,
                            DepartureTime = "16:16",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 454,
                            DepartureTime = "16:23",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 455,
                            DepartureTime = "16:30",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 456,
                            DepartureTime = "16:37",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 457,
                            DepartureTime = "16:44",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 458,
                            DepartureTime = "16:51",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 459,
                            DepartureTime = "16:58",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 460,
                            DepartureTime = "17:05",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 461,
                            DepartureTime = "17:12",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 462,
                            DepartureTime = "17:19",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 463,
                            DepartureTime = "17:26",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 464,
                            DepartureTime = "17:33",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 465,
                            DepartureTime = "17:40",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 466,
                            DepartureTime = "17:47",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 467,
                            DepartureTime = "17:54",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 468,
                            DepartureTime = "18:01",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 469,
                            DepartureTime = "18:08",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 470,
                            DepartureTime = "18:15",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 471,
                            DepartureTime = "18:22",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 472,
                            DepartureTime = "18:29",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 473,
                            DepartureTime = "18:36",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 474,
                            DepartureTime = "18:43",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 475,
                            DepartureTime = "18:50",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 476,
                            DepartureTime = "18:57",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 477,
                            DepartureTime = "19:04",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 478,
                            DepartureTime = "19:11",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 479,
                            DepartureTime = "19:18",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 480,
                            DepartureTime = "19:25",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 481,
                            DepartureTime = "19:32",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 482,
                            DepartureTime = "19:39",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 483,
                            DepartureTime = "19:46",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 484,
                            DepartureTime = "19:53",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 485,
                            DepartureTime = "20:00",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 486,
                            DepartureTime = "20:07",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 487,
                            DepartureTime = "20:14",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 488,
                            DepartureTime = "20:21",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 489,
                            DepartureTime = "20:28",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 490,
                            DepartureTime = "20:35",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 491,
                            DepartureTime = "20:42",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 492,
                            DepartureTime = "20:49",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 493,
                            DepartureTime = "20:56",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 494,
                            DepartureTime = "21:03",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 495,
                            DepartureTime = "21:10",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 496,
                            DepartureTime = "21:17",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 497,
                            DepartureTime = "21:24",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 498,
                            DepartureTime = "21:31",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 499,
                            DepartureTime = "21:38",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 500,
                            DepartureTime = "21:45",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 501,
                            DepartureTime = "21:52",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 502,
                            DepartureTime = "21:59",
                            RouteId = 5
                        },
                        new
                        {
                            Id = 503,
                            DepartureTime = "06:00",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 504,
                            DepartureTime = "06:08",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 505,
                            DepartureTime = "06:16",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 506,
                            DepartureTime = "06:24",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 507,
                            DepartureTime = "06:32",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 508,
                            DepartureTime = "06:40",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 509,
                            DepartureTime = "06:48",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 510,
                            DepartureTime = "06:56",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 511,
                            DepartureTime = "07:04",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 512,
                            DepartureTime = "07:12",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 513,
                            DepartureTime = "07:20",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 514,
                            DepartureTime = "07:28",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 515,
                            DepartureTime = "07:36",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 516,
                            DepartureTime = "07:44",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 517,
                            DepartureTime = "07:52",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 518,
                            DepartureTime = "08:00",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 519,
                            DepartureTime = "08:08",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 520,
                            DepartureTime = "08:16",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 521,
                            DepartureTime = "08:24",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 522,
                            DepartureTime = "08:32",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 523,
                            DepartureTime = "08:40",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 524,
                            DepartureTime = "08:48",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 525,
                            DepartureTime = "08:56",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 526,
                            DepartureTime = "09:04",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 527,
                            DepartureTime = "09:12",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 528,
                            DepartureTime = "09:20",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 529,
                            DepartureTime = "09:28",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 530,
                            DepartureTime = "09:36",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 531,
                            DepartureTime = "09:44",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 532,
                            DepartureTime = "09:52",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 533,
                            DepartureTime = "10:00",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 534,
                            DepartureTime = "10:08",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 535,
                            DepartureTime = "10:16",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 536,
                            DepartureTime = "10:24",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 537,
                            DepartureTime = "10:32",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 538,
                            DepartureTime = "10:40",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 539,
                            DepartureTime = "10:48",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 540,
                            DepartureTime = "10:56",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 541,
                            DepartureTime = "11:04",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 542,
                            DepartureTime = "11:12",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 543,
                            DepartureTime = "11:20",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 544,
                            DepartureTime = "11:28",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 545,
                            DepartureTime = "11:36",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 546,
                            DepartureTime = "11:44",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 547,
                            DepartureTime = "11:52",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 548,
                            DepartureTime = "12:00",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 549,
                            DepartureTime = "12:08",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 550,
                            DepartureTime = "12:16",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 551,
                            DepartureTime = "12:24",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 552,
                            DepartureTime = "12:32",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 553,
                            DepartureTime = "12:40",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 554,
                            DepartureTime = "12:48",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 555,
                            DepartureTime = "12:56",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 556,
                            DepartureTime = "13:04",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 557,
                            DepartureTime = "13:12",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 558,
                            DepartureTime = "13:20",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 559,
                            DepartureTime = "13:28",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 560,
                            DepartureTime = "13:36",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 561,
                            DepartureTime = "13:44",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 562,
                            DepartureTime = "13:52",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 563,
                            DepartureTime = "14:00",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 564,
                            DepartureTime = "14:08",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 565,
                            DepartureTime = "14:16",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 566,
                            DepartureTime = "14:24",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 567,
                            DepartureTime = "14:32",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 568,
                            DepartureTime = "14:40",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 569,
                            DepartureTime = "14:48",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 570,
                            DepartureTime = "14:56",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 571,
                            DepartureTime = "15:04",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 572,
                            DepartureTime = "15:12",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 573,
                            DepartureTime = "15:20",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 574,
                            DepartureTime = "15:28",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 575,
                            DepartureTime = "15:36",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 576,
                            DepartureTime = "15:44",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 577,
                            DepartureTime = "15:52",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 578,
                            DepartureTime = "16:00",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 579,
                            DepartureTime = "16:08",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 580,
                            DepartureTime = "16:16",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 581,
                            DepartureTime = "16:24",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 582,
                            DepartureTime = "16:32",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 583,
                            DepartureTime = "16:40",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 584,
                            DepartureTime = "16:48",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 585,
                            DepartureTime = "16:56",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 586,
                            DepartureTime = "17:04",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 587,
                            DepartureTime = "17:12",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 588,
                            DepartureTime = "17:20",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 589,
                            DepartureTime = "17:28",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 590,
                            DepartureTime = "17:36",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 591,
                            DepartureTime = "17:44",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 592,
                            DepartureTime = "17:52",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 593,
                            DepartureTime = "18:00",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 594,
                            DepartureTime = "18:08",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 595,
                            DepartureTime = "18:16",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 596,
                            DepartureTime = "18:24",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 597,
                            DepartureTime = "18:32",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 598,
                            DepartureTime = "18:40",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 599,
                            DepartureTime = "18:48",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 600,
                            DepartureTime = "18:56",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 601,
                            DepartureTime = "19:04",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 602,
                            DepartureTime = "19:12",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 603,
                            DepartureTime = "19:20",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 604,
                            DepartureTime = "19:28",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 605,
                            DepartureTime = "19:36",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 606,
                            DepartureTime = "19:44",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 607,
                            DepartureTime = "19:52",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 608,
                            DepartureTime = "20:00",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 609,
                            DepartureTime = "20:08",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 610,
                            DepartureTime = "20:16",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 611,
                            DepartureTime = "20:24",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 612,
                            DepartureTime = "20:32",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 613,
                            DepartureTime = "20:40",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 614,
                            DepartureTime = "20:48",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 615,
                            DepartureTime = "20:56",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 616,
                            DepartureTime = "21:04",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 617,
                            DepartureTime = "21:12",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 618,
                            DepartureTime = "21:20",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 619,
                            DepartureTime = "21:28",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 620,
                            DepartureTime = "21:36",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 621,
                            DepartureTime = "21:44",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 622,
                            DepartureTime = "21:52",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 623,
                            DepartureTime = "22:00",
                            RouteId = 6
                        },
                        new
                        {
                            Id = 624,
                            DepartureTime = "06:00",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 625,
                            DepartureTime = "06:18",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 626,
                            DepartureTime = "06:36",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 627,
                            DepartureTime = "06:54",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 628,
                            DepartureTime = "07:12",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 629,
                            DepartureTime = "07:30",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 630,
                            DepartureTime = "07:48",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 631,
                            DepartureTime = "08:06",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 632,
                            DepartureTime = "08:24",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 633,
                            DepartureTime = "08:42",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 634,
                            DepartureTime = "09:00",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 635,
                            DepartureTime = "09:18",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 636,
                            DepartureTime = "09:36",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 637,
                            DepartureTime = "09:54",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 638,
                            DepartureTime = "10:12",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 639,
                            DepartureTime = "10:30",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 640,
                            DepartureTime = "10:48",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 641,
                            DepartureTime = "11:06",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 642,
                            DepartureTime = "11:24",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 643,
                            DepartureTime = "11:42",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 644,
                            DepartureTime = "12:00",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 645,
                            DepartureTime = "12:18",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 646,
                            DepartureTime = "12:36",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 647,
                            DepartureTime = "12:54",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 648,
                            DepartureTime = "13:12",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 649,
                            DepartureTime = "13:30",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 650,
                            DepartureTime = "13:48",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 651,
                            DepartureTime = "14:06",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 652,
                            DepartureTime = "14:24",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 653,
                            DepartureTime = "14:42",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 654,
                            DepartureTime = "15:00",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 655,
                            DepartureTime = "15:18",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 656,
                            DepartureTime = "15:36",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 657,
                            DepartureTime = "15:54",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 658,
                            DepartureTime = "16:12",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 659,
                            DepartureTime = "16:30",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 660,
                            DepartureTime = "16:48",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 661,
                            DepartureTime = "17:06",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 662,
                            DepartureTime = "17:24",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 663,
                            DepartureTime = "17:42",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 664,
                            DepartureTime = "18:00",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 665,
                            DepartureTime = "18:18",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 666,
                            DepartureTime = "18:36",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 667,
                            DepartureTime = "18:54",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 668,
                            DepartureTime = "19:12",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 669,
                            DepartureTime = "19:30",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 670,
                            DepartureTime = "19:48",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 671,
                            DepartureTime = "20:06",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 672,
                            DepartureTime = "20:24",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 673,
                            DepartureTime = "20:42",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 674,
                            DepartureTime = "21:00",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 675,
                            DepartureTime = "21:18",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 676,
                            DepartureTime = "21:36",
                            RouteId = 7
                        },
                        new
                        {
                            Id = 677,
                            DepartureTime = "21:54",
                            RouteId = 7
                        });
                });

            modelBuilder.Entity("TransitApi.Models.Stop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Stops");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "пл. Восстания, 1",
                            Direction = "В центр и на юг",
                            Name = "Центральный вокзал"
                        },
                        new
                        {
                            Id = 2,
                            Address = "пл. Восстания",
                            Direction = "Все направления",
                            Name = "Площадь Восстания"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Невский пр., 28",
                            Direction = "На восток/запад",
                            Name = "Невский проспект"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Сенная пл., 1",
                            Direction = "На юг",
                            Name = "Сенная площадь"
                        },
                        new
                        {
                            Id = 5,
                            Address = "ул. Садовая, 50",
                            Direction = "На север",
                            Name = "Парк Культуры"
                        },
                        new
                        {
                            Id = 6,
                            Address = "Невский пр., 35",
                            Direction = "Центр",
                            Name = "Гостиный Двор"
                        },
                        new
                        {
                            Id = 7,
                            Address = "Лиговский пр., 30а",
                            Direction = "На восток",
                            Name = "Торговый Центр «Галерея»"
                        },
                        new
                        {
                            Id = 8,
                            Address = "Пулковское ш., 41Л1",
                            Direction = "Конечная",
                            Name = "Аэропорт Пулково"
                        },
                        new
                        {
                            Id = 9,
                            Address = "пр. Просвещения, 1",
                            Direction = "На север",
                            Name = "Северный вокзал"
                        },
                        new
                        {
                            Id = 10,
                            Address = "ул. Витебская, 15",
                            Direction = "На юг",
                            Name = "Южный парк"
                        },
                        new
                        {
                            Id = 11,
                            Address = "Университетская наб., 7",
                            Direction = "На запад",
                            Name = "Университет"
                        },
                        new
                        {
                            Id = 12,
                            Address = "пл. Победы, 1",
                            Direction = "На юг",
                            Name = "Площадь Победы"
                        },
                        new
                        {
                            Id = 13,
                            Address = "Большой пр. П.С., 1",
                            Direction = "На север",
                            Name = "Петроградская"
                        },
                        new
                        {
                            Id = 14,
                            Address = "В.О., 1-я линия, 10",
                            Direction = "На запад",
                            Name = "Васильевский остров"
                        },
                        new
                        {
                            Id = 15,
                            Address = "Конная ул., 5",
                            Direction = "Восток",
                            Name = "Конная улица"
                        },
                        new
                        {
                            Id = 16,
                            Address = "Тульская ул., 3",
                            Direction = "Запад",
                            Name = "Тульская улица"
                        });
                });

            modelBuilder.Entity("TransitApi.Models.TransitRoute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EndStop")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FrequencyMinutes")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StartStop")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Routes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndStop = "Аэропорт Пулково",
                            FrequencyMinutes = 10,
                            IsActive = true,
                            Name = "Центр — Аэропорт",
                            Number = "15",
                            StartStop = "Центральный вокзал",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            EndStop = "Южный парк",
                            FrequencyMinutes = 15,
                            IsActive = true,
                            Name = "Северный вокзал — Южный парк",
                            Number = "42",
                            StartStop = "Северный вокзал",
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            EndStop = "Сенная площадь",
                            FrequencyMinutes = 8,
                            IsActive = true,
                            Name = "Гостиный Двор — Сенная",
                            Number = "3",
                            StartStop = "Гостиный Двор",
                            Type = 2
                        },
                        new
                        {
                            Id = 4,
                            EndStop = "Петроградская",
                            FrequencyMinutes = 12,
                            IsActive = true,
                            Name = "Центр — Петроградская",
                            Number = "7",
                            StartStop = "Невский проспект",
                            Type = 1
                        },
                        new
                        {
                            Id = 5,
                            EndStop = "Торговый Центр «Галерея»",
                            FrequencyMinutes = 7,
                            IsActive = true,
                            Name = "Центр — Галерея",
                            Number = "112т",
                            StartStop = "Центральный вокзал",
                            Type = 3
                        },
                        new
                        {
                            Id = 6,
                            EndStop = "Площадь Победы",
                            FrequencyMinutes = 8,
                            IsActive = true,
                            Name = "Университет — Площадь Победы",
                            Number = "8",
                            StartStop = "Университет",
                            Type = 0
                        },
                        new
                        {
                            Id = 7,
                            EndStop = "Тульская улица",
                            FrequencyMinutes = 18,
                            IsActive = true,
                            Name = "Конная улица — Тульская улица",
                            Number = "5",
                            StartStop = "Конная улица",
                            Type = 1
                        });
                });

            modelBuilder.Entity("TransitApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "user@urbantransit.local",
                            IsAdmin = false,
                            PasswordHash = "$2a$11$fyHxJtrVmySwWBdzgeXmVuKyFDRxpnmD3Iy9LipJdsqDVL0aqwuzm",
                            Username = "testuser"
                        },
                        new
                        {
                            Id = 2,
                            Email = "admin@urbantransit.local",
                            IsAdmin = true,
                            PasswordHash = "$2a$11$t7batDXMOA8Y5X76aq0wguUN/v64mtxhbXaUSU0Mt8a/gjojYdcv2",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("NotificationTransitRoute", b =>
                {
                    b.HasOne("TransitApi.Models.TransitRoute", null)
                        .WithMany()
                        .HasForeignKey("AffectedTransitRoutesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransitApi.Models.Notification", null)
                        .WithMany()
                        .HasForeignKey("NotificationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransitApi.Models.FavoritePlace", b =>
                {
                    b.HasOne("TransitApi.Models.User", "User")
                        .WithMany("FavoritePlaces")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TransitApi.Models.FavoriteRoute", b =>
                {
                    b.HasOne("TransitApi.Models.TransitRoute", "TransitRoute")
                        .WithMany()
                        .HasForeignKey("TransitRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransitApi.Models.User", "User")
                        .WithMany("FavoriteRoutes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransitRoute");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TransitApi.Models.FavoriteStop", b =>
                {
                    b.HasOne("TransitApi.Models.Stop", "Stop")
                        .WithMany()
                        .HasForeignKey("StopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransitApi.Models.User", "User")
                        .WithMany("FavoriteStops")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stop");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TransitApi.Models.RouteStop", b =>
                {
                    b.HasOne("TransitApi.Models.TransitRoute", "Route")
                        .WithMany("RouteStops")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransitApi.Models.Stop", "Stop")
                        .WithMany("RouteStops")
                        .HasForeignKey("StopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("Stop");
                });

            modelBuilder.Entity("TransitApi.Models.Schedule", b =>
                {
                    b.HasOne("TransitApi.Models.TransitRoute", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");
                });

            modelBuilder.Entity("TransitApi.Models.Stop", b =>
                {
                    b.Navigation("RouteStops");
                });

            modelBuilder.Entity("TransitApi.Models.TransitRoute", b =>
                {
                    b.Navigation("RouteStops");
                });

            modelBuilder.Entity("TransitApi.Models.User", b =>
                {
                    b.Navigation("FavoritePlaces");

                    b.Navigation("FavoriteRoutes");

                    b.Navigation("FavoriteStops");
                });
#pragma warning restore 612, 618
        }
    }
}
