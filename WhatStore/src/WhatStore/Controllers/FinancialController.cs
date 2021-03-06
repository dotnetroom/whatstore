﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Infrastructure;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("financial")]
    public class FinancialController : Controller
    {

        [HttpPost("register")]
        public async Task<IActionResult>Register(RegisterFinancialViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok() ;
        }
    }
}
