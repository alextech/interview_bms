### Domain
Domains typically have events. Best place to dispatch event for adding user would be in Company.AddUser().
But there was no requirement for a one-to-many relationship with Company as the aggregate root managing its users (implemented as backing field collection).
Alternatively, could dispatch `CompanyCreateEvent` and `UserAddedToCompanyEvent` from MediatR command handler.

### EfCore commands
```
dotnet ef migrations add Companies --project Company/Company.Infrastructure --startup-project Company/Company.Api --context CompanyContext
dotnet ef database update --project Company/Company.Infrastructure --startup-project Company/Company.Api --context CompanyContext
```