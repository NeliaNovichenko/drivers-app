# Drivers API

.NET REST APIs for truck drivers. 

## Local development

### Tests
Tests can be executed using 
Tests are running API and database in docker container.


### Database

1. Run database locally in docker - `docker run -d -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=admin -e POSTGRES_DB=drivers-db --name drivers-db -p 5432:5432  --restart=always postgres`
2. Apply EF migrations
    ```ps
    cd .\src\Drivers.Db
    dotnet ef database update --connection 'Host=drivers-server.postgres.database.azure.com;Port=5432;User Id=driversadmin;Password=passwordnotcontaining!1;Database=drivers-db'
    ```

