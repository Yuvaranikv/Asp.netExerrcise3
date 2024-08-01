using LGym.Models;
using LGym.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LGym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MembersController : ControllerBase
    {
        private readonly ILGymRepository _repository;

        public MembersController(ILGymRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Get all members
        /// </summary>
        /// <returns>All Members</returns>
        // GET: api/members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            //var userClaims = HttpContext.Items["UserClaims"] as IEnumerable<Claim>;
            //var firstNameClaim = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

            //if (string.IsNullOrEmpty(firstNameClaim))
            //{
            //    return Unauthorized("Invalid token claims.");
            //}

            //// Check if the firstName matches
            //if (firstNameClaim != "Antwerp")
            //{
            //    return Forbid();
            //}
            var members = await _repository.GetMembersAsync();
            return Ok(members);
        }
        /// <summary>
        /// get members by id
        /// </summary>
        /// <param name="id">The id of the member</param>
        /// /// <response code="200">Returns the requested member</response>
        /// <returns>A member with id</returns>
        // GET: api/members/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _repository.GetMemberAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        // POST: api/members
        [HttpPost]
        public async Task<ActionResult<Member>> CreateMember(Member member)
        {
            if (member == null)
            {
                return BadRequest("Member cannot be null");
            }

            var createdMember = await _repository.CreateMemberAsync(member);

            return CreatedAtAction(nameof(GetMember), new { id = createdMember.MemberId }, createdMember);
        }

        // PUT: api/members/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMember(int id, Member member)
        {
            //if (id != member.MemberId)
            //{
            //    return BadRequest("Member ID mismatch");
            //}
           
            var existingMember = await _repository.GetMemberAsync(id);
            if (existingMember == null)
            {
                return NotFound();
            }
            existingMember.FirstName = member.FirstName;
            existingMember.LastName = member.LastName;
            existingMember.Email = member.Email;
            existingMember.JoinDate = member.JoinDate;
            try
            {
                await _repository.UpdateMemberAsync(member);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception
                return Conflict("The trainer data has been updated or deleted by another user.");
            }
            return NoContent();  // HTTP 204 No Content
        }
    }
}
