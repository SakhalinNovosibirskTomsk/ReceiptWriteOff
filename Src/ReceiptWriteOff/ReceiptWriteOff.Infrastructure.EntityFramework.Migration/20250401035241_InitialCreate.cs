#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Migration
{
    /// <inheritdoc />
    public partial class InitialCreate : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Author = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WriteOffReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriteOffReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    InventoryNumber = table.Column<int>(type: "integer", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookInstances_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptFacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookInstanceId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptFacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptFacts_BookInstances_BookInstanceId",
                        column: x => x.BookInstanceId,
                        principalTable: "BookInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WriteOffFacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookInstanceId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    WriteOffReasonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriteOffFacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WriteOffFacts_BookInstances_BookInstanceId",
                        column: x => x.BookInstanceId,
                        principalTable: "BookInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WriteOffFacts_WriteOffReasons_WriteOffReasonId",
                        column: x => x.WriteOffReasonId,
                        principalTable: "WriteOffReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookInstances_BookId",
                table: "BookInstances",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFacts_BookInstanceId",
                table: "ReceiptFacts",
                column: "BookInstanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WriteOffFacts_BookInstanceId",
                table: "WriteOffFacts",
                column: "BookInstanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WriteOffFacts_WriteOffReasonId",
                table: "WriteOffFacts",
                column: "WriteOffReasonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptFacts");

            migrationBuilder.DropTable(
                name: "WriteOffFacts");

            migrationBuilder.DropTable(
                name: "BookInstances");

            migrationBuilder.DropTable(
                name: "WriteOffReasons");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
