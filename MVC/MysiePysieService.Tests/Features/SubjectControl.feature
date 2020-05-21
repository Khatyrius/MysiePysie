Feature: SubjectControl
	I want to be able to add, delete, update and view subjects

Scenario: Get a list of all existing subjects
	Given I'm on subjects page
	When I click the 'show all subjects' button
	Then a list of subjects is returned in a table on page:
	| ID | Nazwa | Punkty ECTS | Nauczyiel |
	| "" | ""    | ""          | ""        |

Scenario: Update a subject
	Given I'm on subjects page
	When I click on updatge subject with id
	And I fill in the data
	Then a subject with the given id is updated

Scenario: Add a subject
	Given I'm on subjects page
	When I click on add a new subject
	And I fill in the data
	Then A new subject matching the data is created

Scenario: Get a single subject
	Given I'm on subjects page
	When I click on find a subject by id
	Then a subject with given id is returned

Scenario: Delete a subject
	Given I'm on subjects page
	And I have a list of all subjects
	When I click on delete subject
	Then subject is removed from the database