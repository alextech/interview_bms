using System.Reflection;
using Company.Api.Behaviors;
using Company.Api.Commands;
using Company.Api.Infrastructure.Filters;
using Company.Domain;
using Company.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
});
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

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddTransient<IMediator, Mediator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
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