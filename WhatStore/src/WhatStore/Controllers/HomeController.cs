﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WhatStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("~/{store}")]
        public IActionResult StoreIndex(string store)
        {
            ViewBag.StoreName = store;
            return View();
        }
       
      
    }
}
