## carRental_WebAPI
ASP.NET Core 3.1 Web API project. The solution can be opened in VS 2019.
This project connects to the DB carRental_JulioMoncisbays (steps below to create it). By default the DB server is (localdb)\MSSQLLocalDB, but this can be changed by editing the connection string located in the appsettings.json file.

The project contains the following controllers:
- CarBrands (endpoints: GET, POST, DELETE)
- CarModels (endpoints: GET, POST, DELETE)
- Cars (endpoints: GET, search, POST, DELETE)
- CarTypes (endpoints: GET, POST, DELETE)
- Reports (GET)

Once you run the project, you can try the endpoints under http://localhost:51682/

## Steps to create the carRental_JulioMoncisbays DB
1. Run the script file **DB Script.sql**. It will generate the DB and its tables, creating the .mdf and .ldf files under the C:\Users\<current_user> directory.
2. Run the script file **INSERTs script.sql**. It will populate the tables with dummy data.
