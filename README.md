# ASP.NET_example

Example shows simple implementaion with elements such as:

- simple web api in .NET Core 5
    - two get endpoints in two different API versions
    - swagger with versioning to document the API
    - communication to those endpoints require JWT token
    - token can be received using additional auth controller
    - connection to database for users to authorize and elements for API
        - MS SQL connection
        - entity framework with automatic initialization of the database
    - usage of config file for token signing key
    - automapper for automatic mapping between business models and frontend models

- Angular frontend with:
    - login page
    - two pages for each version of API endpoint
    - passing the JWT token stored in localStorage
        - automatic token addition using `@auth0/angular-jwt`

- automatic frontend contract generation using NSwag
