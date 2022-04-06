### BMS Application Domain Namespace
Task was to register companies and users - sounded like a generic Business Process Management System.
BMS for short.

Assumption is made that there is a many-to-one relationship between companies and user.

### EfCore

While public facing IDs are GUIDs, for best security practice, EfCore is setup to use shadow int fields for internal IDs
for database storage efficiency.

This branch does not use backing field collection technique to store users in a company.
If interested, branch using_backing_field shows users stored as part of company in DDD style.

### Project layout decision
A separate project just for commands is created, BMS.Company.Cmd, to be
able to compile available commands with the domain entities
into consuming clients
(other runnable projects).
This is especially useful in Blazor, or anywhere else that runs C# on both client and server.

This way, client can generate commands and queries in a type-compatible format with the server.

### EfCore connection
```
dotnet ef migrations add Companies --project BC_Company/BMS.Company.Data --startup-project BMS.Api --context CompanyContext
dotnet ef database update --project BC_Company/BMS.Company.Data --startup-project BMS.Api --context CompanyContext
```