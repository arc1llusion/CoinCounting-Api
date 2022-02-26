using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinCounting_Data.Migrations
{
    public partial class CoinDeposits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoinDeposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Pennies = table.Column<int>(type: "int", nullable: false),
                    Nickels = table.Column<int>(type: "int", nullable: false),
                    Dimes = table.Column<int>(type: "int", nullable: false),
                    Quarters = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinDeposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinDeposits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoinDeposits_UserId",
                table: "CoinDeposits",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinDeposits");
        }
    }
}
