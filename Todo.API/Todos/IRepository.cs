namespace Todo.API.Todos;

public interface IRepository
{
    Task<Todo> GetById(Guid id);
    Task<IReadOnlyCollection<Todo>> Get();
    Task Create(Todo todo);
    Task Update(Todo todo);
    Task Delete(Guid id);
}