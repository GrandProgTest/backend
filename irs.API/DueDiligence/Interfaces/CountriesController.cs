using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using irs.API.DueDiligence.Domain.Model.ValueObjects;

namespace irs.API.Controllers
{
    /// <summary>
    /// Controller to handle country-related operations.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountryController : ControllerBase
    {
        /// <summary>
        /// Retrieves a list of all countries.
        /// </summary>
        /// <returns>A list of country names as strings.</returns>
        [HttpGet]
        public IActionResult GetCountries()
        {
            var countries = Enum.GetValues(typeof(ECountry))
                .Cast<ECountry>()
                .Select(c => c.ToString())
                .ToList();
            return Ok(countries);
        }
    }
}