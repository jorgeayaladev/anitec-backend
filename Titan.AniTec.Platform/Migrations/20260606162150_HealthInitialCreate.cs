using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Titan.AniTec.Platform.Migrations
{
    /// <inheritdoc />
    public partial class HealthInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "health_alerts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    animal_id = table.Column<int>(type: "int", nullable: false),
                    alert_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    is_resolved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    resolved_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    resolved_by = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    notes = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_health_alerts", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "treatments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    animal_id = table.Column<int>(type: "int", nullable: false),
                    treatment_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    diagnosis = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    medication_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    dosage = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    administration_route = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    duration_days = table.Column<int>(type: "int", nullable: true),
                    withdrawal_period_days = table.Column<int>(type: "int", nullable: true),
                    treated_by = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    notes = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_treatments", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vaccinations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    animal_id = table.Column<int>(type: "int", nullable: false),
                    vaccine_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    application_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    batch_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    application_route = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    dosage = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    applied_by = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    next_dose_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    notes = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vaccinations", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "veterinary_visits",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    animal_id = table.Column<int>(type: "int", nullable: false),
                    visit_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    vet_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    reason = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    diagnosis = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    recommendations = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    follow_up_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    notes = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_veterinary_visits", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_health_alerts_animal_id",
                table: "health_alerts",
                column: "animal_id");

            migrationBuilder.CreateIndex(
                name: "ix_health_alerts_farm_id",
                table: "health_alerts",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_treatments_animal_id",
                table: "treatments",
                column: "animal_id");

            migrationBuilder.CreateIndex(
                name: "ix_treatments_farm_id",
                table: "treatments",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_vaccinations_animal_id",
                table: "vaccinations",
                column: "animal_id");

            migrationBuilder.CreateIndex(
                name: "ix_vaccinations_farm_id",
                table: "vaccinations",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_veterinary_visits_animal_id",
                table: "veterinary_visits",
                column: "animal_id");

            migrationBuilder.CreateIndex(
                name: "ix_veterinary_visits_farm_id",
                table: "veterinary_visits",
                column: "farm_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "health_alerts");

            migrationBuilder.DropTable(
                name: "treatments");

            migrationBuilder.DropTable(
                name: "vaccinations");

            migrationBuilder.DropTable(
                name: "veterinary_visits");
        }
    }
}
