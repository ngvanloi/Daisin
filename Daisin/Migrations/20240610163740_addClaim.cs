using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daisin.Migrations
{
    /// <inheritdoc />
    public partial class addClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserClaims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f1c16f7-6d56-4dad-9761-d03a63b42e87",
                column: "ConcurrencyStamp",
                value: "c3322547-c1b0-413a-8bf3-c812dea72f7f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa3d9336-4414-4fb6-b5da-d12dfc30e2ef",
                column: "ConcurrencyStamp",
                value: "84fedbb2-fa7b-434f-811c-7c744ba5a955");

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "Discriminator", "UserId" },
                values: new object[] { 1, "AdminObserverExpireDate", "06/10/2023", "AppUserClaim", "e137111e-77b7-40f8-9318-099522ba68af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8cc8635c-47c6-4b98-98c4-a26894b18d24",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c4f83d4-4f23-4982-ba8a-c008087e9d7c", "AQAAAAIAAYagAAAAEKvWr4BHVBjW93YAyooCTP0HxTveQH3lcBt9VxW8qHpEA/tyNqIFMW+kKZ69uX/IEg==", "4533ed65-d7aa-45ce-9972-281dea8624d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e137111e-77b7-40f8-9318-099522ba68af",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "470d01b4-2589-4268-a1c4-1ad18b461a61", "AQAAAAIAAYagAAAAEB78KoecVyHML424G5iTK8xF4K/cn0bnEvX/kTW+sw3o2R9wJqFAVP5DbSmzJjFyfQ==", "c3d765a2-8441-49e5-9e26-0088d9c22b30" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserClaims");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f1c16f7-6d56-4dad-9761-d03a63b42e87",
                column: "ConcurrencyStamp",
                value: "1d1730ea-4e3d-461f-baef-bec7c186d65f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa3d9336-4414-4fb6-b5da-d12dfc30e2ef",
                column: "ConcurrencyStamp",
                value: "3efd52ad-4c9a-4a9c-aff0-3764593cf057");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8cc8635c-47c6-4b98-98c4-a26894b18d24",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe9658c1-8faf-408e-afa0-a88c99a89f63", "AQAAAAIAAYagAAAAELLsRrN30Mci7+jedgad8x43S40XvqHZAjWWBivR7p9R8zlxJlbkc1vlTsekwWxOQQ==", "573b7857-0587-4c59-8384-df2efae0c09e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e137111e-77b7-40f8-9318-099522ba68af",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea74bbdf-5de8-40db-a728-78d719346801", "AQAAAAIAAYagAAAAECYHRgyzjjijo5yB1Zc+IUTWrLE61bjempyYeLWCEmJ92m8WdMOjYGUZwWMg9jTrlg==", "b7931142-3452-4780-b4a6-02e9d1d5f87d" });
        }
    }
}
