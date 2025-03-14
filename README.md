# Survivor API

## General Information
Survivor API is a RESTful API developed to manage competitors belonging to specific categories. This API is designed to provide the necessary data for frontend developers.

## Technologies Used
- **Backend:** C# and ASP.NET Core
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core
- **API Documentation:** Swagger

## Installation and Running
### Requirements
- Visual Studio
- .NET SDK
- SQL Server
- PostgreSQL

### Running Steps
1. Clone the repository:
   ```sh
   git clone <repo-url>
   cd SurvivorApi
   ```
2. Install dependencies:
   ```sh
   dotnet restore
   ```
3. Set the database connection string (`appsettings.json` or via environment variables):
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=Survivor_Api_Db;Username=username;Password=password"
   }
   ```
4. Apply migrations:
   ```sh
   dotnet ef migrations add init
   dotnet ef database update
   ```
5. Run the API:
   ```sh
   dotnet run
   ```
6. Use **Swagger** to test the API:
   - **URL:** `http://localhost:<port>/swagger`

## API Usage
### CategoryController
| HTTP Method | Endpoint | Description |
|------------|---------|-------------|
| POST | `api/Category` | Adds a new category |
| GET | `api/Category` | Retrieves all categories |
| GET | `api/Category/{id}` | Retrieves a category by its ID |
| PUT | `api/Category/{id}` | Updates a category by its ID |
| DELETE | `api/Category/{id}` | Soft deletes a category by its ID |

### CompetitorController
| HTTP Method | Endpoint | Description |
|------------|---------|-------------|
| POST | `api/Competitor` | Adds a new competitor |
| GET | `api/Competitor` | Retrieves all competitors |
| GET | `api/Competitor/Category/{CategoryId}` | Retrieves competitors within a specific category |
| GET | `api/Competitor/{id}` | Retrieves a competitor by its ID |
| PUT | `api/Competitor/{id}` | Updates a competitor by its ID |
| DELETE | `api/Competitor/{id}` | Soft deletes a competitor by its ID |

### Sample Request
```json
{
  "name": "string"
}
```

### Sample Response
```json
[
  {
    "name": "Gönüllüler"
  },
  {
    "name": "Ünlüler"
  }
]
```

## Testing
- **Swagger** is used for API testing.
- API endpoints can be explored via `http://localhost:<port>/swagger`.

## Contribution
There are no specific rules for contributing to this project. Anyone is free to make modifications.

## License
This project is licensed under the MIT License.

