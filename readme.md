# FileManagementService

This is a stub REST API for uploading, downloading, and managing files, built in .NET Core 3.1.  It utilizes MongoDB as a datastore, and is (simplistically) HATEOAS compliant.

It's built on a repository/service pattern and, despite lacking any complex business logic, has appropriate separation to expand or deepen it however desired.  Feel free to use as a scaffold for anything else too (it mostly follows typical API repository pattern architecture).  Swagger is provided at the path root for reference and Postman scaffolding.

## Usage

Edit docker-compose.yml as desired for port mapping, and Dockerfile for API localhost access.

Visual Studio 2019
```
Just run it!
```

Console
```
docker-compose up
docker-compose down
```

Mongo-Express is available by default on port 8081 on local, you can monitor the file collection there (requires refresh).

## Reasoning

MongoDB was chosen because the document store is (unsurprisingly) ideal for storing nonrelational document data.  To avoid loading file contents into memory while getting the collection for summary display, lazy loading and AutoMapper's Project functionality are utilized.  The remainder of the architecture is designed to be as standards-compliant, clean, testable, and expandible as possible.  Moq is lightly utilized in unit testing to mock the repository layer, which allows the testable application logic to be portable between data providers.

## Improvements

The unit test coverage leaves some to be desired, and ideally I would mock internal MongoDB interfaces to provide a context test layer, as well as add unit tests for the controllers (or use a Postman collection).  I might move Automapper to the business layer to allow for logic utilizing multiple DTO models, which could be useful with more integrations or usertypes.  It lacks authentication/authorization, so whatever desired provider could be added as well.  It'd be simple to add a delete link to the file summaries as well, but I chose to omit it for now, as such functionality will likely not be available to all users.