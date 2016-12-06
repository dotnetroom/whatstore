using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Infrastructure.ViewModels.Admin;

namespace WhatStore.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterProduct(RegisterProductViewModel Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
                return Ok();
            

        }
    }
}
