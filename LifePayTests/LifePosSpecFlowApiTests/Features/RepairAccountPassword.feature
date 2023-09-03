Feature: RepairAccountPassword

	
@mytag
Scenario: Add two numbers
	Given path "/account"
	And phone number
	And And request body
	When method is POST
	Then password repaired successfully and status code is correct