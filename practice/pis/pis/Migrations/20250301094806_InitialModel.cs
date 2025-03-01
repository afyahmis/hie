using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pis.Migrations
{
    /// <inheritdoc />
    public partial class InitialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RefId = table.Column<string>(type: "TEXT", nullable: false),
                    RefNo = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Sex = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    QuantityInStock = table.Column<double>(type: "REAL", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RefId = table.Column<string>(type: "TEXT", nullable: false),
                    PrescriptionDrugCode = table.Column<string>(type: "TEXT", nullable: false),
                    PrescriptionDrug = table.Column<string>(type: "TEXT", nullable: false),
                    PrescriptionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DispenseDrugId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DispenseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Drugs",
                columns: new[] { "Id", "Code", "Name", "QuantityInStock", "Updated" },
                values: new object[,]
                {
                    { new Guid("04f73242-74a0-492d-b24b-63cbd788f086"), "D112", "Miltefosine Capsules, hard 50mg", 500.0, null },
                    { new Guid("1725fe0a-4c0b-4fe1-9395-fd1c05dfa750"), "D102", "Praziquantel Tablet, Film-coated 600mg", 500.0, null },
                    { new Guid("2aee5d6b-7777-4927-974e-c9a88bfee23b"), "D107", "Praziquantel Tablet, Film-coated 600mg", 500.0, null },
                    { new Guid("45d5d579-de45-4dcb-b9e5-460f267d7085"), "D113", "Azithromycin Tablet, Film-coated 500mg", 500.0, null },
                    { new Guid("4a213286-9830-48ec-b6ed-610f341f451a"), "D101", "Diethylcarbamazine (citrate) Tablet 100mg", 500.0, null },
                    { new Guid("688e78ac-afef-490d-9281-7ebe59d18aad"), "D108", "Albendazole Tablets, Chewable 400mg", 500.0, null },
                    { new Guid("69941033-ce0b-4cc5-af2d-1b64b1e61f4a"), "D109", "Praziquantel Tablet, Film-coated 600mg", 500.0, null },
                    { new Guid("9a1d53a2-1ca6-4d50-b0b9-f2304f8383e7"), "D104", "Mebendazole Tablets, Chewable 500mg", 500.0, null },
                    { new Guid("b2ffceed-7dac-4bb2-8051-f8ad3e65c462"), "D106", "Ivermectin Tablet 6mg", 500.0, null },
                    { new Guid("c6e1e45f-80d2-4f86-aa92-05df7b0333d1"), "D110", "Albendazole Tablets, Chewable 200mg", 500.0, null },
                    { new Guid("cdfec3c5-b141-4939-a101-5a240810e0c5"), "D114", "Levonorgestrel Tablet, coated 30mcg", 500.0, null },
                    { new Guid("e8725ab5-b927-4ae6-8b02-0a5c4c799a5e"), "D103", "Albendazole Tablet 400mg", 500.0, null },
                    { new Guid("e8f09582-31f1-4b60-9d46-ff045b0665ba"), "D111", "Albendazole Tablets, Chewable 100mg", 500.0, null },
                    { new Guid("f345a67e-5efb-4207-ae2b-fa2ef97520fd"), "D105", "Ivermectin Tablet 3mg", 500.0, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ClientId",
                table: "Requests",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
