﻿using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Items
{
    public class ItemService :
        CrudQueryServiceBase<Item, ItemDto, ItemFilterDto>,
        IItemService
    {
        private readonly IRepository<ItemCategory> itemCategoryRepository;

        public ItemService(IMapper mapper, IRepository<Item> itemRepository,
            QueryObjectBase<ItemDto, Item, ItemFilterDto, IQuery<Item>> itemListQuery,
            IRepository<ItemCategory> itemCategoryRepository)
            : base(mapper, itemRepository, itemListQuery)
        {
            this.itemCategoryRepository = itemCategoryRepository;
        }

        public async Task<IEnumerable<ItemDto>> GetItemsByNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new ItemFilterDto { SearchedName = name });
            return queryResult.Items;
        }

        public async Task<IEnumerable<ItemDto>> GetItemsUserIDAsync(int userID)
        {
            var queryResult = await Query.ExecuteQuery(new ItemFilterDto { OwnerID = userID });
            return queryResult.Items;
        }

        public async Task<IEnumerable<ItemDto>> GetItemsByAuctionIDAsync(int auctionID)
        {
            var queryResult = await Query.ExecuteQuery(new ItemFilterDto { AuctionID = auctionID });
            return queryResult.Items;
        }

        public async Task<IEnumerable<ItemDto>> ListFilteredItems(ItemFilterDto filter)
        {
            var queryResult = await Query.ExecuteQuery(filter);

            return queryResult.Items;

        }

        public async Task<IEnumerable<ItemDto>> GetItemsByCategoriesAsync(List<ItemCategoryDto> category)
        {
            var queryResult = await Query.ExecuteQuery(new ItemFilterDto { ItemCategoryTypes = category });
            return queryResult.Items;
        }

        public async Task<ItemDto> AddItemCategory(Category category, int itemId)
        {
            var item = await Repository.GetAsync(itemId);

            var itemCategory = new ItemCategory
            {
                Category = category,
                CategoryID = category.Id,
                Item = item,
                ItemID = itemId
            };

            itemCategoryRepository.Create(itemCategory);

            item.HasCategories.Add(itemCategory);
            Repository.Update(item);
            return Mapper.Map<ItemDto>(item);
        }

        protected override Task<Item> GetWithIncludesAsync(int entityId)
        {
            return Repository.GetAsync(entityId);
        }

        public async Task AddItemToAuction(ItemDto entityDto)
        {
            var entity = await Repository.GetAsync(entityDto.Id);
            entity.AuctionID = entityDto.AuctionID;
            Repository.Update(entity);
        }

        public To ConverTo<From, To>(From source)
        {
            return Mapper.Map<From, To>(source);
        }
    }
}
