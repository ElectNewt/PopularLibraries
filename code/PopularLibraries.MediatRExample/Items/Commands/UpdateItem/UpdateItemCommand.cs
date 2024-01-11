using MediatR;

namespace PopularLibraries.MediatRExample.Items.Commands.UpdateItem;

public record UpdateItemCommand : IRequest<bool>
{
    public required int Id { get; init; }
    public required decimal Price { get; init; }
    public required string Title { get; init; }
}