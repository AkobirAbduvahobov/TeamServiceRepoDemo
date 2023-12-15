﻿using NTierApplication.DataAccess.Models;
using NTierApplication.Errors;
using NTierApplication.Repository;
using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public class ItemService : IItemService
{
    private IItemRepository ItemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        ItemRepository = itemRepository;
    }

    public ItemViewModelExtended CreateNew(ItemViewModelShort item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        if (string.IsNullOrWhiteSpace(item.ItemName))
        {
            throw new ParameterInvalidException("ItemName cannot be empty");
        }
        if (item.ItemType < 0)
        {
            throw new ParameterInvalidException("Item type must be equal or greater than 0");
        }

        var entity = new Item
        {
            ItemDate = item.ItemDate,
            ItemName = item.ItemName,
            ItemType = item.ItemType
        };
        ItemRepository.Insert(entity);
        ItemRepository.SaveChanges();

        var newExt = new ItemViewModelExtended
        {
            ItemId = entity.ItemId,
            ItemName = entity.ItemName,
            ItemType = entity.ItemType,
            ItemDate = entity.ItemDate,
        };

        return newExt;
    }

    public void Delete(long itemId)
    {
        var itemEntity = ItemRepository.GetAll().FirstOrDefault(x => x.ItemId == itemId);
        if (itemEntity == null)
            throw new EntryNotFoundException("No such item to delete");

        ItemRepository.Delete(itemEntity);
        ItemRepository.SaveChanges();
    }

    public ItemViewModelExtended GetById(long id)
    {
        var result = ItemRepository.GetAll()
            .Select(x => new ItemViewModelExtended
            {
                ItemId = x.ItemId,
                ItemDate = x.ItemDate,
                ItemName = x.ItemName,
                ItemType = x.ItemType
            })
            .FirstOrDefault(x => x.ItemId == id);

        if (result == null)
        {
            throw new EntryNotFoundException("No such item");
        }
        return result;
        //.Where(x => x.ItemId == id)
        //.FirstOrDefault();
    }

    public ICollection<ItemViewModelExtended> GetItems()
    {
        return ItemRepository.GetAll().Select(x => new ItemViewModelExtended
        {
            ItemId = x.ItemId,
            ItemDate = x.ItemDate,
            ItemName = x.ItemName,
            ItemType = x.ItemType
        }).ToList();
    }

    public void Update(ItemViewModelExtended item)
    {
        var itemEntity = ItemRepository.GetAll().FirstOrDefault(x => x.ItemId == item.ItemId);
        if (itemEntity == null)
            throw new EntryNotFoundException("No such item to update");

        itemEntity.ItemDate = item.ItemDate;
        itemEntity.ItemName = item.ItemName;
        itemEntity.ItemType = item.ItemType;
        ItemRepository.SaveChanges();
    }
}
