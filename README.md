#MaterEmergencyCareCentreApp

#Assumptions

* A persistent database was not required
* Comments automatically generated upon admission and discharge.
* When adding comments, need to know which patient one is adding comments for.
* When admitting patients, need to know which bed number.
* When discharging patients, need to know which patient.
* No error handling on API calls yet

#Structure

* MaterEmergencyCareCentreApp - Web App written in ASP.NET 6.0 Core 
* MaterEmergencyCareCentreApp.API - RESTful API written in ASP.NET 6.0 Core Web API with Open API support
* MaterEmergencyCareCentreApp.DataAccess - In memory database db context and repo class library
* MaterEmergencyCareCentreApp.Domain - Model entities and DTOs

#Installation and testing

#IIS Configuration

1. Assuming Windows 10 and IIS 10, go to Turn Windows features on or off
2. Under Internet Information Services, ensure all Application Development Features and Common HTTP Features are turned on, click OK.
3. Ensure .NET 6.0 Hosting Bundle is installed from https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-6.0.15-windows-hosting-bundle-installer

#Create API website

3. Start inetmgr
4. Right mouse click on server, select Add Website..
5. Set Site name: API
6. Set Application pool: API
7. Set Physical path: C:\inetpub\wwwroot\API
8. Set Site Binding Port = 8080, hostname = localhost
9. Select Application Pools > API > Advanced Settings... > Enable 32-Bit Applications > True

#Create WebApp website

10. Right mouse click on server, select Add Website..
11. Set Site name: WebApp
12. Set Application pool: WebApp
. Set Physical path: C:\inetpub\wwwroot\WebApp
13. Set Site Binding Port = 80, hostname = localhost
14. Select Application Pools > API > Advanced Settings... > Enable 32-Bit Applications > True

# Publish API on port 8080

15. Start VS2022 in Admin mode
16. Load MaterEmergencyCareCentreApp\MaterEmergencyCareCentreApp.API.sln
17. Select Build > Publish > C:\inetpub\wwwroot\API
18. Click Publish
19. In IIS, select Web Site > API > Start
20. Open browser to test http://localhost:8080/swagger/index.html


# Publish WebApp on port 80

15. Start VS2022 in Admin mode
16. Load MaterEmergencyCareCentreApp\MaterEmergencyCareCentreApp.sln
17. Select Build > Publish > C:\inetpub\wwwroot\WebApp
18. Click Publish
19. In IIS, select Web Site > WebApp > Start
20. Open browser to test http://localhost


21. If you wish to use a different API port, change C:\git\MaterEmergencyCareCentreApp\MaterEmergencyCareCentreApp\appsettings.json the following to point to the API
base URL

```json
  "API": {
    "BaseURL": "https://localhost:7058"
  }
```
