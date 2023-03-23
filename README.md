MaterEmergencyCareCentreApp

Assumptions

	• A persistent database was not required
	• Comments automatically generated upon admission and discharge.
	• When adding comments, need to know which patient one is adding comments for.
	• When admitting patients, need to know which bed number.
	• When discharging patients, need to know which patient.
	• No error handling on API calls yet

Structure

MaterEmergencyCareCentreApp - Web App written in ASP.NET 6.0 Core 
MaterEmergencyCareCentreApp.API - RESTful API written in ASP.NET 6.0 Core Web API with Open API support
MaterEmergencyCareCentreApp.DataAccess - In memory database db context and repo class library
MaterEmergencyCareCentreApp.Domain - Model entities and DTOs

Installation and testing

Publish MaterEmergencyCareCentreApp.API to IIS C:\inetpub\wwwroot\API
Publish MaterEmergencyCareCentreApp to IIS C:\inetpub\wwwroot\WebApp
Change C:\git\MaterEmergencyCareCentreApp\MaterEmergencyCareCentreApp\appsettings.json the following to point to the API
base URL

```json
  "API": {
    "BaseURL": "https://localhost:7058"
  }
```

Run the API on a different port to the Web App.


