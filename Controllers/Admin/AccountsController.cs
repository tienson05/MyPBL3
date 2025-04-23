using AgainPBL3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AgainPBL3.Controllers.Admin
{
    [Route("api/admin/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public RolesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var result = await _appDbContext.Roles.ToListAsync();
                if (result == null || !result.Any())
                {
                    return NotFound("No roles found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving roles.");
            }
        }
    }
}