Feature: StudentControl
	In order to perform CRUD operations on students
	As the web api client
	I want to be able to Create, Update, Delete, and List Students

Scenario Outline: Create a new student with full data
	Given I am an authorized user
	And I create a new student with full info
	| Forename | Surname  | Age | Status |
	| Bartosz  | Musielak | 23  | Coding |

	When The client puts a request for student creation
	Then a Created status code should be returned
		And the created student info should be returned

Scenario Outline: Create a new student with missing data
	Given I am an authorized user
	And I create a new student with missing info
	| Forename | Surname  | Age | Status |
	|          | Musielak | 23  | Coding |

	When The client puts a request for student creation
	Then a BadRequest status code should be returned

Scenario Outline: Create a new student while unauthorized
	Given I create a new student with full info
	| Forename | Surname  | Age | Status |
	| Bartosz  | Musielak | 23  | Coding |
	When The client puts a request for student creation
	Then a Unauthorized status code should be returned

Scenario Outline: Create a student while a student already exists
	Given I am an authorized user
		And There's a an existing student in database
		| Forename | Surname  | Age | Status |
		| Bartosz  | Musielak | 23  | Coding |

	When I create a new student with same forename, surname and age
	| Forename | Surname  | Age | Status |
	| Bartosz  | Musielak | 23  | Living |
		And The client puts a request for student creation
	Then a Conflict status code should be returned

Scenario Outline: Return an existing student
	Given I am an authorized user
		And There's a an existing student in database
			| Forename | Surname  | Age | Status |
			| Bartosz  | Musielak | 23  | Living |
		And I have the students id
	When The client puts a request for a student with given id
	Then a Ok status code should be returned
		And student with existing student data is returned
	
Scenario Outline: Return a non existing student
	Given I am an authorized user
		And I have the students id
	When The client puts a request for a student with given id
	Then a Not Found status code should be returned

Scenario Outline: Return a list of existing students
	Given I am an authorized user
		And There's a an existing student in database
			| Forename | Surname  | Age | Status |
			| Bartosz  | Musielak | 23  | Living |
		And There's a an existing student in database
			| Forename | Surname | Age | Status		|
			| Szymon   | Sabik   | 22  | Androiding |
		And There's a an existing student in database
			| Forename | Surname | Age | Status |
			| Maciej   | Stachów | 22  | LoLing |
	When The client puts a request for a student list
	Then a Ok status code should be returned
		And a list of existing students is returned

Scenario Outline: Update an existing student
	Given I am an authorized user
		And There's a an existing student in database
			| Forename | Surname  | Age | Status |
			| Bartosz  | Musielak | 23  | Living |
		And I have the students id
	When The client puts a request for a student update
		| Forename | Surname  | Age | Status      |
		| Bartosz  | Musielak | 26  | Still Alive |
	Then a Ok status code should be returned
		And the updated student info should be returned

Scenario Outline: Update users age with a string
	Given I am an authorized user
		And There's a an existing student in database
			| Forename | Surname  | Age | Status |
			| Bartosz  | Musielak | 23  | Living |
		And I have the students id
	When The client puts a request for a student update
		| Forename | Surname  | Age         | Status      |
		| Bartosz  | Musielak | Na pewno ma | Still Alive | 
	Then a BadRequest status code should be returned

Scenario Outline: Update an exisitng student with another student data
	Given I am an authorized user
		And There's a an existing student in database
			| Forename | Surname  | Age | Status |
			| Bartosz  | Musielak | 23  | Living |
		And There's a an existing student in database
			| Forename | Surname | Age | Status  |
			| Maciej   | Sabik   | 23  | Andorid |
		And I have the students id
	When The client puts a request for a student update
		| Forename | Surname | Age | Status  |
		| Maciej   | Sabik   | 23  | Android |
	Then a Conflict status code should be returned

Scenario Outline: Delete an existing student
	Given I am an authorized user
		And There's a an existing student in database
			| Forename | Surname  | Age | Status |
			| Bartosz  | Musielak | 23  | Living |
		And I have the students id
	When The client puts a request for a student deletion
	Then a Ok status code should be returned

Scenario Outline: Delete a non existing student
	Given I am an authorized user
		And I have the students id
	When The client puts a request for a student deletion
	Then a Not Found status code should be returned
