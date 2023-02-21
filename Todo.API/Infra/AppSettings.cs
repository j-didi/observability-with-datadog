namespace Todo.API.Infra;

public class AppSettings
{
    public string DatabaseConnectionString { get; set; }
    public string RedisConnectionString { get; set; }
}