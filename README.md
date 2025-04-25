# EntregaSegura :package:

EntregaSegura é um sistema robusto de gerenciamento de entregas para condomínios. Desenvolvido como parte do Trabalho de Conclusão de Curso (TCC) de pós-graduação em Engenharia de Software, o projeto visa aprimorar o controle e a comunicação de entregas entre a portaria, os moradores e a administração dos condomínios. 

:warning: *Por favor, note que este projeto é puramente acadêmico e não possui associação com quaisquer produtos comerciais de mesmo nome.*

## :gear: Tecnologias

Este projeto foi criado com:

* API: [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)
  * Entity Framework Core: Para mapeamento objeto-relacional e gerenciamento de conexão ao banco de dados.
  * ASP.NET Identity: Para autenticação e gerenciamento de usuários.
  * Fluent Validation: Para validação de regras de negócios.
  * AutoMapper: Para mapeamento entre os objetos de domínio e os objetos DTO.
  * Injeção de Dependência Nativa: Para melhor gerenciamento e controle das dependências do projeto.

* Cliente: [Angular 14](https://angular.io/)
  * Bootstrap 5: Para estilização e componentes de interface.
  * Angular Material: Para componentes de interface do usuário baseados em Material Design.
  * Ngx-mask: Para controle de entrada de dados em campos de formulário.
  * Ngx-spinner: Para exibição de um spinner/carregando durante as operações de carregamento.
  * Ngx-toastr: Para exibição de mensagens de sucesso, erro, informações e alertas.
  * RxJS: Para programação reativa e operações assíncronas.

* Banco de dados: [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## :white_check_mark: Testes

O projeto inclui testes unitários para o back-end (.NET).

## :clipboard: Funcionalidades

* Gerenciamento de entregas, condomínios, unidades e funcionários
* Notificações para moradores sobre entregas recebidas
* Histórico de entregas e retiradas

(Mais funcionalidades planejadas serão adicionadas posteriormente)

## :file_folder: Estrutura do projeto .NET

* EntregaSegura.API
  - Configurations: Contém as configurações da API, como rotas, autenticação, etc.
  - Controllers: Controladores da API, responsáveis por receber requisições e enviar respostas.
  - Extensions: Extensões personalizadas para ajudar na codificação e manutenção.
  - Properties: Configurações de propriedade da API.
  - wwwroot: Diretório de conteúdo estático e arquivos de configuração da web.

* EntregaSegura.Application
  - DTOs: Data Transfer Objects usados para passar dados entre camadas.
  - Interfaces: Contratos para os serviços.
  - Notifications: Serviço de notificações.
  - Services: Contém a lógica de negócios de alto nível e chama métodos do repositório.

* EntregaSegura.Domain
  - Entities: Entidades do domínio.
  - Enums: Enumerações usadas nas entidades e/ou regras de negócio.
  - Interfaces: Contratos para os repositórios.
  - Models: Modelos de domínio.
  - Validators: Regras de validação das entidades.

* EntregaSegura.Infrastructure
  - Configurations: Configurações de infraestrutura, como mapeamento objeto-relacional.
  - Contexts: Contextos do Entity Framework Core.
  - Extensions: Extensões personalizadas de infraestrutura.
  - Migrations: Migrações do banco de dados geradas pelo Entity Framework Core.
  - Repositories: Implementações dos repositórios definidos na camada de Domínio.

## :bust_in_silhouette: Desenvolvedores

* Danilo Silva - [GitHub](https://github.com/ddcsilva) - [Email](danilo.silva@msn.com)

## :balance_scale: Licença

Este projeto é disponibilizado sob a licença MIT License. Essa licença permite o uso, a cópia, a modificação e a distribuição do código-fonte para fins não comerciais, desde que a atribuição seja feita ao autor original.
