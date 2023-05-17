using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntregaSegura.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CONDOMINIOS",
                columns: table => new
                {
                    CND_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave primária do condomínio"),
                    CND_QTD_UNIDADES = table.Column<int>(type: "int", nullable: false, comment: "Quantidade de unidades do condomínio"),
                    CND_QTD_BLOCOS = table.Column<int>(type: "int", nullable: false, comment: "Quantidade de blocos do condomínio"),
                    CND_QTD_ANDARES = table.Column<int>(type: "int", nullable: false, comment: "Quantidade de andares do condomínio"),
                    CND_NOME = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Nome do condomínio"),
                    CND_CNPJ = table.Column<string>(type: "varchar(14)", nullable: false, comment: "CNPJ do condomínio"),
                    CND_TELEFONE = table.Column<string>(type: "varchar(11)", nullable: false, comment: "Telefone do condomínio"),
                    CND_EMAIL = table.Column<string>(type: "varchar(100)", nullable: false, comment: "E-mail do condomínio"),
                    CND_LOGRADOURO = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Logradouro do endereço do condomínio"),
                    CND_NUMERO = table.Column<string>(type: "varchar(10)", nullable: false, comment: "Número do endereço do condomínio"),
                    CND_COMPLEMENTO = table.Column<string>(type: "varchar(50)", nullable: true, comment: "Complemento do endereço do condomínio"),
                    CND_CEP = table.Column<string>(type: "varchar(8)", nullable: false, comment: "CEP do endereço do condomínio"),
                    CND_BAIRRO = table.Column<string>(type: "varchar(50)", nullable: false, comment: "Bairro do endereço do condomínio"),
                    CND_CIDADE = table.Column<string>(type: "varchar(50)", nullable: false, comment: "Cidade do endereço do condomínio"),
                    CND_ESTADO = table.Column<string>(type: "varchar(2)", nullable: false, comment: "Estado do endereço do condomínio"),
                    CND_EXCLUIDO = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Flag de exclusão do condomínio"),
                    CND_DATA_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação do condomínio"),
                    CND_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização do condomínio"),
                    CND_DATA_EXCLUSAO = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Data da exclusão do condomínio")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONDOMINIOS", x => x.CND_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_TRANSPORTADORAS",
                columns: table => new
                {
                    TRA_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave primária da transportadora"),
                    TRA_NOME = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Nome da transportadora"),
                    TRA_CNPJ = table.Column<string>(type: "varchar(14)", nullable: true, comment: "CNPJ da transportadora"),
                    TRA_TELEFONE = table.Column<string>(type: "varchar(11)", nullable: true, comment: "Telefone da transportadora"),
                    TRA_EMAIL = table.Column<string>(type: "varchar(100)", nullable: true, comment: "E-mail da transportadora"),
                    TRA_EXCLUIDO = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Flag de exclusão da transportadora"),
                    TRA_DATA_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação da transportadora"),
                    TRA_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização da transportadora"),
                    TRA_DATA_EXCLUSAO = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Data da exclusão da transportadora")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSPORTADORAS", x => x.TRA_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_FUNCIONARIOS",
                columns: table => new
                {
                    FUN_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave primária do funcionário"),
                    FUN_NOME = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Nome do funcionário"),
                    FUN_CPF = table.Column<string>(type: "varchar(11)", nullable: false, comment: "CPF do funcionário"),
                    FUN_EMAIL = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Email do funcionário"),
                    FUN_TELEFONE = table.Column<string>(type: "varchar(11)", nullable: false, comment: "Telefone do funcionário"),
                    FUN_CARGO = table.Column<int>(type: "int", nullable: false, comment: "Cargo do funcionário"),
                    FUN_DATA_ADMISSAO = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Data de admissão do funcionário"),
                    FUN_DATA_DEMISSAO = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Data de demissão do funcionário"),
                    FUN_EXCLUIDO = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Indica se o funcionário foi excluído"),
                    FUN_DATA_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação do funcionário"),
                    FUN_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização do funcionário"),
                    FUN_DATA_EXCLUSAO = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Data da exclusão do funcionário"),
                    FUN_CONDOMINIO_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave estrangeira do condomínio")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIOS", x => x.FUN_ID);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIO_CONDOMINIO",
                        column: x => x.FUN_CONDOMINIO_ID,
                        principalTable: "TB_CONDOMINIOS",
                        principalColumn: "CND_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_UNIDADES",
                columns: table => new
                {
                    UND_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave primária da unidade"),
                    CON_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave estrangeira do condomínio"),
                    UND_NUMERO = table.Column<string>(type: "varchar(10)", nullable: false, comment: "Número da unidade"),
                    UND_BLOCO = table.Column<string>(type: "varchar(10)", nullable: true, comment: "Bloco da unidade"),
                    UND_EXCLUIDO = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Indica se a unidade foi excluída"),
                    UND_DATA_CRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação da unidade"),
                    UND_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização da unidade"),
                    UND_DATA_EXCLUSAO = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Data da exclusão da unidade")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNIDADES", x => x.UND_ID);
                    table.ForeignKey(
                        name: "FK_UNIDADES_CONDOMINIOS",
                        column: x => x.CON_ID,
                        principalTable: "TB_CONDOMINIOS",
                        principalColumn: "CND_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_MORADORES",
                columns: table => new
                {
                    MOR_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave primária do morador"),
                    MOR_NOME = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Nome do morador"),
                    MOR_CPF = table.Column<string>(type: "varchar(11)", nullable: false, comment: "CPF do morador"),
                    MOR_EMAIL = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Email do morador"),
                    MOR_TELEFONE = table.Column<string>(type: "varchar(11)", nullable: false, comment: "Telefone do morador"),
                    MOR_RAMAL = table.Column<string>(type: "varchar(5)", nullable: true, comment: "Ramal do morador"),
                    MOR_FOTO = table.Column<string>(type: "varchar(100)", nullable: true, comment: "Foto do morador"),
                    MOR_UNIDADE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave estrangeira da unidade do morador"),
                    MOR_EXCLUIDO = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Indica se o morador foi excluído"),
                    MOR_DATA_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Data de criação do morador"),
                    MOR_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Data da última atualização do morador"),
                    MOR_DATA_EXCLUSAO = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Data da exclusão do morador")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MORADORES", x => x.MOR_ID);
                    table.ForeignKey(
                        name: "FK_MORADORES_UNIDADES",
                        column: x => x.MOR_UNIDADE_ID,
                        principalTable: "TB_UNIDADES",
                        principalColumn: "UND_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_ENTREGAS",
                columns: table => new
                {
                    ETG_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave primária da entrega"),
                    TRP_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave estrangeira da transportadora"),
                    FUN_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave estrangeira do funcionário"),
                    MOR_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Chave estrangeira do morador"),
                    ETG_DATA_RECEBIMENTO = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Data de recebimento da entrega"),
                    ETG_DATA_RETIRADA = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Data de retirada da entrega"),
                    ETG_DESCRICAO = table.Column<string>(type: "varchar(200)", nullable: true, comment: "Descrição da entrega"),
                    ETG_OBSERVACAO = table.Column<string>(type: "varchar(200)", nullable: true, comment: "Observação da entrega"),
                    ETG_STATUS = table.Column<int>(type: "int", nullable: false, comment: "Status da entrega"),
                    ETG_EXCLUIDO = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Flag de exclusão da entrega"),
                    ETG_DATA_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação da entrega"),
                    ETG_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização da entrega"),
                    ETG_DATA_EXCLUSAO = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Data da exclusão da entrega")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENTREGAS", x => x.ETG_ID);
                    table.ForeignKey(
                        name: "FK_ENTREGA_TRANSPORTADORA",
                        column: x => x.TRP_ID,
                        principalTable: "TB_TRANSPORTADORAS",
                        principalColumn: "TRA_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIO_ENTREGA",
                        column: x => x.FUN_ID,
                        principalTable: "TB_FUNCIONARIOS",
                        principalColumn: "FUN_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MORADORES_ENTREGAS",
                        column: x => x.MOR_ID,
                        principalTable: "TB_MORADORES",
                        principalColumn: "MOR_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TB_CONDOMINIOS",
                columns: new[] { "CND_ID", "CND_BAIRRO", "CND_CEP", "CND_CNPJ", "CND_CIDADE", "CND_COMPLEMENTO", "CND_DATA_ATUALIZACAO", "CND_DATA_CRIACAO", "CND_DATA_EXCLUSAO", "CND_EMAIL", "CND_ESTADO", "CND_LOGRADOURO", "CND_NOME", "CND_NUMERO", "CND_QTD_ANDARES", "CND_QTD_BLOCOS", "CND_QTD_UNIDADES", "CND_TELEFONE" },
                values: new object[] { new Guid("f26355b2-c097-4582-8a4a-4a9ecbfc7f09"), "Bairro Exemplo", "11111111", "11111111111111", "Cidade Exemplo", "Bloco A", new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5397), new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5386), null, "contato@condominioexemplo.com.br", "SP", "Rua Exemplo", "Condomínio Exemplo", "100", 0, 0, 0, "11999999999" });

            migrationBuilder.InsertData(
                table: "TB_TRANSPORTADORAS",
                columns: new[] { "TRA_ID", "TRA_CNPJ", "TRA_DATA_ATUALIZACAO", "TRA_DATA_CRIACAO", "TRA_DATA_EXCLUSAO", "TRA_EMAIL", "TRA_NOME", "TRA_TELEFONE" },
                values: new object[] { new Guid("5cc12493-0012-43aa-aac0-76cbc18bedb3"), "22222222222222", new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5528), new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5527), null, "contato@transportadoraexemplo.com.br", "Transportadora Exemplo", "11988888888" });

            migrationBuilder.InsertData(
                table: "TB_FUNCIONARIOS",
                columns: new[] { "FUN_ID", "FUN_CPF", "FUN_CARGO", "FUN_CONDOMINIO_ID", "FUN_DATA_ADMISSAO", "FUN_DATA_ATUALIZACAO", "FUN_DATA_CRIACAO", "FUN_DATA_DEMISSAO", "FUN_DATA_EXCLUSAO", "FUN_EMAIL", "FUN_NOME", "FUN_TELEFONE" },
                values: new object[] { new Guid("f1e28b7e-674f-41dd-868c-c245e35de929"), "98765432109", 2, new Guid("f26355b2-c097-4582-8a4a-4a9ecbfc7f09"), new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5514), new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5513), new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5513), null, null, "funcionario@email.com", "Funcionario Exemplo", "11999999999" });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_DATA_EXCLUSAO", "UND_NUMERO" },
                values: new object[] { new Guid("68165d63-fa38-4d27-858f-ac006b1ada92"), "A", new Guid("f26355b2-c097-4582-8a4a-4a9ecbfc7f09"), new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5484), new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5483), null, "101" });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_DATA_ATUALIZACAO", "MOR_DATA_CRIACAO", "MOR_DATA_EXCLUSAO", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID" },
                values: new object[] { new Guid("7b3b9132-0eae-4ba1-8519-347d92873868"), "12345678901", new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5497), new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5496), null, "morador@email.com", null, "Morador Exemplo", "210", "11999999999", new Guid("68165d63-fa38-4d27-858f-ac006b1ada92") });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_ATUALIZACAO", "ETG_DATA_CRIACAO", "ETG_DATA_EXCLUSAO", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRP_ID" },
                values: new object[] { new Guid("8a6b4827-15d3-4d9c-a567-a14a6a0c8ce7"), new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5542), new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5542), null, new DateTime(2023, 5, 16, 21, 14, 24, 80, DateTimeKind.Local).AddTicks(5544), null, "Descrição da entrega", new Guid("f1e28b7e-674f-41dd-868c-c245e35de929"), new Guid("7b3b9132-0eae-4ba1-8519-347d92873868"), "Observação da entrega", 1, new Guid("5cc12493-0012-43aa-aac0-76cbc18bedb3") });

            migrationBuilder.CreateIndex(
                name: "IX_CONDOMINIOS_CNPJ",
                table: "TB_CONDOMINIOS",
                column: "CND_CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CONDOMINIOS_EMAIL",
                table: "TB_CONDOMINIOS",
                column: "CND_EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CONDOMINIOS_NOME",
                table: "TB_CONDOMINIOS",
                column: "CND_NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_ENTREGAS_FUN_ID",
                table: "TB_ENTREGAS",
                column: "FUN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ENTREGAS_MOR_ID",
                table: "TB_ENTREGAS",
                column: "MOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ENTREGAS_TRP_ID",
                table: "TB_ENTREGAS",
                column: "TRP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_CPF",
                table: "TB_FUNCIONARIOS",
                column: "FUN_CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_EMAIL",
                table: "TB_FUNCIONARIOS",
                column: "FUN_EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_FUNCIONARIOS_FUN_CONDOMINIO_ID",
                table: "TB_FUNCIONARIOS",
                column: "FUN_CONDOMINIO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MORADORES_CPF",
                table: "TB_MORADORES",
                column: "MOR_CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MORADORES_EMAIL",
                table: "TB_MORADORES",
                column: "MOR_EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MORADORES_UNIDADE_ID",
                table: "TB_MORADORES",
                column: "MOR_UNIDADE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSPORTADORAS_CNPJ",
                table: "TB_TRANSPORTADORAS",
                column: "TRA_CNPJ",
                unique: true,
                filter: "[TRA_CNPJ] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSPORTADORAS_EMAIL",
                table: "TB_TRANSPORTADORAS",
                column: "TRA_EMAIL",
                unique: true,
                filter: "[TRA_EMAIL] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TRANSPORTADORAS_NOME",
                table: "TB_TRANSPORTADORAS",
                column: "TRA_NOME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TRANSPORTADORAS_TELEFONE",
                table: "TB_TRANSPORTADORAS",
                column: "TRA_TELEFONE",
                unique: true,
                filter: "[TRA_TELEFONE] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UNIDADES_CONDOMINIO_NUMERO_BLOCO",
                table: "TB_UNIDADES",
                columns: new[] { "CON_ID", "UND_NUMERO", "UND_BLOCO" },
                unique: true,
                filter: "[UND_BLOCO] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ENTREGAS");

            migrationBuilder.DropTable(
                name: "TB_TRANSPORTADORAS");

            migrationBuilder.DropTable(
                name: "TB_FUNCIONARIOS");

            migrationBuilder.DropTable(
                name: "TB_MORADORES");

            migrationBuilder.DropTable(
                name: "TB_UNIDADES");

            migrationBuilder.DropTable(
                name: "TB_CONDOMINIOS");
        }
    }
}
