﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Infrastructure.ViewModels.Admin;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        #region Constructors and Injections

        private ILocalizationRepository _localization;

        public AdminController(ILocalizationRepository localization)
        {
            _localization = localization;
        }

        #endregion 

        [Route("financial")]
        // GET: /<controller>/
        public async Task<IActionResult> Financial()
        {
            var states = await _localization.GetStates();

            var viewModel = new FinancialViewModel()
            {
                States = states
            };

            return View(viewModel);
        }

        [Route("product")]
        public IActionResult Product()
        {
            return View();
        }

        [Route("open")]
        public IActionResult Open()

        {
            return View();
        }

        [Route("StoreType")]
         public IActionResult StoreType()
        {
            return View();

        }
    }
}
