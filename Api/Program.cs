using Api.Middleware;
using Application; // AssemblyReference za MediatR
using Application.Commands.Create;
using Infrastructure.DependencyInjection; // ?? ovo dodaj

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ?? jednim pozivom povla?iš DbContext + repoe + UoW
builder.Services.AddInfrastructureLayer(builder.Configuration);

// MediatR (Application CQRS)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateCandidateuseCase.UseCase).Assembly));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
