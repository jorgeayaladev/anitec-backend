using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Titan.AniTec.Platform.Migrations
{
    /// <inheritdoc />
    public partial class TelemetryInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "telemetry_alerts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    device_id = table.Column<int>(type: "int", nullable: false),
                    animal_id = table.Column<int>(type: "int", nullable: true),
                    alert_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    severity = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    message = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    metric_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    threshold_value = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    actual_value = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    is_acknowledged = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    acknowledged_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    acknowledged_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_telemetry_alerts", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "telemetry_readings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    device_id = table.Column<int>(type: "int", nullable: true),
                    animal_id = table.Column<int>(type: "int", nullable: true),
                    metric_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    numeric_value = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    string_value = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    unit = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    recorded_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    metadata = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_telemetry_readings", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "telemetry_thresholds",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    device_id = table.Column<int>(type: "int", nullable: false),
                    metric_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    min_value = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    max_value = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    is_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_telemetry_thresholds", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_telemetry_alerts_animal_id",
                table: "telemetry_alerts",
                column: "animal_id");

            migrationBuilder.CreateIndex(
                name: "ix_telemetry_alerts_device_id",
                table: "telemetry_alerts",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "ix_telemetry_alerts_farm_id",
                table: "telemetry_alerts",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_telemetry_readings_animal_id",
                table: "telemetry_readings",
                column: "animal_id");

            migrationBuilder.CreateIndex(
                name: "ix_telemetry_readings_device_id",
                table: "telemetry_readings",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "ix_telemetry_readings_farm_id",
                table: "telemetry_readings",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_telemetry_readings_metric_type",
                table: "telemetry_readings",
                column: "metric_type");

            migrationBuilder.CreateIndex(
                name: "ix_telemetry_readings_recorded_at",
                table: "telemetry_readings",
                column: "recorded_at");

            migrationBuilder.CreateIndex(
                name: "ix_telemetry_thresholds_device_id",
                table: "telemetry_thresholds",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "ix_telemetry_thresholds_device_id_metric_type",
                table: "telemetry_thresholds",
                columns: new[] { "device_id", "metric_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_telemetry_thresholds_farm_id",
                table: "telemetry_thresholds",
                column: "farm_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "telemetry_alerts");

            migrationBuilder.DropTable(
                name: "telemetry_readings");

            migrationBuilder.DropTable(
                name: "telemetry_thresholds");
        }
    }
}
