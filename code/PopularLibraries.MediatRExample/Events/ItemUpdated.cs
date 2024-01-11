using MediatR;

namespace PopularLibraries.MediatRExample.Events;

public record ItemUpdated : INotification
{
    public required int Id { get; init; }
    public required decimal NewPrice { get; init; }
    public required decimal OldPrice { get; init; }
    public required string NewTitle { get; init; }
    public required string OldTitle { get; init; }
}