
# Project_WebApiProfissional_V1  🚀👩‍💻 &middot; [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/facebook/react/blob/main/LICENSE)

## Descrição

`Project_WebApiProfissional_V1` é uma API moderna desenvolvida para gerenciar operações de funcionários e usuários em ambientes corporativos, com ênfase em Clean Architecture para garantir modularidade e facilidade de manutenção. Este projeto foi criado para servir como base de estudo para desenvolvedores que desejam aprender a construir APIs bem estruturadas e objetivas com C#.

A API é desenvolvida em ASP.NET Core .NET 8.0, oferecendo uma arquitetura robusta que implementa autenticação e autorização seguras. Além disso, ela suporta operações completas de CRUD e manipulação de dados, seguindo as melhores práticas para garantir escalabilidade, separação de responsabilidades e flexibilidade.

## Funcionalidades Principais

- **Autenticação e Autorização:** Implementação de autenticação JWT com suporte a tokens de acesso e refresh tokens, além de tokens revogados.
- **Gerenciamento de Funcionários:** Funcionalidades para criação, leitura e paginação de funcionários, com validação de CPF.
- **Gerenciamento de Usuários:** Controle de usuários com criação, leitura, atualização, verificando permissões administrativas e outras funções.
- **Paginação e Filtragem**: Suporte para paginação de resultados e filtragem de dados.
- **Tokens de Acesso e Atualização**: Geração e gerenciamento de tokens JWT para acesso e atualização de dados.
- **Documentação com Swagger:** Documentação interativa da API utilizando Swagger, facilitando a integração e entendimento da API.
- **Validação com FluentValidation:** Utilização da biblioteca FluentValidation para realizar validações de entrada, garantindo que os dados fornecidos atendam aos critérios definidos, como a validação de CPF.

## Tecnologias Utilizadas

- [**C#:**](https://learn.microsoft.com/en-us/dotnet/csharp/) Linguagem de programação principal utilizada no desenvolvimento da aplicação, permitindo a criação de código robusto, orientado a objetos e com forte tipagem.
- [**ASP.NET Core 8:**](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0) Framework principal utilizado para o desenvolvimento da API.
- [**Entity Framework Core 8.0:**](https://learn.microsoft.com/en-us/ef/) ORM utilizado para comunicação com o banco de dados MySQL.
- [**JWT Authentication:**](https://jwt.io/) Implementação de autenticação baseada em JSON Web Tokens.
- [**Swagger:**](https://swagger.io/) Ferramenta para documentação e teste interativo da API.
- [**IHttpContextAccessor:**](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.ihttpcontextaccessor) Utilizado para acessar informações do contexto HTTP atual, permitindo obter dados do usuário autenticado.
- [**FluentValidation:**](https://fluentvalidation.net/) Biblioteca utilizada para validação de regras de negócio no sistema de forma fluente e extensível.

## Configurações Iniciais

### Dependências

As principais dependências do projeto são:

- :arrow_forward: `Microsoft.EntityFrameworkCore`
- :arrow_forward: `Microsoft.AspNetCore.Authentication.JwtBearer`
- :arrow_forward: `Swashbuckle.AspNetCore`
- :arrow_forward: `Pomelo.EntityFrameworkCore.MySql`
- :arrow_forward: `Microsoft.Extensions.DependencyInjection`
- :arrow_forward: `FluentValidation.AspNetCore`

Demais dependências do projeto são:

- :arrow_forward: `Microsoft.AspNetCore.Mvc.NewtonsoftJson`
- :arrow_forward: `Serilog`
- :arrow_forward: `Microsoft.AspNetCore.Http`
- :arrow_forward: `Microsoft.AspNetCore.Identity`
- :arrow_forward: `Microsoft.EntityFrameworkCore.InMemory`
- :arrow_forward: `Mono.TextTemplating`
- :arrow_forward: `MySqlConnector`
- :arrow_forward: `NetDevPack.Security.Jwt.Core`

Certifique-se de restaurar as dependências do projeto antes de iniciar a API:

```bash
dotnet restore
```

ou

```bash
Update-Package -reinstall
```

## Configuração de Banco de Dados

A aplicação utiliza um banco de dados MySQL. Configure a string de conexão no arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "MySQLConnection": "Server=localhost;Database=seuBanco;User=usuario;Password=suaSenha;"
}
```

Baixe o MySQL [MySQL Community](https://dev.mysql.com/downloads/workbench/).

## Configuração de JWT

Configure as chaves e parâmetros JWT no `appsettings.json`:

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

## Validações com FluentValidation

O projeto utiliza a biblioteca `FluentValidation` para facilitar e padronizar as validações de entrada de dados. Por exemplo, as validações de CPF são realizadas por meio de uma classe dedicada, que garante que os dados enviados estão de acordo com as regras esperadas.

Exemplo de validação de CPF:

```csharp
public class FuncionarioValidator : AbstractValidator<Funcionario>
{
    public FuncionarioValidator()
    {
        RuleFor(f => f.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Must(BeAValidCpf).WithMessage("CPF inválido.");
    }

    private bool BeAValidCpf(string cpf)
    {
        // Lógica para validar CPF
        return ValidaCpf(cpf);
    }
}
```

## Principais Endpoints

### Funcionários

**POST /register**
- Registra um novo funcionário. Requer autorização e dados do funcionário no corpo da solicitação.

**GET /funcionarios**
- Retorna uma lista paginada de funcionários. Requer autorização e parâmetros de paginação na consulta.

### Autenticação

**POST /login**
- Autentica o usuário e gera tokens de acesso e refresh. Requer credenciais de login no corpo da solicitação.

## Contribuição 🚀

Se você quiser contribuir, clone este repositório, crie seu branch de trabalho e comece a desenvolver!

Para contribuir com este projeto:

1. Clone o Repositório:

```bash
git clone https://github.com/davidwallacem1982/WebApiProfissional
```

2. Crie uma branch:

```bash
git checkout -b new-branch
```

3. Faça commit das suas mudanças:

```bash
git commit -m 'Adiciona nova funcionalidade'
```

4. Faça push para a branch:

```bash
git push origin new-branch
```

5. Abra um Pull Request.

Explique o problema resolvido ou o recurso realizado no Pull Request, anexando screenshots, se necessário, e aguarde a revisão!

[How to create a Pull Request](https://www.atlassian.com/br/git/tutorials/making-a-pull-request) |
[Commit pattern](https://gist.github.com/joshbuchea/6f47e86d2510bce28f8e7f42ae84c716)

## License 📃

Este projeto é licenciado sob a licença MIT. Consulte o arquivo [MIT](./LICENSE) para mais detalhes.
