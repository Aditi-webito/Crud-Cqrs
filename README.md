The CQRS (Command Query Responsibility Segregation) pattern is an architectural pattern that separates operations that read data from operations that write data. 
This pattern is particularly useful in Web APIs, where it can help improve performance, scalability, and maintainability.

CQRS separates these operations into two distinct models:

Commands: Handle write operations (Create, Update, Delete). Commands are used to perform actions that modify the state of the application.
Queries: Handle read operations (Read). Queries are used to retrieve data from the application.
