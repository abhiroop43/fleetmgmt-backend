# Fleet Management System Backend
Backend Web API for the Fleet Management System.

Uses **ASP.NET Core v2** and **IdentityServer4** for authentication.

1. Run the IdentityServer first, then the backend server.
2. To run the identity server navigate to the **FleetMgmt.IdentityServer** directory and run the commands:
    
    *dotnet ef database update --context ConfigurationDbContext*
    
    *dotnet run /seed*
    
   The **seed** flag updates the database with the clients, the Identity resources and the API resources.
   
3. Navigate to the Data directory **FleetMgmt.Data** and run the command:

    *dotnet ef database update --context FmDbContext*
   
   This will populate the database with the tables.
   
4. Navigate to the web server directory **FleetMgmt.Web** and run the command *dotnet run*

##### Please Note: This solution (both the Web API and the IdentityServer) uses SQL Local DB. You may change the connection string from the ***appsettings.json*** file.
