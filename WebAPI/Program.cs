var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

var books = new[]
{
    new Book("The Great Gatsby", "F. Scott Fitzgerald", 1925),
    new Book("To Kill a Mockingbird", "Harper Lee", 1960),
    new Book("1984", "George Orwell", 1949),
    new Book("Pride and Prejudice", "Jane Austen", 1813),
    new Book("The Catcher in the Rye", "J.D. Salinger", 1951)
};

var songs = new[]
{
    new Song("Bohemian Rhapsody", "Queen", "Rock"),
    new Song("Imagine", "John Lennon", "Pop"),
    new Song("Hotel California", "Eagles", "Rock"),
    new Song("Billie Jean", "Michael Jackson", "Pop"),
    new Song("Stairway to Heaven", "Led Zeppelin", "Rock")
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapGet("/books", () => books)
    .WithName("GetBooks")
    .WithOpenApi();

app.MapGet("/songs", () => songs)
    .WithName("GetSongs")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record Book(string Title, string Author, int YearPublished);

record Song(string Title, string Artist, string Genre);