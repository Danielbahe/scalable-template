# scalable-template
 .Net 7 Web Api scalable template for big projects.
## How to use
- Install docker
- Execute docker-compose
- Enjoy!
## Concepts applied to the project (Architecture, patterns...)
- SOLID
- Clean Architecture
- Hexagonal Arquitecture (Ports & Adapters)
- VSA (Vertical Slice Architecture)
- DDD (Domain Driven Design)
- Domain Events
- Integration Events
- Outbox Pattern (using database, not included feature in Masstransit)
- CQRS (Command Query Responsibility Segregation)
- Mediator
- Unit of Work
- Repositories
- Postgres SQL (easy to change to another provider)
- Result Control 
- Rest
- Restful Responses
- Docker & Docker Compose
- .Net 7
- Entity Framework
- Serilog
- Simple Create example
- Get all Paginated example
- Unit Tests
- Integration Test (WIP)
- CI (github workflow)
    - Build
    - Unit test
    - Integration Test (WIP)
- CD (WIP)

## Some personal thoughts
As a big fan of YAGNI (You aren't gonna need it) I have opposed feelings about this template.

I know that some projects won't need some parts (yes Integration events I'm looking at you), but, this is a showcase project, so... let me be.

On the other hand, I believe that is really simple to get rid off any part withouy a descomunal effort and in case you really need it, is big work that you get for free, so, you are welcome?
## Is not perfect, I know it, and feedback is welcome!
My intention is to maintain this project updated.
Also I will try to  improve, refactor and apply new concepts continuosly.
Pull request, feedback and questions are widely open, so don't hesitate.