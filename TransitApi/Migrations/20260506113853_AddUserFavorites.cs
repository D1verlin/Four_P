using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 

namespace TransitApi.Migrations
{
    
    public partial class AddUserFavorites : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    AffectedRoutes = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    StartStop = table.Column<string>(type: "TEXT", nullable: false),
                    EndStop = table.Column<string>(type: "TEXT", nullable: false),
                    FrequencyMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Direction = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTransitRoute",
                columns: table => new
                {
                    AffectedTransitRoutesId = table.Column<int>(type: "INTEGER", nullable: false),
                    NotificationsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTransitRoute", x => new { x.AffectedTransitRoutesId, x.NotificationsId });
                    table.ForeignKey(
                        name: "FK_NotificationTransitRoute_Notifications_NotificationsId",
                        column: x => x.NotificationsId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationTransitRoute_Routes_AffectedTransitRoutesId",
                        column: x => x.AffectedTransitRoutesId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RouteId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartureTime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RouteId = table.Column<int>(type: "INTEGER", nullable: false),
                    StopId = table.Column<int>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    OffsetMinutes = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteStops_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteStops_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoritePlaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Longitude = table.Column<double>(type: "REAL", nullable: true),
                    Icon = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritePlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoritePlaces_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TransitRouteId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteRoutes_Routes_TransitRouteId",
                        column: x => x.TransitRouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteRoutes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    StopId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteStops_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteStops_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "AffectedRoutes", "CreatedAt", "Message", "Title", "Type" },
                values: new object[,]
                {
                    { 1, "42,15,8", new DateTime(2024, 5, 1, 11, 55, 0, 0, DateTimeKind.Utc), "Авария на центральном проспекте блокирует движение маршрутов 42, 15 и 8. Ожидаются значительные задержки. Рекомендуется использовать объездные пути.", "Движение парализовано", 0 },
                    { 2, "7", new DateTime(2024, 5, 1, 11, 45, 0, 0, DateTimeKind.Utc), "Троллейбус маршрута 7 задерживается примерно на 15-20 минут из-за плотного трафика на Южной магистрали.", "Отклонение от расписания", 1 },
                    { 3, "42", new DateTime(2024, 5, 1, 10, 0, 0, 0, DateTimeKind.Utc), "В связи с ремонтными работами, остановка «Парк Культуры» временно перенесена на 100 метров вперёд по ходу движения.", "Изменение остановки", 2 },
                    { 4, "3", new DateTime(2024, 4, 30, 12, 0, 0, 0, DateTimeKind.Utc), "В выходные дни движение трамваев по Северной линии будет ограничено. Предоставляются компенсационные автобусные маршруты (КМ). Пожалуйста, планируйте поездки заранее.", "Ремонт путей на Северной линии", 3 }
                });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "EndStop", "FrequencyMinutes", "IsActive", "Name", "Number", "StartStop", "Type" },
                values: new object[,]
                {
                    { 1, "Аэропорт Пулково", 10, true, "Центр — Аэропорт", "15", "Центральный вокзал", 0 },
                    { 2, "Южный парк", 15, true, "Северный вокзал — Южный парк", "42", "Северный вокзал", 0 },
                    { 3, "Сенная площадь", 8, true, "Гостиный Двор — Сенная", "3", "Гостиный Двор", 2 },
                    { 4, "Петроградская", 12, true, "Центр — Петроградская", "7", "Невский проспект", 1 },
                    { 5, "Торговый Центр «Галерея»", 7, true, "Центр — Галерея", "112т", "Центральный вокзал", 3 },
                    { 6, "Площадь Победы", 8, true, "Университет — Площадь Победы", "8", "Университет", 0 },
                    { 7, "Тульская улица", 18, true, "Конная улица — Тульская улица", "5", "Конная улица", 1 }
                });

            migrationBuilder.InsertData(
                table: "Stops",
                columns: new[] { "Id", "Address", "Direction", "Name" },
                values: new object[,]
                {
                    { 1, "пл. Восстания, 1", "В центр и на юг", "Центральный вокзал" },
                    { 2, "пл. Восстания", "Все направления", "Площадь Восстания" },
                    { 3, "Невский пр., 28", "На восток/запад", "Невский проспект" },
                    { 4, "Сенная пл., 1", "На юг", "Сенная площадь" },
                    { 5, "ул. Садовая, 50", "На север", "Парк Культуры" },
                    { 6, "Невский пр., 35", "Центр", "Гостиный Двор" },
                    { 7, "Лиговский пр., 30а", "На восток", "Торговый Центр «Галерея»" },
                    { 8, "Пулковское ш., 41Л1", "Конечная", "Аэропорт Пулково" },
                    { 9, "пр. Просвещения, 1", "На север", "Северный вокзал" },
                    { 10, "ул. Витебская, 15", "На юг", "Южный парк" },
                    { 11, "Университетская наб., 7", "На запад", "Университет" },
                    { 12, "пл. Победы, 1", "На юг", "Площадь Победы" },
                    { 13, "Большой пр. П.С., 1", "На север", "Петроградская" },
                    { 14, "В.О., 1-я линия, 10", "На запад", "Васильевский остров" },
                    { 15, "Конная ул., 5", "Восток", "Конная улица" },
                    { 16, "Тульская ул., 3", "Запад", "Тульская улица" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Username" },
                values: new object[] { 1, "user@urbantransit.local", "testuser" });

            migrationBuilder.InsertData(
                table: "FavoritePlaces",
                columns: new[] { "Id", "Address", "Icon", "Latitude", "Longitude", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Невский проспект, 28", "home", null, null, "Дом", 1 },
                    { 2, "Лиговский пр., 30а", "work", null, null, "Работа", 1 }
                });

            migrationBuilder.InsertData(
                table: "FavoriteRoutes",
                columns: new[] { "Id", "TransitRouteId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 7, 1 }
                });

            migrationBuilder.InsertData(
                table: "FavoriteStops",
                columns: new[] { "Id", "StopId", "UserId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "RouteStops",
                columns: new[] { "Id", "OffsetMinutes", "Order", "RouteId", "StopId" },
                values: new object[,]
                {
                    { 1, 0, 1, 1, 1 },
                    { 2, 5, 2, 1, 2 },
                    { 3, 10, 3, 1, 3 },
                    { 4, 18, 4, 1, 7 },
                    { 5, 24, 5, 1, 4 },
                    { 6, 33, 6, 1, 12 },
                    { 7, 45, 7, 1, 8 },
                    { 8, 0, 1, 2, 9 },
                    { 9, 8, 2, 2, 13 },
                    { 10, 15, 3, 2, 2 },
                    { 11, 22, 4, 2, 5 },
                    { 12, 30, 5, 2, 10 },
                    { 13, 0, 1, 3, 6 },
                    { 14, 5, 2, 3, 3 },
                    { 15, 12, 3, 3, 4 },
                    { 16, 0, 1, 4, 3 },
                    { 17, 6, 2, 4, 2 },
                    { 18, 14, 3, 4, 13 },
                    { 19, 0, 1, 5, 1 },
                    { 20, 4, 2, 5, 2 },
                    { 21, 10, 3, 5, 7 },
                    { 22, 0, 1, 6, 11 },
                    { 23, 7, 2, 6, 14 },
                    { 24, 15, 3, 6, 3 },
                    { 25, 25, 4, 6, 12 },
                    { 26, 0, 1, 7, 15 },
                    { 27, 8, 2, 7, 3 },
                    { 28, 16, 3, 7, 16 }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "DepartureTime", "RouteId" },
                values: new object[,]
                {
                    { 1, "06:00", 1 },
                    { 2, "06:10", 1 },
                    { 3, "06:20", 1 },
                    { 4, "06:30", 1 },
                    { 5, "06:40", 1 },
                    { 6, "06:50", 1 },
                    { 7, "07:00", 1 },
                    { 8, "07:10", 1 },
                    { 9, "07:20", 1 },
                    { 10, "07:30", 1 },
                    { 11, "07:40", 1 },
                    { 12, "07:50", 1 },
                    { 13, "08:00", 1 },
                    { 14, "08:10", 1 },
                    { 15, "08:20", 1 },
                    { 16, "08:30", 1 },
                    { 17, "08:40", 1 },
                    { 18, "08:50", 1 },
                    { 19, "09:00", 1 },
                    { 20, "09:10", 1 },
                    { 21, "09:20", 1 },
                    { 22, "09:30", 1 },
                    { 23, "09:40", 1 },
                    { 24, "09:50", 1 },
                    { 25, "10:00", 1 },
                    { 26, "10:10", 1 },
                    { 27, "10:20", 1 },
                    { 28, "10:30", 1 },
                    { 29, "10:40", 1 },
                    { 30, "10:50", 1 },
                    { 31, "11:00", 1 },
                    { 32, "11:10", 1 },
                    { 33, "11:20", 1 },
                    { 34, "11:30", 1 },
                    { 35, "11:40", 1 },
                    { 36, "11:50", 1 },
                    { 37, "12:00", 1 },
                    { 38, "12:10", 1 },
                    { 39, "12:20", 1 },
                    { 40, "12:30", 1 },
                    { 41, "12:40", 1 },
                    { 42, "12:50", 1 },
                    { 43, "13:00", 1 },
                    { 44, "13:10", 1 },
                    { 45, "13:20", 1 },
                    { 46, "13:30", 1 },
                    { 47, "13:40", 1 },
                    { 48, "13:50", 1 },
                    { 49, "14:00", 1 },
                    { 50, "14:10", 1 },
                    { 51, "14:20", 1 },
                    { 52, "14:30", 1 },
                    { 53, "14:40", 1 },
                    { 54, "14:50", 1 },
                    { 55, "15:00", 1 },
                    { 56, "15:10", 1 },
                    { 57, "15:20", 1 },
                    { 58, "15:30", 1 },
                    { 59, "15:40", 1 },
                    { 60, "15:50", 1 },
                    { 61, "16:00", 1 },
                    { 62, "16:10", 1 },
                    { 63, "16:20", 1 },
                    { 64, "16:30", 1 },
                    { 65, "16:40", 1 },
                    { 66, "16:50", 1 },
                    { 67, "17:00", 1 },
                    { 68, "17:10", 1 },
                    { 69, "17:20", 1 },
                    { 70, "17:30", 1 },
                    { 71, "17:40", 1 },
                    { 72, "17:50", 1 },
                    { 73, "18:00", 1 },
                    { 74, "18:10", 1 },
                    { 75, "18:20", 1 },
                    { 76, "18:30", 1 },
                    { 77, "18:40", 1 },
                    { 78, "18:50", 1 },
                    { 79, "19:00", 1 },
                    { 80, "19:10", 1 },
                    { 81, "19:20", 1 },
                    { 82, "19:30", 1 },
                    { 83, "19:40", 1 },
                    { 84, "19:50", 1 },
                    { 85, "20:00", 1 },
                    { 86, "20:10", 1 },
                    { 87, "20:20", 1 },
                    { 88, "20:30", 1 },
                    { 89, "20:40", 1 },
                    { 90, "20:50", 1 },
                    { 91, "21:00", 1 },
                    { 92, "21:10", 1 },
                    { 93, "21:20", 1 },
                    { 94, "21:30", 1 },
                    { 95, "21:40", 1 },
                    { 96, "21:50", 1 },
                    { 97, "22:00", 1 },
                    { 98, "06:00", 2 },
                    { 99, "06:15", 2 },
                    { 100, "06:30", 2 },
                    { 101, "06:45", 2 },
                    { 102, "07:00", 2 },
                    { 103, "07:15", 2 },
                    { 104, "07:30", 2 },
                    { 105, "07:45", 2 },
                    { 106, "08:00", 2 },
                    { 107, "08:15", 2 },
                    { 108, "08:30", 2 },
                    { 109, "08:45", 2 },
                    { 110, "09:00", 2 },
                    { 111, "09:15", 2 },
                    { 112, "09:30", 2 },
                    { 113, "09:45", 2 },
                    { 114, "10:00", 2 },
                    { 115, "10:15", 2 },
                    { 116, "10:30", 2 },
                    { 117, "10:45", 2 },
                    { 118, "11:00", 2 },
                    { 119, "11:15", 2 },
                    { 120, "11:30", 2 },
                    { 121, "11:45", 2 },
                    { 122, "12:00", 2 },
                    { 123, "12:15", 2 },
                    { 124, "12:30", 2 },
                    { 125, "12:45", 2 },
                    { 126, "13:00", 2 },
                    { 127, "13:15", 2 },
                    { 128, "13:30", 2 },
                    { 129, "13:45", 2 },
                    { 130, "14:00", 2 },
                    { 131, "14:15", 2 },
                    { 132, "14:30", 2 },
                    { 133, "14:45", 2 },
                    { 134, "15:00", 2 },
                    { 135, "15:15", 2 },
                    { 136, "15:30", 2 },
                    { 137, "15:45", 2 },
                    { 138, "16:00", 2 },
                    { 139, "16:15", 2 },
                    { 140, "16:30", 2 },
                    { 141, "16:45", 2 },
                    { 142, "17:00", 2 },
                    { 143, "17:15", 2 },
                    { 144, "17:30", 2 },
                    { 145, "17:45", 2 },
                    { 146, "18:00", 2 },
                    { 147, "18:15", 2 },
                    { 148, "18:30", 2 },
                    { 149, "18:45", 2 },
                    { 150, "19:00", 2 },
                    { 151, "19:15", 2 },
                    { 152, "19:30", 2 },
                    { 153, "19:45", 2 },
                    { 154, "20:00", 2 },
                    { 155, "20:15", 2 },
                    { 156, "20:30", 2 },
                    { 157, "20:45", 2 },
                    { 158, "21:00", 2 },
                    { 159, "21:15", 2 },
                    { 160, "21:30", 2 },
                    { 161, "21:45", 2 },
                    { 162, "22:00", 2 },
                    { 163, "06:00", 3 },
                    { 164, "06:08", 3 },
                    { 165, "06:16", 3 },
                    { 166, "06:24", 3 },
                    { 167, "06:32", 3 },
                    { 168, "06:40", 3 },
                    { 169, "06:48", 3 },
                    { 170, "06:56", 3 },
                    { 171, "07:04", 3 },
                    { 172, "07:12", 3 },
                    { 173, "07:20", 3 },
                    { 174, "07:28", 3 },
                    { 175, "07:36", 3 },
                    { 176, "07:44", 3 },
                    { 177, "07:52", 3 },
                    { 178, "08:00", 3 },
                    { 179, "08:08", 3 },
                    { 180, "08:16", 3 },
                    { 181, "08:24", 3 },
                    { 182, "08:32", 3 },
                    { 183, "08:40", 3 },
                    { 184, "08:48", 3 },
                    { 185, "08:56", 3 },
                    { 186, "09:04", 3 },
                    { 187, "09:12", 3 },
                    { 188, "09:20", 3 },
                    { 189, "09:28", 3 },
                    { 190, "09:36", 3 },
                    { 191, "09:44", 3 },
                    { 192, "09:52", 3 },
                    { 193, "10:00", 3 },
                    { 194, "10:08", 3 },
                    { 195, "10:16", 3 },
                    { 196, "10:24", 3 },
                    { 197, "10:32", 3 },
                    { 198, "10:40", 3 },
                    { 199, "10:48", 3 },
                    { 200, "10:56", 3 },
                    { 201, "11:04", 3 },
                    { 202, "11:12", 3 },
                    { 203, "11:20", 3 },
                    { 204, "11:28", 3 },
                    { 205, "11:36", 3 },
                    { 206, "11:44", 3 },
                    { 207, "11:52", 3 },
                    { 208, "12:00", 3 },
                    { 209, "12:08", 3 },
                    { 210, "12:16", 3 },
                    { 211, "12:24", 3 },
                    { 212, "12:32", 3 },
                    { 213, "12:40", 3 },
                    { 214, "12:48", 3 },
                    { 215, "12:56", 3 },
                    { 216, "13:04", 3 },
                    { 217, "13:12", 3 },
                    { 218, "13:20", 3 },
                    { 219, "13:28", 3 },
                    { 220, "13:36", 3 },
                    { 221, "13:44", 3 },
                    { 222, "13:52", 3 },
                    { 223, "14:00", 3 },
                    { 224, "14:08", 3 },
                    { 225, "14:16", 3 },
                    { 226, "14:24", 3 },
                    { 227, "14:32", 3 },
                    { 228, "14:40", 3 },
                    { 229, "14:48", 3 },
                    { 230, "14:56", 3 },
                    { 231, "15:04", 3 },
                    { 232, "15:12", 3 },
                    { 233, "15:20", 3 },
                    { 234, "15:28", 3 },
                    { 235, "15:36", 3 },
                    { 236, "15:44", 3 },
                    { 237, "15:52", 3 },
                    { 238, "16:00", 3 },
                    { 239, "16:08", 3 },
                    { 240, "16:16", 3 },
                    { 241, "16:24", 3 },
                    { 242, "16:32", 3 },
                    { 243, "16:40", 3 },
                    { 244, "16:48", 3 },
                    { 245, "16:56", 3 },
                    { 246, "17:04", 3 },
                    { 247, "17:12", 3 },
                    { 248, "17:20", 3 },
                    { 249, "17:28", 3 },
                    { 250, "17:36", 3 },
                    { 251, "17:44", 3 },
                    { 252, "17:52", 3 },
                    { 253, "18:00", 3 },
                    { 254, "18:08", 3 },
                    { 255, "18:16", 3 },
                    { 256, "18:24", 3 },
                    { 257, "18:32", 3 },
                    { 258, "18:40", 3 },
                    { 259, "18:48", 3 },
                    { 260, "18:56", 3 },
                    { 261, "19:04", 3 },
                    { 262, "19:12", 3 },
                    { 263, "19:20", 3 },
                    { 264, "19:28", 3 },
                    { 265, "19:36", 3 },
                    { 266, "19:44", 3 },
                    { 267, "19:52", 3 },
                    { 268, "20:00", 3 },
                    { 269, "20:08", 3 },
                    { 270, "20:16", 3 },
                    { 271, "20:24", 3 },
                    { 272, "20:32", 3 },
                    { 273, "20:40", 3 },
                    { 274, "20:48", 3 },
                    { 275, "20:56", 3 },
                    { 276, "21:04", 3 },
                    { 277, "21:12", 3 },
                    { 278, "21:20", 3 },
                    { 279, "21:28", 3 },
                    { 280, "21:36", 3 },
                    { 281, "21:44", 3 },
                    { 282, "21:52", 3 },
                    { 283, "22:00", 3 },
                    { 284, "06:00", 4 },
                    { 285, "06:12", 4 },
                    { 286, "06:24", 4 },
                    { 287, "06:36", 4 },
                    { 288, "06:48", 4 },
                    { 289, "07:00", 4 },
                    { 290, "07:12", 4 },
                    { 291, "07:24", 4 },
                    { 292, "07:36", 4 },
                    { 293, "07:48", 4 },
                    { 294, "08:00", 4 },
                    { 295, "08:12", 4 },
                    { 296, "08:24", 4 },
                    { 297, "08:36", 4 },
                    { 298, "08:48", 4 },
                    { 299, "09:00", 4 },
                    { 300, "09:12", 4 },
                    { 301, "09:24", 4 },
                    { 302, "09:36", 4 },
                    { 303, "09:48", 4 },
                    { 304, "10:00", 4 },
                    { 305, "10:12", 4 },
                    { 306, "10:24", 4 },
                    { 307, "10:36", 4 },
                    { 308, "10:48", 4 },
                    { 309, "11:00", 4 },
                    { 310, "11:12", 4 },
                    { 311, "11:24", 4 },
                    { 312, "11:36", 4 },
                    { 313, "11:48", 4 },
                    { 314, "12:00", 4 },
                    { 315, "12:12", 4 },
                    { 316, "12:24", 4 },
                    { 317, "12:36", 4 },
                    { 318, "12:48", 4 },
                    { 319, "13:00", 4 },
                    { 320, "13:12", 4 },
                    { 321, "13:24", 4 },
                    { 322, "13:36", 4 },
                    { 323, "13:48", 4 },
                    { 324, "14:00", 4 },
                    { 325, "14:12", 4 },
                    { 326, "14:24", 4 },
                    { 327, "14:36", 4 },
                    { 328, "14:48", 4 },
                    { 329, "15:00", 4 },
                    { 330, "15:12", 4 },
                    { 331, "15:24", 4 },
                    { 332, "15:36", 4 },
                    { 333, "15:48", 4 },
                    { 334, "16:00", 4 },
                    { 335, "16:12", 4 },
                    { 336, "16:24", 4 },
                    { 337, "16:36", 4 },
                    { 338, "16:48", 4 },
                    { 339, "17:00", 4 },
                    { 340, "17:12", 4 },
                    { 341, "17:24", 4 },
                    { 342, "17:36", 4 },
                    { 343, "17:48", 4 },
                    { 344, "18:00", 4 },
                    { 345, "18:12", 4 },
                    { 346, "18:24", 4 },
                    { 347, "18:36", 4 },
                    { 348, "18:48", 4 },
                    { 349, "19:00", 4 },
                    { 350, "19:12", 4 },
                    { 351, "19:24", 4 },
                    { 352, "19:36", 4 },
                    { 353, "19:48", 4 },
                    { 354, "20:00", 4 },
                    { 355, "20:12", 4 },
                    { 356, "20:24", 4 },
                    { 357, "20:36", 4 },
                    { 358, "20:48", 4 },
                    { 359, "21:00", 4 },
                    { 360, "21:12", 4 },
                    { 361, "21:24", 4 },
                    { 362, "21:36", 4 },
                    { 363, "21:48", 4 },
                    { 364, "22:00", 4 },
                    { 365, "06:00", 5 },
                    { 366, "06:07", 5 },
                    { 367, "06:14", 5 },
                    { 368, "06:21", 5 },
                    { 369, "06:28", 5 },
                    { 370, "06:35", 5 },
                    { 371, "06:42", 5 },
                    { 372, "06:49", 5 },
                    { 373, "06:56", 5 },
                    { 374, "07:03", 5 },
                    { 375, "07:10", 5 },
                    { 376, "07:17", 5 },
                    { 377, "07:24", 5 },
                    { 378, "07:31", 5 },
                    { 379, "07:38", 5 },
                    { 380, "07:45", 5 },
                    { 381, "07:52", 5 },
                    { 382, "07:59", 5 },
                    { 383, "08:06", 5 },
                    { 384, "08:13", 5 },
                    { 385, "08:20", 5 },
                    { 386, "08:27", 5 },
                    { 387, "08:34", 5 },
                    { 388, "08:41", 5 },
                    { 389, "08:48", 5 },
                    { 390, "08:55", 5 },
                    { 391, "09:02", 5 },
                    { 392, "09:09", 5 },
                    { 393, "09:16", 5 },
                    { 394, "09:23", 5 },
                    { 395, "09:30", 5 },
                    { 396, "09:37", 5 },
                    { 397, "09:44", 5 },
                    { 398, "09:51", 5 },
                    { 399, "09:58", 5 },
                    { 400, "10:05", 5 },
                    { 401, "10:12", 5 },
                    { 402, "10:19", 5 },
                    { 403, "10:26", 5 },
                    { 404, "10:33", 5 },
                    { 405, "10:40", 5 },
                    { 406, "10:47", 5 },
                    { 407, "10:54", 5 },
                    { 408, "11:01", 5 },
                    { 409, "11:08", 5 },
                    { 410, "11:15", 5 },
                    { 411, "11:22", 5 },
                    { 412, "11:29", 5 },
                    { 413, "11:36", 5 },
                    { 414, "11:43", 5 },
                    { 415, "11:50", 5 },
                    { 416, "11:57", 5 },
                    { 417, "12:04", 5 },
                    { 418, "12:11", 5 },
                    { 419, "12:18", 5 },
                    { 420, "12:25", 5 },
                    { 421, "12:32", 5 },
                    { 422, "12:39", 5 },
                    { 423, "12:46", 5 },
                    { 424, "12:53", 5 },
                    { 425, "13:00", 5 },
                    { 426, "13:07", 5 },
                    { 427, "13:14", 5 },
                    { 428, "13:21", 5 },
                    { 429, "13:28", 5 },
                    { 430, "13:35", 5 },
                    { 431, "13:42", 5 },
                    { 432, "13:49", 5 },
                    { 433, "13:56", 5 },
                    { 434, "14:03", 5 },
                    { 435, "14:10", 5 },
                    { 436, "14:17", 5 },
                    { 437, "14:24", 5 },
                    { 438, "14:31", 5 },
                    { 439, "14:38", 5 },
                    { 440, "14:45", 5 },
                    { 441, "14:52", 5 },
                    { 442, "14:59", 5 },
                    { 443, "15:06", 5 },
                    { 444, "15:13", 5 },
                    { 445, "15:20", 5 },
                    { 446, "15:27", 5 },
                    { 447, "15:34", 5 },
                    { 448, "15:41", 5 },
                    { 449, "15:48", 5 },
                    { 450, "15:55", 5 },
                    { 451, "16:02", 5 },
                    { 452, "16:09", 5 },
                    { 453, "16:16", 5 },
                    { 454, "16:23", 5 },
                    { 455, "16:30", 5 },
                    { 456, "16:37", 5 },
                    { 457, "16:44", 5 },
                    { 458, "16:51", 5 },
                    { 459, "16:58", 5 },
                    { 460, "17:05", 5 },
                    { 461, "17:12", 5 },
                    { 462, "17:19", 5 },
                    { 463, "17:26", 5 },
                    { 464, "17:33", 5 },
                    { 465, "17:40", 5 },
                    { 466, "17:47", 5 },
                    { 467, "17:54", 5 },
                    { 468, "18:01", 5 },
                    { 469, "18:08", 5 },
                    { 470, "18:15", 5 },
                    { 471, "18:22", 5 },
                    { 472, "18:29", 5 },
                    { 473, "18:36", 5 },
                    { 474, "18:43", 5 },
                    { 475, "18:50", 5 },
                    { 476, "18:57", 5 },
                    { 477, "19:04", 5 },
                    { 478, "19:11", 5 },
                    { 479, "19:18", 5 },
                    { 480, "19:25", 5 },
                    { 481, "19:32", 5 },
                    { 482, "19:39", 5 },
                    { 483, "19:46", 5 },
                    { 484, "19:53", 5 },
                    { 485, "20:00", 5 },
                    { 486, "20:07", 5 },
                    { 487, "20:14", 5 },
                    { 488, "20:21", 5 },
                    { 489, "20:28", 5 },
                    { 490, "20:35", 5 },
                    { 491, "20:42", 5 },
                    { 492, "20:49", 5 },
                    { 493, "20:56", 5 },
                    { 494, "21:03", 5 },
                    { 495, "21:10", 5 },
                    { 496, "21:17", 5 },
                    { 497, "21:24", 5 },
                    { 498, "21:31", 5 },
                    { 499, "21:38", 5 },
                    { 500, "21:45", 5 },
                    { 501, "21:52", 5 },
                    { 502, "21:59", 5 },
                    { 503, "06:00", 6 },
                    { 504, "06:08", 6 },
                    { 505, "06:16", 6 },
                    { 506, "06:24", 6 },
                    { 507, "06:32", 6 },
                    { 508, "06:40", 6 },
                    { 509, "06:48", 6 },
                    { 510, "06:56", 6 },
                    { 511, "07:04", 6 },
                    { 512, "07:12", 6 },
                    { 513, "07:20", 6 },
                    { 514, "07:28", 6 },
                    { 515, "07:36", 6 },
                    { 516, "07:44", 6 },
                    { 517, "07:52", 6 },
                    { 518, "08:00", 6 },
                    { 519, "08:08", 6 },
                    { 520, "08:16", 6 },
                    { 521, "08:24", 6 },
                    { 522, "08:32", 6 },
                    { 523, "08:40", 6 },
                    { 524, "08:48", 6 },
                    { 525, "08:56", 6 },
                    { 526, "09:04", 6 },
                    { 527, "09:12", 6 },
                    { 528, "09:20", 6 },
                    { 529, "09:28", 6 },
                    { 530, "09:36", 6 },
                    { 531, "09:44", 6 },
                    { 532, "09:52", 6 },
                    { 533, "10:00", 6 },
                    { 534, "10:08", 6 },
                    { 535, "10:16", 6 },
                    { 536, "10:24", 6 },
                    { 537, "10:32", 6 },
                    { 538, "10:40", 6 },
                    { 539, "10:48", 6 },
                    { 540, "10:56", 6 },
                    { 541, "11:04", 6 },
                    { 542, "11:12", 6 },
                    { 543, "11:20", 6 },
                    { 544, "11:28", 6 },
                    { 545, "11:36", 6 },
                    { 546, "11:44", 6 },
                    { 547, "11:52", 6 },
                    { 548, "12:00", 6 },
                    { 549, "12:08", 6 },
                    { 550, "12:16", 6 },
                    { 551, "12:24", 6 },
                    { 552, "12:32", 6 },
                    { 553, "12:40", 6 },
                    { 554, "12:48", 6 },
                    { 555, "12:56", 6 },
                    { 556, "13:04", 6 },
                    { 557, "13:12", 6 },
                    { 558, "13:20", 6 },
                    { 559, "13:28", 6 },
                    { 560, "13:36", 6 },
                    { 561, "13:44", 6 },
                    { 562, "13:52", 6 },
                    { 563, "14:00", 6 },
                    { 564, "14:08", 6 },
                    { 565, "14:16", 6 },
                    { 566, "14:24", 6 },
                    { 567, "14:32", 6 },
                    { 568, "14:40", 6 },
                    { 569, "14:48", 6 },
                    { 570, "14:56", 6 },
                    { 571, "15:04", 6 },
                    { 572, "15:12", 6 },
                    { 573, "15:20", 6 },
                    { 574, "15:28", 6 },
                    { 575, "15:36", 6 },
                    { 576, "15:44", 6 },
                    { 577, "15:52", 6 },
                    { 578, "16:00", 6 },
                    { 579, "16:08", 6 },
                    { 580, "16:16", 6 },
                    { 581, "16:24", 6 },
                    { 582, "16:32", 6 },
                    { 583, "16:40", 6 },
                    { 584, "16:48", 6 },
                    { 585, "16:56", 6 },
                    { 586, "17:04", 6 },
                    { 587, "17:12", 6 },
                    { 588, "17:20", 6 },
                    { 589, "17:28", 6 },
                    { 590, "17:36", 6 },
                    { 591, "17:44", 6 },
                    { 592, "17:52", 6 },
                    { 593, "18:00", 6 },
                    { 594, "18:08", 6 },
                    { 595, "18:16", 6 },
                    { 596, "18:24", 6 },
                    { 597, "18:32", 6 },
                    { 598, "18:40", 6 },
                    { 599, "18:48", 6 },
                    { 600, "18:56", 6 },
                    { 601, "19:04", 6 },
                    { 602, "19:12", 6 },
                    { 603, "19:20", 6 },
                    { 604, "19:28", 6 },
                    { 605, "19:36", 6 },
                    { 606, "19:44", 6 },
                    { 607, "19:52", 6 },
                    { 608, "20:00", 6 },
                    { 609, "20:08", 6 },
                    { 610, "20:16", 6 },
                    { 611, "20:24", 6 },
                    { 612, "20:32", 6 },
                    { 613, "20:40", 6 },
                    { 614, "20:48", 6 },
                    { 615, "20:56", 6 },
                    { 616, "21:04", 6 },
                    { 617, "21:12", 6 },
                    { 618, "21:20", 6 },
                    { 619, "21:28", 6 },
                    { 620, "21:36", 6 },
                    { 621, "21:44", 6 },
                    { 622, "21:52", 6 },
                    { 623, "22:00", 6 },
                    { 624, "06:00", 7 },
                    { 625, "06:18", 7 },
                    { 626, "06:36", 7 },
                    { 627, "06:54", 7 },
                    { 628, "07:12", 7 },
                    { 629, "07:30", 7 },
                    { 630, "07:48", 7 },
                    { 631, "08:06", 7 },
                    { 632, "08:24", 7 },
                    { 633, "08:42", 7 },
                    { 634, "09:00", 7 },
                    { 635, "09:18", 7 },
                    { 636, "09:36", 7 },
                    { 637, "09:54", 7 },
                    { 638, "10:12", 7 },
                    { 639, "10:30", 7 },
                    { 640, "10:48", 7 },
                    { 641, "11:06", 7 },
                    { 642, "11:24", 7 },
                    { 643, "11:42", 7 },
                    { 644, "12:00", 7 },
                    { 645, "12:18", 7 },
                    { 646, "12:36", 7 },
                    { 647, "12:54", 7 },
                    { 648, "13:12", 7 },
                    { 649, "13:30", 7 },
                    { 650, "13:48", 7 },
                    { 651, "14:06", 7 },
                    { 652, "14:24", 7 },
                    { 653, "14:42", 7 },
                    { 654, "15:00", 7 },
                    { 655, "15:18", 7 },
                    { 656, "15:36", 7 },
                    { 657, "15:54", 7 },
                    { 658, "16:12", 7 },
                    { 659, "16:30", 7 },
                    { 660, "16:48", 7 },
                    { 661, "17:06", 7 },
                    { 662, "17:24", 7 },
                    { 663, "17:42", 7 },
                    { 664, "18:00", 7 },
                    { 665, "18:18", 7 },
                    { 666, "18:36", 7 },
                    { 667, "18:54", 7 },
                    { 668, "19:12", 7 },
                    { 669, "19:30", 7 },
                    { 670, "19:48", 7 },
                    { 671, "20:06", 7 },
                    { 672, "20:24", 7 },
                    { 673, "20:42", 7 },
                    { 674, "21:00", 7 },
                    { 675, "21:18", 7 },
                    { 676, "21:36", 7 },
                    { 677, "21:54", 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePlaces_UserId",
                table: "FavoritePlaces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRoutes_TransitRouteId",
                table: "FavoriteRoutes",
                column: "TransitRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRoutes_UserId",
                table: "FavoriteRoutes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteStops_StopId",
                table: "FavoriteStops",
                column: "StopId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteStops_UserId",
                table: "FavoriteStops",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTransitRoute_NotificationsId",
                table: "NotificationTransitRoute",
                column: "NotificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_RouteId",
                table: "RouteStops",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_StopId",
                table: "RouteStops",
                column: "StopId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RouteId",
                table: "Schedules",
                column: "RouteId");
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoritePlaces");

            migrationBuilder.DropTable(
                name: "FavoriteRoutes");

            migrationBuilder.DropTable(
                name: "FavoriteStops");

            migrationBuilder.DropTable(
                name: "NotificationTransitRoute");

            migrationBuilder.DropTable(
                name: "RouteStops");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
