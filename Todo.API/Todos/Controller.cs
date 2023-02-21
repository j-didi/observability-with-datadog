using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Todo.API.Todos;

[ApiController]
[Route("todo")]
public class Controller : ControllerBase
{
    private readonly IRepository _repository;
    private readonly IDistributedCache _cache;

    public Controller(
        IRepository repository,
        IDistributedCache cache
    )
    {
        _repository = repository;
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _repository.Get());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var content = await _cache.GetStringAsync(id.ToString());

        if (!string.IsNullOrEmpty(content))
            return Ok(JsonConvert.DeserializeObject<Todo>(content));

        var todo = await _repository.GetById(id);
        await _cache.SetStringAsync(id.ToString(), JsonConvert.SerializeObject(todo));
        return Ok(todo);
    }
        
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Todo todo)
    {
        await _repository.Create(todo);
        return Ok();
    }
    
    [HttpPost("{id:guid}")]
    public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody] Todo todo)
    {
        todo.Id = id;
        await _repository.Update(todo);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _repository.Delete(id);
        return Ok();
    }
}