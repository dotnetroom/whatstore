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

        [Route("login")]

        [Route("login")]

        public IActionResult Login()
        {
            return View();
        }

        [Route("register")]


        [Route("register")]

        public  IActionResult Register()
        {
            return View();
        }
    }
}
