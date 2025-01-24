using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLibraryMVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MyLibraryMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Books_BookId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookInfo_BookInfoId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_PublishingInfo_PublishingInfoId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_PublishingInfo_Cities_CityOfPublishingId",
                table: "PublishingInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PublishingInfo_PublishingHouses_PublishingHouseId",
                table: "PublishingInfo");

            migrationBuilder.DropIndex(
                name: "IX_Authors_BookId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Authors");

            migrationBuilder.AlterColumn<int>(
                name: "YearOfPublication",
                table: "PublishingInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PublishingHouseId",
                table: "PublishingInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishingDate",
                table: "PublishingInfo",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfPublishing",
                table: "PublishingInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityOfPublishingId",
                table: "PublishingInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PublishingInfoId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookInfoId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Authors",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "BooksAuthor",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksAuthor", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BooksAuthor_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksAuthor_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksAuthor_AuthorId",
                table: "BooksAuthor",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookInfo_BookInfoId",
                table: "Books",
                column: "BookInfoId",
                principalTable: "BookInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_PublishingInfo_PublishingInfoId",
                table: "Books",
                column: "PublishingInfoId",
                principalTable: "PublishingInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublishingInfo_Cities_CityOfPublishingId",
                table: "PublishingInfo",
                column: "CityOfPublishingId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublishingInfo_PublishingHouses_PublishingHouseId",
                table: "PublishingInfo",
                column: "PublishingHouseId",
                principalTable: "PublishingHouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookInfo_BookInfoId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_PublishingInfo_PublishingInfoId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_PublishingInfo_Cities_CityOfPublishingId",
                table: "PublishingInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PublishingInfo_PublishingHouses_PublishingHouseId",
                table: "PublishingInfo");

            migrationBuilder.DropTable(
                name: "BooksAuthor");

            migrationBuilder.AlterColumn<int>(
                name: "YearOfPublication",
                table: "PublishingInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PublishingHouseId",
                table: "PublishingInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishingDate",
                table: "PublishingInfo",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfPublishing",
                table: "PublishingInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityOfPublishingId",
                table: "PublishingInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PublishingInfoId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookInfoId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Authors",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BookId",
                table: "Authors",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Books_BookId",
                table: "Authors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookInfo_BookInfoId",
                table: "Books",
                column: "BookInfoId",
                principalTable: "BookInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_PublishingInfo_PublishingInfoId",
                table: "Books",
                column: "PublishingInfoId",
                principalTable: "PublishingInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublishingInfo_Cities_CityOfPublishingId",
                table: "PublishingInfo",
                column: "CityOfPublishingId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PublishingInfo_PublishingHouses_PublishingHouseId",
                table: "PublishingInfo",
                column: "PublishingHouseId",
                principalTable: "PublishingHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
