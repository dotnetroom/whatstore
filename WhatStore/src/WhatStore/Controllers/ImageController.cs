using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using WhatStore.Domain.Infrastructure.Contexts;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("image")]
    public class ImageController : Controller
    {
        #region Constructors and injections
        private CustomSettings _settings;

        public ImageController(IOptions<CustomSettings> settings)
        {
            _settings = settings.Value;
        }
        #endregion

        [Route("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string name)
        {
            var imageBytes = System.IO.File.ReadAllBytes(_settings.ImagePath + name);
            return File(imageBytes, "image/jpg");
        }
    }
}
