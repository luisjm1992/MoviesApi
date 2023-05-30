using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public string Foto { get; set; }

    }
}
