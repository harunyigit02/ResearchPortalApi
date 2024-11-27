using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantInfoController:ControllerBase
    {
        private readonly IParticipantInfoService _participantInfoService;

        public ParticipantInfoController(IParticipantInfoService participantInfoService)
        {
            _participantInfoService = participantInfoService;

        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipantInfo>>> GetParticipantInfosAsync()
        {
            var participantInfo = await _participantInfoService.GetParticipantInfosAsync();
            return Ok(participantInfo);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipantInfo>> GetParticipantInfo(int id)
        {
            var category = await _participantInfoService.GetParticipantInfoByIdAsync(id);
            if (category == null)
            {
                return NotFound(); // 404 Not Found HTTP response
            }
            return Ok(category); // 200 OK HTTP response
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("UserInfos")]
        public async Task<ActionResult<IEnumerable<ParticipantInfo>>> GetParticipantInfosByUserIdAsync()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var participantInfo = await _participantInfoService.GetParticipantInfosByUserIdAsync(int.Parse(userId));
            return Ok(participantInfo);
        }

        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<ParticipantInfo>> AddAParticipantInfo(ParticipantInfo participantInfo)
        {
            var userId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) 
            {
                return Unauthorized();
            }
            participantInfo.UserId= int.Parse(userId);

            var createdParticipantInfo = await _participantInfoService.AddParticipantInfoAsync(participantInfo);
            return CreatedAtAction(nameof(GetParticipantInfo), new { id = createdParticipantInfo.Id }, createdParticipantInfo); // 201 Created HTTP response
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
