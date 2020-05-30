Feature: TeacherControl
	In order to perform CRUD operations on teachers
	As the web api client
	I want to be able to Create, Update, Delete, and List Teachers

Scenario Outline: Create a teacher
	Given I am an authorized user
		And I create a new teacher with full info
			| Forename | Surname   | Age |
			| Pan      | Algerbyk | 54  |
	When The client puts a request for teacher creation
	Then a Created status code should be returned

Scenario Outline: Get an existing teacher
	Given I am an authorized user
		And There's an existing teacher in database
			| Forename | Surname  | Age |
			| Pan      | Algerbyk | 54  |
		And i have the teachers id
	When The client puts a request for a teacher with given id
	Then a Ok status code should be returned
		And the existing teacher info should match the given info

Scenario Outline: Get a list of all teachers
	Given I am an authorized user
		And There's an existing teacher in database
			| Forename | Surname  | Age |
			| Pan      | Algerbyk | 54  |
		And There's an existing teacher in database
			| Forename | Surname | Age |
			| Pan      | Muzyk   | 54  |
		And There's an existing teacher in database
			| Forename | Surname   | Age |
			| Pan      | Matematyk | 54  |
	When The client puts a request for a teacher list
	Then a Ok status code should be returned
		And the existing teachers list should match the exisitng teachers

Scenario Outline: Update an teacher 
	Given I am an authorized user
		And There's an existing teacher in database
			| Forename | Surname  | Age |
			| Pan      | Algerbyk | 54  |
		And i have the teachers id
	When The client puts a request for teacher update
		| Forename | Surname  | Age |
		| Pan      | Algebryk | 55 |
	Then a Ok status code should be returned
		And the returned teacher should match updated teacher info


Scenario Outline: Delete an existing teacher
	Given I am an authorized user
		And There's an existing teacher in database
			| Forename | Surname  | Age |
			| Pan      | Algerbyk | 54  |
		And i have the teachers id
	When The client puts a request for teacher deletion with given id
	Then a Ok status code should be returned