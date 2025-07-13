using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekt.Migrations
{
    /// <inheritdoc />
    public partial class AddKlientIdKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wypozyczenia_Klienci_KlientId1",
                table: "Wypozyczenia");

            migrationBuilder.DropIndex(
                name: "IX_Wypozyczenia_KlientId1",
                table: "Wypozyczenia");

            migrationBuilder.DropColumn(
                name: "KlientId1",
                table: "Wypozyczenia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KlientId1",
                table: "Wypozyczenia",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wypozyczenia_KlientId1",
                table: "Wypozyczenia",
                column: "KlientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Wypozyczenia_Klienci_KlientId1",
                table: "Wypozyczenia",
                column: "KlientId1",
                principalTable: "Klienci",
                principalColumn: "Id");
        }
    }
}
