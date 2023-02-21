using Todo.API.Infra;
using Todo.API.Todos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRepository, Repository>();

var settings = builder.Configuration.Get<AppSettings>();
builder.Services.InjectDatabase(settings.DatabaseConnectionString);
builder.Services.InjectCache(settings.RedisConnectionString);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureDatabase();    
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();