Feature: CreateAccount

	
@mytag
Scenario: Create account
	Given path "/account"
	And request body
	When method is POST
	Then user is created and response is correct