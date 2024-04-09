using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateAppProg3.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeProperty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeSale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeSale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Upgrade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upgrade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberRooms = table.Column<int>(type: "int", nullable: false),
                    SizeInMeters = table.Column<int>(type: "int", nullable: false),
                    Bathrooms = table.Column<int>(type: "int", nullable: false),
                    IdTypeProperty = table.Column<int>(type: "int", nullable: false),
                    IdTypeSale = table.Column<int>(type: "int", nullable: false),
                    UrlImage1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlImage2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlImage3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlImage4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Property_TypeProperty_IdTypeProperty",
                        column: x => x.IdTypeProperty,
                        principalTable: "TypeProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Property_TypeSale_IdTypeSale",
                        column: x => x.IdTypeSale,
                        principalTable: "TypeSale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyFav",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdProperty = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFav", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyFav_Property_IdProperty",
                        column: x => x.IdProperty,
                        principalTable: "Property",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UpgradesProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProperty = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUpgrade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpgradesProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpgradesProperty_Property_IdProperty",
                        column: x => x.IdProperty,
                        principalTable: "Property",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpgradesProperty_Upgrade_IdUpgrade",
                        column: x => x.IdUpgrade,
                        principalTable: "Upgrade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Property_IdTypeProperty",
                table: "Property",
                column: "IdTypeProperty");

            migrationBuilder.CreateIndex(
                name: "IX_Property_IdTypeSale",
                table: "Property",
                column: "IdTypeSale");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFav_IdProperty",
                table: "PropertyFav",
                column: "IdProperty");

            migrationBuilder.CreateIndex(
                name: "IX_UpgradesProperty_IdProperty",
                table: "UpgradesProperty",
                column: "IdProperty");

            migrationBuilder.CreateIndex(
                name: "IX_UpgradesProperty_IdUpgrade",
                table: "UpgradesProperty",
                column: "IdUpgrade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyFav");

            migrationBuilder.DropTable(
                name: "UpgradesProperty");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "Upgrade");

            migrationBuilder.DropTable(
                name: "TypeProperty");

            migrationBuilder.DropTable(
                name: "TypeSale");
        }
    }
}
