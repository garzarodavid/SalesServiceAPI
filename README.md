### README.md

```markdown
# SalesServiceAPI

SalesServiceAPI é um projeto que oferece serviços de vendas, incluindo criação, atualização, consulta e exclusão de vendas. Este projeto foi desenvolvido para demonstrar a aplicação de padrões de arquitetura, testes de integração e automação de deploy.

## Sumário

- [Visão Geral](#visão-geral)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Requisitos](#requisitos)
- [Configuração](#configuração)
- [Como Executar](#como-executar)
- [Testes](#testes)
- [Licença](#licença)

## Visão Geral

SalesServiceAPI é construído utilizando .NET 8, com uma arquitetura baseada em camadas e padrões de design. Este projeto utiliza Entity Framework Core para o acesso a dados e AutoMapper para mapeamento de objetos.

## Estrutura do Projeto

A estrutura do projeto é organizada da seguinte forma:

```
SalesServiceAPI/
├── SalesServiceAPI.Api/            # Camada de API
├── SalesServiceAPI.Application/    # Camada de Aplicação
├── SalesServiceAPI.Domain/         # Camada de Domínio
├── SalesServiceAPI.Infrastructure/ # Camada de Infraestrutura
├── SalesServiceAPI.Tests/          # Testes de Unidade e Integração
└── SalesServiceAPI.sln             # Arquivo da Solution
```

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads)

## Configuração

1. Clone o repositório para sua máquina local:
   ```sh
   git clone https://github.com/garzarodavid/SalesServiceAPI.git
   ```

2. Navegue até o diretório do projeto:
   ```sh
   cd SalesServiceAPI
   ```

3. Configure o banco de dados no arquivo `appsettings.json` da camada API:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=SalesServiceDB;User Id=sa;Password=YourPassword;"
   }
   ```

## Como Executar

1. Restaure as dependências e compile o projeto:
   ```sh
   dotnet restore
   dotnet build
   ```

2. Aplique as migrações do banco de dados:
   ```sh
   dotnet ef database update --project SalesServiceAPI.Infrastructure
   ```

3. Execute o projeto:
   ```sh
   dotnet run --project SalesServiceAPI.Api
   ```

A API estará disponível em `http://localhost:5000`.

## Testes

Para executar os testes de unidade e integração, utilize o comando:

```sh
dotnet test

## Licença

Este projeto está licenciado sob a Licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
```
