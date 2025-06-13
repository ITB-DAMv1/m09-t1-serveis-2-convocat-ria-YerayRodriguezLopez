using EventAPI.Data;
using EventAPI.DTOs;
using EventAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Obtenir tots els clients ordenats alfabèticament per nom d'empresa
        /// </summary>
        [HttpGet]
        [Authorize(Policy = "AuthenticatedUser")]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients()
        {
            var clients = await _context.Clients
                .OrderBy(c => c.CompanyName)
                .Select(c => new ClientDto
                {
                    Id = c.Id,
                    CompanyName = c.CompanyName,
                    CeoFirstName = c.CeoFirstName,
                    CeoLastName = c.CeoLastName,
                    Email = c.Email,
                    AttendeeCount = c.AttendeeCount,
                    IsVip = c.IsVip,
                    RegisterDate = c.RegisterDate
                })
                .ToListAsync();

            return Ok(clients);
        }

        /// <summary>
        /// Obtenir un client per ID
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Policy = "AuthenticatedUser")]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
                return NotFound();

            // Check if user can access this client
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRoles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();

            if (!userRoles.Contains("Admin") && !userRoles.Contains("PR") && client.UserId != currentUserId)
                return Forbid();

            var clientDto = new ClientDto
            {
                Id = client.Id,
                CompanyName = client.CompanyName,
                CeoFirstName = client.CeoFirstName,
                CeoLastName = client.CeoLastName,
                Email = client.Email,
                AttendeeCount = client.AttendeeCount,
                IsVip = client.IsVip,
                RegisterDate = client.RegisterDate
            };

            return Ok(clientDto);
        }

        /// <summary>
        /// Obtenir client de l'usuari autenticat
        /// </summary>
        [HttpGet("my-profile")]
        [Authorize(Policy = "ClientOnly")]
        public async Task<ActionResult<ClientDto>> GetMyProfile()
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == currentUserId);

            if (client == null)
                return NotFound();

            var clientDto = new ClientDto
            {
                Id = client.Id,
                CompanyName = client.CompanyName,
                CeoFirstName = client.CeoFirstName,
                CeoLastName = client.CeoLastName,
                Email = client.Email,
                AttendeeCount = client.AttendeeCount,
                IsVip = client.IsVip,
                RegisterDate = client.RegisterDate
            };

            return Ok(clientDto);
        }

        /// <summary>
        /// Actualitzar client (només PR i Admin)
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Policy = "PROrAdmin")]
        public async Task<IActionResult> UpdateClient(int id, UpdateClientDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return NotFound();

            client.CompanyName = updateDto.CompanyName;
            client.CeoFirstName = updateDto.CeoFirstName;
            client.CeoLastName = updateDto.CeoLastName;
            client.AttendeeCount = updateDto.AttendeeCount;
            client.IsVip = updateDto.IsVip;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Actualitzar el propi perfil de client
        /// </summary>
        [HttpPut("my-profile")]
        [Authorize(Policy = "ClientOnly")]
        public async Task<IActionResult> UpdateMyProfile(UpdateClientDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == currentUserId);

            if (client == null)
                return NotFound();

            client.CompanyName = updateDto.CompanyName;
            client.CeoFirstName = updateDto.CeoFirstName;
            client.CeoLastName = updateDto.CeoLastName;
            client.AttendeeCount = updateDto.AttendeeCount;
            client.IsVip = updateDto.IsVip;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Eliminar client (només Admin)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return NotFound();

            // Also delete the associated user
            var user = await _userManager.FindByIdAsync(client.UserId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return NoContent();
        }

        /// <summary>
        /// Obtenir el total d'assistents
        /// </summary>
        [HttpGet("total-attendees")]
        [Authorize(Policy = "AuthenticatedUser")]
        public async Task<ActionResult<int>> GetTotalAttendees()
        {
            var total = await _context.Clients.SumAsync(c => c.AttendeeCount);
            return Ok(total);
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
