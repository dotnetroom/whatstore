using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WhatStore.Domain.Infrastructure.Models.Store;
using WhatStore.Domain.Infrastructure.Models.Product;
using WhatStore.Domain.Infrastructure.ViewModels.Admin;

namespace WhatStore.Domain.Infrastructure.Repository.Interfaces
{
    public interface IProductRepository
    {

        Task<bool> InsertProduct(List<string> fileNames, long storeId, string productName, string description, double price, ICollection<IFormFile> picture, 
                                 bool hasVariety, string colors, string sizes, bool isFreeShip, double length, double weigth, 
                                 double widith, string tags, string id);
        Task<bool> UpdateProduct(string productName, string description, double price, bool hasVariety, string colors, 
                                 string sizes, bool isFreeShip, double length, double weigth, double widith, 
                                 string tags, string id, List<string> fileName);
        Task<List<Product>> GetProducts(long storeID);
        Task<Product> GetProduct(string productId);
        Task<List<string>> GetTag(string productId);
        Task<bool> DeleteProduct(string id);
        Task<bool> InsertPictures(string productId, List<string> fileNames);
        Task<string> GetImage(string productId);
        Task<List<PictureProductViewModel>> GetImages(string productId);
        Task<bool> DeleteImage(long productId);
      }
}
