# DigitalBank
Challenge from Functional Tech


## stack
* .net core
* Entity framework ORM
* GraphQL
* MS Test
* PostgreSQL


## instalation
### docker
create docker container
```console
docker build -t digital-bank .\
````
run project container
```console
docker run -d -p 8080:80 --name digital-bank digital-bank
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

all the functionalities created are detailed in the documentation made in this [Link](https://dvc.org).
