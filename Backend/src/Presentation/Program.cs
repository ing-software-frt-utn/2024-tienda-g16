using Application;
using Persistence;
using Infrastructure;
using Presentation;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddPersistence(builder.Configuration)
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();

{
    app.UseExceptionHandler("/error");
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseHttpsRedirection();
    app.MapControllers();

    app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    app.Run();
}
