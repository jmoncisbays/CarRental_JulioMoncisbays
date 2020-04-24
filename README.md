## carRental_WebAPI
ASP.NET Core 3.1 Web API project. The solution can be opened in VS 2019.

This project connects to the DB carRental_JulioMoncisbays (steps below to create it). By default the DB server is (localdb)\MSSQLLocalDB, but this can be changed by editing the connection string located in the appsettings.json file.

Once you run the project, you can test the endpoints under http://localhost:51682/

Info and details of the endpoints can be found via Swagger under http://localhost:51682/swagger, which will be opened when you run the project in VS.

All endpoints are secured so a JWT is required, except for the endpoint http://localhost:51682/auth/login, which is used to get the mentioned token. Since it is a POST type action method it expects the following body:
```
{
	"userName": "admin",
	"password": "Altomobile"
}
```

Please note that the credentials are fake and hardcoded.

Once you run the auth/login endpoint, you'll receive the JWT, which then can be attached to the headers for the secured endpoints like this:
```
...
Key: Authorization
Value: bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlx
...
```

## Steps to create the carRental_JulioMoncisbays DB
1. Run the script file **DB Script.sql**. It will generate the DB and its tables, creating the .mdf and .ldf files under the C:\Users\<current_user> directory.
2. Run the script file **INSERTs script.sql**. It will populate the tables with dummy data.
