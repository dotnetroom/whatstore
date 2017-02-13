using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Domain.Infrastructure.ViewModels.Admin;
using WhatStore.Domain.Infrastructure.Repository.Interfaces;
using WhatStore.Domain.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Identity;
using WhatStore.Domain.Infrastructure;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {

        [HttpGet("open")]
        public IActionResult Open()

        {
            return View();
        }

        [HttpGet("category")]
        public IActionResult CategoryProduct()
        {
            return View();
        }

        [HttpGet("subcategory")]
        public IActionResult SubcategoryProduct()
        {
            return View();
        }
    }
}
