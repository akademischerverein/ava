using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AV.AvA.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "login_tokens",
                columns: table => new
                {
                    login_token_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    token = table.Column<string>(type: "text", nullable: false),
                    av_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    valid_until = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    used_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_login_tokens", x => x.login_token_id);
                });

            migrationBuilder.CreateTable(
                name: "person_versions",
                columns: table => new
                {
                    person_version_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    av_id = table.Column<int>(type: "integer", nullable: false),
                    person = table.Column<string>(type: "jsonb", nullable: false),
                    comitter_av_id = table.Column<int>(type: "integer", nullable: true),
                    committed_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    commit_message = table.Column<string>(type: "text", nullable: false),
                    software = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_versions", x => x.person_version_id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_login_tokens_av_id",
                table: "login_tokens",
                column: "av_id",
                unique: true,
                filter: "valid_until IS NOT NULL AND used_at IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_login_tokens_token",
                table: "login_tokens",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_person_versions_av_id",
                table: "person_versions",
                column: "av_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "login_tokens");

            migrationBuilder.DropTable(
                name: "person_versions");
        }
    }
}
