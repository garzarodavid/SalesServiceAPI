using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesServiceAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Inserir uma Filial
            migrationBuilder.InsertData(
                table: "Filiais",
                columns: new[] { "Id", "Nome", "Endereco" },
                values: new object[] { 1, "Filial Central", "Avenida Principal, 123" }
            );

            // Inserir um Cliente
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Nome", "Email" },
                values: new object[] { 1, "João Silva", "joao.silva@example.com" }
            );

            // Inserir um Produto
            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Nome", "Preco" },
                values: new object[] { 1, "Produto A", 50.0m }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            // Remover os dados inseridos na migração
            migrationBuilder.DeleteData(
                table: "Filiais",
                keyColumn: "Id",
                keyValue: 1
            );

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1
            );

            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "Id",
                keyValue: 1
            );
        }
    }
}
