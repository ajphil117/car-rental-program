# Car Rental Program
This project was completed over 5 weeks and required a complete prototype program using C# for a car rental company.
The prototype utilised tools to read/write to a CSV database to store all information about the vehicles available.
In summary, the functionality of this program included a customer system (enter new, modify/delete current), vehicle system (enter new, modify/delete current), search system, and rental system (rent/return car based on a customer account, produce a full report of rented vehicles). 

As this project was individual, this project taught me the importance of time management for each required task.

## Program Features
### File I/O
- Read CRM, Fleet, and Rentals data
- Write to CRM, Fleet, and Rentals data

### CRM and Fleet Functionality
CRM:
- View customers
- Add customer; with validation (all fields are valid options, no repeated customer ID)
- Remove customer; with validation (only if not renting vehicle)
- Edit customer; with validation (same as add)

Fleet:
- View vehicles
- Add vehicle; with validation (all fields are valid options and there are no repeated registrations)
- Remove vehicle; with validation (only if not already being rented)
- Edit vehicle; with validation (same as add)

### Search Functionality
Possible to query...

Simple:
- Any, be able to see any unrented vehicles in the fleet
- Single attribute query (e.g. Red)
- A choice between two attributes; disjunction (e.g. Family OR Luxury)
- A combination of two attributes; conjunction (e.g. Family AND Luxury)

Intermediate:
- A choice of any number of attributes; disjunction (e.g. Family OR Luxury OR Red)
- A combination of any number of attributes; conjunction (e.g. Luxury AND Red)

Advanced:
- A combination of any number of attributes, using both AND and OR, where the operators AND and OR should have the same priority; precedence (e.g. Economy OR Family AND 4-Cylinders)

Stretch:
- A combination of any number of attributes, using AND and OR, with AND having a higher priority than OR, supporting parenthesis to resolve priority (e.g. (GPS AND Sunroof) OR (Red OR Green) AND Commercial OR Luxury)


## Project Code/Files
If you wish to explore the project further the docs can be downloaded [here](/Docs) and the source code [here](/Source%20Code/MRRC).

The downloads consist of the source code, the class diagrams, the program transcript, the user manual, and a list of all functionality.

The README user manual for the project can be found [here](/Docs/README_(User_Manual).txt).
