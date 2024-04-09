using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateAppProg3.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_fecha_to_property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Property",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Property");
        }
    }
}
