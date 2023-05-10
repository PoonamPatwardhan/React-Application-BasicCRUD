using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanningPokerWebAPI.ApplicationLayer.DTOs;
using PlanningPokerWebAPI.ApplicationLayer.Features.Commands;
using PlanningPokerWebAPI.ApplicationLayer.Features.Queries;
using PlanningPokerWebAPI.ApplicationLayer.Queries.FeatureQueries;
using PlanningPokerWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanningPokerWebAPI.Controllers
{
    //[EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    public class FeaturesController : Controller
    {
        private readonly IMediator mediator;

        public FeaturesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<IEnumerable<FeatureReadDto>> Get()
        {
            return Ok(mediator.Send(new GetAllFeaturesQuery()));           
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoomReadDto>> GetAllFeaturesInRoom([FromBody] Room room)
        {
            return Ok(mediator.Send(new GetAllFeaturesByRoom(room)));
        }
        // GET api/<controller>/5
        [HttpGet("{id}", Name ="GetFeatureById")]
        public ActionResult<FeatureReadDto> GetFeatureById(int id)
        {
            var response = mediator.Send(new GetFeatureByIdQuery(id));
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        public Task<FeatureReadDto> CreateNewFeature([FromBody] FeatureCreateDto featureToCreate)
        {
            return mediator.Send(new CreateFeatureCommand(featureToCreate));

            //return CreatedAtRoute(nameof(GetFeatureById), new { Id = response.FeatureId }, response.FeatureReadDto);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFeature(int id, [FromBody] FeatureUpdateDto featureToUpdate)
        {
            if (id != featureToUpdate.FeatureId)
            {
                return BadRequest();
            }
            await mediator.Send(new UpdateFeatureCommand(id, featureToUpdate));
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
