using PopularLibraries.MediatRExample.Data;
using PopularLibraries.MediatRExample.Dtos;

namespace PopularLibraries.MediatRExample.UseCases;

public class UpdateItem
{
    private readonly IDatabaseRepository _databaseRepository;
    private readonly NotifyWishlist _notifyWishlist;

    public UpdateItem(IDatabaseRepository databaseRepository, NotifyWishlist notifyWishlist)
    {
        _databaseRepository = databaseRepository;
        _notifyWishlist = notifyWishlist;
    }
    public async Task<bool> Execute(ItemDto itemToUpdate)
    {

        if (itemToUpdate.Title.Length > 200)
            throw new Exception("Title must be less than 200 characters");
        if (itemToUpdate.Price <= 0)
            throw new Exception("It can't be free");

        ItemDto existingItem = await _databaseRepository.GetItemById(itemToUpdate.Id);
        await _databaseRepository.UpdateItem(itemToUpdate.Id, itemToUpdate.Price, itemToUpdate.Title);

        decimal percentageDifference = ((itemToUpdate.Price - existingItem.Price) / existingItem.Price) * 100;
        if (percentageDifference <= -30)
        {
           await _notifyWishlist.Execute(itemToUpdate.Id);
        }
            

        return true;
    }
}