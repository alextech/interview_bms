using System.Reflection;
using Company.Api.Commands;
using Company.Domain;
using Company.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddConsole();

builder.Services.AddDbContext<CompanyContext>(
    options => options
        .UseNpgsql(
            builder.Configuration.GetConnectionString("InterviewConnection"),
            srv => srv.MigrationsHistoryTable("__BmsMigrationsHistory")
        ),
    ServiceLifetime.Scoped
);

builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IMediator, Mediator>();
builder.Services.AddMediatR(new[]
{
    typeof(RegisterUserWithCompanyHandler).GetTypeInfo().Assembly
});

WebApplication app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();