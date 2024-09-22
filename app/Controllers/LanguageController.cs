using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.DTOs;
using app.Repositories.Interfaces;
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
        private readonly IRedisRepository _cache;
        public LanguageController(LanguageService languageService, IRedisRepository cache)
        {
            _languageService = languageService;
            _cache = cache;
        }

        [HttpGet()]
        public async Task<IActionResult> GetLanguagesAsync()
        {
            try
            {
                ICollection<LanguageDto>? languages = await _cache.GetCacheAsync<ICollection<LanguageDto>>("languages");
                if(languages is not null)
                    return Ok(languages);

                languages = await _languageService.GetLanguageDtosAsync();
                _cache.SetCacheAsync("languages", languages, TimeSpan.FromHours(1));
                return Ok(languages);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}