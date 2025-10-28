using FluentValidation;
using Task_4.DTOs;
using Task_4.Repositories;
using Task_4.Services;
using Task_4.Validators;
using Task_4.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidFilter>();

} 
);


builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();


builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService,BookService>();

builder.Services.AddScoped<IValidator<AuthorCreateDTO>, AuthorValidator>();
builder.Services.AddScoped<IValidator<BookCreateDTO>, BookValidator>();



builder.Services.AddScoped<IMapperService, MapperService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
