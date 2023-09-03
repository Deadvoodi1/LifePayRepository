Feature: DeleteAccount
	Simple calculator for adding two numbers

@mytag
Scenario: Delete user
	Given deleting user "/account/{username}"
	When method is DELETE
	Then user deleted successfully and status code is correct