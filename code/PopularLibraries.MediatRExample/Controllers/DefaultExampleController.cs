using Microsoft.AspNetCore.Mvc;
using PopularLibraries.MediatRExample.Dtos;
using PopularLibraries.MediatRExample.UseCases;

namespace PopularLibraries.MediatRExample.Controllers;

[ApiController]
[Route("[controller]")]

public class DefaultExampleController : ControllerBase
{
    public readonly UpdateItem _UpdateItem;

    public DefaultExampleController(UpdateItem updateItem)
    {
        _UpdateItem = updateItem;
    }

    [HttpPut("item")]
    public async Task<bool> UpdateItem(ItemDto itemDto)
        => await _UpdateItem.Execute(itemDto);
}