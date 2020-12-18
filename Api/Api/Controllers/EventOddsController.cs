using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class EventOddsController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EventOddsController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("addnewevent")]
        public async Task<ActionResult<EventDto>> AddNewEvent()
        {
            var ev = new Event();
          
            unitOfWork.Repository<Event>().Add(ev);
            await unitOfWork.Complete();

            return mapper.Map<EventDto>(ev);
        }

        [HttpPost("updateevent")]
        public async Task<ActionResult<EventDto>> UpdateEvent(EventDto eventInput)
        {
            Event ev = await unitOfWork.Repository<Event>().GetByIdAsync(eventInput.Id);
            if(ev == null)
            {
                return BadRequest(new ApiResponse(400, "The event you are updating doesn't exist!"));
            }
            ev.EventName = eventInput.EventName;
            ev.EventStartDate = eventInput.EventStartDate;
            ev.OddsForDraw = eventInput.OddsForDraw;
            ev.OddsForFirstTeam = eventInput.OddsForFirstTeam;
            ev.OddsForSecondTeam = eventInput.OddsForSecondTeam;
                    

            unitOfWork.Repository<Event>().Update(ev);
            await unitOfWork.Complete();

            return eventInput;
        }

        [HttpDelete("deleteevent")]
        public async Task<ActionResult> DeleteEvent([FromQuery]int id)
        {
            Event ev = await unitOfWork.Repository<Event>().GetByIdAsync(id);
            if(ev == null)
            {
                return BadRequest(new ApiResponse(400, "The event you are deleting doesn't exist!"));
            }

            unitOfWork.Repository<Event>().Delete(ev);
            await unitOfWork.Complete();
            
            return Ok(new ApiResponse(200,"Successfully deleted event."));      
        }


        [HttpGet("getevents")]
        public async Task<ActionResult<IReadOnlyList<EventDto>>> GetAllEvents()
        {
            IReadOnlyList<EventDto> eventsToReturn = 
               mapper.Map<IReadOnlyList<EventDto>>(await unitOfWork.Repository<Event>().ListAllAsync());

            return Ok(eventsToReturn);      
        }
    }
}