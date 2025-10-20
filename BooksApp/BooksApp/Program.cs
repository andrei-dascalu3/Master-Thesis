using BooksApp.Controllers;
using BooksApp.Library.Adapters.DataAccess;
using BooksApp.Library.Adapters.Primary;
using BooksApp.Library.Domain.Contexts;
using BooksApp.Library.Domain.Contexts.Interfaces;
using BooksApp.Library.Ports.DataAccess;
using BooksApp.Library.Ports.Primary.Interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IOverviewManager, OverviewManager>();
        builder.Services.AddScoped<IOverviewContext, OverviewContext>();
        builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();

        var app = builder.Build();

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
    }
}