using GasStation.Application;
using GasStation.Infrastructure;
using GasStation.WebApi;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation(builder.Configuration)
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");
    app.UseCors();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}