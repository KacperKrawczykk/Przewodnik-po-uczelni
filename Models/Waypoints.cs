using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NowyPrzewodnikMVC.Models
{
    public class Waypoint
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }
        
        public string? Description { get; set; }
        
        public string? ImageUrl { get; set; }

        // Lista połączeń wychodzących
        public virtual ICollection<Connection> OutboundConnections { get; set; } = new List<Connection>();
    }
}