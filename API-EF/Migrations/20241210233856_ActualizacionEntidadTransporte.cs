using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BalearesChallenge.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionEntidadTransporte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTransporte",
                table: "Contacto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transporte",
                columns: table => new
                {
                    IdTransporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoTransporte = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transporte", x => x.IdTransporte);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacto_IdTransporte",
                table: "Contacto",
                column: "IdTransporte",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacto_Transporte_IdTransporte",
                table: "Contacto",
                column: "IdTransporte",
                principalTable: "Transporte",
                principalColumn: "IdTransporte",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacto_Transporte_IdTransporte",
                table: "Contacto");

            migrationBuilder.DropTable(
                name: "Transporte");

            migrationBuilder.DropIndex(
                name: "IX_Contacto_IdTransporte",
                table: "Contacto");

            migrationBuilder.DropColumn(
                name: "IdTransporte",
                table: "Contacto");
        }
    }
}
