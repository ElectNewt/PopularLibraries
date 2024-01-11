using MediatR;
using Microsoft.AspNetCore.Mvc;
using PopularLibraries.MediatRExample.Dtos;
using PopularLibraries.MediatRExample.Items.Commands.UpdateItem;

namespace PopularLibraries.MediatRExample.Controllers;

[ApiController]
[Route("[controller]")]
public class MediatRExampleController : ControllerBase
{
    private readonly ISender _sender;

    public MediatRExampleController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPut("item")]
    public async Task<bool> UpdateItem(ItemDto itemDto)
        => await _sender.Send(new UpdateItemCommand()
        {
            Id = itemDto.Id,
            Price = itemDto.Price,
            Title = itemDto.Title
        });
}