using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatStore.Crosscutting.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WhatStore.Controllers
{
    [Route("localization")]
    public class LocalizationController : Controller
    {
        #region Constructors and Injections

        private ILocalizationRepository _localization;
        public LocalizationController(ILocalizationRepository localization)
        {
            _localization = localization;
        }

        #endregion

        [HttpGet]
        [AllowAnonymous]
        [Route("list/cities/{state}")]
        public async Task<IActionResult> ListCities(int state)
        { 
            try
            {
                var cities = await _localization.GetCities(state);

                if (cities == null) return BadRequest("There was an error to load the cities.");

                return Json(cities);
            }

            catch(Exception ex)
            {
                return BadRequest("There was an internal error");
            }
        }
    }
}
