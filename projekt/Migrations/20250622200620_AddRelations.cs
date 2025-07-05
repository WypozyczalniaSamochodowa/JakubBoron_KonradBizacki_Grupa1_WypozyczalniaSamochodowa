using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projekt.Migrations
{
    /// <inheritdoc />
    public partial class AddRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klienci_Samochody_CarId",
                table: "Klienci");

            migrationBuilder.DropForeignKey(
                name: "FK_Wypozyczenia_Klienci_KlientId",
                table: "Wypozyczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_Wypozyczenia_Samochody_CarId",
                table: "Wypozyczenia");

            migrationBuilder.DropIndex(
                name: "IX_Klienci_CarId",
                table: "Klienci");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Klienci");

            migrationBuilder.DropColumn(
                name: "Cena",
                table: "Klienci");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Klienci");

            migrationBuilder.DropColumn(
                name: "Dni",
                table: "Klienci");

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
                name: "FK_Wypozyczenia_Klienci_KlientId",
                table: "Wypozyczenia",
                column: "KlientId",
                principalTable: "Klienci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wypozyczenia_Klienci_KlientId1",
                table: "Wypozyczenia",
                column: "KlientId1",
                principalTable: "Klienci",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wypozyczenia_Samochody_CarId",
                table: "Wypozyczenia",
                column: "CarId",
                principalTable: "Samochody",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wypozyczenia_Klienci_KlientId",
                table: "Wypozyczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_Wypozyczenia_Klienci_KlientId1",
                table: "Wypozyczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_Wypozyczenia_Samochody_CarId",
                table: "Wypozyczenia");

            migrationBuilder.DropIndex(
                name: "IX_Wypozyczenia_KlientId1",
                table: "Wypozyczenia");

            migrationBuilder.DropColumn(
                name: "KlientId1",
                table: "Wypozyczenia");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Klienci",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Cena",
                table: "Klienci",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Klienci",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Dni",
                table: "Klienci",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Klienci_CarId",
                table: "Klienci",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Klienci_Samochody_CarId",
                table: "Klienci",
                column: "CarId",
                principalTable: "Samochody",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wypozyczenia_Klienci_KlientId",
                table: "Wypozyczenia",
                column: "KlientId",
                principalTable: "Klienci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wypozyczenia_Samochody_CarId",
                table: "Wypozyczenia",
                column: "CarId",
                principalTable: "Samochody",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
