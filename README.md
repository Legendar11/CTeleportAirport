# CTeleportAirport

## REST service to measure distance in miles between two airports

Airports are identified by 3-letter IATA code.

Example request: http://localhost:5000/distance/between?from=SCF&to=BGF

## Requirements
- .NET 5.0;
- ports 5000, 5100, 5101, 5102 on localhost are free.

## Docker support
Example: docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d

## Architecture
