using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMA.DB.Migrations.SQLServer
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "商品のID"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "商品の名前")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.product_id);
                },
                comment: "商品のテーブル");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
