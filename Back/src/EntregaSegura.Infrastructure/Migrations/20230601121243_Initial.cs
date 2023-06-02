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
                    CND_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave primária do condomínio")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CND_QTD_UNIDADES = table.Column<int>(type: "int", nullable: false, comment: "Quantidade de unidades do condomínio"),
                    CND_QTD_BLOCOS = table.Column<int>(type: "int", nullable: false, comment: "Quantidade de blocos do condomínio"),
                    CND_QTD_ANDARES = table.Column<int>(type: "int", nullable: false, comment: "Quantidade de andares do condomínio"),
                    CND_NOME = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Nome do condomínio"),
                    CND_CNPJ = table.Column<string>(type: "varchar(14)", nullable: false, comment: "CNPJ do condomínio"),
                    CND_TELEFONE = table.Column<string>(type: "varchar(11)", nullable: false, comment: "Telefone do condomínio"),
                    CND_EMAIL = table.Column<string>(type: "varchar(100)", nullable: false, comment: "E-mail do condomínio"),
                    CND_LOGRADOURO = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Logradouro do endereço do condomínio"),
                    CND_NUMERO = table.Column<string>(type: "varchar(10)", nullable: false, comment: "Número do endereço do condomínio"),
                    CND_CEP = table.Column<string>(type: "varchar(8)", nullable: false, comment: "CEP do endereço do condomínio"),
                    CND_BAIRRO = table.Column<string>(type: "varchar(50)", nullable: false, comment: "Bairro do endereço do condomínio"),
                    CND_CIDADE = table.Column<string>(type: "varchar(50)", nullable: false, comment: "Cidade do endereço do condomínio"),
                    CND_ESTADO = table.Column<string>(type: "varchar(2)", nullable: false, comment: "Estado do endereço do condomínio"),
                    CND_DATA_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação do condomínio"),
                    CND_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização do condomínio")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONDOMINIOS", x => x.CND_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_TRANSPORTADORAS",
                columns: table => new
                {
                    TRA_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave primária da transportadora")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TRA_NOME = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Nome da transportadora"),
                    TRA_CNPJ = table.Column<string>(type: "varchar(14)", nullable: true, comment: "CNPJ da transportadora"),
                    TRA_TELEFONE = table.Column<string>(type: "varchar(11)", nullable: true, comment: "Telefone da transportadora"),
                    TRA_EMAIL = table.Column<string>(type: "varchar(100)", nullable: true, comment: "E-mail da transportadora"),
                    TRA_DATA_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação da transportadora"),
                    TRA_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização da transportadora")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRANSPORTADORAS", x => x.TRA_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_FUNCIONARIOS",
                columns: table => new
                {
                    FUN_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave primária do funcionário")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FUN_NOME = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Nome do funcionário"),
                    FUN_CPF = table.Column<string>(type: "varchar(11)", nullable: false, comment: "CPF do funcionário"),
                    FUN_EMAIL = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Email do funcionário"),
                    FUN_TELEFONE = table.Column<string>(type: "varchar(11)", nullable: false, comment: "Telefone do funcionário"),
                    FUN_CARGO = table.Column<int>(type: "int", nullable: false, comment: "Cargo do funcionário"),
                    FUN_DATA_ADMISSAO = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Data de admissão do funcionário"),
                    FUN_DATA_DEMISSAO = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Data de demissão do funcionário"),
                    FUN_DATA_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação do funcionário"),
                    FUN_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização do funcionário"),
                    FUN_CONDOMINIO_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave estrangeira do condomínio")
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
                    UND_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave primária da unidade")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CON_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave estrangeira do condomínio"),
                    UND_NUMERO = table.Column<string>(type: "varchar(10)", nullable: false, comment: "Número da unidade"),
                    UND_ANDAR = table.Column<string>(type: "varchar(10)", nullable: false, comment: "Andar da unidade"),
                    UND_BLOCO = table.Column<string>(type: "varchar(10)", nullable: true, comment: "Bloco da unidade"),
                    UND_DATA_CRIACAO = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação da unidade"),
                    UND_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização da unidade")
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
                    MOR_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave primária do morador")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MOR_NOME = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Nome do morador"),
                    MOR_CPF = table.Column<string>(type: "varchar(11)", nullable: false, comment: "CPF do morador"),
                    MOR_EMAIL = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Email do morador"),
                    MOR_TELEFONE = table.Column<string>(type: "varchar(11)", nullable: false, comment: "Telefone do morador"),
                    MOR_RAMAL = table.Column<string>(type: "varchar(5)", nullable: true, comment: "Ramal do morador"),
                    MOR_FOTO = table.Column<string>(type: "varchar(100)", nullable: true, comment: "Foto do morador"),
                    MOR_UNIDADE_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave estrangeira da unidade do morador"),
                    MOR_DATA_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Data de criação do morador"),
                    MOR_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Data da última atualização do morador")
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
                    ETG_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave primária da entrega")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TRP_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave estrangeira da transportadora"),
                    FUN_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave estrangeira do funcionário"),
                    MOR_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave estrangeira do morador"),
                    ETG_DATA_RECEBIMENTO = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Data de recebimento da entrega"),
                    ETG_DATA_RETIRADA = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Data de retirada da entrega"),
                    ETG_DESCRICAO = table.Column<string>(type: "varchar(200)", nullable: true, comment: "Descrição da entrega"),
                    ETG_OBSERVACAO = table.Column<string>(type: "varchar(200)", nullable: true, comment: "Observação da entrega"),
                    ETG_STATUS = table.Column<int>(type: "int", nullable: false, comment: "Status da entrega"),
                    ETG_DATA_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação da entrega"),
                    ETG_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização da entrega")
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
                columns: new[] { "CND_ID", "CND_BAIRRO", "CND_CEP", "CND_CNPJ", "CND_CIDADE", "CND_DATA_ATUALIZACAO", "CND_DATA_CRIACAO", "CND_EMAIL", "CND_ESTADO", "CND_LOGRADOURO", "CND_NOME", "CND_NUMERO", "CND_QTD_ANDARES", "CND_QTD_BLOCOS", "CND_QTD_UNIDADES", "CND_TELEFONE" },
                values: new object[,]
                {
                    { 1, "Jardim Paulistano", "04567010", "17540623000150", "São Paulo", new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9025), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9016), "contato@boavista.com.br", "SP", "Rua das Acácias", "Condomínio Boa Vista", "55", 7, 2, 4, "1140028922" },
                    { 2, "Copacabana", "22021001", "27004428000169", "Rio de Janeiro", new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9074), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9074), "contato@raiodesol.com.br", "RJ", "Avenida Atlântica", "Condomínio Raio de Sol", "700", 10, 3, 8, "2130033211" }
                });

            migrationBuilder.InsertData(
                table: "TB_TRANSPORTADORAS",
                columns: new[] { "TRA_ID", "TRA_CNPJ", "TRA_DATA_ATUALIZACAO", "TRA_DATA_CRIACAO", "TRA_EMAIL", "TRA_NOME", "TRA_TELEFONE" },
                values: new object[,]
                {
                    { 1, "12345678912347", new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9897), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9897), "transportadora1@teste.com", "Transportadora Teste 1", "1234567894" },
                    { 2, "12345678912348", new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9899), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9899), "transportadora2@teste.com", "Transportadora Teste 2", "1234567895" }
                });

            migrationBuilder.InsertData(
                table: "TB_FUNCIONARIOS",
                columns: new[] { "FUN_ID", "FUN_CPF", "FUN_CARGO", "FUN_CONDOMINIO_ID", "FUN_DATA_ADMISSAO", "FUN_DATA_ATUALIZACAO", "FUN_DATA_CRIACAO", "FUN_DATA_DEMISSAO", "FUN_EMAIL", "FUN_NOME", "FUN_TELEFONE" },
                values: new object[,]
                {
                    { 1, "12345678903", 3, 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9878), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9876), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9876), null, "funcionario1@teste.com", "Funcionario Teste 1", "1234567892" },
                    { 2, "12345678904", 2, 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9881), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9880), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9880), null, "funcionario2@teste.com", "Funcionario Teste 2", "1234567893" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 1, "1", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9176), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9176), "1" },
                    { 2, "1", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9187), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9186), "2" },
                    { 3, "1", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9188), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9188), "3" },
                    { 4, "1", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9189), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9189), "4" },
                    { 5, "2", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9191), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9191), "1" },
                    { 6, "2", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9194), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9194), "2" },
                    { 7, "2", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9196), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9195), "3" },
                    { 8, "2", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9197), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9197), "4" },
                    { 9, "3", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9199), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9198), "1" },
                    { 10, "3", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9201), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9201), "2" },
                    { 11, "3", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9202), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9202), "3" },
                    { 12, "3", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9204), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9203), "4" },
                    { 13, "4", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9205), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9205), "1" },
                    { 14, "4", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9206), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9206), "2" },
                    { 15, "4", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9208), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9207), "3" },
                    { 16, "4", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9209), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9208), "4" },
                    { 17, "5", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9210), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9210), "1" },
                    { 18, "5", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9212), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9212), "2" },
                    { 19, "5", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9214), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9213), "3" },
                    { 20, "5", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9215), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9214), "4" },
                    { 21, "6", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9216), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9216), "1" },
                    { 22, "6", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9218), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9217), "2" },
                    { 23, "6", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9219), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9218), "3" },
                    { 24, "6", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9220), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9220), "4" },
                    { 25, "7", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9221), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9221), "1" },
                    { 26, "7", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9223), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9222), "2" },
                    { 27, "7", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9224), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9224), "3" },
                    { 28, "7", "1", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9225), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9225), "4" },
                    { 29, "1", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9227), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9227), "1" },
                    { 30, "1", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9228), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9228), "2" },
                    { 31, "1", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9230), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9229), "3" },
                    { 32, "1", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9231), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9231), "4" },
                    { 33, "2", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9233), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9232), "1" },
                    { 34, "2", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9235), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9234), "2" },
                    { 35, "2", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9236), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9236), "3" },
                    { 36, "2", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9237), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9237), "4" },
                    { 37, "3", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9239), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9238), "1" },
                    { 38, "3", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9240), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9240), "2" },
                    { 39, "3", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9241), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9241), "3" },
                    { 40, "3", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9242), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9242), "4" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 41, "4", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9244), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9243), "1" },
                    { 42, "4", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9245), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9245), "2" },
                    { 43, "4", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9247), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9246), "3" },
                    { 44, "4", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9248), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9247), "4" },
                    { 45, "5", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9249), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9249), "1" },
                    { 46, "5", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9251), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9250), "2" },
                    { 47, "5", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9252), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9251), "3" },
                    { 48, "5", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9253), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9253), "4" },
                    { 49, "6", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9255), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9254), "1" },
                    { 50, "6", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9256), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9255), "2" },
                    { 51, "6", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9257), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9257), "3" },
                    { 52, "6", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9258), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9258), "4" },
                    { 53, "7", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9260), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9259), "1" },
                    { 54, "7", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9261), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9261), "2" },
                    { 55, "7", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9262), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9262), "3" },
                    { 56, "7", "2", 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9264), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9263), "4" },
                    { 57, "1", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9266), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9265), "1" },
                    { 58, "1", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9267), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9267), "2" },
                    { 59, "1", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9304), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9304), "3" },
                    { 60, "1", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9306), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9306), "4" },
                    { 61, "1", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9307), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9307), "5" },
                    { 62, "1", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9309), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9308), "6" },
                    { 63, "1", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9310), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9309), "7" },
                    { 64, "1", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9311), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9311), "8" },
                    { 65, "2", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9313), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9312), "1" },
                    { 66, "2", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9315), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9314), "2" },
                    { 67, "2", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9316), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9316), "3" },
                    { 68, "2", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9318), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9317), "4" },
                    { 69, "2", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9319), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9318), "5" },
                    { 70, "2", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9320), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9320), "6" },
                    { 71, "2", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9321), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9321), "7" },
                    { 72, "2", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9323), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9322), "8" },
                    { 73, "3", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9324), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9324), "1" },
                    { 74, "3", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9325), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9325), "2" },
                    { 75, "3", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9326), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9326), "3" },
                    { 76, "3", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9328), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9327), "4" },
                    { 77, "3", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9329), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9328), "5" },
                    { 78, "3", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9330), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9330), "6" },
                    { 79, "3", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9331), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9331), "7" },
                    { 80, "3", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9332), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9332), "8" },
                    { 81, "4", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9334), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9333), "1" },
                    { 82, "4", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9335), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9335), "2" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 83, "4", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9336), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9336), "3" },
                    { 84, "4", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9338), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9337), "4" },
                    { 85, "4", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9339), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9338), "5" },
                    { 86, "4", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9340), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9340), "6" },
                    { 87, "4", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9341), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9341), "7" },
                    { 88, "4", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9342), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9342), "8" },
                    { 89, "5", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9344), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9343), "1" },
                    { 90, "5", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9345), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9345), "2" },
                    { 91, "5", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9346), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9346), "3" },
                    { 92, "5", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9347), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9347), "4" },
                    { 93, "5", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9349), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9348), "5" },
                    { 94, "5", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9350), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9350), "6" },
                    { 95, "5", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9351), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9351), "7" },
                    { 96, "5", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9353), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9352), "8" },
                    { 97, "6", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9354), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9354), "1" },
                    { 98, "6", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9355), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9355), "2" },
                    { 99, "6", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9357), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9356), "3" },
                    { 100, "6", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9358), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9357), "4" },
                    { 101, "6", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9359), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9359), "5" },
                    { 102, "6", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9360), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9360), "6" },
                    { 103, "6", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9361), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9361), "7" },
                    { 104, "6", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9363), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9362), "8" },
                    { 105, "7", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9364), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9364), "1" },
                    { 106, "7", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9365), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9365), "2" },
                    { 107, "7", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9366), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9366), "3" },
                    { 108, "7", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9368), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9367), "4" },
                    { 109, "7", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9369), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9369), "5" },
                    { 110, "7", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9370), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9370), "6" },
                    { 111, "7", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9371), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9371), "7" },
                    { 112, "7", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9373), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9372), "8" },
                    { 113, "8", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9374), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9374), "1" },
                    { 114, "8", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9375), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9375), "2" },
                    { 115, "8", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9376), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9376), "3" },
                    { 116, "8", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9378), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9377), "4" },
                    { 117, "8", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9379), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9379), "5" },
                    { 118, "8", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9380), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9380), "6" },
                    { 119, "8", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9381), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9381), "7" },
                    { 120, "8", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9383), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9382), "8" },
                    { 121, "9", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9384), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9384), "1" },
                    { 122, "9", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9385), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9385), "2" },
                    { 123, "9", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9421), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9420), "3" },
                    { 124, "9", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9423), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9423), "4" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 125, "9", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9424), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9424), "5" },
                    { 126, "9", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9425), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9425), "6" },
                    { 127, "9", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9427), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9426), "7" },
                    { 128, "9", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9428), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9427), "8" },
                    { 129, "10", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9429), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9429), "1" },
                    { 130, "10", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9431), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9431), "2" },
                    { 131, "10", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9433), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9432), "3" },
                    { 132, "10", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9434), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9434), "4" },
                    { 133, "10", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9435), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9435), "5" },
                    { 134, "10", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9437), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9436), "6" },
                    { 135, "10", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9438), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9437), "7" },
                    { 136, "10", "1", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9439), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9439), "8" },
                    { 137, "1", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9441), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9440), "1" },
                    { 138, "1", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9442), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9442), "2" },
                    { 139, "1", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9443), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9443), "3" },
                    { 140, "1", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9444), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9444), "4" },
                    { 141, "1", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9446), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9445), "5" },
                    { 142, "1", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9447), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9446), "6" },
                    { 143, "1", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9448), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9448), "7" },
                    { 144, "1", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9449), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9449), "8" },
                    { 145, "2", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9451), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9451), "1" },
                    { 146, "2", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9452), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9452), "2" },
                    { 147, "2", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9453), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9453), "3" },
                    { 148, "2", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9455), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9454), "4" },
                    { 149, "2", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9456), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9455), "5" },
                    { 150, "2", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9457), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9457), "6" },
                    { 151, "2", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9458), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9458), "7" },
                    { 152, "2", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9459), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9459), "8" },
                    { 153, "3", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9461), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9460), "1" },
                    { 154, "3", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9462), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9462), "2" },
                    { 155, "3", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9463), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9463), "3" },
                    { 156, "3", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9464), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9464), "4" },
                    { 157, "3", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9466), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9465), "5" },
                    { 158, "3", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9467), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9467), "6" },
                    { 159, "3", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9468), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9468), "7" },
                    { 160, "3", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9469), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9469), "8" },
                    { 161, "4", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9471), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9470), "1" },
                    { 162, "4", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9472), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9472), "2" },
                    { 163, "4", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9473), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9473), "3" },
                    { 164, "4", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9475), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9474), "4" },
                    { 165, "4", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9476), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9475), "5" },
                    { 166, "4", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9477), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9477), "6" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 167, "4", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9478), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9478), "7" },
                    { 168, "4", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9479), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9479), "8" },
                    { 169, "5", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9481), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9480), "1" },
                    { 170, "5", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9482), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9481), "2" },
                    { 171, "5", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9483), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9483), "3" },
                    { 172, "5", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9484), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9484), "4" },
                    { 173, "5", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9486), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9485), "5" },
                    { 174, "5", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9487), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9486), "6" },
                    { 175, "5", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9488), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9488), "7" },
                    { 176, "5", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9489), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9489), "8" },
                    { 177, "6", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9522), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9521), "1" },
                    { 178, "6", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9524), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9523), "2" },
                    { 179, "6", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9525), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9524), "3" },
                    { 180, "6", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9526), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9526), "4" },
                    { 181, "6", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9527), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9527), "5" },
                    { 182, "6", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9529), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9528), "6" },
                    { 183, "6", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9530), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9529), "7" },
                    { 184, "6", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9531), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9531), "8" },
                    { 185, "7", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9532), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9532), "1" },
                    { 186, "7", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9534), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9533), "2" },
                    { 187, "7", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9535), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9534), "3" },
                    { 188, "7", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9536), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9536), "4" },
                    { 189, "7", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9537), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9537), "5" },
                    { 190, "7", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9538), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9538), "6" },
                    { 191, "7", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9540), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9539), "7" },
                    { 192, "7", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9541), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9541), "8" },
                    { 193, "8", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9542), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9542), "1" },
                    { 194, "8", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9544), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9543), "2" },
                    { 195, "8", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9545), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9544), "3" },
                    { 196, "8", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9546), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9546), "4" },
                    { 197, "8", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9547), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9547), "5" },
                    { 198, "8", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9548), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9548), "6" },
                    { 199, "8", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9550), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9549), "7" },
                    { 200, "8", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9551), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9550), "8" },
                    { 201, "9", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9552), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9552), "1" },
                    { 202, "9", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9553), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9553), "2" },
                    { 203, "9", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9555), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9554), "3" },
                    { 204, "9", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9556), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9556), "4" },
                    { 205, "9", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9557), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9557), "5" },
                    { 206, "9", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9558), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9558), "6" },
                    { 207, "9", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9560), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9559), "7" },
                    { 208, "9", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9561), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9560), "8" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 209, "10", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9562), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9562), "1" },
                    { 210, "10", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9563), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9563), "2" },
                    { 211, "10", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9565), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9564), "3" },
                    { 212, "10", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9566), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9565), "4" },
                    { 213, "10", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9567), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9567), "5" },
                    { 214, "10", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9568), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9568), "6" },
                    { 215, "10", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9570), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9569), "7" },
                    { 216, "10", "2", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9571), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9570), "8" },
                    { 217, "1", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9572), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9572), "1" },
                    { 218, "1", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9574), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9573), "2" },
                    { 219, "1", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9575), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9574), "3" },
                    { 220, "1", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9576), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9576), "4" },
                    { 221, "1", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9577), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9577), "5" },
                    { 222, "1", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9578), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9578), "6" },
                    { 223, "1", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9580), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9579), "7" },
                    { 224, "1", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9581), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9581), "8" },
                    { 225, "2", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9582), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9582), "1" },
                    { 226, "2", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9584), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9583), "2" },
                    { 227, "2", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9585), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9584), "3" },
                    { 228, "2", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9586), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9586), "4" },
                    { 229, "2", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9587), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9587), "5" },
                    { 230, "2", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9588), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9588), "6" },
                    { 231, "2", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9590), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9589), "7" },
                    { 232, "2", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9591), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9590), "8" },
                    { 233, "3", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9592), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9592), "1" },
                    { 234, "3", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9593), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9593), "2" },
                    { 235, "3", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9595), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9594), "3" },
                    { 236, "3", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9596), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9595), "4" },
                    { 237, "3", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9597), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9597), "5" },
                    { 238, "3", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9598), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9598), "6" },
                    { 239, "3", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9600), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9599), "7" },
                    { 240, "3", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9601), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9600), "8" },
                    { 241, "4", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9602), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9602), "1" },
                    { 242, "4", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9603), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9603), "2" },
                    { 243, "4", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9605), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9604), "3" },
                    { 244, "4", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9606), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9605), "4" },
                    { 245, "4", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9607), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9607), "5" },
                    { 246, "4", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9608), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9608), "6" },
                    { 247, "4", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9609), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9609), "7" },
                    { 248, "4", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9611), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9610), "8" },
                    { 249, "5", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9612), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9612), "1" },
                    { 250, "5", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9647), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9646), "2" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 251, "5", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9648), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9648), "3" },
                    { 252, "5", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9650), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9649), "4" },
                    { 253, "5", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9651), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9650), "5" },
                    { 254, "5", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9652), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9652), "6" },
                    { 255, "5", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9653), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9653), "7" },
                    { 256, "5", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9654), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9654), "8" },
                    { 257, "6", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9656), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9655), "1" },
                    { 258, "6", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9659), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9658), "2" },
                    { 259, "6", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9660), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9660), "3" },
                    { 260, "6", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9661), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9661), "4" },
                    { 261, "6", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9663), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9662), "5" },
                    { 262, "6", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9664), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9663), "6" },
                    { 263, "6", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9665), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9665), "7" },
                    { 264, "6", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9666), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9666), "8" },
                    { 265, "7", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9668), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9667), "1" },
                    { 266, "7", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9669), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9669), "2" },
                    { 267, "7", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9670), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9670), "3" },
                    { 268, "7", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9671), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9671), "4" },
                    { 269, "7", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9673), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9672), "5" },
                    { 270, "7", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9674), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9674), "6" },
                    { 271, "7", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9675), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9675), "7" },
                    { 272, "7", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9676), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9676), "8" },
                    { 273, "8", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9678), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9678), "1" },
                    { 274, "8", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9679), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9679), "2" },
                    { 275, "8", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9681), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9680), "3" },
                    { 276, "8", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9682), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9681), "4" },
                    { 277, "8", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9683), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9683), "5" },
                    { 278, "8", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9684), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9684), "6" },
                    { 279, "8", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9686), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9685), "7" },
                    { 280, "8", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9687), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9686), "8" },
                    { 281, "9", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9688), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9688), "1" },
                    { 282, "9", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9690), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9689), "2" },
                    { 283, "9", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9691), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9690), "3" },
                    { 284, "9", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9692), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9692), "4" },
                    { 285, "9", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9693), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9693), "5" },
                    { 286, "9", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9728), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9728), "6" },
                    { 287, "9", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9730), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9729), "7" },
                    { 288, "9", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9731), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9731), "8" },
                    { 289, "10", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9733), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9732), "1" },
                    { 290, "10", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9734), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9733), "2" },
                    { 291, "10", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9735), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9735), "3" },
                    { 292, "10", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9737), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9737), "4" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 293, "10", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9738), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9738), "5" },
                    { 294, "10", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9739), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9739), "6" },
                    { 295, "10", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9741), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9740), "7" },
                    { 296, "10", "3", 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9742), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9742), "8" }
                });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_DATA_ATUALIZACAO", "MOR_DATA_CRIACAO", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID" },
                values: new object[] { 1, "12345678901", new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9853), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9852), "morador1@teste.com", "foto1.jpg", "Morador Teste 1", "123", "1234567890", 1 });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_DATA_ATUALIZACAO", "MOR_DATA_CRIACAO", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID" },
                values: new object[] { 2, "12345678902", new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9857), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9857), "morador2@teste.com", "foto2.jpg", "Morador Teste 2", "456", "1234567891", 2 });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_ATUALIZACAO", "ETG_DATA_CRIACAO", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRP_ID" },
                values: new object[] { 1, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9910), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9910), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9911), null, "Entrega Teste 1", 1, 1, "Observação Teste 1", 1, 1 });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_ATUALIZACAO", "ETG_DATA_CRIACAO", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRP_ID" },
                values: new object[] { 2, new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9913), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9913), new DateTime(2023, 6, 1, 9, 12, 43, 131, DateTimeKind.Local).AddTicks(9914), null, "Entrega Teste 2", 2, 2, "Observação Teste 2", 1, 2 });

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
                columns: new[] { "CON_ID", "UND_NUMERO", "UND_BLOCO" });
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
