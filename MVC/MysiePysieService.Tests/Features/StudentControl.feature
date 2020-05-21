Feature: StudentControl
	I want to be able to add,delete, update and view students

Background: We're on students page, where we want to be able to view, update, add and delete students

Scenario: We want to add a student having admin role
	Given I'm on a students page
		And I'm logged in with "admin" role
	When I fill in students <forename>, <surname>, <age> and <status>
		And I click on add button
	Then The student should be added to the database
		And All data should match the input
	But the student shouldn't be added if he already exists

Scenario: We want to add a student whithout admin role
	Given I'm on a students page
		And I'm logged in without "admin" role
	When I fill in students <forename>, <surname>, <age> and <status>
		And I click on add button
	Then An error should be returned
		And the student shouldn't be added to the database

Scenario: Get a list of all students
	Given I'm on a students page
	Then A list of all students should appears on page:
	Examples: 
	| ID | Imie       | Nazwisko     | Wiek | Status      |
	| 1  | "Tomek"    | "Stachowiak" | "24" | "Semestr 7" |
	| 2  | "Bartosz   | "Musielak"   | "23" | "Semestr 6" |
	| 3  | "Krystian" | "Sarul"      | "22" | "Semestr 2" |

Scenario: We want to update an student with admin role
	Given I'm on a students page
	And I'm logged in with "admin" role
	When I fill in Students <id>
		And I fill in new <age> which i wished to change
		And Click on update student
	Then Student is updated
		And New age should match input age

Scenario: We want to update an student without admin role
	Given I'm on a students page
	And I'm logged in without "admin" role
	When I fill in Students <id>
		And I fill in new <age> which i wished to change
		And Click on update student
	Then Student shouldn't be updated

Scenario: Get a single student
	Given I'm on a students page
	When I fill in students <id>
		And Click on get a single student
	Then A single student object matching the id should be returned

Scenario: Delete a student with admin role
	Given I'm on a students page
		And I'm logged in with "admin" role
	When I fill in student <id>
		And click on delete student
	Then Student no longer exists in database

Scenario: Delete a student without admin role
	Given I'm on a students page
		And I'm logged in without "admin" role
	When I fill in Students <id>
		And click on delete student
	Then Student shouldn't be deleted and still exist in database

	