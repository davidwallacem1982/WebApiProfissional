# Project_WebApiProfissional_V1  🚀👩‍💻&middot; [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/facebook/react/blob/main/LICENSE)

## Descrição

`Project_WebApiProfissional_V1` é uma API desenvolvida para gerenciar operações relacionadas a funcionários e usuários em um ambiente corporativo. Este projeto é projetado para ajudar no estudo de pessoas que desejam criar APIs organizadas e objetivas, utilizando a linhagem C#.
A API é construída com **ASP.NET Core .NET 8.0** e implementa uma estrutura robusta para autenticação e autorização, além de fornecer suporte para operações de CRUD e manipulação de dados.

## Funcionalidades Principais

- **Autenticação e Autorização:** Implementação de autenticação JWT com suporte a tokens de acesso e refresh tokens, além de tokens revogados.
- **Gerenciamento de Funcionários:** Funcionalidades para criação, leitura e paginação de funcionários, com validação de CPF.
- **Gerenciamento de Usuários:** Controle de usuários com criação, leitura, atualização, verificando permissões administrativas e outras funções.
- **Paginação e Filtragem**: Suporte para paginação de resultados e filtragem de dados.
- **Tokens de Acesso e Atualização**: Geração e gerenciamento de tokens JWT para acesso e atualização de dados.
- **Documentação com Swagger:** Documentação interativa da API utilizando Swagger, facilitando a integração e entendimento da API.

## Tecnologias Utilizadas

- [**C#:**](https://learn.microsoft.com/en-us/dotnet/csharp/) Linguagem de programação principal utilizada no desenvolvimento da aplicação, permitindo a criação de código robusto, orientado a objetos e com forte tipagem.
- [**ASP.NET Core 8:**](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0) Framework principal utilizado para o desenvolvimento da API.
- [**Entity Framework Core 8.0:**](https://learn.microsoft.com/en-us/ef/) ORM utilizado para comunicação com o banco de dados MySQL.
- [**JWT Authentication:**](https://jwt.io/) Implementação de autenticação baseada em JSON Web Tokens.
- [**Swagger:**](https://swagger.io/) Ferramenta para documentação e teste interativo da API.
- [**IHttpContextAccessor:**](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.ihttpcontextaccessor) Utilizado para acessar informações do contexto HTTP atual, permitindo obter dados do usuário autenticado.

## Configurações Iniciais

### Dependências

As principais dependências do projeto são:

- :arrow_forward:`Microsoft.EntityFrameworkCore`
- :arrow_forward:`Microsoft.AspNetCore.Authentication.JwtBearer`
- :arrow_forward:`Swashbuckle.AspNetCore`
- :arrow_forward:`Pomelo.EntityFrameworkCore.MySql`
- :arrow_forward:`Microsoft.Extensions.DependencyInjection`
- :arrow_forward:`FluentValidation.AspNetCore`

Demais dependências do projeto são:

- :arrow_forward:`Microsoft.AspNetCore.Mvc.NewtonsoftJson`
- :arrow_forward:`Serilog`
- :arrow_forward:`Microsoft.AspNetCore.Http`
- :arrow_forward:`Microsoft.AspNetCore.Identity`
- :arrow_forward:`Microsoft.EntityFrameworkCore.InMemory`
- :arrow_forward:`Mono.TextTemplating`
- :arrow_forward:`MySqlConnector`
- :arrow_forward:`NetDevPack.Security.Jwt.Core`
  
Certifique-se de restaurar as dependências do projeto antes de iniciar a API:

```bash
dotnet restore
```
ou

```bash
Update-Package -reinstall
```

## Configuração de Banco de Dados

A aplicação utiliza um banco de dados MySQL. Configure a string de conexão no arquivo appsettings.json:
```json
"ConnectionStrings": {
  "MySQLConnection": "Server=localhost;Database=seuBanco;User=usuario;Password=suaSenha;"
}
```
## download do MySQL Community Downloads  (https://dev.mysql.com/downloads/workbench/)

## Configuração de JWT

Configure as chaves e parâmetros JWT no appsettings.json:
```json
"jwt": {
  "issuer": "seuIssuer",
  "audience": "seuAudience",
  "secretKey": "suaChaveSecreta"
}
```

## Executando a Aplicação
Para rodar o projeto localmente, utilize o comando:
```bash
dotnet run
```

Acesse o Swagger para explorar e testar os endpoints da API:
```bash
https://localhost:{PORTA}/swagger/index.html
```

## Estrutura do Projeto
- **Controllers:** Contém as controladoras responsáveis por expor os endpoints da API.
- **Logic:** Contém a lógica de negócios da aplicação, como validações e regras específicas.
- **Repositories:** Responsável pela comunicação com o banco de dados.
- **Services:** Serviços que fazem a ponte entre a lógica de negócios e a camada de infraestrutura.
- **Models:** Modelos de dados usados para receber e enviar informações entre as camadas.

## Principais Endpoints

### Funcionários

**POST/register**
- Registra um novo funcionário. Requer autorização e dados do funcionário no corpo da solicitação.

**GET/funcionarios**
- Retorna uma lista paginada de funcionários. Requer autorização e parâmetros de paginação na consulta.

### Autenticação

**POST/login**
- Autentica o usuário e gera tokens de acesso e refresh. Requer credenciais de login no corpo da solicitação.

<h2 id="contribute">Contribuição 🚀</h2>

Se você quiser contribuir, clone este repository, crie seu branch de trabalho e coloque a mão na massa!

Para contribuir com este projeto:

Clone o Repository
```bash
git clone https://github.com/davidwallacem1982/WebApiProfissional
```
Crie uma branch.
```bash
git checkout -b new-branch
```
Faça commit das suas mudanças.
```bash
git commit -m 'Adiciona nova funcionalidade'
```
Faça push para a branch.
```bash
git push origin new-branch
```
Abra um Pull Request.

Ao final, abra um Pull Request explicando o problema resolvido ou recurso realizado, se existir, anexe screenshot das modificações visuais e aguarde a revisão!

[How to create a Pull Request](https://www.atlassian.com/br/git/tutorials/making-a-pull-request) |
[Commit pattern](https://gist.github.com/joshbuchea/6f47e86d2510bce28f8e7f42ae84c716)

<h2 id="license">License 📃 </h2>

Este projeto é licenciado sob a licença MIT. Consulte o arquivo [MIT](./LICENSE) para mais detalhes.
