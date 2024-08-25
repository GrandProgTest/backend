using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using irs.API.DueDiligence.Domain.Model.ValueObjects;

namespace irs.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CountryController : ControllerBase
    {
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