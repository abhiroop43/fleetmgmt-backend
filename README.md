# Fleet Management System Backend

Backend Web API for the Fleet Management System.

Uses **ASP.NET Core v2** and **IdentityServer4** for authentication.

1. Run the IdentityServer first, then the backend server.
2. To run the identity server navigate to the **FleetMgmt.IdentityServer** directory and run the commands:

    _dotnet ef database update --context ConfigurationDbContext_

    _dotnet run /seed_

    The **seed** flag updates the database with the clients, the Identity resources and the API resources.

3. Navigate to the Data directory **FleetMgmt.Data** and run the command:

    _dotnet ef database update --context FmDbContext_

    This will populate the database with the tables.

4. Navigate to the web server directory **FleetMgmt.Web** and run the command _dotnet run_

##### Please Note: This solution (both the Web API and the IdentityServer) uses SQL Local DB. You may change the connection string from the **_appsettings.json_** file.

## Updating from a previous version

If updating from a previous version, first create a new migration, to handle the breaking changes introduced by EF Core update.

1. Navigate to the **FleetMgmt.IdentityServer** directory and run the commands:

_dotnet ef migrations add FrameworkUpdate_

_dotnet ef migrations add FrameworkUpdate --context ConfigurationDbContext_

Now, you may seed the database again using:

_dotnet run /seed_

2. Navigate to the Data directory **FleetMgmt.Data** and run the commands:

_dotnet ef migrations add FrameworkUpdateWeb --context FmDbContext -s ..\\FleetMgmt.Web\\_

_dotnet ef database update --context FmDbContext -s ..\\FleetMgmt.Web\\_

3. Now navigate to the web server directory **FleetMgmt.Web** and run the command _dotnet run_
