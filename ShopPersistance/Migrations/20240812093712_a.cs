using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopPersistance.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flowers",
                columns: table => new
                {
                    FlowerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlowerDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlowertPrice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.FlowerId);
                });

            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    GiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiftType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiftDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiftPrice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.GiftId);
                });

            migrationBuilder.CreateTable(
                name: "FlowerGift",
                columns: table => new
                {
                    FlowersFlowerId = table.Column<int>(type: "int", nullable: false),
                    GiftsGiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerGift", x => new { x.FlowersFlowerId, x.GiftsGiftId });
                    table.ForeignKey(
                        name: "FK_FlowerGift_Flowers_FlowersFlowerId",
                        column: x => x.FlowersFlowerId,
                        principalTable: "Flowers",
                        principalColumn: "FlowerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowerGift_Gifts_GiftsGiftId",
                        column: x => x.GiftsGiftId,
                        principalTable: "Gifts",
                        principalColumn: "GiftId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowerGift_GiftsGiftId",
                table: "FlowerGift",
                column: "GiftsGiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowerGift");

            migrationBuilder.DropTable(
                name: "Flowers");

            migrationBuilder.DropTable(
                name: "Gifts");
        }
    }
}
