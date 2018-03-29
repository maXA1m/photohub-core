using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PhotoHub.DAL.Migrations
{
    public partial class IsoForeign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Iso",
                table: "Photos",
                newName: "IsoId");

            migrationBuilder.RenameColumn(
                name: "Exposure",
                table: "Photos",
                newName: "ExposureId");

            migrationBuilder.RenameColumn(
                name: "Aperture",
                table: "Photos",
                newName: "ApertureId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ApertureId",
                table: "Photos",
                column: "ApertureId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ExposureId",
                table: "Photos",
                column: "ExposureId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_IsoId",
                table: "Photos",
                column: "IsoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Apertures_ApertureId",
                table: "Photos",
                column: "ApertureId",
                principalTable: "Apertures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Exposures_ExposureId",
                table: "Photos",
                column: "ExposureId",
                principalTable: "Exposures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Isos_IsoId",
                table: "Photos",
                column: "IsoId",
                principalTable: "Isos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Apertures_ApertureId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Exposures_ExposureId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Isos_IsoId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ApertureId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ExposureId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_IsoId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "IsoId",
                table: "Photos",
                newName: "Iso");

            migrationBuilder.RenameColumn(
                name: "ExposureId",
                table: "Photos",
                newName: "Exposure");

            migrationBuilder.RenameColumn(
                name: "ApertureId",
                table: "Photos",
                newName: "Aperture");
        }
    }
}
