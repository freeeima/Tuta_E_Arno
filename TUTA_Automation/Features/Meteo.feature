Feature: Meteo


Background: 
	Given I am using meteo base url 'https://api.meteo.lt/v1/places/' value

Scenario: As a Service I validate forecast value in API Response
	Given I setup the request to GET for city resource 'kaunas/forecasts/long-term' value
	When I send the meteo request
    Then I should receive meteo response
	And I should have response status code of 200
	And I validate of 'administrativeDivision' should have 'Kauno miesto savivaldybė' value
	And I validate of 'longitude' should have '23.904482' value

	## Negative test case
	Scenario: As a Service I validate forecast false value in API Response
	Given I setup the request to GET for city resource 'kaznas/forecasts/long-term' value
	When I send the meteo request
	Then I should receive meteo response
	And I should have response status code of 404
	##
#	## Update test case ussing 'Outline and Examples'
	Scenario Outline: As a Service I validate forecast value in Kaunas in API Response
	Given I setup the request to GET for city resource '<cityResource>' value
	When I send the meteo request
	Then I should receive meteo response
	And I should have response status code of <responseCode>
	And I validate of '<responseObject>' should have '<ResponseObjectValue>' value
	Examples:
	| cityResource | responseCode | responseObject | ResponseObjectValue |
	| kaunas/forecasts/long-term | 200 | administrativeDivision | Kauno miesto savivaldybė |
	| kaunas/forecasts/long-term | 200 | longitude | 23.904482 |
	| kavnas/forecasts/long-term | 404 | error | Not Found |



 