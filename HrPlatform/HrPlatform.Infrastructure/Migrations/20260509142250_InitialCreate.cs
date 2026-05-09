using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HrPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateSkills",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSkills", x => new { x.CandidateId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CandidateSkills_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "ContactNumber", "DateOfBirth", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "0601234567", new DateTime(1995, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ana.petrovic@email.com", "Ana Petrović" },
                    { 2, "0637654321", new DateTime(1992, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "marko.nikolic@email.com", "Marko Nikolić" },
                    { 3, "0651112233", new DateTime(1998, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "jelena.jovic@email.com", "Jelena Jović" },
                    { 4, "0623334455", new DateTime(1990, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "stefan.djordjevic@email.com", "Stefan Đorđević" },
                    { 5, "0615556677", new DateTime(1997, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "milica.stojanovic@email.com", "Milica Stojanović" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "C# Programming" },
                    { 2, "Java Programming" },
                    { 3, "Database Design" },
                    { 4, "React" },
                    { 5, "English" },
                    { 6, "German" },
                    { 7, "Russian" },
                    { 8, "Project Management" }
                });

            migrationBuilder.InsertData(
                table: "CandidateSkills",
                columns: new[] { "CandidateId", "SkillId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 1, 3, 0 },
                    { 1, 5, 0 },
                    { 2, 2, 0 },
                    { 2, 3, 0 },
                    { 3, 1, 0 },
                    { 3, 4, 0 },
                    { 3, 5, 0 },
                    { 4, 5, 0 },
                    { 4, 6, 0 },
                    { 4, 8, 0 },
                    { 5, 1, 0 },
                    { 5, 4, 0 },
                    { 5, 7, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Email",
                table: "Candidates",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_SkillId",
                table: "CandidateSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                table: "Skills",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateSkills");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
