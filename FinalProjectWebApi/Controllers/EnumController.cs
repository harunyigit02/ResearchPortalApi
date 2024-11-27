using FinalProjectWebApi.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EnumController:ControllerBase
    {
        [HttpGet("gender")]
        public IActionResult GetGenderEnum()
        {
            var genders = Enum.GetValues(typeof(Gender))
                              .Cast<Gender>()
                              .Select(g => new { Id = (int)g, Name = g.ToString() })
                              .ToList();
            return Ok(genders);
        }

        // EducationLevel enum'ını döndürme
        [HttpGet("educationLevel")]
        public IActionResult GetEducationLevelEnum()
        {
            var educationLevels = Enum.GetValues(typeof(EducationLevel))
                                       .Cast<EducationLevel>()
                                       .Select(e => new { Id = (int)e, Name = e.ToString() })
                                       .ToList();
            return Ok(educationLevels);
        }

        // Occupation enum'ını döndürme
        [HttpGet("occupation")]
        public IActionResult GetOccupationEnum()
        {
            var occupations = Enum.GetValues(typeof(Occupation))
                                  .Cast<Occupation>()
                                  .Select(o => new { Id = (int)o, Name = o.ToString() })
                                  .ToList();
            return Ok(occupations);
        }
    }
}
