using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntregaSegura.Infra.Data.Migrations
{
    public partial class Inicial : Migration
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
                name: "TB_ROLES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ROLES", x => x.Id);
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
                name: "TB_USERS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USERS", x => x.Id);
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
                name: "TB_ROLECLAIMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ROLECLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ROLECLAIMS_TB_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    FUN_USER_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave estrangeira do usuário"),
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
                    table.ForeignKey(
                        name: "FK_FUNCIONARIOS_USERS",
                        column: x => x.FUN_USER_ID,
                        principalTable: "TB_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_USERCLAIMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USERCLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_USERCLAIMS_TB_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_USERLOGINS",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USERLOGINS", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_TB_USERLOGINS_TB_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_USERROLES",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USERROLES", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TB_USERROLES_TB_ROLES_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_ROLES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_USERROLES_TB_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_USERTOKENS",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USERTOKENS", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_TB_USERTOKENS_TB_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    MOR_USER_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave estrangeira do usuário do morador"),
                    MOR_DATA_CRIACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data de criação do morador"),
                    MOR_DATA_ATUALIZACAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Data da última atualização do morador")
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
                    table.ForeignKey(
                        name: "FK_MORADORES_USERS",
                        column: x => x.MOR_USER_ID,
                        principalTable: "TB_USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_ENTREGAS",
                columns: table => new
                {
                    ETG_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave primária da entrega")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TRA_ID = table.Column<int>(type: "int", nullable: false, comment: "Chave estrangeira da transportadora"),
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
                        column: x => x.TRA_ID,
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
                columns: new[] { "CND_ID", "CND_BAIRRO", "CND_CEP", "CND_CIDADE", "CND_CNPJ", "CND_EMAIL", "CND_ESTADO", "CND_LOGRADOURO", "CND_NOME", "CND_NUMERO", "CND_QTD_ANDARES", "CND_QTD_BLOCOS", "CND_QTD_UNIDADES", "CND_TELEFONE" },
                values: new object[] { 1, "Jardim Paulistano", "04567010", "São Paulo", "17540623000150", "contato@boavista.com.br", "SP", "Rua das Acácias", "Condomínio Boa Vista", 55, 4, 2, 7, "1140028922" });

            migrationBuilder.InsertData(
                table: "TB_ROLES",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "1ec5bd79-f06d-496b-8692-5fab2b5b0ac4", "Admin", "ADMIN" },
                    { 2, "74cb8045-95f9-4683-ab59-51de28eceeda", "Sindico", "SINDICO" },
                    { 3, "30ecfea0-9c6a-4437-bd23-ee386177d0c5", "Funcionario", "FUNCIONARIO" },
                    { 4, "574ee489-8249-40e2-a4aa-ea4edf735fd6", "Morador", "MORADOR" }
                });

            migrationBuilder.InsertData(
                table: "TB_TRANSPORTADORAS",
                columns: new[] { "TRA_ID", "TRA_CNPJ", "TRA_EMAIL", "TRA_NOME", "TRA_TELEFONE" },
                values: new object[] { 1, "12345678912347", "transportadora1@teste.com", "Transportadora Teste 1", "1234567894" });

            migrationBuilder.InsertData(
                table: "TB_USERS",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "c7d02d32-8b74-4862-ac28-6d1af82a0578", "admin@localhost", true, false, null, "ADMIN@LOCALHOST", "ADMIN", "AQAAAAEAACcQAAAAEAbuA5DLsgFEzYOvDLAOtxTjbKgEYA6nX3v61HZ1wtaApkWEcL+tQK6Nhy8kWjq8Ug==", null, false, "bf8ff509-8331-42f4-a5d5-e2e9a2228146", false, "admin" },
                    { 2, 0, "5f737e7b-9048-46b7-958d-0036a2926901", "sindico@localhost", true, false, null, "SINDICO@LOCALHOST", "SINDICO", "AQAAAAEAACcQAAAAEDXCuuxUelMQ07gTQjvv8TC0LtDETWDynmXCdSqoYi16BzKj0vBNYrR971hwoXAmEA==", null, false, "f654a5ed-c23b-4688-9936-40e211c403a3", false, "sindico" },
                    { 3, 0, "e0b4a955-609d-452c-a64a-21740941f516", "funcionario@localhost", true, false, null, "FUNCIONARIO@LOCALHOST", "FUNCIONARIO", "AQAAAAEAACcQAAAAEKj23SLanJkUnvvuKnxxVmLTjwi58kVqNCLlI9cLgVcIGhdP6QPPJyjhSGBlXdhoiA==", null, false, "e90757f2-25ac-4340-b2fb-925b194f4735", false, "funcionario" },
                    { 4, 0, "2e75a399-042f-42cb-8f7e-60761f9bb29f", "morador@localhost", true, false, null, "MORADOR@LOCALHOST", "MORADOR", "AQAAAAEAACcQAAAAEK8cIg5kHELCNsgWx1aOMEHtdwEjaVT04XkqiEGZopNwi0IYC/whEh8nfJZkC9WSKw==", null, false, "23aabd2b-3f89-48d2-a606-e030263fb733", false, "morador" }
                });

            migrationBuilder.InsertData(
                table: "TB_FUNCIONARIOS",
                columns: new[] { "FUN_ID", "FUN_CARGO", "FUN_CONDOMINIO_ID", "FUN_CPF", "FUN_DATA_ADMISSAO", "FUN_DATA_DEMISSAO", "FUN_EMAIL", "FUN_NOME", "FUN_TELEFONE", "FUN_USER_ID" },
                values: new object[] { 1, 2, 1, "12345678903", new DateTime(2023, 6, 15, 21, 19, 9, 655, DateTimeKind.Local).AddTicks(6277), null, "funcionario1@teste.com", "Funcionário Teste 1", "1234567892", 2 });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_NUMERO" },
                values: new object[,]
                {
                    { 1, 1, "1", 1, 1 },
                    { 2, 1, "1", 1, 2 },
                    { 3, 1, "1", 1, 3 },
                    { 4, 1, "1", 1, 4 },
                    { 5, 1, "1", 1, 5 },
                    { 6, 1, "1", 1, 6 },
                    { 7, 1, "1", 1, 7 },
                    { 8, 2, "1", 1, 1 },
                    { 9, 2, "1", 1, 2 },
                    { 10, 2, "1", 1, 3 },
                    { 11, 2, "1", 1, 4 },
                    { 12, 2, "1", 1, 5 },
                    { 13, 2, "1", 1, 6 },
                    { 14, 2, "1", 1, 7 },
                    { 15, 3, "1", 1, 1 },
                    { 16, 3, "1", 1, 2 },
                    { 17, 3, "1", 1, 3 },
                    { 18, 3, "1", 1, 4 },
                    { 19, 3, "1", 1, 5 },
                    { 20, 3, "1", 1, 6 },
                    { 21, 3, "1", 1, 7 },
                    { 22, 4, "1", 1, 1 },
                    { 23, 4, "1", 1, 2 },
                    { 24, 4, "1", 1, 3 },
                    { 25, 4, "1", 1, 4 },
                    { 26, 4, "1", 1, 5 },
                    { 27, 4, "1", 1, 6 },
                    { 28, 4, "1", 1, 7 },
                    { 29, 1, "2", 1, 1 },
                    { 30, 1, "2", 1, 2 },
                    { 31, 1, "2", 1, 3 },
                    { 32, 1, "2", 1, 4 },
                    { 33, 1, "2", 1, 5 },
                    { 34, 1, "2", 1, 6 },
                    { 35, 1, "2", 1, 7 },
                    { 36, 2, "2", 1, 1 },
                    { 37, 2, "2", 1, 2 },
                    { 38, 2, "2", 1, 3 },
                    { 39, 2, "2", 1, 4 },
                    { 40, 2, "2", 1, 5 },
                    { 41, 2, "2", 1, 6 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_NUMERO" },
                values: new object[,]
                {
                    { 42, 2, "2", 1, 7 },
                    { 43, 3, "2", 1, 1 },
                    { 44, 3, "2", 1, 2 },
                    { 45, 3, "2", 1, 3 },
                    { 46, 3, "2", 1, 4 },
                    { 47, 3, "2", 1, 5 },
                    { 48, 3, "2", 1, 6 },
                    { 49, 3, "2", 1, 7 },
                    { 50, 4, "2", 1, 1 },
                    { 51, 4, "2", 1, 2 },
                    { 52, 4, "2", 1, 3 },
                    { 53, 4, "2", 1, 4 },
                    { 54, 4, "2", 1, 5 },
                    { 55, 4, "2", 1, 6 },
                    { 56, 4, "2", 1, 7 }
                });

            migrationBuilder.InsertData(
                table: "TB_USERROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID", "MOR_USER_ID" },
                values: new object[] { 1, "12345678901", "morador1@teste.com", "foto1.jpg", "Morador Teste 1", "123", "1234567890", 1, 2 });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRA_ID" },
                values: new object[] { 1, new DateTime(2023, 6, 15, 21, 19, 9, 655, DateTimeKind.Local).AddTicks(6308), null, "Entrega Teste 1", 1, 1, "Observação Teste 1", 3, 1 });

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
                name: "IX_TB_ENTREGAS_TRA_ID",
                table: "TB_ENTREGAS",
                column: "TRA_ID");

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
                name: "IX_TB_FUNCIONARIOS_FUN_USER_ID",
                table: "TB_FUNCIONARIOS",
                column: "FUN_USER_ID",
                unique: true);

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
                name: "IX_TB_MORADORES_MOR_USER_ID",
                table: "TB_MORADORES",
                column: "MOR_USER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_ROLECLAIMS_RoleId",
                table: "TB_ROLECLAIMS",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "TB_ROLES",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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

            migrationBuilder.CreateIndex(
                name: "IX_TB_USERCLAIMS_UserId",
                table: "TB_USERCLAIMS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USERLOGINS_UserId",
                table: "TB_USERLOGINS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USERROLES_RoleId",
                table: "TB_USERROLES",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "TB_USERS",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "TB_USERS",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ENTREGAS");

            migrationBuilder.DropTable(
                name: "TB_ROLECLAIMS");

            migrationBuilder.DropTable(
                name: "TB_USERCLAIMS");

            migrationBuilder.DropTable(
                name: "TB_USERLOGINS");

            migrationBuilder.DropTable(
                name: "TB_USERROLES");

            migrationBuilder.DropTable(
                name: "TB_USERTOKENS");

            migrationBuilder.DropTable(
                name: "TB_TRANSPORTADORAS");

            migrationBuilder.DropTable(
                name: "TB_FUNCIONARIOS");

            migrationBuilder.DropTable(
                name: "TB_MORADORES");

            migrationBuilder.DropTable(
                name: "TB_ROLES");

            migrationBuilder.DropTable(
                name: "TB_UNIDADES");

            migrationBuilder.DropTable(
                name: "TB_USERS");

            migrationBuilder.DropTable(
                name: "TB_CONDOMINIOS");
        }
    }
}
