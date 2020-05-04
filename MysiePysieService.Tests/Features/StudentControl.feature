Feature: StudentControl
	I want to be able to add,delete, update and view students

Scenario: Add a student
	Given I'm on a students add page
	And I clicked on add student button
	When All student info is filled in
	And I press add
	Then The student should be added to the database
	And All data should match the input

Scenario: Get a list of all students
	Given I'm on students page
	When I click on show all students
	Then A list of students appears on page:
	| ID | Imie | Nazwiko | Wiek | Status |
	| "  | ""   | ""      | ""  | ""    |

Scenario: Update a student 
	Given I'm on a students update page
	And I have perrmision to update a user
	When I fill in the data
	And Click on update
	Then Student is updated
	And All data matches the input

Scenario: Get a single student
	Given I'm on a students page
	When I click on show a single student
	And I fill in student id
	Then A single student object matching the id should be returned

Scenario: Delete a student
	Given I'm on a students page
	And I have a list of students open
	When I click on delete student
	Then Student no longer exists in database

	