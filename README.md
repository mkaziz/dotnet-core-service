# dotnet-core-service

This project is intended to explore the usage of .NET Core as a Web API.

To run it, clone the the project and load the solution in Visual Studio, and run locally on IIS express.

## API Contract
### Rates
* The API Endpoint is `/api/Rates/?StartDate=&EndDate=`
* Start and end dates are accepted in the following iso format: 2015-07-01T12:00:00Z
* A "Rates" value in cents is returned

### Statistics
There are two API EndPoints exposed. Note that statistics are saved in memory only, so if the application restarts, logged times will be lost. 

#### `/api/statistics/getall`
Returns a list of all API calls and associated average times. 

#### `/api/statistics/getbykey?key=api/rates`
Returns a list of the API call requested (by key)

## Data Source
* The data source is currently a local json file, whose path is configured in the `appsettings.json` of the Api project. 
* This could in the future be updated to full from SQL via EF, or other more robust data sources
* The data source is expected to correctly follow the format of the provided sample file. 

## Project Structure
The project uses .NET Core Dependency Injection

### API Project
This contains only the endpoint from which the data is accessed

### BusObj Project
The key class here is the `RatesRepository`. It basically uses the `JsonFileRetrievalService` and `JsonFileParserService` to create a set of models that are then used to perform the request.
Note that currently, all of these are Request-scoped. 
