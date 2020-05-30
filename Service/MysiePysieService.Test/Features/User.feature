Feature: User
	In order to register, login and authorize user actions
	I want to be able to create, update and delete an user

Scenario Outline: Create a new user
	Given I create a new user
		| Username  | FirstName | LastName | Email          | Password | Phone     | userStatus |
		| Pantescik | test      | test     | test@test.test | 1234     | 123456789 | 1          |
	When The client puts a request for account creation
	Then a Created status code should be returned
		And the created user should be returned

Scenario Outline: Login user
	Given There's an existing user in database
		| Username  | FirstName | LastName | Email          | Password | Phone     | userStatus |
		| Pantescik | test      | test     | test@test.test | 1234     | 123456789 | 1          |
	When The client puts a request for a login
	Then a Ok status code should be returned
		And a bearer token should be returned

Scenario Outline: Update user info
	Given I am an authorized user
		And There's an existing user in database
			| Username  | FirstName | LastName | Email          | Password | Phone     | userStatus |
			| Pantescik | test      | test     | test@test.test | 1234     | 123456789 | 1          |
		And i have the clients id
	When The client puts a request for user update
		| FirstName   | LastName    | Email        | Password | Phone     | userStatus |
		| updatedtest | updatedtest | test@test.pl | 12345    | 123456489 | 2          |
	Then a Ok status code should be returned
		And the updated user id and username should match existing user

Scenario Outline: Delete user
	Given I am an authorized user
		And There's an existing user in database
			| Username  | FirstName | LastName | Email          | Password | Phone     | userStatus |
			| Pantescik | test      | test     | test@test.test | 1234     | 123456789 | 1          |
		And i have the clients id
	When The client puts a request for user deletion
	Then a Ok status code should be returned

Scenario Outline: Get a single user wit user id
	Given I am an authorized user
		And There's an existing user in database
			| Username  | FirstName | LastName | Email          | Password | Phone     | userStatus |
			| Pantescik | test      | test     | test@test.test | 1234     | 123456789 | 1          |
		And i have the clients id
	When The client puts a request for a single user
	Then a Ok status code should be returned
		And the existing user is returned

Scenario Outline: Get a list of users
		Given I am an authorized user
			And There's an existing user in database
				| Username  | FirstName | LastName | Email          | Password | Phone     | userStatus |
				| Pantescik | test      | test     | test@test.test | 1234     | 123456789 | 1          |
			And There's an existing user in database
				| Username | FirstName | LastName | Email           | Password | Phone     | userStatus |
				| Pandrugi | Test2     | Test2    | tes2t@test.test | 1234     | 123456789 | 1          |
			And There's an existing user in database
				| Username     | FirstName | LastName | Email             | Password | Phone     | userStatus |
				| PannieTescik | Nietest   | Nietest  | nietest@test.test | 1234     | 123456789 | 1          |
		When The client puts a request for a student list
		Then a Ok status code should be returned
			And a list of existing users is returned