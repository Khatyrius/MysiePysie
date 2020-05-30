Feature: SubjectControl
	In order to perform CRUD operations on subjects
	As the web api client
	I want to be able to Create, Update, Delete, and List subjects

Scenario Outline: Create a new subject
	Given I am an authorized user
		And There's an existing teacher in database
			| Forename | Surname  | Age |
			| Pan      | Muzyk | 54  |
		And i have the teachers id
		And i create a new subject
			| Name   | EctsPoints | Teacher |
			| Muzyka | 5          |         |
	When The client puts a request for subject creation
	Then a Created status code should be returned
		And the created subject is returned

Scenario Outline: Get an existing subject
	Given I am an authorized user
		And There's an existing teacher in database
			| Forename | Surname | Age |
			| Pan      | Muzyk   | 54  |
		And there's an existing subject
			| Name   | EctsPoints | Teacher |
			| Muzyka | 5          |         |
		And i have the subject id
	When The client puts a request for a subject with givenId
	Then a Ok status code should be returned
		And the returned subject matches the existing one

Scenario Outline: Update an existing subject
	Given I am an authorized user
		And There's an existing teacher in database
			| Forename | Surname | Age |
			| Pan      | Muzyk   | 54  |
		And There's an existing teacher in database
			| Forename | Surname     | Age |
			| Pan      | LepszyMuzyk | 55  |
		And there's an existing subject
			| Name   | EctsPoints | Teacher |
			| Muzyka | 5          |         |
		And i have the subject id
	When i update the subject teacher to the second one
	Then a Ok status code should be returned
		And the subject teacher is changed