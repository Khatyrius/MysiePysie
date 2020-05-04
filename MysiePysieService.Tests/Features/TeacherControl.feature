Feature: TeacherControl
	I want to be able to add,delete, update and view Teachers

Scenario: Add a tacher
	Given I'm on a tachers add page
	And I clicked on add tacher button
	When All tacher info is filled in
	And I press add
	Then The tacher should be added to the database
	And All data should match the input

Scenario: Get a list of all tachers
	Given I'm on tachers page
	When I click on show all tachers
	Then A list of tachers appears on page:
	| ID | Imie | Nazwiko | Wiek |
	| "  | ""   | ""      | ""   |

Scenario: Update a tacher 
	Given I'm on a tachers update page
	And I have perrmision to update a tacher
	When I fill in the data
	And I click on update
	Then Teacher is updated
	And All data matches the input

Scenario: Find teacher by subject taught
	Given I'm on a teachers page
	When I click on find teacher by subject
	And I fill in subject name
	Then a teacher who teaches that subject is returned

Scenario: Get a single tacher
	Given I'm on a tachers page
	When I click on show a single tacher
	And I fill in tacher id
	Then A single tacher object matching the id should be returned

Scenario: Delete a tacher
	Given I'm on a tachers page
	And I have a list of tachers open
	When I click on delete teacher
	Then Teacher no longer exists in database

	