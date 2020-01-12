# Developer Blog Backend

The backend for my developer blog is built with ASP.NET Core 3.1. You can download .NET Core from [here](https://dotnet.microsoft.com/download). The API uses a PostgreSQL database, so you'll need to install [PostgreSQL](https://www.postgresql.org/download/) before you can start the API.

Before running the API, you'll need to add an `appsettings.json` file at the root. It should contain the following:

``` JSON
{
    "ConnectionStrings": {
        "DefaultConnection": "Host=YOUR_HOST;Database=YOUR_DB_NAME;Username=YOUR_USERNAME;Password=YOUR_PASSWORD"
    }
}
```

To run the API, run the command `dotnet run`. This will start the API at `http://localhost:5000`.
