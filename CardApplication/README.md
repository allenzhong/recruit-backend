# Credit Card Api

## Requirement
- .Net Core 3.1
- Auth0 Api Creditials
- SQLServer (Docker version recommended)

## Projects and Folders

- CardApplication: WebApi Application
- CardApplication.DbMigration: DB Migration Project by using FluentMigrator
- CardApplication.Test: Unit tests and DB tests. 
- CardApplication.IntegrationTest: Integration Test with positive flows
- Document: Open Api standard document. [SwaggerHub Document](https://app.swaggerhub.com/apis/AllenZhong/CardManagement/1.0.0)
  
### CardApplication 

The endpoints have been implemented Authorization with Auth0's client credentials flow. To run the application, the following sections need to be done

```json
  "Auth0": {
    "Domain": "",
    "ApiIdentifier": ""
  },
  "ConnectionStrings": {  
    "DefaultConnection": ""
  }
```

**Implemented Endpoints**:
- [x] Register Card
- [x] Get All 
- [x] Get By Id
 

To be continue:
- Authorization Code Flow: Authorized User can login and use the token to access Api.
- Get All with query string


### CardApplication.DbMigration

This project is to manage database change and to run the changes consistently. 
It is integrated into WebAPI application so that the migrations will be run when the WebApi server spin up. 

### CardApplication.Test

This is test project. To run the db tests, the db connection string needs to be configured in `appsetting.json` in the project.

```json
  "ConnectionStrings": {  
    "DefaultConnection": ""
  }
```

### CardApplication.IntegrationTest

This is the integration test. The tests are in order. Ideally it should test the Api flows with different status code. 

The current flow
1. Test all endpoints without access token should return 401
2. Test register endpoint to create a record
3. Test get all and get id endpoint to make sure record has been saved. 


To run it, please config the follow part. 

```json
  "Auth0": {
    "Domain": "",
    "Audience": "",
    "ClientId": "",
    "ClientSecrets": "",
    "GrandType": ""
  },
  "Server": {
    "BaseUrl": ""
  }
```

And start up the server, then this project can be run as tests project. 