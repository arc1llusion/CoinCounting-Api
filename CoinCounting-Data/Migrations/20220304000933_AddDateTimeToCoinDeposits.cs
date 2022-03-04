using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinCounting_Data.Migrations
{
    public partial class AddDateTimeToCoinDeposits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateDesposited",
                table: "CoinDeposits",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDesposited",
                table: "CoinDeposits");
        }
    }
}
