Feature: Login

	
	@mytag
	Scenario: User Login
		Given login path "/account/login"
		When method is GET
		Then login is successfull and status code is correct