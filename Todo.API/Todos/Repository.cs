using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Todo.API.Infra;

namespace Todo.API.Todos;

public class Repository : IRepository
{
    private readonly DatabaseContext _context;
    private readonly IDistributedCache _cache;
    private readonly DbSet<Todo> _todos;

    public Repository(
        DatabaseContext context,
        IDistributedCache cache
    )
    {
        _context = context;
        _cache = cache;
        _todos = context.Set<Todo>();
    }

    public async Task<Todo> GetById(Guid id) =>
        await _todos.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IReadOnlyCollection<Todo>> Get() =>
        await _todos.ToListAsync();

    public async Task Create(Todo todo)
    {
        todo.Id = Guid.NewGuid();
        await _todos.AddAsync(todo);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Todo todo)
    {
        await _cache.RemoveAsync(todo.Id.ToString());
        _context.Entry(todo).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    
    public async Task Delete(Guid id)
    {
        await _cache.RemoveAsync(id.ToString());
        var todo = await GetById(id);
        _todos.Remove(todo!);
        await _context.SaveChangesAsync();
    }
}