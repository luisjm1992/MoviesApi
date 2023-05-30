using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Entities;
using MoviesApi.Migrations;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> GetActor()
        {
            var entities = await context.Actors.ToListAsync();
            return mapper.Map<List<ActorDTO>>(entities);
        }


        [HttpGet("{id}", Name = "getActorById")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var entitie = await context.Actors.FirstOrDefaultAsync(x => x.Id == id);

            if(entitie == null)
            {
                return NotFound();
            }

            return mapper.Map<ActorDTO>(entitie);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ActorCreationDTO actorCreationDTO)
        {
            var entitie = mapper.Map<Actor>(actorCreationDTO);
            context.Add(entitie);

            await context.SaveChangesAsync();
            var dto = mapper.Map<ActorDTO>(entitie);
            return new CreatedAtRouteResult("getActorById", new { id = entitie.Id }, dto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutActor(int id, [FromBody] ActorCreationDTO actorCreationDTO)
        {
            var entitie = mapper.Map<Actor>(actorCreationDTO);
            entitie.Id = id;

            context.Entry(entitie).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var existe = await context.Actors.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                //return 404
                return NotFound();
            }

            context.Remove(new Actor(){Id = id});
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
