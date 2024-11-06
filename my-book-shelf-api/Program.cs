using my_book_shelf_api.Core.Data;
using my_book_shelf_api.Repositories;
using my_book_shelf_api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaLiberada", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<ConnectionManager>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthRepository>();

var app = builder.Build();


app.UseCors("PoliticaLiberada");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
