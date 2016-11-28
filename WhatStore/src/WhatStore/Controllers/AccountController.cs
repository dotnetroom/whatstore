using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        // GET: /<controller>/
<<<<<<< HEAD
        [Route("login")]
=======
        [Route("account/login")]
>>>>>>> bdf8ca71e8eb397dcac9d8b66718bf6ef7794e80
        public IActionResult Login()
        {
            return View();
        }
<<<<<<< HEAD
        [Route("register")]
=======

        [Route("account/register")]
>>>>>>> bdf8ca71e8eb397dcac9d8b66718bf6ef7794e80
        public  IActionResult Register()
        {
            return View();
        }
    }
}
