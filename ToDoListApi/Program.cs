using Microsoft.EntityFrameworkCore;
using ToDoListApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//получение строки подключения к бд из конфигурации 
var connection = builder.Configuration.GetConnectionString("MyCon");
//настройка зависимости, добавляет при необходимости обьект MyContext
builder.Services.AddDbContext<MyContext>(options => options.UseSqlite(connection));
//настройка зависимости, добавляет при необходимости обьект Repository
builder.Services.AddScoped<RepositoryGroupModel>();
builder.Services.AddScoped<RepositoryToDoModel>();

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
