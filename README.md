# Bookings API

A basic booking api to reserve up to four seats at a venue. A name and email is required for each seat that is booked and a person is not able to book several seats under their name and email.

## Instructions
+ Clone the repository.
+ Load the project into Visual Studio.
+ Run the API.

## Endpoints

+ Get all seats:

  GET http://localhost:49979/api/bookings/

+ Get all available seats:

  GET http://localhost:49979/api/bookings/available

+ Get a particular booking:

  GET http://localhost:49979/api/bookings/{seatId} with a given seat ID, eg A1.

+ Create a booking:

  POST to localhost:49979/api/bookings with the below JSON in the body.
  ```
  [
    {
      "seatId" : "C7",
      "userName": "John234",
      "userEmail": "joh1n@email.com"
    },
    {
      "seatId" : "C8",
      "userName": "John345",
      "userEmail": "john@email.com"
    },
    {
      "seatId" : "C9",
      "userName": "Johnny12",
      "userEmail": "jon@email.com"
    },
    {
      "seatId" : "D10", 
      "userName": "Jo245hny",
      "userEmail": "jo43n@email.com"
    }
  ]
  ```
  
## Future Improvements
+ Attach a database (currently uses in memory database).
+ Add authentication.
+ Add authorisation.
+ Add unit tests.
+ Add a cancelations system to free up seats.
+ Add a reservation system to reserve seats while paying or other.
+ Add restrictions on the GET calls for security.
+ Add payment system.
