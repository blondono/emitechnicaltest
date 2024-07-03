# EMI API Employees
## Architecture design
This repository has implemented an hexagonal architecture.

![Hexagonal architecture](https://raw.githubusercontent.com/blondono/emitechnicaltest/main/Resources/hexagonalArchitecture.png)

This architecture is represented by these projects

![projects](https://github.com/blondono/emitechnicaltest/blob/main/Resources/layers.JPG?raw=true)

## Design patterns
The design patterns used are as follows: 
- ### Repository
This patter unify data access, if you have to change data repository just have to change the implementation of IRepository interface.

![repository](https://raw.githubusercontent.com/blondono/emitechnicaltest/main/Resources/repository.png)

- ### Mediator
This pattern centralizes the interactions between the components of a system in a single mediating object. This pattern helps to reduce direct dependencies between objects, in this project I use MediatR library for implement this design pattern.

![repository](https://github.com/blondono/emitechnicaltest/blob/main/Resources/mediator.png?raw=true)
## Endpoints specifications
This solution has 5 endpoints to manage the Employees Information, 1 endpoint to get the Position History and 1 login endpoint.

![repository](https://raw.githubusercontent.com/blondono/emitechnicaltest/main/Resources/endpoints.jpg)

## Others specifications
- This solution implement JWT authentication and authorization with SQL Server tables for store users (Employees) and roles (Position)
- This solution implement code first to manage the database tables 
- This solution has a log middleware that track all request.
