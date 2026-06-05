using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Titan.AniTec.Platform.Migrations
{
    /// <inheritdoc />
    public partial class ProfileInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clinics",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    clinic_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    address = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    state = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    postal_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    latitude = table.Column<double>(type: "double", nullable: true),
                    longitude = table.Column<double>(type: "double", nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clinics", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "farms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    farm_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    farm_size = table.Column<double>(type: "double", nullable: true),
                    address = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    city = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    state = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    postal_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    latitude = table.Column<double>(type: "double", nullable: true),
                    longitude = table.Column<double>(type: "double", nullable: true),
                    description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_farms", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    message = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    is_read = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    read_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "professional_licenses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    clinic_id = table.Column<int>(type: "int", nullable: false),
                    license_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    issuing_authority = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    issue_date = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    expiry_date = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    is_verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_professional_licenses", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_preferences",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    language = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    theme = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    notifications_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    email_notifications = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    push_notifications = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sms_notifications = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_preferences", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_profiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    first_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    avatar_url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    bio = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_profiles", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    role = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    password_hash = table.Column<string>(type: "longtext", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    email_verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    email_verified_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    last_login_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clinic_availabilities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    clinic_id = table.Column<int>(type: "int", nullable: false),
                    day_of_week = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    is_available = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clinic_availabilities", x => x.id);
                    table.ForeignKey(
                        name: "fk_clinic_availabilities_clinics_clinic_id",
                        column: x => x.clinic_id,
                        principalTable: "clinics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clinic_schedules",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    clinic_id = table.Column<int>(type: "int", nullable: false),
                    day_of_week = table.Column<int>(type: "int", nullable: false),
                    open_time = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    close_time = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    is_available = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clinic_schedules", x => x.id);
                    table.ForeignKey(
                        name: "fk_clinic_schedules_clinics_clinic_id",
                        column: x => x.clinic_id,
                        principalTable: "clinics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "service_area_locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    clinic_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    address = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    latitude = table.Column<double>(type: "double", nullable: true),
                    longitude = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_service_area_locations", x => x.id);
                    table.ForeignKey(
                        name: "fk_service_area_locations_clinics_clinic_id",
                        column: x => x.clinic_id,
                        principalTable: "clinics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "specialties",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    clinic_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specialties", x => x.id);
                    table.ForeignKey(
                        name: "fk_specialties_clinics_clinic_id",
                        column: x => x.clinic_id,
                        principalTable: "clinics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "farm_certifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    issuing_authority = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    issue_date = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    expiry_date = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    certification_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_farm_certifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_farm_certifications_farms_farm_id",
                        column: x => x.farm_id,
                        principalTable: "farms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "farm_locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    area = table.Column<double>(type: "double", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_farm_locations", x => x.id);
                    table.ForeignKey(
                        name: "fk_farm_locations_farms_farm_id",
                        column: x => x.farm_id,
                        principalTable: "farms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "farm_staffs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    full_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    role = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_farm_staffs", x => x.id);
                    table.ForeignKey(
                        name: "fk_farm_staffs_farms_farm_id",
                        column: x => x.farm_id,
                        principalTable: "farms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_clinic_availabilities_clinic_id",
                table: "clinic_availabilities",
                column: "clinic_id");

            migrationBuilder.CreateIndex(
                name: "ix_clinic_schedules_clinic_id",
                table: "clinic_schedules",
                column: "clinic_id");

            migrationBuilder.CreateIndex(
                name: "ix_clinics_user_id",
                table: "clinics",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_farm_certifications_farm_id",
                table: "farm_certifications",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_farm_locations_farm_id",
                table: "farm_locations",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_farm_staffs_farm_id",
                table: "farm_staffs",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_farms_user_id",
                table: "farms",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_notifications_user_id",
                table: "notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_professional_licenses_license_number",
                table: "professional_licenses",
                column: "license_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_service_area_locations_clinic_id",
                table: "service_area_locations",
                column: "clinic_id");

            migrationBuilder.CreateIndex(
                name: "ix_specialties_clinic_id",
                table: "specialties",
                column: "clinic_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_preferences_user_id",
                table: "user_preferences",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_profiles_user_id",
                table: "user_profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clinic_availabilities");

            migrationBuilder.DropTable(
                name: "clinic_schedules");

            migrationBuilder.DropTable(
                name: "farm_certifications");

            migrationBuilder.DropTable(
                name: "farm_locations");

            migrationBuilder.DropTable(
                name: "farm_staffs");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "professional_licenses");

            migrationBuilder.DropTable(
                name: "service_area_locations");

            migrationBuilder.DropTable(
                name: "specialties");

            migrationBuilder.DropTable(
                name: "user_preferences");

            migrationBuilder.DropTable(
                name: "user_profiles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "farms");

            migrationBuilder.DropTable(
                name: "clinics");
        }
    }
}
