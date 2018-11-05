using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HOTCAPILibrary.Migrations
{
    public partial class InititalV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureFile",
                table: "Events");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "PictureFile",
                table: "Events",
                nullable: true);
        }
    }
}
