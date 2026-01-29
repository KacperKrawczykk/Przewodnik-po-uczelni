using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NowyPrzewodnikMVC.Models
{
    public class Connection
    {
        [Key]
        public int Id { get; set; }

        // Skąd idziemy (np. ID Korytarza)
        public int SourceId { get; set; }

        // Dokąd idziemy (np. ID Kuchni)
        public int TargetId { get; set; }

        // Kierunek: "FORWARD", "LEFT", "RIGHT", "BACK"
        public string Direction { get; set; }

        // To pozwala bazie danych zrozumieć relacje (Entity Framework)
        [ForeignKey("SourceId")]
        public virtual Waypoint Source { get; set; }

        [ForeignKey("TargetId")]
        public virtual Waypoint Target { get; set; }
    }
}