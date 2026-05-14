using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransitApi.Migrations
{
    
    public partial class AddAdminSupport : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsAdmin", "PasswordHash" },
                values: new object[] { false, "$2a$11$5xQAyMyPNpiu7LxpPQeBTu.g6PdN6Z6vxxVYSJimnMTZ903ymqwVm" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "PasswordHash", "Username" },
                values: new object[] { 2, "admin@urbantransit.local", true, "$2a$11$im8qXXOOa5Zz3HPC5ipA0uHbLzhM/rqI.R.KJh2/H79WW0maDBXLu", "admin" });
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");
        }
    }
}
