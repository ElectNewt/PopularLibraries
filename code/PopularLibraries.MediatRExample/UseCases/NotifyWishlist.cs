namespace PopularLibraries.MediatRExample.UseCases;

public class NotifyWishlist
{
    public Task Execute(int id)
    {
        //We dont need this working for the example.
        Console.WriteLine("Logic to get who is wishlisting that ID and send the emails");
        
        return Task.FromResult(true);
    }
}