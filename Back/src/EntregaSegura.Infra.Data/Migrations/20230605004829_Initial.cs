using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntregaSegura.Infra.Data.Migrations
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
                    CND_NOME = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Nome do condomínio"),
                    CND_CNPJ = table.Column<string>(type: "varchar(14)", nullable: false, comment: "CNPJ do condomínio"),
                    CND_TELEFONE = table.Column<string>(type: "varchar(11)", nullable: false, comment: "Telefone do condomínio"),
                    CND_EMAIL = table.Column<string>(type: "varchar(100)", nullable: false, comment: "E-mail do condomínio"),
                    CND_CEP = table.Column<string>(type: "varchar(8)", nullable: false, comment: "CEP do endereço do condomínio"),
                    CND_LOGRADOURO = table.Column<string>(type: "varchar(100)", nullable: false, comment: "Logradouro do endereço do condomínio"),
                    CND_NUMERO = table.Column<int>(type: "int", nullable: false, comment: "Número do endereço do condomínio"),
                    CND_BAIRRO = table.Column<string>(type: "varchar(50)", nullable: false, comment: "Bairro do endereço do condomínio"),
                    CND_CIDADE = table.Column<string>(type: "varchar(50)", nullable: false, comment: "Cidade do endereço do condomínio"),
                    CND_ESTADO = table.Column<string>(type: "varchar(2)", nullable: false, comment: "Estado do endereço do condomínio"),
                    CND_QTD_UNIDADES = table.Column<int>(type: "int", nullable: false, comment: "Quantidade de unidades do condomínio"),
                    CND_QTD_BLOCOS = table.Column<int>(type: "int", nullable: false, comment: "Quantidade de blocos do condomínio"),
                    CND_QTD_ANDARES = table.Column<int>(type: "int", nullable: false, comment: "Quantidade de andares do condomínio"),
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
                columns: new[] { "CND_ID", "CND_BAIRRO", "CND_CEP", "CND_CIDADE", "CND_CNPJ", "CND_DATA_ATUALIZACAO", "CND_DATA_CRIACAO", "CND_EMAIL", "CND_ESTADO", "CND_LOGRADOURO", "CND_NOME", "CND_NUMERO", "CND_QTD_ANDARES", "CND_QTD_BLOCOS", "CND_QTD_UNIDADES", "CND_TELEFONE" },
                values: new object[,]
                {
                    { 1, "Jardim Paulistano", "04567010", "São Paulo", "17540623000150", new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4801), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4793), "contato@boavista.com.br", "SP", "Rua das Acácias", "Condomínio Boa Vista", 55, 7, 2, 4, "1140028922" },
                    { 2, "Copacabana", "22021001", "Rio de Janeiro", "27004428000169", new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4809), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4808), "contato@raiodesol.com.br", "RJ", "Avenida Atlântica", "Condomínio Raio de Sol", 700, 10, 3, 8, "2130033211" }
                });

            migrationBuilder.InsertData(
                table: "TB_TRANSPORTADORAS",
                columns: new[] { "TRA_ID", "TRA_CNPJ", "TRA_DATA_ATUALIZACAO", "TRA_DATA_CRIACAO", "TRA_EMAIL", "TRA_NOME", "TRA_TELEFONE" },
                values: new object[,]
                {
                    { 1, "12345678912347", new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5687), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5686), "transportadora1@teste.com", "Transportadora Teste 1", "1234567894" },
                    { 2, "12345678912348", new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5689), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5688), "transportadora2@teste.com", "Transportadora Teste 2", "1234567895" }
                });

            migrationBuilder.InsertData(
                table: "TB_FUNCIONARIOS",
                columns: new[] { "FUN_ID", "FUN_CPF", "FUN_CARGO", "FUN_CONDOMINIO_ID", "FUN_DATA_ADMISSAO", "FUN_DATA_ATUALIZACAO", "FUN_DATA_CRIACAO", "FUN_DATA_DEMISSAO", "FUN_EMAIL", "FUN_NOME", "FUN_TELEFONE" },
                values: new object[,]
                {
                    { 1, "12345678903", 3, 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5668), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5668), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5667), null, "funcionario1@teste.com", "Funcionario Teste 1", "1234567892" },
                    { 2, "12345678904", 2, 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5672), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5671), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5671), null, "funcionario2@teste.com", "Funcionario Teste 2", "1234567893" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 1, "1", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4908), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4907), "1" },
                    { 2, "1", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4922), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4922), "2" },
                    { 3, "1", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4924), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4923), "3" },
                    { 4, "1", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4925), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4925), "4" },
                    { 5, "2", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4927), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4926), "1" },
                    { 6, "2", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4930), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4929), "2" },
                    { 7, "2", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4931), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4931), "3" },
                    { 8, "2", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4933), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4932), "4" },
                    { 9, "3", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4934), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4934), "1" },
                    { 10, "3", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4937), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4936), "2" },
                    { 11, "3", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4938), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4937), "3" },
                    { 12, "3", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4939), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4939), "4" },
                    { 13, "4", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4941), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4940), "1" },
                    { 14, "4", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4942), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4941), "2" },
                    { 15, "4", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4943), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4943), "3" },
                    { 16, "4", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4944), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4944), "4" },
                    { 17, "5", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4946), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4945), "1" },
                    { 18, "5", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4989), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4988), "2" },
                    { 19, "5", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4990), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4990), "3" },
                    { 20, "5", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4992), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4991), "4" },
                    { 21, "6", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4993), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4993), "1" },
                    { 22, "6", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4995), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4994), "2" },
                    { 23, "6", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4996), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4996), "3" },
                    { 24, "6", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4997), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4997), "4" },
                    { 25, "7", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4999), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(4999), "1" },
                    { 26, "7", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5000), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5000), "2" },
                    { 27, "7", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5002), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5001), "3" },
                    { 28, "7", "1", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5003), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5003), "4" },
                    { 29, "1", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5005), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5005), "1" },
                    { 30, "1", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5006), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5006), "2" },
                    { 31, "1", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5008), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5007), "3" },
                    { 32, "1", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5009), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5009), "4" },
                    { 33, "2", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5011), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5010), "1" },
                    { 34, "2", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5013), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5013), "2" },
                    { 35, "2", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5015), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5014), "3" },
                    { 36, "2", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5016), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5016), "4" },
                    { 37, "3", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5018), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5017), "1" },
                    { 38, "3", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5019), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5019), "2" },
                    { 39, "3", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5020), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5020), "3" },
                    { 40, "3", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5022), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5021), "4" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 41, "4", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5023), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5023), "1" },
                    { 42, "4", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5025), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5024), "2" },
                    { 43, "4", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5026), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5025), "3" },
                    { 44, "4", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5027), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5027), "4" },
                    { 45, "5", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5029), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5028), "1" },
                    { 46, "5", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5030), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5030), "2" },
                    { 47, "5", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5032), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5031), "3" },
                    { 48, "5", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5033), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5033), "4" },
                    { 49, "6", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5035), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5034), "1" },
                    { 50, "6", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5036), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5036), "2" },
                    { 51, "6", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5037), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5037), "3" },
                    { 52, "6", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5039), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5038), "4" },
                    { 53, "7", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5040), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5040), "1" },
                    { 54, "7", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5042), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5041), "2" },
                    { 55, "7", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5043), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5043), "3" },
                    { 56, "7", "2", 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5045), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5044), "4" },
                    { 57, "1", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5047), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5047), "1" },
                    { 58, "1", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5049), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5049), "2" },
                    { 59, "1", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5050), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5050), "3" },
                    { 60, "1", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5052), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5051), "4" },
                    { 61, "1", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5053), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5053), "5" },
                    { 62, "1", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5055), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5054), "6" },
                    { 63, "1", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5056), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5056), "7" },
                    { 64, "1", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5058), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5057), "8" },
                    { 65, "2", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5060), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5059), "1" },
                    { 66, "2", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5062), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5062), "2" },
                    { 67, "2", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5064), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5063), "3" },
                    { 68, "2", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5065), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5065), "4" },
                    { 69, "2", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5066), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5066), "5" },
                    { 70, "2", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5068), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5067), "6" },
                    { 71, "2", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5069), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5069), "7" },
                    { 72, "2", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5071), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5070), "8" },
                    { 73, "3", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5072), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5072), "1" },
                    { 74, "3", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5074), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5073), "2" },
                    { 75, "3", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5106), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5105), "3" },
                    { 76, "3", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5107), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5107), "4" },
                    { 77, "3", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5109), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5108), "5" },
                    { 78, "3", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5110), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5110), "6" },
                    { 79, "3", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5111), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5111), "7" },
                    { 80, "3", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5112), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5112), "8" },
                    { 81, "4", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5114), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5114), "1" },
                    { 82, "4", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5115), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5115), "2" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 83, "4", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5116), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5116), "3" },
                    { 84, "4", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5118), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5117), "4" },
                    { 85, "4", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5119), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5119), "5" },
                    { 86, "4", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5120), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5120), "6" },
                    { 87, "4", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5122), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5121), "7" },
                    { 88, "4", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5123), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5122), "8" },
                    { 89, "5", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5124), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5124), "1" },
                    { 90, "5", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5125), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5125), "2" },
                    { 91, "5", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5127), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5126), "3" },
                    { 92, "5", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5128), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5128), "4" },
                    { 93, "5", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5129), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5129), "5" },
                    { 94, "5", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5131), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5130), "6" },
                    { 95, "5", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5132), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5131), "7" },
                    { 96, "5", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5133), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5133), "8" },
                    { 97, "6", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5134), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5134), "1" },
                    { 98, "6", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5136), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5135), "2" },
                    { 99, "6", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5137), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5137), "3" },
                    { 100, "6", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5138), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5138), "4" },
                    { 101, "6", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5139), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5139), "5" },
                    { 102, "6", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5141), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5140), "6" },
                    { 103, "6", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5142), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5141), "7" },
                    { 104, "6", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5143), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5143), "8" },
                    { 105, "7", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5145), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5144), "1" },
                    { 106, "7", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5146), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5145), "2" },
                    { 107, "7", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5147), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5147), "3" },
                    { 108, "7", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5148), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5148), "4" },
                    { 109, "7", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5150), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5149), "5" },
                    { 110, "7", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5151), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5151), "6" },
                    { 111, "7", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5152), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5152), "7" },
                    { 112, "7", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5153), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5153), "8" },
                    { 113, "8", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5155), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5154), "1" },
                    { 114, "8", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5156), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5156), "2" },
                    { 115, "8", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5157), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5157), "3" },
                    { 116, "8", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5159), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5158), "4" },
                    { 117, "8", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5160), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5159), "5" },
                    { 118, "8", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5161), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5161), "6" },
                    { 119, "8", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5162), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5162), "7" },
                    { 120, "8", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5164), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5163), "8" },
                    { 121, "9", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5165), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5165), "1" },
                    { 122, "9", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5166), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5166), "2" },
                    { 123, "9", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5168), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5167), "3" },
                    { 124, "9", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5169), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5168), "4" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 125, "9", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5170), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5170), "5" },
                    { 126, "9", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5171), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5171), "6" },
                    { 127, "9", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5173), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5172), "7" },
                    { 128, "9", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5174), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5173), "8" },
                    { 129, "10", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5175), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5175), "1" },
                    { 130, "10", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5210), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5209), "2" },
                    { 131, "10", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5211), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5211), "3" },
                    { 132, "10", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5213), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5212), "4" },
                    { 133, "10", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5214), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5213), "5" },
                    { 134, "10", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5215), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5215), "6" },
                    { 135, "10", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5217), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5216), "7" },
                    { 136, "10", "1", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5218), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5217), "8" },
                    { 137, "1", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5220), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5219), "1" },
                    { 138, "1", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5221), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5221), "2" },
                    { 139, "1", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5222), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5222), "3" },
                    { 140, "1", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5224), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5223), "4" },
                    { 141, "1", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5225), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5225), "5" },
                    { 142, "1", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5226), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5226), "6" },
                    { 143, "1", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5228), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5227), "7" },
                    { 144, "1", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5229), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5229), "8" },
                    { 145, "2", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5231), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5230), "1" },
                    { 146, "2", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5232), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5232), "2" },
                    { 147, "2", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5234), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5233), "3" },
                    { 148, "2", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5235), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5234), "4" },
                    { 149, "2", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5236), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5236), "5" },
                    { 150, "2", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5238), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5237), "6" },
                    { 151, "2", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5239), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5239), "7" },
                    { 152, "2", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5240), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5240), "8" },
                    { 153, "3", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5242), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5241), "1" },
                    { 154, "3", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5243), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5243), "2" },
                    { 155, "3", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5244), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5244), "3" },
                    { 156, "3", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5246), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5245), "4" },
                    { 157, "3", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5247), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5247), "5" },
                    { 158, "3", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5249), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5248), "6" },
                    { 159, "3", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5250), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5250), "7" },
                    { 160, "3", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5251), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5251), "8" },
                    { 161, "4", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5253), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5252), "1" },
                    { 162, "4", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5254), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5254), "2" },
                    { 163, "4", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5256), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5255), "3" },
                    { 164, "4", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5257), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5256), "4" },
                    { 165, "4", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5258), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5258), "5" },
                    { 166, "4", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5260), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5259), "6" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 167, "4", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5261), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5260), "7" },
                    { 168, "4", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5262), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5262), "8" },
                    { 169, "5", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5264), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5263), "1" },
                    { 170, "5", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5265), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5265), "2" },
                    { 171, "5", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5267), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5266), "3" },
                    { 172, "5", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5268), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5267), "4" },
                    { 173, "5", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5269), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5269), "5" },
                    { 174, "5", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5270), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5270), "6" },
                    { 175, "5", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5272), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5271), "7" },
                    { 176, "5", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5273), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5273), "8" },
                    { 177, "6", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5275), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5274), "1" },
                    { 178, "6", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5276), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5275), "2" },
                    { 179, "6", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5277), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5277), "3" },
                    { 180, "6", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5279), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5278), "4" },
                    { 181, "6", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5280), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5280), "5" },
                    { 182, "6", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5281), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5281), "6" },
                    { 183, "6", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5283), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5282), "7" },
                    { 184, "6", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5284), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5284), "8" },
                    { 185, "7", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5286), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5285), "1" },
                    { 186, "7", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5287), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5287), "2" },
                    { 187, "7", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5288), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5288), "3" },
                    { 188, "7", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5290), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5289), "4" },
                    { 189, "7", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5291), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5291), "5" },
                    { 190, "7", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5292), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5292), "6" },
                    { 191, "7", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5294), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5293), "7" },
                    { 192, "7", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5295), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5295), "8" },
                    { 193, "8", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5297), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5296), "1" },
                    { 194, "8", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5298), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5297), "2" },
                    { 195, "8", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5299), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5299), "3" },
                    { 196, "8", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5301), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5300), "4" },
                    { 197, "8", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5302), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5301), "5" },
                    { 198, "8", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5303), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5303), "6" },
                    { 199, "8", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5305), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5304), "7" },
                    { 200, "8", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5306), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5306), "8" },
                    { 201, "9", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5307), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5307), "1" },
                    { 202, "9", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5340), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5339), "2" },
                    { 203, "9", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5342), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5341), "3" },
                    { 204, "9", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5343), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5343), "4" },
                    { 205, "9", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5344), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5344), "5" },
                    { 206, "9", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5346), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5345), "6" },
                    { 207, "9", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5347), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5346), "7" },
                    { 208, "9", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5348), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5348), "8" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 209, "10", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5350), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5349), "1" },
                    { 210, "10", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5351), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5350), "2" },
                    { 211, "10", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5352), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5352), "3" },
                    { 212, "10", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5353), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5353), "4" },
                    { 213, "10", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5355), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5354), "5" },
                    { 214, "10", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5356), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5356), "6" },
                    { 215, "10", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5357), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5357), "7" },
                    { 216, "10", "2", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5358), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5358), "8" },
                    { 217, "1", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5360), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5360), "1" },
                    { 218, "1", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5361), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5361), "2" },
                    { 219, "1", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5363), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5362), "3" },
                    { 220, "1", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5364), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5363), "4" },
                    { 221, "1", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5365), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5365), "5" },
                    { 222, "1", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5366), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5366), "6" },
                    { 223, "1", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5367), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5367), "7" },
                    { 224, "1", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5369), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5368), "8" },
                    { 225, "2", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5370), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5370), "1" },
                    { 226, "2", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5371), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5371), "2" },
                    { 227, "2", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5373), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5372), "3" },
                    { 228, "2", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5374), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5373), "4" },
                    { 229, "2", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5375), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5375), "5" },
                    { 230, "2", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5376), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5376), "6" },
                    { 231, "2", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5378), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5377), "7" },
                    { 232, "2", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5379), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5378), "8" },
                    { 233, "3", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5380), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5380), "1" },
                    { 234, "3", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5381), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5381), "2" },
                    { 235, "3", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5383), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5382), "3" },
                    { 236, "3", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5384), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5384), "4" },
                    { 237, "3", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5385), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5385), "5" },
                    { 238, "3", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5387), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5386), "6" },
                    { 239, "3", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5388), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5387), "7" },
                    { 240, "3", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5389), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5389), "8" },
                    { 241, "4", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5390), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5390), "1" },
                    { 242, "4", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5392), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5391), "2" },
                    { 243, "4", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5393), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5393), "3" },
                    { 244, "4", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5394), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5394), "4" },
                    { 245, "4", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5395), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5395), "5" },
                    { 246, "4", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5397), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5396), "6" },
                    { 247, "4", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5398), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5397), "7" },
                    { 248, "4", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5399), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5399), "8" },
                    { 249, "5", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5401), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5400), "1" },
                    { 250, "5", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5402), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5401), "2" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 251, "5", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5403), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5403), "3" },
                    { 252, "5", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5404), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5404), "4" },
                    { 253, "5", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5406), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5405), "5" },
                    { 254, "5", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5407), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5406), "6" },
                    { 255, "5", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5408), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5408), "7" },
                    { 256, "5", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5409), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5409), "8" },
                    { 257, "6", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5411), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5410), "1" },
                    { 258, "6", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5444), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5444), "2" },
                    { 259, "6", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5446), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5445), "3" },
                    { 260, "6", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5447), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5447), "4" },
                    { 261, "6", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5449), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5448), "5" },
                    { 262, "6", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5450), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5449), "6" },
                    { 263, "6", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5451), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5451), "7" },
                    { 264, "6", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5453), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5452), "8" },
                    { 265, "7", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5454), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5454), "1" },
                    { 266, "7", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5456), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5455), "2" },
                    { 267, "7", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5457), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5456), "3" },
                    { 268, "7", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5458), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5458), "4" },
                    { 269, "7", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5460), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5459), "5" },
                    { 270, "7", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5461), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5460), "6" },
                    { 271, "7", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5462), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5462), "7" },
                    { 272, "7", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5464), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5463), "8" },
                    { 273, "8", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5465), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5465), "1" },
                    { 274, "8", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5466), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5466), "2" },
                    { 275, "8", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5468), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5467), "3" },
                    { 276, "8", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5469), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5469), "4" },
                    { 277, "8", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5471), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5470), "5" },
                    { 278, "8", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5472), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5472), "6" },
                    { 279, "8", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5474), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5473), "7" },
                    { 280, "8", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5475), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5474), "8" },
                    { 281, "9", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5476), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5476), "1" },
                    { 282, "9", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5478), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5477), "2" },
                    { 283, "9", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5479), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5479), "3" },
                    { 284, "9", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5480), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5480), "4" },
                    { 285, "9", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5482), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5481), "5" },
                    { 286, "9", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5483), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5483), "6" },
                    { 287, "9", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5484), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5484), "7" },
                    { 288, "9", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5486), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5485), "8" },
                    { 289, "10", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5487), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5487), "1" },
                    { 290, "10", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5489), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5488), "2" },
                    { 291, "10", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5490), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5490), "3" },
                    { 292, "10", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5492), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5491), "4" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 293, "10", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5493), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5493), "5" },
                    { 294, "10", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5494), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5494), "6" },
                    { 295, "10", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5496), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5495), "7" },
                    { 296, "10", "3", 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5497), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5497), "8" }
                });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_DATA_ATUALIZACAO", "MOR_DATA_CRIACAO", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID" },
                values: new object[] { 1, "12345678901", new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5647), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5646), "morador1@teste.com", "foto1.jpg", "Morador Teste 1", "123", "1234567890", 1 });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_DATA_ATUALIZACAO", "MOR_DATA_CRIACAO", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID" },
                values: new object[] { 2, "12345678902", new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5651), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5650), "morador2@teste.com", "foto2.jpg", "Morador Teste 2", "456", "1234567891", 2 });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_ATUALIZACAO", "ETG_DATA_CRIACAO", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRP_ID" },
                values: new object[] { 1, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5704), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5704), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5705), null, "Entrega Teste 1", 1, 1, "Observação Teste 1", 1, 1 });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_ATUALIZACAO", "ETG_DATA_CRIACAO", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRP_ID" },
                values: new object[] { 2, new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5707), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5707), new DateTime(2023, 6, 4, 21, 48, 29, 127, DateTimeKind.Local).AddTicks(5708), null, "Entrega Teste 2", 2, 2, "Observação Teste 2", 1, 2 });

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
