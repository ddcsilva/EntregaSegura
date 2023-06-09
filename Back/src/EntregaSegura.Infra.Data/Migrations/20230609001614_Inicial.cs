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
                    MoradorId = table.Column<int>(type: "int", nullable: false),
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
                    { 1, "Jardim Paulistano", "04567010", "São Paulo", "17540623000150", new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6613), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6604), "contato@boavista.com.br", "SP", "Rua das Acácias", "Condomínio Boa Vista", 55, 7, 2, 4, "1140028922" },
                    { 2, "Copacabana", "22021001", "Rio de Janeiro", "27004428000169", new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6620), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6620), "contato@raiodesol.com.br", "RJ", "Avenida Atlântica", "Condomínio Raio de Sol", 700, 10, 3, 8, "2130033211" }
                });

            migrationBuilder.InsertData(
                table: "TB_TRANSPORTADORAS",
                columns: new[] { "TRA_ID", "TRA_CNPJ", "TRA_DATA_ATUALIZACAO", "TRA_DATA_CRIACAO", "TRA_EMAIL", "TRA_NOME", "TRA_TELEFONE" },
                values: new object[,]
                {
                    { 1, "12345678912347", new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7464), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7464), "transportadora1@teste.com", "Transportadora Teste 1", "1234567894" },
                    { 2, "12345678912348", new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7466), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7466), "transportadora2@teste.com", "Transportadora Teste 2", "1234567895" }
                });

            migrationBuilder.InsertData(
                table: "TB_FUNCIONARIOS",
                columns: new[] { "FUN_ID", "FUN_CPF", "FUN_CARGO", "FUN_CONDOMINIO_ID", "FUN_DATA_ADMISSAO", "FUN_DATA_ATUALIZACAO", "FUN_DATA_CRIACAO", "FUN_DATA_DEMISSAO", "FUN_EMAIL", "FUN_NOME", "FUN_TELEFONE" },
                values: new object[,]
                {
                    { 1, "12345678903", 3, 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7449), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7448), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7448), null, "funcionario1@teste.com", "Funcionario Teste 1", "1234567892" },
                    { 2, "12345678904", 2, 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7451), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7451), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7450), null, "funcionario2@teste.com", "Funcionario Teste 2", "1234567893" }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 1, 1, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6733), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6732), 1 },
                    { 2, 1, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6747), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6747), 2 },
                    { 3, 1, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6749), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6748), 3 },
                    { 4, 1, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6750), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6750), 4 },
                    { 5, 2, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6752), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6751), 1 },
                    { 6, 2, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6755), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6754), 2 },
                    { 7, 2, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6756), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6756), 3 },
                    { 8, 2, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6757), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6757), 4 },
                    { 9, 3, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6759), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6758), 1 },
                    { 10, 3, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6761), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6760), 2 },
                    { 11, 3, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6762), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6762), 3 },
                    { 12, 3, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6763), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6763), 4 },
                    { 13, 4, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6765), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6764), 1 },
                    { 14, 4, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6766), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6766), 2 },
                    { 15, 4, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6767), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6767), 3 },
                    { 16, 4, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6769), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6768), 4 },
                    { 17, 5, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6770), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6770), 1 },
                    { 18, 5, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6772), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6772), 2 },
                    { 19, 5, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6773), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6773), 3 },
                    { 20, 5, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6775), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6774), 4 },
                    { 21, 6, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6776), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6776), 1 },
                    { 22, 6, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6777), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6777), 2 },
                    { 23, 6, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6779), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6778), 3 },
                    { 24, 6, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6780), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6779), 4 },
                    { 25, 7, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6781), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6781), 1 },
                    { 26, 7, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6783), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6782), 2 },
                    { 27, 7, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6784), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6783), 3 },
                    { 28, 7, "1", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6785), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6785), 4 },
                    { 29, 1, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6787), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6787), 1 },
                    { 30, 1, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6788), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6788), 2 },
                    { 31, 1, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6789), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6789), 3 },
                    { 32, 1, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6791), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6790), 4 },
                    { 33, 2, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6792), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6792), 1 },
                    { 34, 2, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6794), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6794), 2 },
                    { 35, 2, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6795), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6795), 3 },
                    { 36, 2, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6797), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6796), 4 },
                    { 37, 3, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6798), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6798), 1 },
                    { 38, 3, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6799), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6799), 2 },
                    { 39, 3, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6801), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6800), 3 },
                    { 40, 3, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6802), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6801), 4 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 41, 4, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6803), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6803), 1 },
                    { 42, 4, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6804), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6804), 2 },
                    { 43, 4, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6806), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6805), 3 },
                    { 44, 4, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6807), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6807), 4 },
                    { 45, 5, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6808), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6808), 1 },
                    { 46, 5, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6810), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6809), 2 },
                    { 47, 5, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6811), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6811), 3 },
                    { 48, 5, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6812), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6812), 4 },
                    { 49, 6, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6814), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6813), 1 },
                    { 50, 6, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6815), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6814), 2 },
                    { 51, 6, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6816), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6816), 3 },
                    { 52, 6, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6817), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6817), 4 },
                    { 53, 7, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6853), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6852), 1 },
                    { 54, 7, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6854), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6854), 2 },
                    { 55, 7, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6856), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6855), 3 },
                    { 56, 7, "2", 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6857), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6857), 4 },
                    { 57, 1, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6859), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6859), 1 },
                    { 58, 1, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6861), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6860), 2 },
                    { 59, 1, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6862), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6862), 3 },
                    { 60, 1, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6863), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6863), 4 },
                    { 61, 1, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6865), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6864), 5 },
                    { 62, 1, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6866), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6865), 6 },
                    { 63, 1, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6867), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6867), 7 },
                    { 64, 1, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6869), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6868), 8 },
                    { 65, 2, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6870), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6870), 1 },
                    { 66, 2, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6872), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6872), 2 },
                    { 67, 2, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6874), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6873), 3 },
                    { 68, 2, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6875), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6875), 4 },
                    { 69, 2, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6876), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6876), 5 },
                    { 70, 2, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6878), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6877), 6 },
                    { 71, 2, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6879), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6878), 7 },
                    { 72, 2, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6880), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6880), 8 },
                    { 73, 3, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6882), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6881), 1 },
                    { 74, 3, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6883), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6882), 2 },
                    { 75, 3, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6884), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6884), 3 },
                    { 76, 3, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6886), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6885), 4 },
                    { 77, 3, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6887), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6886), 5 },
                    { 78, 3, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6888), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6888), 6 },
                    { 79, 3, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6889), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6889), 7 },
                    { 80, 3, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6891), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6890), 8 },
                    { 81, 4, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6892), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6892), 1 },
                    { 82, 4, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6894), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6893), 2 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 83, 4, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6895), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6894), 3 },
                    { 84, 4, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6896), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6896), 4 },
                    { 85, 4, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6897), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6897), 5 },
                    { 86, 4, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6899), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6898), 6 },
                    { 87, 4, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6900), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6899), 7 },
                    { 88, 4, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6901), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6901), 8 },
                    { 89, 5, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6903), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6902), 1 },
                    { 90, 5, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6904), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6903), 2 },
                    { 91, 5, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6905), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6905), 3 },
                    { 92, 5, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6907), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6906), 4 },
                    { 93, 5, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6908), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6907), 5 },
                    { 94, 5, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6909), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6909), 6 },
                    { 95, 5, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6910), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6910), 7 },
                    { 96, 5, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6912), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6911), 8 },
                    { 97, 6, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6913), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6913), 1 },
                    { 98, 6, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6914), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6914), 2 },
                    { 99, 6, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6916), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6915), 3 },
                    { 100, 6, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6917), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6917), 4 },
                    { 101, 6, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6918), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6918), 5 },
                    { 102, 6, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6920), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6919), 6 },
                    { 103, 6, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6921), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6920), 7 },
                    { 104, 6, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6922), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6922), 8 },
                    { 105, 7, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6924), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6923), 1 },
                    { 106, 7, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6925), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6924), 2 },
                    { 107, 7, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6926), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6926), 3 },
                    { 108, 7, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6927), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6927), 4 },
                    { 109, 7, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6929), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6928), 5 },
                    { 110, 7, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6930), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6930), 6 },
                    { 111, 7, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6931), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6931), 7 },
                    { 112, 7, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6933), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6932), 8 },
                    { 113, 8, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6934), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6934), 1 },
                    { 114, 8, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6935), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6935), 2 },
                    { 115, 8, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6937), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6936), 3 },
                    { 116, 8, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6938), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6938), 4 },
                    { 117, 8, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6970), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6969), 5 },
                    { 118, 8, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6971), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6971), 6 },
                    { 119, 8, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6973), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6972), 7 },
                    { 120, 8, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6974), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6974), 8 },
                    { 121, 9, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6975), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6975), 1 },
                    { 122, 9, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6977), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6976), 2 },
                    { 123, 9, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6978), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6978), 3 },
                    { 124, 9, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6979), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6979), 4 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 125, 9, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6981), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6980), 5 },
                    { 126, 9, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6982), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6982), 6 },
                    { 127, 9, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6983), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6983), 7 },
                    { 128, 9, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6985), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6984), 8 },
                    { 129, 10, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6986), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6986), 1 },
                    { 130, 10, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6988), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6988), 2 },
                    { 131, 10, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6990), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6989), 3 },
                    { 132, 10, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6991), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6991), 4 },
                    { 133, 10, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6992), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6992), 5 },
                    { 134, 10, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6993), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6993), 6 },
                    { 135, 10, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6995), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6994), 7 },
                    { 136, 10, "1", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6996), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6996), 8 },
                    { 137, 1, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6998), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6997), 1 },
                    { 138, 1, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6999), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(6998), 2 },
                    { 139, 1, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7000), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7000), 3 },
                    { 140, 1, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7001), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7001), 4 },
                    { 141, 1, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7002), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7002), 5 },
                    { 142, 1, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7004), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7003), 6 },
                    { 143, 1, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7005), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7005), 7 },
                    { 144, 1, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7006), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7006), 8 },
                    { 145, 2, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7008), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7007), 1 },
                    { 146, 2, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7009), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7009), 2 },
                    { 147, 2, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7010), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7010), 3 },
                    { 148, 2, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7011), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7011), 4 },
                    { 149, 2, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7013), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7012), 5 },
                    { 150, 2, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7014), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7013), 6 },
                    { 151, 2, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7015), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7015), 7 },
                    { 152, 2, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7016), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7016), 8 },
                    { 153, 3, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7018), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7017), 1 },
                    { 154, 3, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7019), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7019), 2 },
                    { 155, 3, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7020), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7020), 3 },
                    { 156, 3, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7021), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7021), 4 },
                    { 157, 3, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7023), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7022), 5 },
                    { 158, 3, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7024), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7023), 6 },
                    { 159, 3, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7025), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7025), 7 },
                    { 160, 3, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7026), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7026), 8 },
                    { 161, 4, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7028), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7027), 1 },
                    { 162, 4, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7029), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7029), 2 },
                    { 163, 4, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7030), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7030), 3 },
                    { 164, 4, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7031), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7031), 4 },
                    { 165, 4, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7033), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7032), 5 },
                    { 166, 4, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7034), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7034), 6 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 167, 4, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7035), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7035), 7 },
                    { 168, 4, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7037), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7036), 8 },
                    { 169, 5, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7038), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7037), 1 },
                    { 170, 5, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7039), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7039), 2 },
                    { 171, 5, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7094), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7094), 3 },
                    { 172, 5, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7096), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7096), 4 },
                    { 173, 5, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7097), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7097), 5 },
                    { 174, 5, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7099), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7098), 6 },
                    { 175, 5, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7100), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7100), 7 },
                    { 176, 5, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7101), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7101), 8 },
                    { 177, 6, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7103), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7102), 1 },
                    { 178, 6, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7104), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7103), 2 },
                    { 179, 6, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7105), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7105), 3 },
                    { 180, 6, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7106), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7106), 4 },
                    { 181, 6, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7108), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7107), 5 },
                    { 182, 6, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7109), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7108), 6 },
                    { 183, 6, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7110), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7110), 7 },
                    { 184, 6, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7111), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7111), 8 },
                    { 185, 7, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7113), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7112), 1 },
                    { 186, 7, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7114), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7113), 2 },
                    { 187, 7, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7115), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7115), 3 },
                    { 188, 7, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7116), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7116), 4 },
                    { 189, 7, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7118), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7117), 5 },
                    { 190, 7, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7119), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7118), 6 },
                    { 191, 7, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7120), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7120), 7 },
                    { 192, 7, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7121), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7121), 8 },
                    { 193, 8, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7123), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7122), 1 },
                    { 194, 8, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7124), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7123), 2 },
                    { 195, 8, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7125), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7125), 3 },
                    { 196, 8, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7126), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7126), 4 },
                    { 197, 8, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7127), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7127), 5 },
                    { 198, 8, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7129), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7128), 6 },
                    { 199, 8, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7130), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7129), 7 },
                    { 200, 8, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7131), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7131), 8 },
                    { 201, 9, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7133), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7132), 1 },
                    { 202, 9, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7134), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7133), 2 },
                    { 203, 9, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7135), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7135), 3 },
                    { 204, 9, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7136), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7136), 4 },
                    { 205, 9, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7137), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7137), 5 },
                    { 206, 9, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7139), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7138), 6 },
                    { 207, 9, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7140), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7139), 7 },
                    { 208, 9, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7141), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7141), 8 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 209, 10, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7142), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7142), 1 },
                    { 210, 10, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7144), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7143), 2 },
                    { 211, 10, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7145), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7145), 3 },
                    { 212, 10, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7146), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7146), 4 },
                    { 213, 10, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7147), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7147), 5 },
                    { 214, 10, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7149), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7148), 6 },
                    { 215, 10, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7150), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7149), 7 },
                    { 216, 10, "2", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7151), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7151), 8 },
                    { 217, 1, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7153), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7152), 1 },
                    { 218, 1, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7154), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7153), 2 },
                    { 219, 1, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7155), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7155), 3 },
                    { 220, 1, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7156), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7156), 4 },
                    { 221, 1, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7158), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7157), 5 },
                    { 222, 1, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7159), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7158), 6 },
                    { 223, 1, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7160), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7160), 7 },
                    { 224, 1, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7161), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7161), 8 },
                    { 225, 2, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7163), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7162), 1 },
                    { 226, 2, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7164), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7164), 2 },
                    { 227, 2, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7165), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7165), 3 },
                    { 228, 2, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7167), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7166), 4 },
                    { 229, 2, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7168), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7167), 5 },
                    { 230, 2, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7169), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7169), 6 },
                    { 231, 2, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7170), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7170), 7 },
                    { 232, 2, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7172), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7171), 8 },
                    { 233, 3, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7173), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7173), 1 },
                    { 234, 3, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7174), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7174), 2 },
                    { 235, 3, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7176), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7175), 3 },
                    { 236, 3, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7177), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7176), 4 },
                    { 237, 3, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7178), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7178), 5 },
                    { 238, 3, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7179), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7179), 6 },
                    { 239, 3, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7180), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7180), 7 },
                    { 240, 3, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7182), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7181), 8 },
                    { 241, 4, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7183), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7183), 1 },
                    { 242, 4, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7184), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7184), 2 },
                    { 243, 4, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7185), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7185), 3 },
                    { 244, 4, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7218), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7217), 4 },
                    { 245, 4, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7220), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7219), 5 },
                    { 246, 4, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7221), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7221), 6 },
                    { 247, 4, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7222), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7222), 7 },
                    { 248, 4, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7223), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7223), 8 },
                    { 249, 5, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7225), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7224), 1 },
                    { 250, 5, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7226), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7226), 2 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 251, 5, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7227), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7227), 3 },
                    { 252, 5, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7229), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7228), 4 },
                    { 253, 5, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7230), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7229), 5 },
                    { 254, 5, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7231), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7231), 6 },
                    { 255, 5, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7232), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7232), 7 },
                    { 256, 5, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7233), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7233), 8 },
                    { 257, 6, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7235), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7235), 1 },
                    { 258, 6, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7239), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7238), 2 },
                    { 259, 6, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7240), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7239), 3 },
                    { 260, 6, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7241), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7241), 4 },
                    { 261, 6, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7242), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7242), 5 },
                    { 262, 6, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7243), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7243), 6 },
                    { 263, 6, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7245), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7244), 7 },
                    { 264, 6, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7246), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7246), 8 },
                    { 265, 7, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7248), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7247), 1 },
                    { 266, 7, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7249), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7249), 2 },
                    { 267, 7, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7250), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7250), 3 },
                    { 268, 7, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7252), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7251), 4 },
                    { 269, 7, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7253), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7252), 5 },
                    { 270, 7, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7254), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7254), 6 },
                    { 271, 7, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7255), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7255), 7 },
                    { 272, 7, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7257), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7256), 8 },
                    { 273, 8, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7258), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7258), 1 },
                    { 274, 8, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7259), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7259), 2 },
                    { 275, 8, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7260), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7260), 3 },
                    { 276, 8, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7262), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7261), 4 },
                    { 277, 8, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7263), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7262), 5 },
                    { 278, 8, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7264), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7264), 6 },
                    { 279, 8, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7265), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7265), 7 },
                    { 280, 8, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7267), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7266), 8 },
                    { 281, 9, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7299), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7298), 1 },
                    { 282, 9, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7300), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7300), 2 },
                    { 283, 9, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7301), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7301), 3 },
                    { 284, 9, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7302), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7302), 4 },
                    { 285, 9, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7304), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7303), 5 },
                    { 286, 9, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7305), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7304), 6 },
                    { 287, 9, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7306), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7306), 7 },
                    { 288, 9, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7307), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7307), 8 },
                    { 289, 10, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7309), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7308), 1 },
                    { 290, 10, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7310), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7309), 2 },
                    { 291, 10, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7311), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7311), 3 },
                    { 292, 10, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7312), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7312), 4 }
                });

            migrationBuilder.InsertData(
                table: "TB_UNIDADES",
                columns: new[] { "UND_ID", "UND_ANDAR", "UND_BLOCO", "CON_ID", "UND_DATA_ATUALIZACAO", "UND_DATA_CRIACAO", "UND_NUMERO" },
                values: new object[,]
                {
                    { 293, 10, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7313), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7313), 5 },
                    { 294, 10, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7315), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7314), 6 },
                    { 295, 10, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7316), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7316), 7 },
                    { 296, 10, "3", 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7317), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7317), 8 }
                });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_DATA_ATUALIZACAO", "MOR_DATA_CRIACAO", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID" },
                values: new object[] { 1, "12345678901", new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7427), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7426), "morador1@teste.com", "foto1.jpg", "Morador Teste 1", "123", "1234567890", 1 });

            migrationBuilder.InsertData(
                table: "TB_MORADORES",
                columns: new[] { "MOR_ID", "MOR_CPF", "MOR_DATA_ATUALIZACAO", "MOR_DATA_CRIACAO", "MOR_EMAIL", "MOR_FOTO", "MOR_NOME", "MOR_RAMAL", "MOR_TELEFONE", "MOR_UNIDADE_ID" },
                values: new object[] { 2, "12345678902", new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7431), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7430), "morador2@teste.com", "foto2.jpg", "Morador Teste 2", "456", "1234567891", 2 });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_ATUALIZACAO", "ETG_DATA_CRIACAO", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRP_ID" },
                values: new object[] { 1, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7478), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7477), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7478), null, "Entrega Teste 1", 1, 1, "Observação Teste 1", 1, 1 });

            migrationBuilder.InsertData(
                table: "TB_ENTREGAS",
                columns: new[] { "ETG_ID", "ETG_DATA_ATUALIZACAO", "ETG_DATA_CRIACAO", "ETG_DATA_RECEBIMENTO", "ETG_DATA_RETIRADA", "ETG_DESCRICAO", "FUN_ID", "MOR_ID", "ETG_OBSERVACAO", "ETG_STATUS", "TRP_ID" },
                values: new object[] { 2, new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7480), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7480), new DateTime(2023, 6, 8, 21, 16, 14, 152, DateTimeKind.Local).AddTicks(7481), null, "Entrega Teste 2", 2, 2, "Observação Teste 2", 1, 2 });

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
