using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Titan.AniTec.Platform.Migrations
{
    /// <inheritdoc />
    public partial class DeviceInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "device_alerts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    device_id = table.Column<int>(type: "int", nullable: false),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    alert_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    is_resolved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    resolved_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_device_alerts", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "device_assignments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    device_id = table.Column<int>(type: "int", nullable: false),
                    animal_id = table.Column<int>(type: "int", nullable: false),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    assigned_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    unassigned_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_device_assignments", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "device_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    category = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    specifications = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
                    metrics = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_device_types", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "farm_devices",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    device_type_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    serial_number = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    firmware_version = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    last_ping_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    battery_level = table.Column<double>(type: "double", nullable: true),
                    signal_strength = table.Column<double>(type: "double", nullable: true),
                    last_reading_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    current_animal_id = table.Column<int>(type: "int", nullable: true),
                    current_location_id = table.Column<int>(type: "int", nullable: true),
                    configuration = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_farm_devices", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_device_alerts_farm_id",
                table: "device_alerts",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_device_assignments_animal_id",
                table: "device_assignments",
                column: "animal_id");

            migrationBuilder.CreateIndex(
                name: "ix_device_assignments_device_id",
                table: "device_assignments",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "ix_device_assignments_farm_id",
                table: "device_assignments",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_device_types_name",
                table: "device_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_farm_devices_farm_id",
                table: "farm_devices",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_farm_devices_serial_number",
                table: "farm_devices",
                column: "serial_number");

            migrationBuilder.CreateIndex(
                name: "ix_farm_devices_status",
                table: "farm_devices",
                column: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "device_alerts");

            migrationBuilder.DropTable(
                name: "device_assignments");

            migrationBuilder.DropTable(
                name: "device_types");

            migrationBuilder.DropTable(
                name: "farm_devices");
        }
    }
}
