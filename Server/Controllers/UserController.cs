using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BlazorMovies.Library;
using BlazorMovies.Server.Helpers;
using BlazorMovies.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorMovies.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MoviesDbContext db;

        public UserController(MoviesDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.db = dbContext;
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get ([FromQuery] PaginationDTO dto)
        {
            var dbUsers = db.Users.AsQueryable();
            await HttpContext.AddPagesResponse(dbUsers, dto.Records);
            return await dbUsers.Paginate(dto).Select(x => new UserDTO { Email = x.Email, UserId = x.Id }).ToListAsync();
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RoleDTO>>> GetRoles ()
        {
            return await db.Roles.Select(x=> new RoleDTO { RoleName = x.Name}).ToListAsync();
        }

        [HttpPost("AssignRoles")]
        public async Task<ActionResult> AssignRoles(EditRoleDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user!=null)
            {
                // var currentRoles = await _userManager.GetRolesAsync(user);
                // await _userManager.RemoveFromRolesAsync(user, currentRoles.ToArray());
                foreach (var role in dto.RoleNames) {
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim(ClaimTypes.Role, role));
                }
                // await db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("RemoveRoles")]
        public async Task<ActionResult> RemoveRoles(EditRoleDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user != null)
            {
                // var currentRoles = await _userManager.GetRolesAsync(user);
                // await _userManager.RemoveFromRolesAsync(user, currentRoles.ToArray());
                foreach (var role in dto.RoleNames)
                {
                    await _userManager.RemoveClaimAsync(user, new System.Security.Claims.Claim(ClaimTypes.Role, role));
                }
                // await db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
