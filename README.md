# Issue Tracker
A simple software development issue tracking app. Built using .NET 6 / ASP.NET.


## Solution Architecture
![Image](.github\readme\arch.png)

## Running 
Currently, the project is configured to use an SQL server running in a docker container. 

Running *./SetupSQLDocker.bat* should setup a locally hosted SQL Server on port 1433. To use a custom sql server, modify *IssueTracker/Common/appsettings.json*
and change the *default* connection string. The connection string below would use a locally installed version of SQL Server.
```json
{
  "ConnectionStrings": {
    "default": "Server=(localdb)\\mssqllocaldb;Database=aspnet-IssueTracker;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```

Eventually, IssueTracker itself will run from a Docker container too (WIP)
