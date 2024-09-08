# Project_WebApiProfissional_V1  🚀👩‍💻

## Descrição

`Project_WebApiProfissional_V1` é uma API desenvolvida para gerenciar operações relacionadas a funcionários e usuários em um ambiente corporativo. Este projeto é projetado para ajudar no estudo de pessoas que desejam criar APIs organizadas e objetivas, utilizando a linhagem C#.
A API é construída com **ASP.NET Core .NET 8.0** e implementa uma estrutura robusta para autenticação e autorização, além de fornecer suporte para operações de CRUD e manipulação de dados.

## Funcionalidades Principais

- **Autenticação e Autorização:** Implementação de autenticação JWT com suporte a tokens de acesso e refresh tokens, além de tokens revogados.
- **Gerenciamento de Funcionários:** Funcionalidades para criação, leitura e paginação de funcionários, com validação de CPF.
- **Gerenciamento de:** Controle de usuários com criação, leitura, atualização, verificando permissões administrativas e outras funções.
- **Paginação e Filtragem**: Suporte para paginação de resultados e filtragem de dados.
- - **Tokens de Acesso e Atualização**: Geração e gerenciamento de tokens JWT para acesso e atualização de dados.
- **Documentação com Swagger:** Documentação interativa da API utilizando Swagger, facilitando a integração e entendimento da API.

## Tecnologias Utilizadas

- **ASP.NET Core .NET 8.0:** Framework principal utilizado para o desenvolvimento da API.
- **Entity Framework Core:** ORM utilizado para comunicação com o banco de dados MySQL.
- **JWT Authentication:** Implementação de autenticação baseada em JSON Web Tokens.
- **Swagger:** Ferramenta para documentação e teste interativo da API.
- **IHttpContextAccessor:** Utilizado para acessar informações do contexto HTTP atual, permitindo obter dados do usuário autenticado.

## Configurações Iniciais

### Dependências

As principais dependências do projeto são:

- `Microsoft.EntityFrameworkCore`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Swashbuckle.AspNetCore`
- `Pomelo.EntityFrameworkCore.MySql`

Certifique-se de restaurar as dependências do projeto antes de iniciar a API:

```bash
dotnet restore
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