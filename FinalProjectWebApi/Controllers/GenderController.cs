using FinalProjectWebApi.DataAccess;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController:ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public GenderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Gender>> GetGenders()
        {
            return await _context.Genders.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<Gender> GetGenderById(int id)
        {
            return await _context.Genders.FindAsync(id);
        }

        [HttpPost]
        public async Task<Gender> AddGender([FromBody] Gender gender)
        {
            await _context.Genders.AddAsync(gender);
            await _context.SaveChangesAsync();
            return gender;

           
        }
    }
}
