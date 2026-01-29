using System.ComponentModel.DataAnnotations;

namespace NowyPrzewodnikMVC.Models
{
    public class Waypoint
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Nowe pole na opis (może być puste)
        public string? Description { get; set; }

        public string ImageUrl { get; set; }

        public virtual List<Connection> OutboundConnections { get; set; } = new List<Connection>();
    }
}