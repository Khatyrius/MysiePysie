Feature: User
	In order to make register, login
	And validate user permissions

Scenario: Create a new user
	Given I'm on register page
	And I have filled in the data
	When I press register user
	And all data has valid form
	Then New user is created

Scenario: Create users from list
	Given I have a list of users to add
	When i click on add users from list
	Then all users from the list are created

Scenario: User wants to log in
	Given I'm on login page
	And user info is filled in
	When I click on login button
	Then User is logged in

Scenario: User wants to log out
	Given I'm on any page
	When I click logout button
	Then User gets logged out and redirected to home site

Scenario: Delete a user
	Given I'm on users page
	And i have a list of users
	And admin status
	When I click on delete user
	Then user gets deleted

Scenario: Update a user
	Given I'm on users page
	And i have a list of users
	When I click on update user
	And I fill in the data
	Then User gets updated
	
