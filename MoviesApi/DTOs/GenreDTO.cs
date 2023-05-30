using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOs
{
    public class GenreDTO : GenreCreationDTO
    {
        public int Id { get; set; }
    }
}
