Feature: UpdateAccountSettings

	
@mytag
Scenario: Update account settings
	Given update user "/account/{username}"
	And request body
	When method is PUT
	Then username updated successfully and status code is correct