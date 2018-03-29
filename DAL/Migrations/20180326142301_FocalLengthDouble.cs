using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PhotoHub.DAL.Migrations
{
    public partial class FocalLengthDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "FocalLength",
                table: "Photos",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FocalLength",
                table: "Photos",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
