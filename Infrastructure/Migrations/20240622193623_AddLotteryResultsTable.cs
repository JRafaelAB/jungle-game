using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLotteryResultsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LotteryResults",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumbersPerLottery = table.Column<long>(type: "bigint", nullable: false),
                    Lottery1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lottery2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lottery3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lottery4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lottery5 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotteryResults");
        }
    }
}
