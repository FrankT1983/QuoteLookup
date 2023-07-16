using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Quotes.Storage.InMemory;
using Quotes.Storage.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IQuoteStorage, InMemoryQuotesStorage>();

var allowLocal = "allowLocal";
builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: allowLocal,
              policy =>
              {
                  policy.WithOrigins("http://localhost:3000");
                  policy.AllowAnyMethod();
                  policy.AllowAnyHeader();
              });
    });



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
app.UseCors(allowLocal);

app.Run();
