using LibraryService.DAL;
using LibraryService.Processors;

//dependency injection

// there are different ways to implement this - singleton, scoped and transient

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// using the below were telling the dependency injection container to create a new
// instance of the library processor for each http request in the application
// This allows you to ensure that different parts of your application can work with separate instances of
// library processor

// this dependency injection whenever you need an instance of ILibraryProcessor, you can request it as a
// constructor parameter in your controllers or other components
builder.Services.AddScoped<ILibraryProcessor, LibraryProcessor>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IDomainBookMapper, DomainBookMapper>();


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

