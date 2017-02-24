using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using WhatStore.Domain.Infrastructure.Repository;

namespace WhatStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _storeRepository;

        public HomeController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("~/{store}")]
        public async Task<IActionResult> StoreIndex(string store)
        {
            var logo = await _storeRepository.GetLogo(store);
            if (logo != null)
            {
                logo = Url.Action(logo, "image");
            }
            ViewBag.Logo = logo;
            ViewBag.StoreName = store;
            return View();
        }


    }
}
