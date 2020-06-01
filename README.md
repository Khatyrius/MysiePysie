# MysiePysie
## Project Goals

To create a web service using BDD, with the use of SpecFlow, NUnit and Asp.Net.

## Instalation
1. For the project to work you have to have the listed software installed
- Visual Studio 2019
- MySql Server
2. For the sake of testing the service you need
- Postman
- A web browser(chrome, brave, edge, whatever)
3. Configuring the MySql Server
For the project to work there has to be MySql Server existing, on localhost with username root and password root.
If you already have an existing server with different username and passsword you need to change the files 'Startup.cs' and 'TestStartup.cs' in MysiePysieService folder. Also the server should allow remote connections.

Change the
```var connection = @"server=localhost;uid=root; pwd=root; database=mysiepysie2;"```
variable to match your server, username and password, leave database name as is.

Next thing to do is to import the standard databases from the folder Database, it will create the tables with existing data needed for the service(Standard user etc.).

With it the service should be ready to go, the tests should work and the interaction by postman will be possible.
4. Authorize user throught postman
To get access token you need to make a post request, throught postman, on the path ``localhost:8080/users/login`` with and existing user data. The standard user is "KhatAdmin" with password "SilneHasl@12".
Example:
```
{
	"username":"KhatAdmin",
	"password":"SilneHasl@12"
}
```
It returns an access token which can be put in the authorization section of postman, the type is Bearer Token. It gives access to the rest of the service.

To make a new user to get the access token you need to push a post request on path ``loclhost:8080/users`` with following data in json format:
```
{
    "username": "testUser6",
    "firstName": "Test",
    "lastName": "Test",
    "email": "test6@test.test",
    "password": "Test6",
    "userStatus": 1
}
```
UserStatus has to be set higher than 0 for you to get the access token.
5. Configure MVC part

For the MVC part to work we need to write in the MVC projects nuget package command line the following command:
```
Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
```

It will repair some files which get damaged while downloading the project(tried to solve that, didn't work).

6. Running the project

After setting everything up, you can now start the service project and the mvc project. 
To the MVC you need to login with an existing account to get an authorization. Before doing that no data will be shown in any of the subpages. After logging in you should be ablo to view, update, delete and edit data in the database throught the service.

If you want to do this directly throught the service you need to use postman. The following paths and body templates:

- localhost:8080/users

POST:
```
{
    "username": "testUser6",
    "firstName": "Test",
    "lastName": "Test",
    "email": "test6@test.test",
    "password": "Test6",
    "userStatus": 1
}

```
- localhost:8080/users/login

POST:
```
{
	"username":"KhatAdmin",
	"password":"SilneHasl@12"
}
```

- localhost:8080/students
POST:
```
{
    "forename": "Name",
    "surname": "Surname",
    "age": 23,
    "status": "Alive"
}
```
PUT:
```
{
    "id": id,
    "forename": "Name",
    "surname": "Surname",
    "age": 23,
    "status": "Still Alive"
}
```


The following request paths work the same way: /teachers, /subjects, /classes.
Body templates:

- Teacher:
```
{
    "forename": "Name",
    "surname": "Surname",
    "age": 20
}
```
- Subject(it has to have an exsiting teacher):
```
{
    "name": "Name",
    "ECTSpoints": 20,
    "teacher": {
    	"id": id,
    	"forename": "Name",
    	"surname": "Sutname",
    	"age": 23
    }
}
```
- Classes:
```
{
    "name": "Name",
    "students": [
        {
        	"forename": "Bartosz",
        	"surname": "Musielak",
        	"age": 23,
        	"status": "Getting Even Bettah"
        },
        {
            "forename": "Kuba",
            "surname": "Wojewodzki",
            "age": 50,
            "status": "TV maker"
        }
    ]
}
```

The students list may be empty, it will then create an empty class. If you fill it with data it should assign the existing students to the class or if they don't exist, create them.
