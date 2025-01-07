1. To run the application install the latest .NET SDK and Visual Solution. Open the solution in Visual Studio, build the solution and run GuestLineHotelBookings.Console project.
2. I've assumed that when the Availability command is requested without an end date then it's a 1-night stay
3. I've assumed that when the Search command is requested we want to list any range that has availability but not any combination of ranges
4. I've assumed that check out date releases room for availability
5. The solution could have been a little bit more simplified if only DTOs were used without proper domain models but this adds good business and validation layer aspect