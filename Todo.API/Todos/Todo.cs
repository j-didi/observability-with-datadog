namespace Todo.API.Todos;

public class Todo
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool Done { get; set; }
}