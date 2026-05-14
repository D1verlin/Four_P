using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransitApi.Migrations
{
    
    public partial class FixedHashes : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$fyHxJtrVmySwWBdzgeXmVuKyFDRxpnmD3Iy9LipJdsqDVL0aqwuzm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$t7batDXMOA8Y5X76aq0wguUN/v64mtxhbXaUSU0Mt8a/gjojYdcv2");
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$5xQAyMyPNpiu7LxpPQeBTu.g6PdN6Z6vxxVYSJimnMTZ903ymqwVm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$im8qXXOOa5Zz3HPC5ipA0uHbLzhM/rqI.R.KJh2/H79WW0maDBXLu");
        }
    }
}
