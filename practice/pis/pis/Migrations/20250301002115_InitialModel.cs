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
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                    { new Guid("020b9967-9099-4e7a-8592-f4c30eb6787d"), "D113", "Azithromycin Tablet, Film-coated 500mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4953) },
                    { new Guid("24b4da21-9ae6-4f07-8ef2-f45468ea8075"), "D112", "Miltefosine Capsules, hard 50mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4952) },
                    { new Guid("2c1b4f36-b224-49d0-8759-bff5b24e2e8c"), "D111", "Albendazole Tablets, Chewable 400mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4950) },
                    { new Guid("332814e6-d15e-4e2d-8f8a-f4e8c330e409"), "D105", "Ivermectin Tablet 3mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4914) },
                    { new Guid("3e1762e2-3c53-46aa-ab0d-4b9902befb5b"), "D107", "Praziquantel Tablet, Film-coated 600mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4919) },
                    { new Guid("44a14375-899b-42af-9a3b-76b9e805c0de"), "D101", "Diethylcarbamazine (citrate) Tablet 100mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4780) },
                    { new Guid("88baedfc-2558-4f4f-a25c-ebba4ac8c09f"), "D108", "Albendazole Tablets, Chewable 400mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4946) },
                    { new Guid("90a46328-b1f1-4654-883f-2af18ace836c"), "D104", "Mebendazole Tablets, Chewable 500mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4913) },
                    { new Guid("b691d1eb-2020-4f15-812b-7ab6c2af24d4"), "D102", "Praziquantel Tablet, Film-coated 600mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4811) },
                    { new Guid("c56b4f97-48e8-42f5-bf1b-4167997dbc70"), "D109", "Praziquantel Tablet, Film-coated 600mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4948) },
                    { new Guid("e26c0632-8f7a-4bbd-94db-f1ed3fa123ad"), "D114", "Levonorgestrel Tablet, coated 30mcg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4954) },
                    { new Guid("ea19e9f9-b7e0-41f1-a624-1084a959e2ff"), "D103", "Albendazole Tablet 400mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4911) },
                    { new Guid("eac51b5a-a01c-4775-96fb-615b8b2cd4bb"), "D110", "Albendazole Tablets, Chewable 400mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4949) },
                    { new Guid("f1b1e424-6767-4c4d-80fa-a4ecca13a8be"), "D106", "Ivermectin Tablet 3mg", 500.0, new DateTime(2025, 3, 1, 3, 21, 15, 363, DateTimeKind.Local).AddTicks(4915) }
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
