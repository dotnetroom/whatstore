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
<<<<<<< HEAD
=======

<<<<<<< HEAD
=======
        [Route("login")]

>>>>>>> d81fda1973052028c07aa359d58c2c9509355d13
>>>>>>> origin/1.0
        public IActionResult Login()
        {
            return View();
        }

        [Route("register")]
<<<<<<< HEAD
        public IActionResult Register()
=======

        public  IActionResult Register()
>>>>>>> d81fda1973052028c07aa359d58c2c9509355d13
        {
            return View();
        }
    }
}
