using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Infrastructure.ViewModels.Admin;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("store")]
    public class StoreController : Controller
    {
        [HttpPost("register/information")]
        public async Task<IActionResult> RegisterInformation (RegisterStoreDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok();            
        }
    }
}
