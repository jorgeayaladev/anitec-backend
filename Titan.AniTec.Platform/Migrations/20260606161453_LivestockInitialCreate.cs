using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Titan.AniTec.Platform.Migrations
{
    /// <inheritdoc />
    public partial class LivestockInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "breeds",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    species = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breeds", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "animals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    breed_id = table.Column<int>(type: "int", nullable: true),
                    code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    species = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    sex = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    weight = table.Column<double>(type: "double", nullable: true),
                    color = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    notes = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    purchase_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    purchase_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    mother_id = table.Column<int>(type: "int", nullable: true),
                    father_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_animals", x => x.id);
                    table.ForeignKey(
                        name: "fk_animals_animals_father_id",
                        column: x => x.father_id,
                        principalTable: "animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_animals_animals_mother_id",
                        column: x => x.mother_id,
                        principalTable: "animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_animals_breed_breed_id",
                        column: x => x.breed_id,
                        principalTable: "breeds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "births",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    mother_id = table.Column<int>(type: "int", nullable: false),
                    father_id = table.Column<int>(type: "int", nullable: true),
                    birth_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    offspring_count = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_births", x => x.id);
                    table.ForeignKey(
                        name: "fk_births_animals_father_id",
                        column: x => x.father_id,
                        principalTable: "animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_births_animals_mother_id",
                        column: x => x.mother_id,
                        principalTable: "animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "matings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    female_id = table.Column<int>(type: "int", nullable: false),
                    male_id = table.Column<int>(type: "int", nullable: false),
                    mating_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    result = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    notes = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    confirmed_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_matings", x => x.id);
                    table.ForeignKey(
                        name: "fk_matings_animals_female_id",
                        column: x => x.female_id,
                        principalTable: "animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_matings_animals_male_id",
                        column: x => x.male_id,
                        principalTable: "animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "weanings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    farm_id = table.Column<int>(type: "int", nullable: false),
                    calf_id = table.Column<int>(type: "int", nullable: false),
                    mother_id = table.Column<int>(type: "int", nullable: false),
                    weaning_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    weight = table.Column<double>(type: "double", nullable: true),
                    notes = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_weanings", x => x.id);
                    table.ForeignKey(
                        name: "fk_weanings_animals_calf_id",
                        column: x => x.calf_id,
                        principalTable: "animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_weanings_animals_mother_id",
                        column: x => x.mother_id,
                        principalTable: "animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_animals_breed_id",
                table: "animals",
                column: "breed_id");

            migrationBuilder.CreateIndex(
                name: "ix_animals_code",
                table: "animals",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_animals_farm_id",
                table: "animals",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_animals_father_id",
                table: "animals",
                column: "father_id");

            migrationBuilder.CreateIndex(
                name: "ix_animals_mother_id",
                table: "animals",
                column: "mother_id");

            migrationBuilder.CreateIndex(
                name: "ix_births_farm_id",
                table: "births",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_births_father_id",
                table: "births",
                column: "father_id");

            migrationBuilder.CreateIndex(
                name: "ix_births_mother_id",
                table: "births",
                column: "mother_id");

            migrationBuilder.CreateIndex(
                name: "ix_breeds_name_species",
                table: "breeds",
                columns: new[] { "name", "species" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_matings_farm_id",
                table: "matings",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_matings_female_id",
                table: "matings",
                column: "female_id");

            migrationBuilder.CreateIndex(
                name: "ix_matings_male_id",
                table: "matings",
                column: "male_id");

            migrationBuilder.CreateIndex(
                name: "ix_weanings_calf_id",
                table: "weanings",
                column: "calf_id");

            migrationBuilder.CreateIndex(
                name: "ix_weanings_farm_id",
                table: "weanings",
                column: "farm_id");

            migrationBuilder.CreateIndex(
                name: "ix_weanings_mother_id",
                table: "weanings",
                column: "mother_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "births");

            migrationBuilder.DropTable(
                name: "matings");

            migrationBuilder.DropTable(
                name: "weanings");

            migrationBuilder.DropTable(
                name: "animals");

            migrationBuilder.DropTable(
                name: "breeds");
        }
    }
}
