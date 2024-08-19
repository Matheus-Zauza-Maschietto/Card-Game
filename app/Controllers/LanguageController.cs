using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly LanguageService _languageService;
        public LanguageController(LanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetLanguagesAsync()
        {
            try
            {
                ICollection<LanguageDto> languages = await _languageService.GetLanguageDtosAsync();
                return Ok(languages);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}