using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMail.Infra.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class CriarEnvioESchedulamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracoesDeEmail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Remetente = table.Column<string>(type: "TEXT", maxLength: 254, nullable: false),
                    SenhaProtegida = table.Column<string>(type: "TEXT", nullable: false),
                    ServidorSmtp = table.Column<string>(type: "TEXT", maxLength: 253, nullable: false),
                    PortaSmtp = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesDeEmail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DMails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Destinatario = table.Column<string>(type: "TEXT", maxLength: 254, nullable: false),
                    Assunto = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Mensagem = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    DataAgendadaUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CriadoEmUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Recorrencia = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ErroDeEnvio = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DMails_Status_DataAgendadaUtc",
                table: "DMails",
                columns: new[] { "Status", "DataAgendadaUtc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracoesDeEmail");

            migrationBuilder.DropTable(
                name: "DMails");
        }
    }
}
