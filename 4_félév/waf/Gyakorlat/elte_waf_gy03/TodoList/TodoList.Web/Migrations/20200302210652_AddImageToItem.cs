using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Web.Migrations
{
    public partial class AddImageToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Lists",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Items",
                newName: "Id");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lists",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Items",
                newName: "ID");
        }
    }
}
