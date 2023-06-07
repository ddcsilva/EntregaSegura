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
                    UND_NUMERO = table.Column<int>(type: "int", nullable: false, comment: "Número da unidade"),
                    UND_ANDAR = table.Column<int>(type: "int", nullable: false, comment: "Andar da unidade"),
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
                    { 1, "Jardim Paulistano", "04567010", "São Paulo", "17540623000150", new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1467), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1458), "contato@boavista.com.br", "SP", "Rua das Acácias", "Condomínio Boa Vista", 55, 7, 2, 4, "1140028922" },
                    { 2, "Copacabana", "22021001", "Rio de Janeiro", "27004428000169", new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1474), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1474), "contato@raiodesol.com.br", "RJ", "Avenida Atlântica", "Condomínio Raio de Sol", 700, 10, 3, 8, "2130033211" }
                });

            migrationBuilder.InsertData(
                table: "TB_TRANSPORTADORAS",
                columns: new[] { "TRA_ID", "TRA_CNPJ", "TRA_DATA_ATUALIZACAO", "TRA_DATA_CRIACAO", "TRA_EMAIL", "TRA_NOME", "TRA_TELEFONE" },
                values: new object[,]
                {
                    { 1, "12345678912347", new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2328), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2328), "transportadora1@teste.com", "Transportadora Teste 1", "1234567894" },
                    { 2, "12345678912348", new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2330), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2330), "transportadora2@teste.com", "Transportadora Teste 2", "1234567895" }
                });

            migrationBuilder.InsertData(
                table: "TB_FUNCIONARIOS",
                columns: new[] { "FUN_ID", "FUN_CPF", "FUN_CARGO", "FUN_CONDOMINIO_ID", "FUN_DATA_ADMISSAO", "FUN_DATA_ATUALIZACAO", "FUN_DATA_CRIACAO", "FUN_DATA_DEMISSAO", "FUN_EMAIL", "FUN_NOME", "FUN_TELEFONE" },
                values: new object[,]
                {
                    { 1, "12345678903", 3, 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2312), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2312), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2311), null, "funcionario1@teste.com", "Funcionario Teste 1", "1234567892" },
                    { 2, "12345678904", 2, 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2315), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2314), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2314), null, "funcionario2@teste.com", "Funcionario Teste 2", "1234567893" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 1, 1, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1576), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1575), 1 },
                    { 2, 1, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1588), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1587), 2 },
                    { 3, 1, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1590), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1589), 3 },
                    { 4, 1, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1591), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1591), 4 },
                    { 5, 2, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1593), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1592), 1 },
                    { 6, 2, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1596), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1595), 2 },
                    { 7, 2, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1597), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1597), 3 },
                    { 8, 2, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1599), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1598), 4 },
                    { 9, 3, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1600), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1600), 1 },
                    { 10, 3, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1602), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1602), 2 },
                    { 11, 3, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1604), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1603), 3 },
                    { 12, 3, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1605), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1605), 4 },
                    { 13, 4, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1607), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1606), 1 },
                    { 14, 4, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1608), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1608), 2 },
                    { 15, 4, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1609), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1609), 3 },
                    { 16, 4, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1611), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1610), 4 },
                    { 17, 5, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1612), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1612), 1 },
                    { 18, 5, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1614), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1614), 2 },
                    { 19, 5, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1616), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1615), 3 },
                    { 20, 5, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1617), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1617), 4 },
                    { 21, 6, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1619), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1618), 1 },
                    { 22, 6, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1620), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1619), 2 },
                    { 23, 6, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1621), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1621), 3 },
                    { 24, 6, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1623), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1622), 4 },
                    { 25, 7, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1624), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1624), 1 },
                    { 26, 7, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1625), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1625), 2 },
                    { 27, 7, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1667), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1667), 3 },
                    { 28, 7, "1", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1669), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1668), 4 },
                    { 29, 1, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1671), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1670), 1 },
                    { 30, 1, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1672), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1671), 2 },
                    { 31, 1, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1673), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1673), 3 },
                    { 32, 1, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1674), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1674), 4 },
                    { 33, 2, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1676), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1676), 1 },
                    { 34, 2, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1678), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1678), 2 },
                    { 35, 2, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1680), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1679), 3 },
                    { 36, 2, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1681), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1680), 4 },
                    { 37, 3, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1682), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1682), 1 },
                    { 38, 3, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1684), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1683), 2 },
                    { 39, 3, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1685), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1685), 3 },
                    { 40, 3, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1686), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1686), 4 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 41, 4, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1688), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1688), 1 },
                    { 42, 4, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1689), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1689), 2 },
                    { 43, 4, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1691), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1690), 3 },
                    { 44, 4, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1692), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1691), 4 },
                    { 45, 5, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1693), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1693), 1 },
                    { 46, 5, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1695), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1694), 2 },
                    { 47, 5, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1696), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1695), 3 },
                    { 48, 5, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1697), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1697), 4 },
                    { 49, 6, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1698), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1698), 1 },
                    { 50, 6, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1700), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1699), 2 },
                    { 51, 6, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1701), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1701), 3 },
                    { 52, 6, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1702), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1702), 4 },
                    { 53, 7, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1704), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1703), 1 },
                    { 54, 7, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1705), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1705), 2 },
                    { 55, 7, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1706), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1706), 3 },
                    { 56, 7, "2", 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1707), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1707), 4 },
                    { 57, 1, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1710), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1709), 1 },
                    { 58, 1, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1711), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1711), 2 },
                    { 59, 1, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1713), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1712), 3 },
                    { 60, 1, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1714), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1714), 4 },
                    { 61, 1, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1715), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1715), 5 },
                    { 62, 1, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1717), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1716), 6 },
                    { 63, 1, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1718), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1717), 7 },
                    { 64, 1, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1719), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1719), 8 },
                    { 65, 2, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1721), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1720), 1 },
                    { 66, 2, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1723), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1723), 2 },
                    { 67, 2, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1725), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1724), 3 },
                    { 68, 2, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1726), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1726), 4 },
                    { 69, 2, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1727), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1727), 5 },
                    { 70, 2, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1729), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1728), 6 },
                    { 71, 2, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1730), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1729), 7 },
                    { 72, 2, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1731), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1731), 8 },
                    { 73, 3, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1733), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1732), 1 },
                    { 74, 3, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1734), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1733), 2 },
                    { 75, 3, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1735), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1735), 3 },
                    { 76, 3, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1736), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1736), 4 },
                    { 77, 3, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1738), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1737), 5 },
                    { 78, 3, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1739), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1738), 6 },
                    { 79, 3, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1740), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1740), 7 },
                    { 80, 3, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1741), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1741), 8 },
                    { 81, 4, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1743), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1742), 1 },
                    { 82, 4, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1744), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1744), 2 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 83, 4, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1745), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1745), 3 },
                    { 84, 4, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1746), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1746), 4 },
                    { 85, 4, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1748), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1747), 5 },
                    { 86, 4, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1780), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1780), 6 },
                    { 87, 4, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1782), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1781), 7 },
                    { 88, 4, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1783), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1783), 8 },
                    { 89, 5, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1784), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1784), 1 },
                    { 90, 5, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1786), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1785), 2 },
                    { 91, 5, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1787), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1787), 3 },
                    { 92, 5, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1788), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1788), 4 },
                    { 93, 5, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1789), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1789), 5 },
                    { 94, 5, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1791), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1790), 6 },
                    { 95, 5, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1792), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1792), 7 },
                    { 96, 5, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1794), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1793), 8 },
                    { 97, 6, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1795), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1795), 1 },
                    { 98, 6, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1796), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1796), 2 },
                    { 99, 6, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1798), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1797), 3 },
                    { 100, 6, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1799), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1798), 4 },
                    { 101, 6, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1800), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1800), 5 },
                    { 102, 6, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1801), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1801), 6 },
                    { 103, 6, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1803), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1802), 7 },
                    { 104, 6, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1804), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1803), 8 },
                    { 105, 7, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1805), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1805), 1 },
                    { 106, 7, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1807), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1806), 2 },
                    { 107, 7, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1808), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1807), 3 },
                    { 108, 7, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1809), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1809), 4 },
                    { 109, 7, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1810), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1810), 5 },
                    { 110, 7, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1811), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1811), 6 },
                    { 111, 7, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1813), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1812), 7 },
                    { 112, 7, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1814), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1814), 8 },
                    { 113, 8, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1816), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1815), 1 },
                    { 114, 8, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1817), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1816), 2 },
                    { 115, 8, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1818), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1818), 3 },
                    { 116, 8, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1819), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1819), 4 },
                    { 117, 8, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1821), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1820), 5 },
                    { 118, 8, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1822), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1821), 6 },
                    { 119, 8, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1823), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1823), 7 },
                    { 120, 8, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1824), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1824), 8 },
                    { 121, 9, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1826), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1825), 1 },
                    { 122, 9, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1827), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1826), 2 },
                    { 123, 9, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1828), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1828), 3 },
                    { 124, 9, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1829), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1829), 4 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 125, 9, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1831), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1830), 5 },
                    { 126, 9, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1832), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1832), 6 },
                    { 127, 9, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1833), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1833), 7 },
                    { 128, 9, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1834), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1834), 8 },
                    { 129, 10, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1836), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1835), 1 },
                    { 130, 10, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1838), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1838), 2 },
                    { 131, 10, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1839), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1839), 3 },
                    { 132, 10, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1841), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1840), 4 },
                    { 133, 10, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1842), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1841), 5 },
                    { 134, 10, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1843), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1843), 6 },
                    { 135, 10, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1844), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1844), 7 },
                    { 136, 10, "1", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1845), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1845), 8 },
                    { 137, 1, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1847), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1847), 1 },
                    { 138, 1, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1848), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1848), 2 },
                    { 139, 1, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1850), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1849), 3 },
                    { 140, 1, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1882), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1882), 4 },
                    { 141, 1, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1884), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1884), 5 },
                    { 142, 1, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1885), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1885), 6 },
                    { 143, 1, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1887), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1886), 7 },
                    { 144, 1, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1888), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1887), 8 },
                    { 145, 2, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1889), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1889), 1 },
                    { 146, 2, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1891), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1890), 2 },
                    { 147, 2, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1892), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1892), 3 },
                    { 148, 2, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1893), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1893), 4 },
                    { 149, 2, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1895), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1894), 5 },
                    { 150, 2, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1896), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1895), 6 },
                    { 151, 2, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1897), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1897), 7 },
                    { 152, 2, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1898), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1898), 8 },
                    { 153, 3, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1900), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1899), 1 },
                    { 154, 3, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1901), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1901), 2 },
                    { 155, 3, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1902), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1902), 3 },
                    { 156, 3, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1904), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1903), 4 },
                    { 157, 3, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1905), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1905), 5 },
                    { 158, 3, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1906), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1906), 6 },
                    { 159, 3, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1907), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1907), 7 },
                    { 160, 3, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1909), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1908), 8 },
                    { 161, 4, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1910), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1910), 1 },
                    { 162, 4, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1912), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1911), 2 },
                    { 163, 4, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1913), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1912), 3 },
                    { 164, 4, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1914), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1914), 4 },
                    { 165, 4, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1916), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1915), 5 },
                    { 166, 4, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1917), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1916), 6 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 167, 4, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1918), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1918), 7 },
                    { 168, 4, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1919), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1919), 8 },
                    { 169, 5, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1921), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1920), 1 },
                    { 170, 5, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1922), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1922), 2 },
                    { 171, 5, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1923), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1923), 3 },
                    { 172, 5, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1924), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1924), 4 },
                    { 173, 5, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1926), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1925), 5 },
                    { 174, 5, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1927), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1927), 6 },
                    { 175, 5, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1928), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1928), 7 },
                    { 176, 5, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1929), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1929), 8 },
                    { 177, 6, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1931), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1930), 1 },
                    { 178, 6, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1932), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1932), 2 },
                    { 179, 6, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1933), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1933), 3 },
                    { 180, 6, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1935), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1934), 4 },
                    { 181, 6, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1936), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1935), 5 },
                    { 182, 6, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1937), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1937), 6 },
                    { 183, 6, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1938), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1938), 7 },
                    { 184, 6, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1940), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1939), 8 },
                    { 185, 7, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1941), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1941), 1 },
                    { 186, 7, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1942), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1942), 2 },
                    { 187, 7, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1944), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1943), 3 },
                    { 188, 7, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1945), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1945), 4 },
                    { 189, 7, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1946), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1946), 5 },
                    { 190, 7, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1947), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1947), 6 },
                    { 191, 7, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1949), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1948), 7 },
                    { 192, 7, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1950), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1950), 8 },
                    { 193, 8, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1951), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1951), 1 },
                    { 194, 8, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1953), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1952), 2 },
                    { 195, 8, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1954), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1954), 3 },
                    { 196, 8, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1955), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1955), 4 },
                    { 197, 8, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1956), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1956), 5 },
                    { 198, 8, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1958), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1957), 6 },
                    { 199, 8, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1959), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1959), 7 },
                    { 200, 8, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1960), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1960), 8 },
                    { 201, 9, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1962), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1961), 1 },
                    { 202, 9, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1963), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1962), 2 },
                    { 203, 9, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1964), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1964), 3 },
                    { 204, 9, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1965), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1965), 4 },
                    { 205, 9, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1967), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1966), 5 },
                    { 206, 9, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1968), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1968), 6 },
                    { 207, 9, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1969), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1969), 7 },
                    { 208, 9, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1970), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1970), 8 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 209, 10, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1972), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1971), 1 },
                    { 210, 10, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1973), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1973), 2 },
                    { 211, 10, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1974), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1974), 3 },
                    { 212, 10, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1976), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(1975), 4 },
                    { 213, 10, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2009), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2007), 5 },
                    { 214, 10, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2010), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2010), 6 },
                    { 215, 10, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2012), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2011), 7 },
                    { 216, 10, "2", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2013), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2013), 8 },
                    { 217, 1, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2015), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2014), 1 },
                    { 218, 1, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2016), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2016), 2 },
                    { 219, 1, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2017), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2017), 3 },
                    { 220, 1, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2018), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2018), 4 },
                    { 221, 1, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2020), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2019), 5 },
                    { 222, 1, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2021), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2020), 6 },
                    { 223, 1, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2022), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2022), 7 },
                    { 224, 1, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2023), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2023), 8 },
                    { 225, 2, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2025), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2024), 1 },
                    { 226, 2, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2026), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2026), 2 },
                    { 227, 2, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2027), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2027), 3 },
                    { 228, 2, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2029), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2028), 4 },
                    { 229, 2, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2030), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2029), 5 },
                    { 230, 2, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2031), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2031), 6 },
                    { 231, 2, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2032), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2032), 7 },
                    { 232, 2, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2034), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2033), 8 },
                    { 233, 3, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2035), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2035), 1 },
                    { 234, 3, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2036), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2036), 2 },
                    { 235, 3, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2037), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2037), 3 },
                    { 236, 3, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2039), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2038), 4 },
                    { 237, 3, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2040), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2039), 5 },
                    { 238, 3, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2041), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2041), 6 },
                    { 239, 3, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2042), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2042), 7 },
                    { 240, 3, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2044), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2043), 8 },
                    { 241, 4, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2045), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2045), 1 },
                    { 242, 4, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2046), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2046), 2 },
                    { 243, 4, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2048), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2047), 3 },
                    { 244, 4, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2049), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2049), 4 },
                    { 245, 4, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2050), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2050), 5 },
                    { 246, 4, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2051), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2051), 6 },
                    { 247, 4, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2053), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2052), 7 },
                    { 248, 4, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2054), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2054), 8 },
                    { 249, 5, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2055), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2055), 1 },
                    { 250, 5, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2057), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2056), 2 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 251, 5, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2058), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2058), 3 },
                    { 252, 5, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2059), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2059), 4 },
                    { 253, 5, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2061), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2060), 5 },
                    { 254, 5, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2062), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2062), 6 },
                    { 255, 5, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2063), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2063), 7 },
                    { 256, 5, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2065), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2064), 8 },
                    { 257, 6, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2066), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2066), 1 },
                    { 258, 6, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2100), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2100), 2 },
                    { 259, 6, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2102), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2101), 3 },
                    { 260, 6, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2103), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2103), 4 },
                    { 261, 6, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2104), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2104), 5 },
                    { 262, 6, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2106), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2105), 6 },
                    { 263, 6, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2107), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2106), 7 },
                    { 264, 6, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2108), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2108), 8 },
                    { 265, 7, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2110), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2109), 1 },
                    { 266, 7, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2111), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2110), 2 },
                    { 267, 7, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2112), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2112), 3 },
                    { 268, 7, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2114), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2113), 4 },
                    { 269, 7, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2115), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2114), 5 },
                    { 270, 7, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2116), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2116), 6 },
                    { 271, 7, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2117), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2117), 7 },
                    { 272, 7, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2119), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2118), 8 },
                    { 273, 8, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2120), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2120), 1 },
                    { 274, 8, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2121), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2121), 2 },
                    { 275, 8, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2122), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2122), 3 },
                    { 276, 8, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2124), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2123), 4 },
                    { 277, 8, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2125), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2125), 5 },
                    { 278, 8, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2126), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2126), 6 },
                    { 279, 8, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2128), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2127), 7 },
                    { 280, 8, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2129), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2128), 8 },
                    { 281, 9, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2130), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2130), 1 },
                    { 282, 9, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2131), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2131), 2 },
                    { 283, 9, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2133), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2132), 3 },
                    { 284, 9, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2134), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2134), 4 },
                    { 285, 9, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2135), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2135), 5 },
                    { 286, 9, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2136), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2136), 6 },
                    { 287, 9, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2138), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2137), 7 },
                    { 288, 9, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2139), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2139), 8 },
                    { 289, 10, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2140), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2140), 1 },
                    { 290, 10, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2142), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2141), 2 },
                    { 291, 10, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2143), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2142), 3 },
                    { 292, 10, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2144), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2144), 4 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 293, 10, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2145), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2145), 5 },
                    { 294, 10, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2147), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2146), 6 },
                    { 295, 10, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2148), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2147), 7 },
                    { 296, 10, "3", 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2149), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2149), 8 }
                });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_DATA_ATUALIZACAO", "MOR_DATA_CRIACAO", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID" },
                values: new object[] { 1, "12345678901", new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2294), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2294), "morador1@teste.com", "foto1.jpg", "Morador Teste 1", "123", "1234567890", 1 });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_DATA_ATUALIZACAO", "MOR_DATA_CRIACAO", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID" },
                values: new object[] { 2, "12345678902", new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2298), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2297), "morador2@teste.com", "foto2.jpg", "Morador Teste 2", "456", "1234567891", 2 });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_ATUALIZACAO", "ETG_DATA_CRIACAO", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRP_ID" },
                values: new object[] { 1, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2342), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2342), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2342), null, "Entrega Teste 1", 1, 1, "Observação Teste 1", 1, 1 });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_ATUALIZACAO", "ETG_DATA_CRIACAO", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRP_ID" },
                values: new object[] { 2, new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2346), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2345), new DateTime(2023, 6, 6, 22, 16, 46, 775, DateTimeKind.Local).AddTicks(2346), null, "Entrega Teste 2", 2, 2, "Observação Teste 2", 1, 2 });

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
