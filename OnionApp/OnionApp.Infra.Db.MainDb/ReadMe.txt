Migrations Notes
-----------------------

Install-Package Microsoft.EntityFrameworkCore -Version 5.0.9
Install-Package Microsoft.EntityFrameworkCore.Tools   //for script migration
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0.9
Install-Package Microsoft.Extensions.Configuration.Json -Version 5.0.0


Migrations https://docs.microsoft.com/en-us/ef/core/cli/dotnet
------------------------------------------------------------------------------
if not already done:
dotnet tool install --global dotnet-ef  //from package manager console, to install dotnet cli tools
dotnet tool update --global dotnet-ef

From solution directory:
dotnet ef migrations add <name> --project OnionApp.Infra.Db.MainDb  //add new migration snapshot with the specified <name>
dotnet ef migrations remove --project OnionApp.Infra.Db.MainDb //remove last migration snapshot that is not synced to DB.

dotnet ef database update <name/prevName> --project OnionApp.Infra.Db.MainDb  //Sync DB with the particular migration <name/prevName>
dotnet ef database update 0 --project OnionApp.Infra.Db.MainDb  //Remove all migrations from DB

dotnet ef migrations script <fromName> <toName> //create script out of migration. fromName: all changes after fromName. toName: all changes up to toName (including toName) Use 0 in the place of fromName for initial migration.

