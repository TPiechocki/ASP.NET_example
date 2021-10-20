# ASP.NET_example

## Description

Example shows simple implementaion with elements such as:

- simple web api in .NET Core 5
    - two get endpoints in two different API versions (`/api/v1/satellites` or `/api/v2/satellites`)
    - swagger with versioning to document the API
    - communication to those endpoints require JWT token
    - token can be received using additional auth controller (`/api/v2/Auth/login`, also accesible with v1 url)
    - connection to database for users to authorize and elements for API
        - MS SQL connection
        - entity framework with automatic initialization of the database
    - usage of config file for token signing key
    - automapper for automatic mapping between business models and frontend models

- Angular frontend with:
    - login page (landing page or `/login`)
    - two pages for each version of API endpoint (`/satellites` or `/satellites-obsolete`)
    - passing the JWT token stored in localStorage
        - automatic token addition using `@auth0/angular-jwt`

- automatic frontend contract generation using NSwag

## How to run

### Backend
Run command 
```
dotnet run
```
inside folder `backend/Example.WebApi/Example.WebApi`. After that backend should be available under `https://localhost:5001/`

### Frontend
Run command 
```
npm start
```
inside folder `frontend/example-app`. After that frontend should be available under `http://localhost:4200`
