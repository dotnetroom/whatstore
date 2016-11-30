﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        [Route("management")]
        // GET: /<controller>/
        public IActionResult Management()
        {
            return View();
        }

        [Route("product")]
        public IActionResult Product()
        {
            return View();
        }
    }
}