# OnionArchitectureDemo

This solution demonstrates Onion architecture

**Domain - Innermost**
------------------------------------------------------------
**OnionApp.Domain.Entities**:
The central core (1st layer) of the Onion and contains the main domain models (aka the database entities). 
It does not depend on other layers.

**OnionApp.Domain.Services**:
2nd layer in the Onion. Contains the Repository operations contracts, i.e, operations against the domain models.  
Depends on the OnionApp.Domain.Entities

AppServices - Next outer layer
-----------------------------------------------------------
**OnionApp.AppServices.Common**:
A segment of the 3rd layer in the Onion. Contains a bunch of contracts used across the different segments in the AppService layer.
Mainly: DbContext interfaces, Service interfaces, DTOs

**OnionApp.AppServices.Api**:
A segment of the 3rd layer in the Onion. Contains implementations of the Business contracts defined in the OnionApp.AppServices.Common layer.
Depends on OnionApp.AppServices.Common - to consume the business operation contracts.
Depends on the OnionApp.Domain.Entities - to refer to domain models
Depends on the OnionApp.Domain.Services - to consume domain operation contracts, i.e., operations against domain models.

**OnionApp.AppServices.Repository**:
Another segment of the 3rd layer in the Onion. Contains the implementation of the data access required by the business operations defined in OnionApp.AppServices.Api.
It also contains the contracts required for the data access (DbContext contracts) to the underlying database infrastructure
Depends on OnionApp.AppServices.Common - to consume the DbContext contracts.
Depends on the OnionApp.Domain.Entities - to refer to domain models
Depends on the OnionApp.Domain.Services - to refer to the operations (interfaces to be implemented) against domain models.

Apps + Infrastructure - Outermost layer (aka external services)
-----------------------------------------------------------------

**OnionApp.Infra.Db.MainDb**
A segment of the 4th (outermost) layer in the Onion. 
Contains the code to do the plumbing to the database infrastructure, i.e., implements the DbContext interfaces defined in the 3rd layer above.
It also contains the code required to do DB schema migrations.
Depends on 


**OnionApp.Ui.Api**:
A segment of the 4th (outermost) layer in the Onion. 
The consumer application + orchestrator of the other layers.

CrossCutting - a layer that can used by any other layer
-----------------------------------------------------------------
**OnionApp.CrossCutting.Logging**:
Congains Logger interfaces and implementations, possibly to log to different targets such as DB, text file, event streams, etc.

