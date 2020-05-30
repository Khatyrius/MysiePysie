Feature: ClassesControl
	In order to perform CRUD operations on classes
	As the web api client
	I want to be able to Create, Update, Delete, and List classes

Scenario Outline: Create a new empty class
	Given I am an authorized user
		And I create a new class
			| Name   |
			| Klasa1 |
	When The client puts a request for a class creation
	Then a Created status code should be returned
		And the created class is returned

Scenario Outline: Get an existing class
	Given I am an authorized user
		And There's an existing class
			| Name   |
			| Klasa1 |
		And I have the class id
	When The client puts a request for class with given id
	Then a Ok status code should be returned
		And The exisitng class is returned

Scenario Outline: Get a list of classes
	Given I am an authorized user
		And There's an existing class
			| Name   |
			| Klasa1 |
		And There's an existing class
			| Name   |
			| Klasa2 |
		And There's an existing class
			| Name   |
			| Klasa3 |
	When The client puts a request for classes list
	Then a Ok status code should be returned
		And a list of classes is returned

Scenario Outline: Update a class
	Given I am an authorized user
		And There's an existing class
			| Name   |
			| Klasa1 |
		And I have the class id
	When The client puts a request for class update
		| Name   |
		| Klasa2 |
	Then a Ok status code should be returned
		And the updated class id matches the existing class id

Scenario Outline: Delete a class
	Given I am an authorized user
		And There's an existing class
			| Name   |
			| Klasa1 |
		And I have the class id
	When The client puts a request for class deletion
	Then a Ok status code should be returned

Scenario Outline: Add a student to a class
	Given I am an authorized user
		And There's an existing class
			| Name   |
			| Klasa1 |
		And I have the class id
		And There's a an existing student in database
			| Forename | Surname  | Age | Status |
			| Bartosz  | Musielak | 23  | Coding |
		And I have the students id
	When The client puts a request to add a student to class
	Then a Ok status code should be returned
		And the student should be in the classes student list

Scenario Outline: Remove student from class
	Given I am an authorized user
		And There's an existing class
			| Name   |
			| Klasa1 |
		And I have the class id
		And There's a an existing student in database
			| Forename | Surname  | Age | Status |
			| Bartosz  | Musielak | 23  | Coding |
		And I have the students id
		When the student is in the class
			And The client puts a request to remove student from class
		Then a Ok status code should be returned
			And student is no longer in class