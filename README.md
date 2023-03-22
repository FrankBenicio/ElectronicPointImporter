## Metodologias e Designs:
- TDD
- Clean Architecture
- Modular Design
- Use Cases

## Bibliotecas e Ferramentas implementadas:
 
- .NET 7.0
- ASP.NET WebApi
- ASP.NET Web MVC
- FluentValidators
- UI Swagger
- Entity Framework Core
- Entity Framework Core InMemory
- xUnit
- Moq
- AutoFixture
- Stryker.net
- SonarLint
- Docker
- Docker Compose
- Git
- Azure Functions
- Azure Service Bus
- Azure Blob Storage
- Boostrap 5
- Refit
- SQL Server
- Visual Studio

## Features de Testes:

- Testes Unitários
- Testes de Integração
- Teste de mutação
- Cobertura de Testes
- Mocks

## Web:
- Interface do projeto.

## API:
- Disponibilização das rotas POST, GET e PUT.

## Processor:
- Responsável por processar os arquivos csv do ponto eletronico.

## Instalação:
Passo 1:
Crie o tópico e fila no Service bus:
<br/>
<img src="https://i.imgur.com/Uzphl02.png" alt="Service bus"> 

Passo 2: 
Configure as variaveis de Ambiente;
- API: "appsettings.json" 
```json
{
  "ConnectionStrings": 
    {
      "Database": "",
      "ServiceBus": "",
      "BlobStorage": ""  
    }
}
```
- Processor: "local.settings.json" 
```json
{
"Values": 
  {
    "blobStorage": "",
    "AzureWebJobsMyServiceBus": ""
  }  
}
  ```
Passo 3:   
Com a IDE Visual Studio ou Visual Studio Code: File > Open project. E execute o projeto com docker-compose.

### Imagens Web:
<img src="https://i.imgur.com/fQ3Lwg0.png" alt="UploadCsv" width="100%"> 

### Imagens API:
<img src="https://i.imgur.com/ewxDK3K.png" alt="Api swagger ui" width="100%"> 
<img src="https://i.imgur.com/hqtYcwP.png" alt="Fechamento de pagamento" width="100%"> 

