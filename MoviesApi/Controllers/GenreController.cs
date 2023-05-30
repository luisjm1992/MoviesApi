using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.DTOs;
using MoviesApi.Entities;

namespace MoviesApi.Controllers
{
    [ApiController]
    [Route("api/genre")]
    public class GenreController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenreController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            var entities = await context.Genres.ToListAsync();
            var dtos = mapper.Map<List<GenreDTO>>(entities);
            return dtos;
        }


        [HttpGet("{id:int}", Name ="getNameById")]
        public async Task<ActionResult<GenreDTO>> GetById(int id)
        {
            var idGenre = await context.Genres.FirstOrDefaultAsync(x => x.Id== id);
            if(idGenre == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<GenreDTO>(idGenre);
            return dto;
        }


        [HttpPost]
        public async Task<ActionResult<GenreDTO>> PostGenre([FromBody] GenreCreationDTO genreCreationDTO)
        {
            var entity = mapper.Map<Genre>(genreCreationDTO);
            context.Add(entity);
            await context.SaveChangesAsync();

            // Retornar Id
            var genreDTO = mapper.Map<GenreDTO>(entity);
            return CreatedAtRoute("getNameById", new { id = genreDTO.Id }, genreDTO);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutGnere(int id, [FromBody] GenreCreationDTO genreCreationDTO)
        {
            var entitie = mapper.Map<Genre>(genreCreationDTO);
            entitie.Id = id;

            context.Entry(entitie).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var existe = await context.Genres.AnyAsync(x => x.Id == id);
            if(!existe)
            {
                //return 404
                return NotFound();
            }    

            context.Remove(new Genre() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
