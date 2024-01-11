using PopularLibraries.MediatRExample.Dtos;

namespace PopularLibraries.MediatRExample.Data;

public interface IDatabaseRepository
{
    Task<bool> UpdateItem(int itemId, decimal newPrice, string title);
    Task<ItemDto> GetItemById(int id);
}


/// <summary>
/// This is only to simulate the example.
/// </summary>
public class FakeDatabaseRepository : IDatabaseRepository
{
    public Task<bool> UpdateItem(int itemId, decimal newPrice, string title)
    {
        return Task.FromResult(true);
    }

    public Task<ItemDto> GetItemById(int id)
    {
        return Task.FromResult(new ItemDto(id, 12, "Title string"));
    }
}


