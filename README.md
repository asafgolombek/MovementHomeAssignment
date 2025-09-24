.NET API with Multi-Layered Data Storage
This project is a .NET Web API that demonstrates a layered data retrieval service using caching, file storage, and a database. It is built with a clean architecture, utilizing several common design patterns to ensure the code is maintainable, testable, and scalable.

Features
.NET 9 Web API: Built on a modern, cross-platform framework.

Layered Architecture: Follows a clean architecture pattern to separate concerns (API, Application, Infrastructure).

Multi-Layered Data Retrieval: Implements a Cache -> File -> Database data fetching strategy.

Dependency Injection: Properly configured DI for loose coupling and testability.

Repository Pattern: Abstracts database interactions.

Factory Pattern: Used to create instances of different data sources (Cache, File, DB).

Swagger/OpenAPI: Provides interactive API documentation.

FluentValidation: For clean and robust request model validation.

AutoMapper: For simplified object-to-object mapping (e.g., Entity to DTO).

Polly: Implements a resilience policy for database calls to handle transient faults.

Setup Instructions
Prerequisites
.NET 9 SDK or newer.

A .NET IDE like JetBrains Rider or Visual Studio 2022.

Postman for testing the API.

Running the Application
Clone the Repository:

git clone <your-repository-url>
cd <your-project-directory>

Open the Solution: Open the .sln file in Rider or Visual Studio.

Restore Dependencies: The IDE should automatically restore the required NuGet packages. If not, you can do it manually via the terminal:

dotnet restore

Run the Project:

In Rider/Visual Studio, click the green "Run" button to start the web API project.

Alternatively, run from the terminal:

dotnet run --project <path-to-your-webapi.csproj>

Access the API:

The API will be running locally, typically at http://localhost:5045.

You can access the interactive Swagger documentation by navigating to the base URL in your browser.

How to Use the Postman Collection
The postman_collection.json file in the Canvas contains pre-configured requests to test all the API's functionality.

1. Import the Collection
Open Postman.

Click the "Import" button at the top left.

Drag and drop the postman_collection.json file into the import window.

A new collection named "Multi-Layered Data API" will appear on the left sidebar.

2. Configure the baseUrl Variable (if needed)
The collection comes with a baseUrl variable set to http://localhost:5045. If your application runs on a different port, you can edit this variable:

Click on the "Multi-Layered Data API" collection.

Go to the "Variables" tab.

Update the CURRENT VALUE of baseUrl to match your application's URL.

3. Testing Workflows
The collection is organized into two folders for different testing scenarios.

Automated Workflow
This workflow is designed for quickly testing the create-read-update cycle.

Run 1. Create Data (POST): This request creates a new data item. A test script automatically saves the id of the newly created item to a collection variable.

Run 2. Get Data by ID (GET): This request will automatically use the id saved in the previous step to fetch the data.

Run 3. Update Data (PUT): This request will also use the saved id to update the content of the data item.

Manual Workflow
This workflow allows you to test any specific id you want.

First, run the Create Data (POST) request (or get an ID from your database/logs).

Copy the id from the response body of a created item.

Go to the collection's "Variables" tab.

Paste the id into the CURRENT VALUE field for the manualDataId variable.

Now you can run the Get Data by Manual ID and Update Data by Manual ID requests, and they will target the specific ID you pasted.