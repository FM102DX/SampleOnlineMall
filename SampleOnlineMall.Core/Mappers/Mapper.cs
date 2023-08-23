﻿using SampleOnlineMall.Core.Models;
using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOnlineMall.Core.Mappers
{
    public class Mapper
    {
        public CommodityItem CommodityItemFromCommodityItemApiFeed(CommodityItemApiFeed item)
        {
            var newItem = new CommodityItem();
            newItem.Id = item.Id;
            newItem.Name = item.Name;
            newItem.Description = item.Description;
            newItem.Price = item.Price;
            newItem.SupplierId = item.SupplierId;
            return newItem;
        }

        public CommodityItemFrontend CommodityItemFrontendFromCommodityItem(CommodityItem item)
        {
            var newItem = new CommodityItemFrontend();
            newItem.Id = item.Id;
            newItem.Name = item.Name;
            newItem.Price = item.Price;
            newItem.Description = item.Description;
            newItem.Pictures = new List<PictureInfo>();

            newItem.SupplierId = item.SupplierId;
            return newItem;

        }
        public CommodityItemFrontendDisplayed CommodityItemFrontendDisplayedFromTransport (CommodityItemFrontend item)
        {
            var newItem = new CommodityItemFrontendDisplayed();
            newItem.Id = item.Id;
            newItem.Name = item.Name;
            newItem.Price= item.Price;
            newItem.Description = item.Description;
            newItem.Pictures = new List<PictureInfo>();
            foreach (var x in item.Pictures)
            {
                newItem.Pictures.Add(x.Clone());
            }
            newItem.SupplierId = item.SupplierId;
            newItem.Supplier = item.Supplier;
            return newItem;
        }

        public RepositoryResponce<CommodityItemFrontend> ResponceFrontFromResponceCommItem(RepositoryResponce<CommodityItem> sourceResp)
        {
            var targetResp = new RepositoryResponce<CommodityItemFrontend>();
            targetResp.ItemsPerPage = sourceResp.ItemsPerPage;
            targetResp.UsedPagination = sourceResp.UsedPagination;
            targetResp.UsedSearch = sourceResp.UsedSearch;
            targetResp.Page = sourceResp.Page;
            targetResp.TotalCount = sourceResp.TotalCount;
            targetResp.Result = new Service.CommonOperationResult();
            targetResp.Result.Success = sourceResp.Result.Success;
            targetResp.Result.Message = sourceResp.Result.Message;
            var itemList = new List<CommodityItemFrontend>();

            foreach (var item in sourceResp.Items)
            {
                itemList.Add(CommodityItemFrontendFromCommodityItem(item));
            }
            targetResp.Items = itemList;
            return targetResp;
        }
        public RepositoryResponce<CommodityItemFrontendDisplayed> ResponceFrontDisplayedFromTransport(RepositoryResponce<CommodityItemFrontend> sourceResp)
        {
            var targetResp = new RepositoryResponce<CommodityItemFrontendDisplayed>();
            targetResp.ItemsPerPage = sourceResp.ItemsPerPage;
            targetResp.UsedSearch = sourceResp.UsedSearch;
            targetResp.UsedPagination = sourceResp.UsedPagination;
            targetResp.Page = sourceResp.Page;
            targetResp.TotalCount = sourceResp.TotalCount;
            targetResp.Result = new Service.CommonOperationResult();
            targetResp.Result.Success = sourceResp.Result.Success;
            targetResp.Result.Message = sourceResp.Result.Message;
            var itemList = new List<CommodityItemFrontendDisplayed>();

            foreach (var item in sourceResp.Items)
            {
                itemList.Add(CommodityItemFrontendDisplayedFromTransport(item));
            }
            targetResp.Items = itemList;
            return targetResp;
        }


    }
}
