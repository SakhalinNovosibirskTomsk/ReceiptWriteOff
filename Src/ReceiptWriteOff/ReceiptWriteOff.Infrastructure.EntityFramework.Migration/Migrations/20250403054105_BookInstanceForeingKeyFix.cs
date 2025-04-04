using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceiptWriteOff.Infrastructure.EntityFramework.Migration.Migrations
{
    /// <inheritdoc />
    public partial class BookInstanceForeingKeyFix : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceipdFactId",
                table: "BookInstances",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceipdFactId",
                table: "BookInstances");
        }
    }
}
