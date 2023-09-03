Feature: GetUser


@mytag
Scenario: Get user
	Given user "/account/{username}"
	When method is GET
	Then user found and status code is correct