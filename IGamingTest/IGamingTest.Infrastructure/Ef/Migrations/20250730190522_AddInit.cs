using IGamingTest.Core.Entities.Common;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IGamingTest.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class AddInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Geos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Geo = table.Column<Geo>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meteorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameType = table.Column<string>(type: "text", nullable: true),
                    RecClass = table.Column<string>(type: "text", nullable: true),
                    Mass = table.Column<double>(type: "double precision", nullable: true),
                    Fall = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: true),
                    RecLat = table.Column<string>(type: "text", nullable: true),
                    RecLong = table.Column<string>(type: "text", nullable: true),
                    GeolocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meteorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meteorites_Geos_GeolocationId",
                        column: x => x.GeolocationId,
                        principalTable: "Geos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meteorites_GeolocationId",
                table: "Meteorites",
                column: "GeolocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Meteorites_Name",
                table: "Meteorites",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Meteorites_RecClass",
                table: "Meteorites",
                column: "RecClass");

            migrationBuilder.CreateIndex(
                name: "IX_Meteorites_Year",
                table: "Meteorites",
                column: "Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meteorites");

            migrationBuilder.DropTable(
                name: "Geos");
        }
    }
}
