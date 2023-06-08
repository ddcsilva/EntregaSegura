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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                    { 1, "Jardim Paulistano", "04567010", "São Paulo", "17540623000150", new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(4879), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(4870), "contato@boavista.com.br", "SP", "Rua das Acácias", "Condomínio Boa Vista", 55, 7, 2, 4, "1140028922" },
                    { 2, "Copacabana", "22021001", "Rio de Janeiro", "27004428000169", new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(4886), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(4885), "contato@raiodesol.com.br", "RJ", "Avenida Atlântica", "Condomínio Raio de Sol", 700, 10, 3, 8, "2130033211" }
                });

            migrationBuilder.InsertData(
                table: "TB_TRANSPORTADORAS",
                columns: new[] { "TRA_ID", "TRA_CNPJ", "TRA_DATA_ATUALIZACAO", "TRA_DATA_CRIACAO", "TRA_EMAIL", "TRA_NOME", "TRA_TELEFONE" },
                values: new object[,]
                {
                    { 1, "12345678912347", new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5780), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5780), "transportadora1@teste.com", "Transportadora Teste 1", "1234567894" },
                    { 2, "12345678912348", new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5782), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5782), "transportadora2@teste.com", "Transportadora Teste 2", "1234567895" }
                });

            migrationBuilder.InsertData(
                table: "TB_FUNCIONARIOS",
                columns: new[] { "FUN_ID", "FUN_CPF", "FUN_CARGO", "FUN_CONDOMINIO_ID", "FUN_DATA_ADMISSAO", "FUN_DATA_ATUALIZACAO", "FUN_DATA_CRIACAO", "FUN_DATA_DEMISSAO", "FUN_EMAIL", "FUN_NOME", "FUN_TELEFONE" },
                values: new object[,]
                {
                    { 1, "12345678903", 3, 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5765), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5764), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5764), null, "funcionario1@teste.com", "Funcionario Teste 1", "1234567892" },
                    { 2, "12345678904", 2, 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5768), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5767), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5767), null, "funcionario2@teste.com", "Funcionario Teste 2", "1234567893" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 1, 1, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5018), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5017), 1 },
                    { 2, 1, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5029), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5028), 2 },
                    { 3, 1, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5030), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5030), 3 },
                    { 4, 1, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5032), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5031), 4 },
                    { 5, 2, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5033), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5033), 1 },
                    { 6, 2, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5037), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5036), 2 },
                    { 7, 2, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5038), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5038), 3 },
                    { 8, 2, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5040), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5039), 4 },
                    { 9, 3, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5041), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5041), 1 },
                    { 10, 3, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5043), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5043), 2 },
                    { 11, 3, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5045), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5044), 3 },
                    { 12, 3, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5046), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5046), 4 },
                    { 13, 4, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5047), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5047), 1 },
                    { 14, 4, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5049), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5048), 2 },
                    { 15, 4, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5050), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5050), 3 },
                    { 16, 4, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5051), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5051), 4 },
                    { 17, 5, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5053), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5052), 1 },
                    { 18, 5, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5055), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5054), 2 },
                    { 19, 5, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5056), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5056), 3 },
                    { 20, 5, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5057), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5057), 4 },
                    { 21, 6, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5059), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5059), 1 },
                    { 22, 6, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5060), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5060), 2 },
                    { 23, 6, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5094), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5093), 3 },
                    { 24, 6, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5096), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5096), 4 },
                    { 25, 7, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5097), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5097), 1 },
                    { 26, 7, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5099), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5098), 2 },
                    { 27, 7, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5100), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5100), 3 },
                    { 28, 7, "1", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5101), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5101), 4 },
                    { 29, 1, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5103), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5102), 1 },
                    { 30, 1, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5104), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5104), 2 },
                    { 31, 1, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5105), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5105), 3 },
                    { 32, 1, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5107), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5106), 4 },
                    { 33, 2, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5108), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5108), 1 },
                    { 34, 2, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5110), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5110), 2 },
                    { 35, 2, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5112), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5111), 3 },
                    { 36, 2, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5113), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5113), 4 },
                    { 37, 3, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5114), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5114), 1 },
                    { 38, 3, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5116), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5115), 2 },
                    { 39, 3, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5117), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5116), 3 },
                    { 40, 3, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5118), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5118), 4 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 41, 4, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5120), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5119), 1 },
                    { 42, 4, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5121), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5120), 2 },
                    { 43, 4, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5122), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5122), 3 },
                    { 44, 4, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5123), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5123), 4 },
                    { 45, 5, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5125), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5124), 1 },
                    { 46, 5, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5126), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5126), 2 },
                    { 47, 5, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5127), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5127), 3 },
                    { 48, 5, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5128), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5128), 4 },
                    { 49, 6, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5130), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5130), 1 },
                    { 50, 6, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5131), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5131), 2 },
                    { 51, 6, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5133), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5132), 3 },
                    { 52, 6, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5134), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5133), 4 },
                    { 53, 7, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5135), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5135), 1 },
                    { 54, 7, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5136), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5136), 2 },
                    { 55, 7, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5138), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5137), 3 },
                    { 56, 7, "2", 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5139), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5139), 4 },
                    { 57, 1, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5141), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5141), 1 },
                    { 58, 1, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5143), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5142), 2 },
                    { 59, 1, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5144), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5144), 3 },
                    { 60, 1, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5145), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5145), 4 },
                    { 61, 1, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5147), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5146), 5 },
                    { 62, 1, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5148), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5148), 6 },
                    { 63, 1, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5149), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5149), 7 },
                    { 64, 1, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5150), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5150), 8 },
                    { 65, 2, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5152), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5152), 1 },
                    { 66, 2, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5154), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5154), 2 },
                    { 67, 2, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5156), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5155), 3 },
                    { 68, 2, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5157), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5157), 4 },
                    { 69, 2, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5158), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5158), 5 },
                    { 70, 2, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5159), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5159), 6 },
                    { 71, 2, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5161), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5160), 7 },
                    { 72, 2, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5162), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5162), 8 },
                    { 73, 3, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5163), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5163), 1 },
                    { 74, 3, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5165), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5164), 2 },
                    { 75, 3, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5166), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5166), 3 },
                    { 76, 3, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5167), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5167), 4 },
                    { 77, 3, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5168), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5168), 5 },
                    { 78, 3, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5170), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5169), 6 },
                    { 79, 3, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5171), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5170), 7 },
                    { 80, 3, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5172), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5172), 8 },
                    { 81, 4, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5173), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5173), 1 },
                    { 82, 4, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5205), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5204), 2 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 83, 4, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5206), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5206), 3 },
                    { 84, 4, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5208), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5207), 4 },
                    { 85, 4, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5209), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5208), 5 },
                    { 86, 4, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5210), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5210), 6 },
                    { 87, 4, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5211), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5211), 7 },
                    { 88, 4, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5212), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5212), 8 },
                    { 89, 5, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5214), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5214), 1 },
                    { 90, 5, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5215), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5215), 2 },
                    { 91, 5, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5216), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5216), 3 },
                    { 92, 5, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5218), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5217), 4 },
                    { 93, 5, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5219), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5219), 5 },
                    { 94, 5, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5220), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5220), 6 },
                    { 95, 5, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5221), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5221), 7 },
                    { 96, 5, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5223), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5222), 8 },
                    { 97, 6, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5224), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5224), 1 },
                    { 98, 6, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5225), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5225), 2 },
                    { 99, 6, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5226), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5226), 3 },
                    { 100, 6, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5228), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5227), 4 },
                    { 101, 6, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5229), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5229), 5 },
                    { 102, 6, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5230), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5230), 6 },
                    { 103, 6, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5231), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5231), 7 },
                    { 104, 6, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5233), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5232), 8 },
                    { 105, 7, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5234), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5234), 1 },
                    { 106, 7, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5235), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5235), 2 },
                    { 107, 7, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5237), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5236), 3 },
                    { 108, 7, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5238), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5237), 4 },
                    { 109, 7, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5239), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5239), 5 },
                    { 110, 7, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5240), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5240), 6 },
                    { 111, 7, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5241), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5241), 7 },
                    { 112, 7, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5243), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5242), 8 },
                    { 113, 8, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5244), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5244), 1 },
                    { 114, 8, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5245), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5245), 2 },
                    { 115, 8, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5247), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5246), 3 },
                    { 116, 8, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5248), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5247), 4 },
                    { 117, 8, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5249), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5249), 5 },
                    { 118, 8, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5250), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5250), 6 },
                    { 119, 8, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5251), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5251), 7 },
                    { 120, 8, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5253), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5252), 8 },
                    { 121, 9, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5254), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5254), 1 },
                    { 122, 9, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5255), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5255), 2 },
                    { 123, 9, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5257), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5256), 3 },
                    { 124, 9, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5258), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5257), 4 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 125, 9, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5259), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5259), 5 },
                    { 126, 9, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5260), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5260), 6 },
                    { 127, 9, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5261), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5261), 7 },
                    { 128, 9, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5263), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5262), 8 },
                    { 129, 10, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5264), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5264), 1 },
                    { 130, 10, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5266), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5266), 2 },
                    { 131, 10, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5268), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5267), 3 },
                    { 132, 10, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5269), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5268), 4 },
                    { 133, 10, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5270), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5270), 5 },
                    { 134, 10, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5271), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5271), 6 },
                    { 135, 10, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5272), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5272), 7 },
                    { 136, 10, "1", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5305), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5304), 8 },
                    { 137, 1, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5307), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5307), 1 },
                    { 138, 1, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5309), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5308), 2 },
                    { 139, 1, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5310), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5310), 3 },
                    { 140, 1, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5311), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5311), 4 },
                    { 141, 1, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5312), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5312), 5 },
                    { 142, 1, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5314), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5313), 6 },
                    { 143, 1, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5315), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5315), 7 },
                    { 144, 1, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5316), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5316), 8 },
                    { 145, 2, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5318), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5317), 1 },
                    { 146, 2, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5319), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5318), 2 },
                    { 147, 2, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5320), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5320), 3 },
                    { 148, 2, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5321), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5321), 4 },
                    { 149, 2, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5322), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5322), 5 },
                    { 150, 2, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5324), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5323), 6 },
                    { 151, 2, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5325), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5325), 7 },
                    { 152, 2, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5326), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5326), 8 },
                    { 153, 3, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5328), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5327), 1 },
                    { 154, 3, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5329), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5328), 2 },
                    { 155, 3, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5330), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5330), 3 },
                    { 156, 3, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5331), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5331), 4 },
                    { 157, 3, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5333), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5332), 5 },
                    { 158, 3, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5334), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5333), 6 },
                    { 159, 3, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5335), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5335), 7 },
                    { 160, 3, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5336), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5336), 8 },
                    { 161, 4, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5338), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5337), 1 },
                    { 162, 4, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5339), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5339), 2 },
                    { 163, 4, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5340), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5340), 3 },
                    { 164, 4, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5342), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5341), 4 },
                    { 165, 4, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5343), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5343), 5 },
                    { 166, 4, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5344), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5344), 6 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 167, 4, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5345), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5345), 7 },
                    { 168, 4, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5347), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5346), 8 },
                    { 169, 5, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5348), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5348), 1 },
                    { 170, 5, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5349), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5349), 2 },
                    { 171, 5, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5350), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5350), 3 },
                    { 172, 5, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5352), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5351), 4 },
                    { 173, 5, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5353), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5353), 5 },
                    { 174, 5, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5354), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5354), 6 },
                    { 175, 5, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5355), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5355), 7 },
                    { 176, 5, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5357), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5356), 8 },
                    { 177, 6, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5358), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5358), 1 },
                    { 178, 6, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5359), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5359), 2 },
                    { 179, 6, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5360), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5360), 3 },
                    { 180, 6, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5362), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5361), 4 },
                    { 181, 6, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5363), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5362), 5 },
                    { 182, 6, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5364), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5364), 6 },
                    { 183, 6, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5365), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5365), 7 },
                    { 184, 6, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5366), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5366), 8 },
                    { 185, 7, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5368), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5367), 1 },
                    { 186, 7, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5369), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5369), 2 },
                    { 187, 7, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5370), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5370), 3 },
                    { 188, 7, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5372), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5371), 4 },
                    { 189, 7, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5373), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5372), 5 },
                    { 190, 7, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5374), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5374), 6 },
                    { 191, 7, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5375), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5375), 7 },
                    { 192, 7, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5377), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5376), 8 },
                    { 193, 8, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5378), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5378), 1 },
                    { 194, 8, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5379), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5379), 2 },
                    { 195, 8, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5380), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5380), 3 },
                    { 196, 8, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5382), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5381), 4 },
                    { 197, 8, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5383), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5382), 5 },
                    { 198, 8, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5384), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5384), 6 },
                    { 199, 8, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5385), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5385), 7 },
                    { 200, 8, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5386), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5386), 8 },
                    { 201, 9, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5388), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5387), 1 },
                    { 202, 9, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5389), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5389), 2 },
                    { 203, 9, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5390), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5390), 3 },
                    { 204, 9, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5392), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5391), 4 },
                    { 205, 9, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5393), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5392), 5 },
                    { 206, 9, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5394), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5394), 6 },
                    { 207, 9, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5395), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5395), 7 },
                    { 208, 9, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5396), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5396), 8 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 209, 10, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5429), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5428), 1 },
                    { 210, 10, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5430), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5430), 2 },
                    { 211, 10, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5432), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5431), 3 },
                    { 212, 10, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5433), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5432), 4 },
                    { 213, 10, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5434), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5434), 5 },
                    { 214, 10, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5435), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5435), 6 },
                    { 215, 10, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5437), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5436), 7 },
                    { 216, 10, "2", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5438), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5437), 8 },
                    { 217, 1, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5439), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5439), 1 },
                    { 218, 1, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5441), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5440), 2 },
                    { 219, 1, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5442), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5441), 3 },
                    { 220, 1, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5443), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5443), 4 },
                    { 221, 1, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5444), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5444), 5 },
                    { 222, 1, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5445), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5445), 6 },
                    { 223, 1, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5447), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5446), 7 },
                    { 224, 1, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5448), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5448), 8 },
                    { 225, 2, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5449), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5449), 1 },
                    { 226, 2, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5450), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5450), 2 },
                    { 227, 2, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5452), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5451), 3 },
                    { 228, 2, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5453), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5453), 4 },
                    { 229, 2, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5454), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5454), 5 },
                    { 230, 2, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5456), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5455), 6 },
                    { 231, 2, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5457), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5456), 7 },
                    { 232, 2, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5458), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5458), 8 },
                    { 233, 3, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5459), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5459), 1 },
                    { 234, 3, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5461), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5460), 2 },
                    { 235, 3, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5462), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5461), 3 },
                    { 236, 3, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5463), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5463), 4 },
                    { 237, 3, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5464), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5464), 5 },
                    { 238, 3, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5465), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5465), 6 },
                    { 239, 3, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5467), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5466), 7 },
                    { 240, 3, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5468), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5467), 8 },
                    { 241, 4, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5469), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5469), 1 },
                    { 242, 4, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5470), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5470), 2 },
                    { 243, 4, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5472), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5471), 3 },
                    { 244, 4, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5473), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5472), 4 },
                    { 245, 4, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5474), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5474), 5 },
                    { 246, 4, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5475), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5475), 6 },
                    { 247, 4, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5476), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5476), 7 },
                    { 248, 4, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5478), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5477), 8 },
                    { 249, 5, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5479), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5479), 1 },
                    { 250, 5, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5480), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5480), 2 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 251, 5, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5481), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5481), 3 },
                    { 252, 5, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5483), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5482), 4 },
                    { 253, 5, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5484), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5484), 5 },
                    { 254, 5, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5485), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5485), 6 },
                    { 255, 5, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5486), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5486), 7 },
                    { 256, 5, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5488), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5487), 8 },
                    { 257, 6, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5489), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5489), 1 },
                    { 258, 6, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5526), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5526), 2 },
                    { 259, 6, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5528), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5528), 3 },
                    { 260, 6, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5529), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5529), 4 },
                    { 261, 6, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5530), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5530), 5 },
                    { 262, 6, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5532), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5531), 6 },
                    { 263, 6, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5533), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5533), 7 },
                    { 264, 6, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5534), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5534), 8 },
                    { 265, 7, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5536), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5535), 1 },
                    { 266, 7, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5537), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5537), 2 },
                    { 267, 7, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5538), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5538), 3 },
                    { 268, 7, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5539), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5539), 4 },
                    { 269, 7, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5541), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5540), 5 },
                    { 270, 7, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5542), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5541), 6 },
                    { 271, 7, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5543), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5543), 7 },
                    { 272, 7, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5544), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5544), 8 },
                    { 273, 8, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5546), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5545), 1 },
                    { 274, 8, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5547), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5546), 2 },
                    { 275, 8, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5548), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5548), 3 },
                    { 276, 8, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5549), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5549), 4 },
                    { 277, 8, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5551), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5550), 5 },
                    { 278, 8, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5552), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5551), 6 },
                    { 279, 8, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5553), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5553), 7 },
                    { 280, 8, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5554), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5554), 8 },
                    { 281, 9, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5556), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5555), 1 },
                    { 282, 9, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5557), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5556), 2 },
                    { 283, 9, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5558), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5558), 3 },
                    { 284, 9, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5559), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5559), 4 },
                    { 285, 9, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5561), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5560), 5 },
                    { 286, 9, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5562), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5561), 6 },
                    { 287, 9, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5563), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5563), 7 },
                    { 288, 9, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5564), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5564), 8 },
                    { 289, 10, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5566), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5565), 1 },
                    { 290, 10, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5567), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5567), 2 },
                    { 291, 10, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5568), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5568), 3 },
                    { 292, 10, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5570), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5569), 4 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 293, 10, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5571), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5570), 5 },
                    { 294, 10, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5572), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5572), 6 },
                    { 295, 10, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5573), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5573), 7 },
                    { 296, 10, "3", 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5574), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5574), 8 }
                });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_DATA_ATUALIZACAO", "MOR_DATA_CRIACAO", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID" },
                values: new object[] { 1, "12345678901", new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5745), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5744), "morador1@teste.com", "foto1.jpg", "Morador Teste 1", "123", "1234567890", 1 });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_DATA_ATUALIZACAO", "MOR_DATA_CRIACAO", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID" },
                values: new object[] { 2, "12345678902", new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5748), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5748), "morador2@teste.com", "foto2.jpg", "Morador Teste 2", "456", "1234567891", 2 });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_ATUALIZACAO", "ETG_DATA_CRIACAO", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRP_ID" },
                values: new object[] { 1, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5794), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5794), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5795), null, "Entrega Teste 1", 1, 1, "Observação Teste 1", 1, 1 });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_ATUALIZACAO", "ETG_DATA_CRIACAO", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRP_ID" },
                values: new object[] { 2, new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5797), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5797), new DateTime(2023, 6, 8, 1, 5, 58, 191, DateTimeKind.Local).AddTicks(5797), null, "Entrega Teste 2", 2, 2, "Observação Teste 2", 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TB_ENTREGAS");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
