# HomesForSale

Homes for sale is a project that allows users manage different types of properites by adding, viewing, deleting and updating them. 
The project is separated into Business logic and Data access layers.  

## Technologies used: 
* C#
* .NET
* GTK#

## Property types

The project has two types of properties
* Commercials
* Residentals

All properties inherit from the base class Estate.
All commercials inherit from the commercial base class which in turn inherits from the estate class.
Residentals inherit from the residental class, which inherits from the estate class as well.

### Commercials
* Warehouse
* Shop

### Residentals
* Apartment
* House
* Villa
* Townhouse
