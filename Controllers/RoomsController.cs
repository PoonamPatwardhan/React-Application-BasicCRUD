using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanningPokerWebAPI.ApplicationLayer.Commands.Rooms;
using PlanningPokerWebAPI.ApplicationLayer.DTOs;
using PlanningPokerWebAPI.ApplicationLayer.Queries.RoomQueries;
using PlanningPokerWebAPI.ApplicationLayer.Queries.UserQueries;
using PlanningPokerWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanningPokerWebAPI.Controllers
{
    // only authenticated users can access this controller.
    // To restrict access even further, we can simply add “Roles” to this attribute. Here we are adding both roles of User and Admin

    //[Authorize(Roles = "User, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private UserManager<User> userManager;
        private readonly IMediator mediator;

        public RoomsController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpGet("getAllRooms/{userName}")]
        public ActionResult<IEnumerable<Room>> GetAllRoomsForUser([FromBody] string userId)
        {
            return Ok(mediator.Send(new GetAllRoomsOfUser(userId)));
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetRoomById")]
        public ActionResult<Room> GetRoomById(int id)
        {
            var response = mediator.Send(new GetRoomById(id));
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        // POST api/<controller>
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public Task<Room> CreateNewRoom([FromBody] Room roomToAdd)
        {
            //if (roomToAdd.Admin.Role != "Admin")
                
            return mediator.Send(new CreateRoomCommand(roomToAdd, roomToAdd.Admin));
            //return CreatedAtRoute(nameof(GetFeatureById), new { Id = response.FeatureId }, response.FeatureReadDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]        
        public async Task<ActionResult> AddTeamMembersToRoom(int id, [FromBody]Room roomToUpdate)
        {
            if (id != roomToUpdate.RoomId)
            {
                return BadRequest();
            }
            await mediator.Send(new AddUsersToRoomCommand(roomToUpdate, roomToUpdate.TeamMembers.ToList()));
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
