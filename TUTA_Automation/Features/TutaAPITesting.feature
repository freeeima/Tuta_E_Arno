Feature: TutaAPITesting

Background: 
	Given I am using the base url 'http://api.postcodes.io/postcodes/' value

Scenario Outline: As a Service I validate admind_district value in API Response
	Given I setup the request to GET for resource '<postCode>' value
	When I send the request
    Then I should receive a response
	And I should have a status code of <responseCode>
	And I validate '<responseObject>' should have '<ResponseObjectValue>' value 
	Examples: 
	| postCode | responseCode | responseObject  | ResponseObjectValue |
	| LS3 1EP  | 200          | admind_district | Leeds               |
	| NR35 2PF | 200          | region          | East of England     |
	| NR35 222 | 404          | error           | Invalid postcode    |
