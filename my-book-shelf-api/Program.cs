using my_book_shelf_api.Core.Extensions;

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

builder.Services.AddDataBaseConnection(builder.Configuration);
builder.Services.RegistersDependencies();

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

app.MapGet("/", () =>
{
    return $"Hello from {typeof(Program).Assembly.GetName().Name}!";
}).ExcludeFromDescription();

app.Run();
