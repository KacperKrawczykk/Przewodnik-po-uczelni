using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NowyPrzewodnikMVC.Models
{
    public class Connection
    {
        public int Id { get; set; }

        // Skąd
        public int SourceId { get; set; }
        [ForeignKey("SourceId")]
        public virtual Waypoint Source { get; set; }

        // Dokąd
        public int TargetId { get; set; }
        [ForeignKey("TargetId")]
        public virtual Waypoint Target { get; set; }

        // Kierunek (FORWARD, BACK, LEFT, RIGHT)
        [Required]
        public string Direction { get; set; }
    }
}