Feature: ClassesControl
	I want to be able to add,delete, update and view classes

Scenario: Get a list of all classes
	Given I'm on classes page
	When I click on get all classes
	Then a list of all classes is returned in a table:
	| ID | Name | Students                            |
	| "" | ""   | link do listy studentow danej klasy |

Scenario: Update a class 
	Given I'm on classes page
	When I fill in the data
	And Click on update
	Then Class is updated
	And All data matches the input

Scenario: Get a single class
	Given I'm on classes page
	When I click on find a class
	And I fill in ID
	Then a class with given id is returned

Scenario: Delete a class
	Given I'm on classes page
	And I have a list of classes
	When I click on delete class
	Then The class is deleted from database

Scenario: Add a user to class
	Given I'm on a classes page
	And I have a list of classes
	When I click on add student to class
	And fill in the student id
	Then the student is added to the class list of students