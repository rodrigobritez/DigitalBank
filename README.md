# DigitalBank
Challenge from Functional Tech


## Stack
* .NET
* PostgreSQL

## Libraries
* Framework: [.NET core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* ORM: 
[Entity framework ORM](https://www.nuget.org/packages/EntityFramework)
* GraphQL: [GraphQL .NET](nuget.org/packages/GraphQL/4.7.1?_src=template)
* Unit tests: [MS Test](https://www.nuget.org/packages/MSTest.TestFramework/2.2.8?_src=template)
* Coverage: [Coverlet](https://www.nuget.org/packages/coverlet.collector/3.1.2?_src=template)
* Validations: [fluent validation](https://www.nuget.org/packages/FluentValidation)
* API Documentation: [Swagger](https://www.nuget.org/packages/Swashbuckle.AspNetCore/6.2.3?_src=template)

## Modules
**API** ⇒ Sendo a Web API e contendo a parte de configuração do sistema, também é onde se encontra os *controllers* da parte *Rest* do sistema.

**Data** ⇒ Sendo uma Classlib e contendo a parte de configuração do banco de dados, onde se encontra as *migrations* do sistema e também onde fica a implementação dos *repositories* da API.

**Domain** ⇒ Sendo uma Classlib e contendo a parte central da aplicação onde se encontra todas as interfaces do sistema, além disso os commands, handlers, *entities* e *validators*.

**GraphQL** ⇒ Sendo uma Classlib e contendo a configuração do Graphql e suas *queries* e *mutations.*

**Tests** ⇒ Sendo uma MStest e contendo todos os testes unitários do sistema.
## Instalation
### Docker
create docker container
```console
docker build -t digital-bank .\
````
run project container
```console
docker run -d -p 8080:80 --name digital-bank digital-bank
```

### Database
Database hosted on aws servers
```
http://database-postgres.cub4bbenssqn.us-east-2.rds.amazonaws.com/
```

## API

### REST
All informations from the API and Schemas structure can be founded on Swagger
```
http://localhost:8080/swagger
```
![](./swagger.png)

### GraphQL
As well as the REST all informations from the API and Schemas structure can be founded on GraphQL Playgrounds

```
http://localhost:8080/ui/playground
```
![](./graphql.png)

## Code details

all the functionalities created are detailed in the documentation made in this [Link on notion](https://cubic-chips-6a7.notion.site/Digital-Bank-Documenta-o-9ae29300086b4a0ba5b7671837828f12).
