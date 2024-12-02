# Drivers API

This project provides .NET REST APIs for managing truck drivers. 
- .NET 8: Framework for building RESTful APIs.
- Azure Functions: Serverless platform for hosting APIs.
- PostgreSQL: Database for persisting data.
- Entity Framework Core: ORM for database management.
- Docker: Containerization for local development and testing

## API Documentation
The API is available at:
https://driversfunctionapp.azurewebsites.net/api/swagger/ui. 

Use this link to explore and test the endpoints interactively via Swagger UI.


## Local development

### Prerequisites
Ensure the following tools are installed on your machine:

- [.NET SDK 8.0](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)

### Tests
Tests can be executed using using VS test runner or `dotnet test` command. Tests are running API and database in docker containers.


### Database

1. Run database locally in docker - `docker run -d -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=admin -e POSTGRES_DB=drivers-db --name drivers-db -p 5432:5432  --restart=always postgres`
2. Apply EF migrations
    ```ps
    cd .\src\Drivers.Db
    dotnet ef database update --connection 'Host=drivers-server.postgres.database.azure.com;Port=5432;User Id=driversadmin;Password=passwordnotcontaining!1;Database=drivers-db'
    ```
    