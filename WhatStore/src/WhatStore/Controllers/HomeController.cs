using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using WhatStore.Domain.Infrastructure.Repository;
using WhatStore.Domain.Infrastructure.Models.Product;

namespace WhatStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _storeRepository;
        private IProductRepository _productRepository;

        public HomeController(IStoreRepository storeRepository, IProductRepository productRepository)
        {
            _storeRepository = storeRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("~/{store}")]
        public async Task<IActionResult> StoreIndex(string store)
        {
            var logo = await _storeRepository.GetLogo(store);
            List<SubCategory> subCategories = new List<SubCategory> { };
            if (logo != null)
            {
                logo = Url.Action(logo, "image");
            }
            var storeId = await _storeRepository.GetStoreId(store);
            var categories = await _productRepository.GetCategories(storeId);
            foreach (var item in categories)
            {
                subCategories = await _productRepository.GetSubCategories(item.Id);
            }
            ViewBag.Categories = categories;
            ViewBag.SubCategories = subCategories;
            ViewBag.Logo = logo;
            ViewBag.StoreName = store;
            
            return View();
        }


    }
}
