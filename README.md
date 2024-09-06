# N5BackendPermissions
## Net 7.0 Project
### Instructions to Run the Project
1. Navigate to the Project Folder

    Open your terminal or command prompt and change to the N5Api directory:

    ```cmd
    cd path\to\N5Api
    ```

2. Build and Start the Docker Containers

    Execute the following command to build and start the Docker containers in detached mode:

    ```cmd
    docker-compose up --build -d
    ```

    This command will create and start the services defined in the docker-compose.yml file.

## For Development

1. Change appsettings
   Change:
   ```json
   "Kafka": {
        "Server": "kafka:29092"
    }
   ```

    for

   ```json
   "Kafka": {
        "Server": "localhost:29092"
    }
   ```

   and in docker-compose.yml it must be:

    ```yml
        KAFKA_ADVERTISED_LISTENERS: INSIDE://kafka:9092,OUTSIDE://localhost:29092
        # KAFKA_ADVERTISED_LISTENERS: INSIDE://kafka:9092,OUTSIDE://kafka:29092
    ```

2. Update the Database

    Run the following command to apply database migrations using Entity Framework Core:

    ```cmd
    dotnet ef database update --project .\N5Infrastructure\N5Infrastructure.csproj --startup-project .\N5Api\N5Api.csproj
    ```

    This command updates the database schema to match the current model.

## Notes
- Ensure that Docker and .NET SDK (version 7.0) are installed on your machine before running the above commands.
- For any additional configuration or troubleshooting, refer to the project's documentation or contact the development team.