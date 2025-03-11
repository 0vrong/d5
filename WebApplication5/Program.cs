using Microsoft.AspNetCore.Mvc;
using System.Xml;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
List<Pills> repo = [];

app.MapGet("/", () => repo);
app.MapPost("/", (CreatePillsDTO dto) =>
{
    var pills = new Pills
    {
        id = Guid.NewGuid(),
        name = dto.name,
        mg = dto.mg,
        price = dto.price,
        DateNow = DateTime.Now,
        DateEnd = DateTime.Now.AddYears(3)
    };
    repo.Add(pills);
});
app.MapPut("/", ([FromQuery]Guid id,UpdatePillsDTO dto) =>
{
    Pills buffer = repo.Find(x => x.id == id);
    buffer.name = dto.name;
    buffer.mg = dto.mg;
    buffer.price = dto.price;
});
app.MapDelete("/", ([FromQuery] Guid id) =>
{
    Pills buffer = repo.Find(x => x.id == id);
    repo.Remove(buffer);
});
app.Run();

class Pills
{
    public Guid id { get; set; }
    public string? name { get; set; }
    public int? mg { get; set; }
    public double? price { get; set; }
    public DateTime DateNow { get; set; }
    public DateTime DateEnd { get; set; }

}

record class CreatePillsDTO(string name, int mg, double price);
record class UpdatePillsDTO (string name, int mg, double price);
