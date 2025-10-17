using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VetCareAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FullName = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Species = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartsAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndsAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<int>(type: "int", nullable: false),
                    ChiefComplaint = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiagnosisCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiagnosisText = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Severity = table.Column<int>(type: "int", nullable: true),
                    ClinicId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PetId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visits_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Clinics",
                columns: new[] { "Id", "Address", "City", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Laisvės al. 1", "Kaunas", "ZooVet Kaunas" },
                    { new Guid("11111111-1111-1111-1111-222222222222"), "Gedimino pr. 2", "Vilnius", "VetHelp Vilnius" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), "rokas@example.com", "Rokas Meškauskas" },
                    { new Guid("22222222-2222-2222-2222-333333333333"), "auste@example.com", "Austė Petrauskaitė" }
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "Name", "Species", "UserId" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-111111111111"), "Maksis", "Dog", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("33333333-3333-3333-3333-222222222222"), "Murka", "Cat", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Pūkis", "Rabbit", new Guid("22222222-2222-2222-2222-333333333333") }
                });

            migrationBuilder.InsertData(
                table: "Visits",
                columns: new[] { "Id", "ChiefComplaint", "ClinicId", "DiagnosisCode", "DiagnosisText", "EndsAt", "Notes", "PetId", "Reason", "Severity", "StartsAt", "Status" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-111111111111"), null, new Guid("11111111-1111-1111-1111-111111111111"), null, null, new DateTime(2025, 9, 15, 9, 30, 0, 0, DateTimeKind.Utc), "Vaccination completed.", new Guid("33333333-3333-3333-3333-111111111111"), 1, 0, new DateTime(2025, 9, 15, 9, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("44444444-4444-4444-4444-222222222222"), "Limping on right hind leg", new Guid("11111111-1111-1111-1111-111111111111"), null, null, new DateTime(2025, 10, 12, 9, 0, 0, 0, DateTimeKind.Utc), "Annual check.", new Guid("33333333-3333-3333-3333-111111111111"), 0, 1, new DateTime(2025, 10, 12, 8, 30, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("44444444-4444-4444-4444-333333333333"), "Bad breath", new Guid("11111111-1111-1111-1111-222222222222"), null, null, new DateTime(2025, 10, 20, 14, 30, 0, 0, DateTimeKind.Utc), "Dental check.", new Guid("33333333-3333-3333-3333-222222222222"), 5, 0, new DateTime(2025, 10, 20, 14, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Loss of appetite", new Guid("11111111-1111-1111-1111-222222222222"), null, null, new DateTime(2025, 10, 5, 16, 30, 0, 0, DateTimeKind.Utc), "Owner cancelled day before.", new Guid("33333333-3333-3333-3333-333333333333"), 2, 1, new DateTime(2025, 10, 5, 16, 0, 0, 0, DateTimeKind.Utc), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ClinicId",
                table: "Visits",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_PetId",
                table: "Visits",
                column: "PetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
